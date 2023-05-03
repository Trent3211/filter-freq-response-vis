import dearpygui.dearpygui as dpg
import serial
from serial.serialutil import SerialException
import serial.tools.list_ports
import pyscreenshot
import pygetwindow
import webbrowser
import pandas as pd
import datetime
import math
from math import pi
from math import acos
import os

# Application constants
title = "Filter Frequency Response Visualizer"
# Icon
icon = "./resources/icon.ico"

ser = None
magnitude = []
phase = []
frequency = []
ports = []
average_frequency = []
average_magnitude = []
average_phase = []
calibration_phase = []
calibration_magnitude = []
calibration_frequency = []

# Start and Stop commands
start_command = "s"
stop_command = "e"
ready_init = "!"

# Data constants
baud_rate = [9600, 19200, 38400, 57600, 115200]

# Plot axis constraints
freq_start = 20
freq_end = 30000
mag_min = -60
mag_max = 60
phase_min = -225
phase_max = 225

# Current directory screenshot folder
config_name = "config.ini"
screenshot_path = "./resources/screenshots/"
data_path = "./resources/data/"
github = "https://github.com/Trent3211/filter-freq-response-vis"
about = "https://github.com/Trent3211/filter-freq-response-vis/blob/main/README.md"
documentation = "https://dearpygui.readthedocs.io/en/latest/documentation/plots.html"

dpg.create_context()

def config_app():
    dpg.configure_app(init_file="config.ini")
    log("Config file loaded - " + config_name)

def save_init():
    dpg.save_init_file(config_name, overwrite=True, include_data=True, include_theme=True)
    log("Config file created.")

def github_link():
    webbrowser.open(github)
    log("Github page opened in browser.")

def about_link():
    webbrowser.open(about)
    log("About page opened in browser.")

def plotting_documentation_link():
    webbrowser.open(documentation)
    log("Plotting documentation opened in browser.")

def print_me(sender):
    log(f"[{sender}] was clicked")

def on_drag_line(sender):
    log(dpg.get_value(sender))

def on_drag_point(sender):
    drag_point_value = dpg.get_value(sender)
    log(sender)
    # Find the index of the frequency that is closest to the drag point
    drag_x = drag_point_value[0]
    closest_index = min(range(len(frequency)), key=lambda i: abs(frequency[i] - drag_x))

    # Snap to the closest frequency and associated y-value
    snap_x = frequency[closest_index]
    snap_y = magnitude[closest_index]
    dpg.set_value(sender, [snap_x, snap_y])

def clear_data():
    # Clear the data lists
    magnitude.clear()
    phase.clear()
    frequency.clear()
    average_frequency.clear()
    average_magnitude.clear()
    average_phase.clear()

def clear_calibration_data():
    # Clear the calibration data lists
    calibration_frequency.clear()
    calibration_magnitude.clear()
    calibration_phase.clear()

def get_available_ports():
    # Clear the ports list
    ports.clear()

    # Now that the combo box has been cleared, add the available ports
    for port in serial.tools.list_ports.comports():
        ports.append(port.device + " - " + port.description)
    return ports

def stop_data():
    ser.write(stop_command.encode())
    log("Sweep stop flag sent to device on " + ser.name)

def on_connect_button():
    global ser
    try:
        if dpg.get_value('port_name') == "Select a port...":
            raise SerialException("Please select a valid serial port.")
            
        ser = serial.Serial(
            port=dpg.get_value('port_name').split(" - ")[0],
            baudrate=dpg.get_value('baud_rate'),
            timeout=1
        )
        log("Connected to serial port:" + ser.name + " at " + str(ser.baudrate) + " baud")

        dpg.configure_item('calibrate_btn', enabled=True)
        dpg.set_value('connection_status', "Connected")
    except SerialException as e:
        log(f"Error connecting to device: {e}")
    return

def on_disconnect_button():
    global ser 
    if ser is not None and ser.is_open:
        ser.close()
        dpg.configure_item('calibrate_btn', enabled=False)
        log("Disconnected from serial port: " + ser.name)
        dpg.set_value('connection_status', "Disconnected")
    elif ser is None:
        log("Serial port has not been initialized yet.")
    else:
        log("Serial port is already closed.")

def set_autofit():
    dpg.set_axis_limits_auto('frequency_axis_1')
    dpg.set_axis_limits_auto('frequency_axis_2')
    dpg.set_axis_limits_auto('magnitude_axis_1')
    dpg.set_axis_limits_auto('phase_axis_1')

