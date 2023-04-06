import dearpygui.dearpygui as dpg
import serial
from serial.serialutil import SerialException
import serial.tools.list_ports
import pyscreenshot
import webbrowser
import pandas as pd
import datetime
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

# Start and Stop commands
start_command = "s"
stop_command = "e"

# Data constants
baud_rate = [9600, 19200, 38400, 57600, 115200]

# Plot axis constraints
freq_start = 20
freq_end = 30000
mag_start = -2
mag_end = 2
phase_start = -2
phase_end = 2

# Current directory screenshot folder
screenshot_path = "./resources/screenshots/"
data_path = "./resources/data/"
github = "https://github.com/Trent3211/filter-freq-response-vis"
about = "https://github.com/Trent3211/filter-freq-response-vis/blob/main/README.md"
documentation = "https://dearpygui.readthedocs.io/en/latest/documentation/plots.html"

dpg.create_context()

def github():
    webbrowser.open(github)
    log("GitHub page opened in browser.")

def about():
    webbrowser.open(about)
    log("About page opened in browser.")

def plotting_documentation():
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
    global ser  # Make the ser object global so that it can be used in other functions
    try:
        ser = serial.Serial(
            # The port name is the selected item in the combo box, but it must be parsed to remove the description
            port=dpg.get_value('port_name').split(" - ")[0],
            baudrate=dpg.get_value('baud_rate'),
            timeout=1
        )
        log("Connected to serial port:", ser.name, "at", ser.baudrate, "baud")
        dpg.set_value('connection_status', "Connected")
    except SerialException:
        log(f"Error connecting to device: Serial port {dpg.get_value('port_name')} is not available")
        return 

def on_disconnect_button():
    global ser  # Make sure the ser object is the same one used in on_connect_button
    if ser is not None and ser.is_open:
        ser.close()
        log("Disconnected from serial port:", ser.name)
        dpg.set_value('connection_status', "Disconnected")
    elif ser is None:
        log("Serial port has not been initialized yet.")
    else:
        log("Serial port is already closed.")

def set_autofit():
    dpg.set_axis_limits_auto('frequency_axis_1')
    dpg.set_axis_limits_auto('frequency_axis_2')
    dpg.set_axis_limits_auto('magnitude_axis_1')
    dpg.set_axis_limits_auto('magnitude_axis_2')
    dpg.set_axis_limits_auto('phase_axis_1')
    dpg.set_axis_limits_auto('phase_axis_2')

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
    log("Available ports have been refreshed.")

def get_data_from_serial():
    global ser
    global magnitude
    global phase
    global frequency
    global average_frequency
    global average_magnitude
    global average_phase

    last_frequency_value = None
    last_frequency_values = []

    # Clear the data in the raw_table and average_table
    clear_data()

    # Send the character command 's'
    ser.write(start_command.encode())
    
    # Clear the data in the raw_table using dpg.get_item_children(table_tag, 1)
    for child in dpg.get_item_children("raw_table", 1):
        dpg.delete_item(child)

    for child in dpg.get_item_children("average_table", 1):
        dpg.delete_item(child)

    # Read the data from the serial port until e is read
    while True:
        data = ser.readline().decode('utf-8').strip()
        log(data)
        if data.strip() == stop_command: # Edit this for the terminating character
            log("Sweep completed.")
            break
        else:
            # Split the data into its frequency, magnitude, and phase components
            data = data.split(',')
            if len(data) >= 3:
                try:
                    frequency_value = float(data[0])
                    magnitude_value = round((3.3 / 1023.0) * float(data[1]), 3) # Convert magnitude to volts and round to 3 decimal points
                    phase_value = round((3.3 / 1023.0) * float(data[2]), 3) # Convert phase to volts and round to 3 decimal points

                    # Append the values to the frequency, magnitude, and phase lists
                    frequency.append(frequency_value)
                    magnitude.append(magnitude_value)
                    phase.append(phase_value)

                    dpg.set_value('magnitude_series_1', [frequency, magnitude])
                    dpg.set_value('phase_series_1', [frequency, phase])

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
                            average_frequency.append(last_frequency_value)
                            average_magnitude.append(avg_mag)
                            average_phase.append(avg_ph)

                            dpg.set_value('phase_series_2', [average_frequency, average_phase])
                            dpg.set_value('magnitude_series_2', [average_frequency, average_magnitude])

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
    
    # The plots have axis constraints, but now that the sweep is done, we can set it back to autofit
    set_autofit()

    # Show the raw table
    #show_raw_data()

    # Send

def take_screenshot():
    # Just take a screenshot of the entire window
    im = pyscreenshot.grab()
    now = datetime.datetime.now()
    timestamp = now.strftime("%Y-%m-%d_%H-%M-%S")
    im.save(screenshot_path + "screenshot_{}.png".format(timestamp))
    log("Saved to" + screenshot_path + "screenshot_{}.png".format(timestamp))