def add_cursors(sender):
    # Get the values of the checkboxes
    checkbox_name = sender
    drag_line_name = checkbox_name.replace("checkbox", "drag_line")

    if dpg.get_value(checkbox_name):
        dpg.configure_item(drag_line_name + "_1", show=True)
        dpg.configure_item(drag_line_name + "_2", show=True)
    else:
        dpg.configure_item(drag_line_name + "_1", show=False)
        dpg.configure_item(drag_line_name + "_2", show=False)

def add_drag_points(sender):
    # Get the state of the checkbox
    checkbox_name = sender
    drag_point_name = checkbox_name.replace("checkbox", "dragpoint")

    # Now write the logic to add the drag points
    if dpg.get_value(checkbox_name):
        dpg.configure_item(drag_point_name, default_value=[2000, 0], show=True)
    else:
        dpg.configure_item(drag_point_name, default_value=[3000, 0], show=False)

def show_raw_data():
    dpg.configure_item('data_menu', label='Raw Data')
    dpg.configure_item('raw_table', show=True)
    dpg.configure_item('average_table', show=False)

def show_averaged_data():
    dpg.configure_item('data_menu', label='Averaged Data')
    dpg.configure_item('raw_table', show=False)
    dpg.configure_item('average_table', show=True)
 
def update_ports():
    # Refresh the combo box with the available ports
    dpg.configure_item('port_name', items=get_available_ports())
    log("Available ports refreshed.")

def calibration_sweep():
    # This will calibrate the values of the magnitude and phase arrays based on the attenuation picked up from the calibration sweep
    # Create calibration arrays
    global calibration_magnitude
    global calibration_phase
    global calibration_frequency

    clear_calibration_data()

    log("Calibration started...")

    # Send the calibration command to the device
    ser.write(start_command.encode())

    num_consecutive_empty_reads = 0
    max_consecutive_empty_reads = 10

    while True:
        data = ser.readline().decode('utf-8').rstrip()
        if data.strip() == stop_command:
            log("Calibration completed.")
            break
        elif not data:
            num_consecutive_empty_reads += 1
            log("No data received... (" + str(num_consecutive_empty_reads) + " of " + str(max_consecutive_empty_reads) + ")")
            if num_consecutive_empty_reads >= max_consecutive_empty_reads:
                log("Calibration failed. No data received.")
                break
        else:
            num_consecutive_empty_reads = 0
            data = data.split(",")
            calibration_frequency = float(data[0])
            calibration_magnitude = round((3.3 / 4095.0) * float(data[1]), 3)
            calibration_phase = round((3.3 / 4095.0) * float(data[2]), 3)

            calibration_magnitude = round(20*(math.log10(calibration_magnitude / 1.1 )), 5)
            calibration_phase = round(2 * acos(calibration_phase), 5)
            calibration_phase = round(calibration_phase * (180 / pi) - 90, 5)


def get_data_from_serial():
    global ser
    global magnitude
    global phase
    global frequency
    global average_frequency
    global average_magnitude
    global average_phase
    global calibration_magnitude
    global calibration_phase
    global calibration_frequency

    last_frequency_value = None
    last_frequency_values = []

    # Clear the data in the raw_table and average_table
    clear_data()

    ser.write(ready_init.encode()) # This somehow worked, but on connect it did not?
    # Send the character command 's'
    ser.write(start_command.encode())
    
    # Clear the data in the raw_table using dpg.get_item_children(table_tag, 1)
    for child in dpg.get_item_children("raw_table", 1):
        dpg.delete_item(child)

    for child in dpg.get_item_children("average_table", 1):
        dpg.delete_item(child)

    # Read the data from the serial port until e is read
    num_consecutive_empty_reads = 0
    max_consecutive_empty_reads = 10
    while True:
        data = ser.readline().decode('utf-8').strip()
        if data.strip() == stop_command: # Edit this for the terminating character
            log("Sweep completed.")
            break
        elif not data:
            num_consecutive_empty_reads += 1
            log("No data received... (" + str(num_consecutive_empty_reads) + " of " + str(max_consecutive_empty_reads) + ")")
            if num_consecutive_empty_reads >= max_consecutive_empty_reads:
                log("No data received, stopping the check for the sweep.")
                break
        else:
            num_consecutive_empty_reads = 0
            # Split the data into its frequency, magnitude, and phase components
            data = data.split(',')
            if len(data) >= 3:
                try:
                    frequency_value = float(data[0])
                    # Convert magnitude to Voltage
                    magnitude_value = round((3.3 / 4095.0) * float(data[1]), 3) 
                    # Convert phase to Voltage
                    phase_value = round((3.3 / 4095.0) * float(data[2]), 3)
                    # Convert magnitude to dB
                    magnitude_value = round(20*(math.log10(magnitude_value/1.1)), 5)
                    # Calculate the phase in degrees
                    phase_value = round(2 * acos(phase_value), 5)
                    # Now convert to radians
                    phase_value = round(phase_value * (180 / pi) - 90, 5)

                    # Append the values to the frequency, magnitude, and phase lists
                    frequency.append(frequency_value)
                    magnitude.append(magnitude_value)
                    phase.append(phase_value)

                    #dpg.set_value('magnitude_series_1', [frequency, magnitude])
                    #dpg.set_value('phase_series_1', [frequency, phase])

                    # Add the data to the table
                    with dpg.table_row(parent="raw_table"):
                        dpg.add_text(str(frequency_value))
                        dpg.add_text(str(magnitude_value))
                        dpg.add_text(str(phase_value))

                    # Check if the frequency value has changed
                    if frequency_value != last_frequency_value:
                        if last_frequency_values:
                            # Compute average magnitude and phase values for the previous set of frequency values
                            avg_mag = sum([val[0] for val in last_frequency_values]) / len(last_frequency_values)
                            avg_ph = sum([val[1] for val in last_frequency_values]) / len(last_frequency_values)

                            # Now I need to add the calibration values to each of the average values.
                            # This is done by adding the calibration values to the average values.
                            avg_mag = round(avg_mag + calibration_magnitude, 5)
                            avg_ph = round(avg_ph + calibration_phase, 5)

                            average_frequency.append(last_frequency_value)
                            average_magnitude.append(avg_mag)
                            average_phase.append(avg_ph)
                            

                            dpg.set_value('phase_series_1', [average_frequency, average_phase])
                            dpg.set_value('magnitude_series_1', [average_frequency, average_magnitude])

                            # Add the data to the average_table
                            with dpg.table_row(parent="average_table"):
                                dpg.add_text(str(last_frequency_value))
                                dpg.add_text(str(avg_mag))
                                dpg.add_text(str(avg_ph))

                        # Reset the last_frequency_values list and update the last_frequency_value variable
                        last_frequency_values = []
                        last_frequency_value = frequency_value

                    # Append the current magnitude and phase values to the last_frequency_values list
                    last_frequency_values.append((magnitude_value, phase_value))

                except ValueError:
                    log("Invalid data received.")
                    continue
    set_autofit()

def set_axis_min_max(user_data):

    if user_data == "p_axis_btn":
        phase_min = dpg.get_value('p_min_axis')
        phase_max = dpg.get_value('p_max_axis')
        log("New phase axis limits: " + phase_min + " " + phase_max + "")
        # Now set the axis limits for the phase plot
        dpg.set_axis_limits('phase_axis_1', int(phase_min), int(phase_max))
    elif user_data == "m_axis_btn":
        mag_min = dpg.get_value('m_min_axis')
        mag_max = dpg.get_value('m_max_axis')
        log("New magnitude axis limits: " + mag_min + " " + mag_max + "")
        # Now set the axis limits for the magnitude plot
        dpg.set_axis_limits('magnitude_axis_1', int(mag_min), int(mag_max))
    else:
        log("Invalid button ID")

def take_screenshot():
    # Just take a screenshot of the entire window
    im = pyscreenshot.grab()
    now = datetime.datetime.now()
    timestamp = now.strftime("%Y-%m-%d_%H-%M-%S")
    im.save(screenshot_path + "screenshot_{}.png".format(timestamp))
    log("Saved to" + screenshot_path + "screenshot_{}.png".format(timestamp))