def table_to_csv(user_id):
    global frequency
    global magnitude
    global phase
    global average_frequency
    global average_magnitude
    global average_phase

    if user_id == 34:
        data_type = 'raw'
        freq = frequency
        mag = magnitude
        ph = phase
    elif user_id == 35:
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
        log("{} data saved to".format(data_type.capitalize()), data_path + "{}_data_{}.csv".format(data_type, timestamp))
    else:
        log("No data to save")
    
def log(message):
    current_value = dpg.get_value('logger_text')
    if current_value == "":
        dpg.set_value('logger_text', "[LOG] " + message)
    else:
        dpg.set_value('logger_text', f"{current_value}\n[LOG] {message}")

# ---------- BEGINNING OF GUI CODE ---------- #

with dpg.window(label="Object Window", width=1450, height=1000, pos=(0, 0), tag="main_window"):      
    with dpg.theme(tag="plot_theme"):
        # Make a fun looking theme perfect for me.
        with dpg.theme_component(dpg.mvLineSeries):
            dpg.add_theme_color(dpg.mvPlotCol_Line, (200, 80, 100), category=dpg.mvThemeCat_Plots)
            

    # Menu bar #
    with dpg.menu_bar():
        with dpg.menu(label="File"):
            dpg.add_menu_item(label="Save Settings", callback=print_me)
            dpg.add_menu_item(label="Close", callback=print_me)

        with dpg.menu(label="Sweep"):
            dpg.add_menu_item(label="Start Sweep", callback=get_data_from_serial)
            # Add the stop sweep button and send the stop command to the serial port
            dpg.add_menu_item(label="Stop Sweep", callback=stop_data)

        with dpg.menu(label="Screenshot"):
            dpg.add_menu_item(label="Save Application Screenshot", callback=take_screenshot)

        with dpg.menu(label="Data"):
            dpg.add_menu_item(label="Export Raw Data", callback=table_to_csv, user_data=34)
            dpg.add_menu_item(label="Export Average Data", callback=table_to_csv, user_data=35)

        with dpg.menu(label="Help"):
            dpg.add_menu_item(label="Plotting Documentation", callback=plotting_documentation)
            dpg.add_menu_item(label="Github Repository", callback=github)
            dpg.add_menu_item(label="About", callback=about)

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
            with dpg.menu(label="Raw Data", tag="data_menu"):
                dpg.add_menu_item(label="Raw Data", callback=show_raw_data)
                dpg.add_menu_item(label="Averaged Data", callback=show_averaged_data)
            
        
        with dpg.table(label="Table Data", tag="raw_table", header_row=True, no_host_extendX=True,
                                        borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                        borders_outerV=True, row_background=True, hideable=False, reorderable=False,
                                        resizable=False, sortable=False, policy=dpg.mvTable_SizingFixedFit,
                                        scrollX=False, delay_search=True, scrollY=True, show=True):

                    c1 = dpg.add_table_column(label="Freq. (Hz)", width_fixed=True, init_width_or_weight=90)
                    c2 = dpg.add_table_column(label="Mag. (dB)", width_fixed=True, init_width_or_weight=90)
                    c3 = dpg.add_table_column(label="Phase (deg)", width_fixed=True, init_width_or_weight=90)
        
        with dpg.table(label="Table Data", tag="average_table", header_row=True, no_host_extendX=True,
                                        borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                        borders_outerV=True, row_background=True, hideable=False, reorderable=False,
                                        resizable=False, sortable=False, policy=dpg.mvTable_SizingFixedFit,
                                        scrollX=False, delay_search=True, scrollY=True, show=False):

                    c1 = dpg.add_table_column(label="Freq. (Hz)", width_fixed=True, init_width_or_weight=90)
                    c2 = dpg.add_table_column(label="Mag. (dB)", width_fixed=True, init_width_or_weight=90)
                    c3 = dpg.add_table_column(label="Phase (deg)", width_fixed=True, init_width_or_weight=90)

    # Logger Child Window #
    with dpg.child_window(label="Object Window", width=520, height=210, pos=(6, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_button(label="Clear", callback=print_me)
        # Add the logger textbox with a font size of 8, the logger tag is logger_text
        dpg.add_input_text(tag="logger_text", width=504, height=172, enabled=False, multiline=True)
        
    with dpg.child_window(label="Raw Plot Options", width=450, height=210, pos=(528, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Raw Plot Options (WIP)")
        dpg.add_text("Magnitude")
        with dpg.group(horizontal=True, tag="raw_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="r_m0_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="r_m1_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="r_m2_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="Raw Plot Drag Lines", default_value=False, callback=add_cursors, tag="r_checkbox", parent="raw_plot")
        dpg.add_text("Phase")
        with dpg.group(horizontal=True, tag="raw_checkbox_group_1"):
            dpg.add_checkbox(label="M0", tag="r_m3_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="r_m4_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="r_m5_checkbox", default_value=False, callback=add_drag_points)

    with dpg.child_window(label="Average Plot Options", width=450, height=210, pos=(980, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Average Plot Options (WIP)")
            # Make the below horizontal
        dpg.add_text("Magnitude")
        with dpg.group(horizontal=True, tag="average_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="a_m0_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="a_m1_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="a_m2_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="Average Plot Drag Lines", default_value=False, callback=add_cursors, tag="a_checkbox", parent="average_plot")
        dpg.add_text("Phase")
        with dpg.group(horizontal=True, tag="average_checkbox_group_1"):
            dpg.add_checkbox(label="M0", tag="a_m3_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M1", tag="a_m4_checkbox", default_value=False, callback=add_drag_points)
            dpg.add_checkbox(label="M2", tag="a_m5_checkbox", default_value=False, callback=add_drag_points)
    with dpg.plot(label="Raw Data Plot", height=350, width=1100, pos=(330, 42), tag="raw_plot"):
        # Optionally create legend
        dpg.add_plot_legend(horizontal=True)

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_1")
        # Set the axis limits using set_axis_limits(axis, min, max)
        dpg.set_axis_limits(dpg.last_item(), freq_start, freq_end)

        with dpg.plot_axis(dpg.mvYAxis, label="Phase (deg)", tag="phase_axis_1"):
            dpg.set_axis_limits(dpg.last_item(), phase_start, phase_end)

        with dpg.plot_axis(dpg.mvYAxis, label="Magnitude (dB)", tag="magnitude_axis_1"):
            dpg.set_axis_limits(dpg.last_item(), mag_start, mag_end)

        dpg.add_line_series([0, 0], [0, 0], label="Phase", parent="phase_axis_1", tag="phase_series_1")
        dpg.add_line_series([0, 0], [0, 0], label="Magnitude", parent="magnitude_axis_1", tag="magnitude_series_1")

        # Average Plot Drag Lines and Points
        dpg.add_drag_line(label="Raw_Data_1_x", color=[255, 0, 0], thickness=1.0,
                                vertical=True, show=False, default_value=100,
                                callback=on_drag_line, tag="r_drag_line_1")
        
        dpg.add_drag_line(label="Raw_Data_2_x", color=[255, 0, 0], thickness=1.0,
                                vertical=True, show=False, default_value=6000,
                                callback=on_drag_line, tag="r_drag_line_2")
        
        label_names = ["Raw_Data_1_p", "Raw_Data_2_p", "Raw_Data_3_p", "Raw_Data_4_p", "Raw_Data_5_p", "Raw_Data_6_p"]
        tag_names = ["r_m0_dragpoint", "r_m1_dragpoint", "r_m2_dragpoint", "r_m3_dragpoint", "r_m4_dragpoint", "r_m5_dragpoint"]
        
        for label, tag in zip(label_names, tag_names):
            dpg.add_drag_point(label=label, default_value=[0,0], color=[255, 255, 0], tag=tag, show=False, callback=on_drag_point)

        
        dpg.bind_item_theme("phase_series_1", "plot_theme")

    # Make a second plot for the Magnitude
    with dpg.plot(label="Average Plot", height=350, width=1100, pos=(330, 391), tag="average_plot"):
        dpg.add_plot_legend(horizontal=True)

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_2")
        dpg.set_axis_limits(dpg.last_item(), freq_start, freq_end)

        with dpg.plot_axis(dpg.mvYAxis, label="Phase (deg)", tag="phase_axis_2"):
            dpg.set_axis_limits(dpg.last_item(), phase_start, phase_end)
        
        with dpg.plot_axis(dpg.mvYAxis, label="Magnitude (dB)", tag="magnitude_axis_2"):
            dpg.set_axis_limits(dpg.last_item(), mag_start, mag_end)

        # Set a placeholder for the magnitude data
        dpg.add_line_series([0, 0], [0, 0], label="Phase", parent="phase_axis_2", tag="phase_series_2")
        dpg.add_line_series([0, 0], [0, 0], label="Magnitude", parent="magnitude_axis_2", tag="magnitude_series_2")

        dpg.add_drag_line(label="Average_Data_1_x", color=[0, 255, 0], thickness=1.0,
                                vertical=True, show=False, default_value=100,
                                callback=on_drag_line, tag="a_drag_line_1")
        
        dpg.add_drag_line(label="Average_Data_2_x", color=[0, 255, 0], thickness=1.0,
                                vertical=True, show=False, default_value=6000,
                                callback=on_drag_line, tag="a_drag_line_2")
        
        label_names = ["Average_Data_1_p", "Average_Data_2_p", "Average_Data_3_p", "Average_Data_4_p", "Average_Data_5_p", "Average_Data_6_p"]
        tag_names = ["a_m0_dragpoint", "a_m1_dragpoint", "a_m2_dragpoint", "a_m3_dragpoint", "a_m4_dragpoint", "a_m5_dragpoint"]

        for label, tag in zip(label_names, tag_names):
            dpg.add_drag_point(label=label, default_value=[0,0], color=[255, 255, 0], tag=tag, show=False, callback=on_drag_point)


        dpg.bind_item_theme("phase_series_2", "plot_theme")
        

# Create the viewport and start the Dear PyGui event loop
dpg.create_viewport(title=title, width=1450, height=1000, resizable=False)
dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()

while dpg.is_dearpygui_running():
    # Run the get_data_from_serial function every 100ms
    dpg.render_dearpygui_frame()
# Once the event loop has finished, clean up

dpg.destroy_context()