def table_to_csv(user_data):
    global frequency
    global magnitude
    global phase
    global average_frequency
    global average_magnitude
    global average_phase

    if user_data == 40:
        data_type = 'raw'
        freq = frequency
        mag = magnitude
        ph = phase
    elif user_data == 41:
        data_type = 'average'
        freq = average_frequency
        mag = average_magnitude
        ph = average_phase
    else:
        log("Invalid button ID")
        return

    if len(freq) > 0:
        now = datetime.datetime.now()
        timestamp = now.strftime("%Y-%m-%d_%H-%M-%S")

        # Check if the directory exists, create it if it does not exist
        if not os.path.exists(data_path):
            os.makedirs(data_path)

        # Create a pandas dataframe from the data
        df = pd.DataFrame({
            "Frequency (Hz)": freq,
            "Magnitude (dB)": mag,
            "Phase (deg)": ph
        })
        # Save the dataframe to a csv file with the syntax "data_type_YYYY-MM-DD_HH-MM-SS.csv"
        df.to_csv(data_path + "{}_data_{}.csv".format(data_type, timestamp), index=False)
        log("{} data saved to".format(data_type.capitalize()) + data_path + "{}_data_{}.csv".format(data_type, timestamp))
    else:
        log("No data to save")

def clear_log():
    dpg.set_value('logger_box', "")
    
def log(message):
    current_value = dpg.get_value('logger_box')
    if current_value == "":
        dpg.set_value('logger_box', "[LOG] " + message)
    else:
        dpg.set_value('logger_box', f"{current_value}\n[LOG] {message}")
        dpg.set_y_scroll('logger_text', -1.0)

# ---------- BEGINNING OF GUI CODE ---------- #

with dpg.window(label="Object Window", width=1450, height=1000, pos=(0, 0), tag="main_window", no_resize=True, no_close=True, no_move=True, no_collapse=True):      
    with dpg.theme(tag="mag_theme"):
        # Make a fun looking theme perfect for me.
        with dpg.theme_component(dpg.mvLineSeries):
            dpg.add_theme_color(dpg.mvPlotCol_Line, (200, 80, 100), category=dpg.mvThemeCat_Plots)
    
    with dpg.theme(tag="phase_theme"):
        with dpg.theme_component(dpg.mvLineSeries):
            dpg.add_theme_color(dpg.mvPlotCol_Line, (100, 100, 200), category=dpg.mvThemeCat_Plots)
            

    # Menu bar #
    with dpg.menu_bar():
        with dpg.menu(label="File"):
            dpg.add_menu_item(label="Save Settings", callback=save_init)

        dpg.add_menu_item(label="Calibrate", callback=calibration_sweep, enabled=False, tag="calibrate_btn")
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("Calibrate the hardware to compensate for the BNC dB attenuation, ensure you are connected to the device.")

        with dpg.menu(label="Sweep"):
            dpg.add_menu_item(label="Start Sweep", callback=get_data_from_serial)
            # Add the stop sweep button and send the stop command to the serial port
            dpg.add_menu_item(label="Stop Sweep", callback=stop_data)

        with dpg.menu(label="Screenshot"):
            dpg.add_menu_item(label="Save Application Screenshot", callback=take_screenshot)

        with dpg.menu(label="Data"):
            dpg.add_menu_item(label="Export Raw Data", callback=table_to_csv)
            dpg.add_menu_item(label="Export Average Data", callback=table_to_csv)

        with dpg.menu(label="Help"):
            dpg.add_menu_item(label="Plotting Documentation", callback=plotting_documentation_link)
            dpg.add_menu_item(label="Github Repository", callback=github_link)
            dpg.add_menu_item(label="About", callback=about_link)

    # Connection Handler Child Window #
    with dpg.child_window(label="Connection Handler", width=320, height=130, pos=(6, 42), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Connection Handling")

        dpg.add_combo(label="Baud Rate", default_value=115200, items=baud_rate, width=230, tag='baud_rate')
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("Match the baud rate of your arduino due to the baud rate of the GUI.")
        dpg.add_combo(label="Port Name", default_value="Select a port...", items=get_available_ports(), width=230, tag='port_name')
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("Check the device manager to see which port your device is connected to.")

        with dpg.group(horizontal=True, parent="connect_group"):
            dpg.add_button(label="Connect", callback=on_connect_button, width=111, tag='connect_button')
            dpg.add_button(label="Disconnect", callback=on_disconnect_button, width=111, tag='disconnect_button')
            dpg.add_button(label="Refresh", callback=update_ports, width=65, tag='refresh_button')
            
        # Provide the status of the connection and allow it to be updated
        with dpg.group(horizontal=True, parent="status_group"):
            dpg.add_text("Connection Status: ")
            dpg.add_text("Disconnected", tag='connection_status')  

    # Data Child Window #
    with dpg.child_window(label="Object Window", width=320, height=560, pos=(6, 180), menubar=True):
        with dpg.menu_bar():
            with dpg.menu(label="Averaged Data", tag="data_menu"):
                dpg.add_menu_item(label="Raw Data", callback=show_raw_data)
                dpg.add_menu_item(label="Averaged Data", callback=show_averaged_data)
            
        
        with dpg.table(label="Table Data", tag="raw_table", header_row=True, no_host_extendX=True,
                                        borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                        borders_outerV=True, row_background=True, hideable=False, reorderable=False,
                                        resizable=False, sortable=False, policy=dpg.mvTable_SizingFixedFit,
                                        scrollX=False, delay_search=True, scrollY=True, show=False):

                    c1 = dpg.add_table_column(label="Freq. (Hz)", width_fixed=True, init_width_or_weight=90)
                    c2 = dpg.add_table_column(label="Mag. (dB)", width_fixed=True, init_width_or_weight=90)
                    c3 = dpg.add_table_column(label="Phase (deg)", width_fixed=True, init_width_or_weight=90)
        
        with dpg.table(label="Table Data", tag="average_table", header_row=True, no_host_extendX=True,
                                        borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                        borders_outerV=True, row_background=True, hideable=False, reorderable=False,
                                        resizable=False, sortable=False, policy=dpg.mvTable_SizingFixedFit,
                                        scrollX=False, delay_search=True, scrollY=True, show=True):

                    c1 = dpg.add_table_column(label="Freq. (Hz)", width_fixed=True, init_width_or_weight=90)
                    c2 = dpg.add_table_column(label="Mag. (dB)", width_fixed=True, init_width_or_weight=90)
                    c3 = dpg.add_table_column(label="Phase (deg)", width_fixed=True, init_width_or_weight=90)

    # Logger Child Window #
    with dpg.child_window(tag="logger_text", label="Object Window", width=520, height=210, pos=(6, 744), menubar=True):
        with dpg.menu_bar(label=""):
            dpg.add_menu(label="Logging Window")
            dpg.add_button(label="Clear", callback=clear_log, pos=(450, 0), width=70, height=20)
        # Add the logger textbox with a font size of 8, the logger tag is logger_text
        dpg.add_text(tag="logger_box")
        
    with dpg.child_window(label="Phase Plot Options", width=450, height=210, pos=(528, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Phase Plot Options")
        # Checkbox grouping #
        with dpg.group(horizontal=True, tag="phase_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="p_m0_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="p_m1_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="p_m2_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="Phase Plot Drag Lines", default_value=False, callback=add_cursors, tag="p_checkbox", parent="phase_plot")

        with dpg.group(horizontal=True, tag="phase_axis_min_group"):
            # Two text and two text boxes, one for min axis value, and one for maximum axis value for the phase
            dpg.add_text("Phase Axis Minimum")
            dpg.add_input_text(default_value=phase_min, tag="p_min_axis", callback=print_me, width=50)

        with dpg.group(horizontal=True, tag="phase_axis_max_group"):
            dpg.add_text("Phase Axis Maximum")
            dpg.add_input_text(default_value=phase_max, tag="p_max_axis", callback=print_me, width=50)

        with dpg.group(horizontal=True, tag="phase_axis_btn_group"):
            dpg.add_button(label="Set Phase Axis", callback=set_axis_min_max, width=150, user_data=37, tag="p_axis_btn")
            # Add a button for auto fit
            #dpg.add_button(label="Auto Fit", callback=set_autofit, width=150, user_data=40, tag="p_autofit_btn")
        

    with dpg.child_window(label="Magnitude Plot Options", width=450, height=210, pos=(980, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Magnitude Plot Options")
            # Make the below horizontal
        with dpg.group(horizontal=True, tag="magnitude_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="m_m0_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="m_m1_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="m_m2_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="Magnitude Plot Drag Lines", default_value=False, callback=add_cursors, tag="m_checkbox", parent="magnitude_plot")

        # Do the same thing for the magnitude axis as is done with the phase axis
        with dpg.group(horizontal=True, tag="magnitude_axis_min_group"):
            dpg.add_text("Magnitude Axis Minimum")
            dpg.add_input_text(default_value=mag_min, tag="m_min_axis", callback=print_me, width=50)

        with dpg.group(horizontal=True, tag="magnitude_axis_max_group"):
            dpg.add_text("Magnitude Axis Maximum")
            dpg.add_input_text(default_value=mag_max, tag="m_max_axis", callback=print_me, width=50)
        
        with dpg.group(horizontal=True, tag="magnitude_axis_btn_group"):
            dpg.add_button(label="Set Magnitude Axis", callback=set_axis_min_max, width=150, user_data=41, tag="m_axis_btn")
            # Add a button for auto fit
            #.add_button(label="Auto Fit", callback=set_autofit, width=150, user_data=38, tag="m_autofit_btn")

    # Phase Plot #

    with dpg.plot(label="Phase Plot", height=350, width=1100, pos=(330, 42), tag="phase_plot"):
        # Optionally create legend
        dpg.add_plot_legend(horizontal=True)

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_1")
        # Set the axis limits using set_axis_limits(axis, min, max)
        dpg.set_axis_limits(dpg.last_item(), freq_start, freq_end)

        with dpg.plot_axis(dpg.mvYAxis, label="Phase (deg)", tag="phase_axis_1"):
            dpg.set_axis_limits(dpg.last_item(), phase_min, phase_max)

        dpg.add_line_series([0, 0], [0, 0], label="Phase", parent="phase_axis_1", tag="phase_series_1")

        # Average Plot Drag Lines and Points
        dpg.add_drag_line(label="Phase_Data_1_x", color=[255, 0, 0], thickness=1.0,
                                vertical=True, show=False, default_value=100, tag="p_drag_line_1")
        
        dpg.add_drag_line(label="Phase_Data_2_x", color=[255, 0, 0], thickness=1.0,
                                vertical=True, show=False, default_value=6000, tag="p_drag_line_2")
        
        label_names = ["Phase_Data_1_p", "Phase_Data_2_p", "Phase_Data_3_p", "Phase_Data_4_p", "Phase_Data_5_p", "Phase_Data_6_p"]
        tag_names = ["p_m0_dragpoint", "p_m1_dragpoint", "p_m2_dragpoint", "p_m3_dragpoint", "p_m4_dragpoint", "p_m5_dragpoint"]
        
        for label, tag in zip(label_names, tag_names):
            dpg.add_drag_point(label=label, default_value=[0,0], color=[255, 255, 0], tag=tag, show=False, callback=on_drag_point)

        dpg.bind_item_theme("phase_series_1", "phase_theme")

    # Magnitude Plot #
    with dpg.plot(label="Magnitude Plot", height=350, width=1100, pos=(330, 391), tag="magnitude_plot"):
        dpg.add_plot_legend(horizontal=True)

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_2")
        dpg.set_axis_limits(dpg.last_item(), freq_start, freq_end)
        
        with dpg.plot_axis(dpg.mvYAxis, label="Magnitude (dB)", tag="magnitude_axis_1"):
            dpg.set_axis_limits(dpg.last_item(), mag_min, mag_max)

        # Set a placeholder for the magnitude data
        dpg.add_line_series([0, 0], [0, 0], label="Magnitude", parent="magnitude_axis_1", tag="magnitude_series_1")

        dpg.add_drag_line(label="Magnitude_Data_1_x", color=[0, 255, 0], thickness=1.0,
                                vertical=True, show=False, default_value=100, tag="m_drag_line_1")
        
        dpg.add_drag_line(label="Magnitude_Data_2_x", color=[0, 255, 0], thickness=1.0,
                                vertical=True, show=False, default_value=6000, tag="m_drag_line_2")
        
        label_names = ["Magnitude_Data_1_p", "Magnitude_Data_2_p", "Magnitude_Data_3_p", "Magnitude_Data_4_p", "Magnitude_Data_5_p", "Magnitude_Data_6_p"]
        tag_names = ["m_m0_dragpoint", "m_m1_dragpoint", "m_m2_dragpoint", "m_m3_dragpoint", "m_m4_dragpoint", "m_m5_dragpoint"]

        for label, tag in zip(label_names, tag_names):
            dpg.add_drag_point(label=label, default_value=[0,0], color=[255, 255, 0], tag=tag, show=False, callback=on_drag_point)


        dpg.bind_item_theme("magnitude_series_1", "mag_theme")
        
# ---------- End of GUI code ---------- #

# Create the viewport and start the Dear PyGui event loop
dpg.create_viewport(title=title, width=1450, height=1000, resizable=False)
# Dearpygui version log()
log("Dear PyGui Version: " + dpg.get_dearpygui_version())
config_app()
log("Welcome to the FFRV Application.")
log("Please select a COM port and click the 'Connect' button to begin.")
dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()

while dpg.is_dearpygui_running():
    # Run the get_data_from_serial function every 100ms
    dpg.render_dearpygui_frame()
# Once the event loop has finished, clean up

dpg.destroy_context()