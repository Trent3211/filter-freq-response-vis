import dearpygui.dearpygui as dpg
import serial
import math
from math import sin, cos
from serial.serialutil import SerialException
import serial.tools.list_ports
import pyscreenshot
import pygetwindow
from PIL import Image
import webbrowser
import pandas as pd
import datetime
import os

ser = None
magnitude = []
phase = []
frequency = []
ports = []
# Current directory screenshot folder
screenshot_path = "./screenshots/"
data_path = "./data/"
titles = pygetwindow.getAllTitles()


dpg.create_context()

def github():
    webbrowser.open("https://github.com/Trent3211/filter-freq-response-vis")

def about():
    webbrowser.open("https://github.com/Trent3211/filter-freq-response-vis/blob/main/README.md")

def plotting_documentation():
    webbrowser.open("https://dearpygui.readthedocs.io/en/latest/documentation/plots.html")


def print_me(sender):
    print(f"[{sender}] was clicked")

def get_available_ports():

    # Now that the combo box has been cleared, add the available ports
    for port in serial.tools.list_ports.comports():
        ports.append(port.device + " - " + port.description)
    return ports

def stop_data():
    # Send the character command 'r'
    command = "e\n"
    ser.write(command.encode())
    print("Sweep stopped")

def on_connect_button():
    global ser  # Make the ser object global so that it can be used in other functions
    try:
        ser = serial.Serial(
            # The port name is the selected item in the combo box, but it must be parsed to remove the description
            port=dpg.get_value('port_name').split(" - ")[0],
            baudrate=dpg.get_value('baud_rate'),
            timeout=1
        )
        print("Connected to serial port:", ser.name, "at", ser.baudrate, "baud")
        dpg.set_value('connection_status', "Connected")
    except SerialException:
        print(f"Error connecting to device: Serial port {dpg.get_value('port_name')} is not available")
        return 

def on_disconnect_button():
    global ser  # Make sure the ser object is the same one used in on_connect_button
    if ser is not None and ser.is_open:
        ser.close()
        print("Disconnected from serial port:", ser.name)
        dpg.set_value('connection_status', "Disconnected")
    elif ser is None:
        print("Serial port has not been initialized yet.")
    else:
        print("Serial port is already closed.")

# Create a definition to add the cursors to the plots based on which checkbox is selected
# There are two checkboxes, one called phase_cursors_checkbox and one called magnitude_cursors_checkbox
# Depending on which one is checked, the cursors will be added to the phase plot or the magnitude plot
def add_cursors():
    # Get the values of the checkboxes
    phase_cursors_checked = dpg.get_value("phase_cursors_checkbox")
    mag_cursors_checked = dpg.get_value("magnitude_cursors_checkbox")
    
    if phase_cursors_checked:
        # Using dpg.add_drag_line() to add the cursors to the phase plot
        dpg.add_drag_line(label="Drag Line Phase 1", color=[255, 0, 0, 255], tag="dline1_phase")
        dpg.add_drag_line(label="Drag Line Phase 2", color=[255, 0, 0, 255], tag="dline2_phase")
        print("Phase cursors added")
    else:
        # Remove cursors from the phase plot
        dpg.delete_item("dline1_phase")
        dpg.delete_item("dline2_phase")
        print("Phase cursors removed")
    
    if mag_cursors_checked:
        # Add cursors to the magnitude plot
        dpg.add_drag_line(label="Drag Line Magnitude 1", color=[255, 0, 0, 255], tag="dline1_mag")
        dpg.add_drag_line(label="Drag Line Magnitude 2", color=[255, 0, 0, 255], tag="dline2_mag")
        print("Magnitude cursors added")
    else:
        # Remove cursors from the magnitude plot
        dpg.delete_item("dline1_mag")
        dpg.delete_item("dline2_mag")
        print("Magnitude cursors removed")



def get_data_from_serial():
    global ser
    global magnitude
    global phase
    global frequency

    # Clear the data lists
    magnitude.clear()
    phase.clear()
    frequency.clear()

    # Send the character command 's'
    command = "s\n"
    ser.write(command.encode())
    
    # Clear the data in the main_table using dpg.get_item_children(table_tag, 1)
    for child in dpg.get_item_children("main_table", 1):
        dpg.delete_item(child)

    # Read the data from the serial port until e is read
    while True:
        data = ser.readline().decode('utf-8').strip()
        print(data)
        if data.strip() == 'e': # Edit this for the terminating character
            print("Sweep completed.")
            break
        else:
            # Split the data into its frequency, magnitude, and phase components
            data = data.split(',')
            if len(data) >= 3:
                try:
                    frequency.append(float(data[0]))
                    magnitude.append(float(data[1]))
                    phase.append(float(data[2]))

                    dpg.set_value('phase_series', [frequency, phase])
                    dpg.set_item_label('phase_series', f"Phase (deg) - {frequency[-1]} Hz")

                    dpg.set_value('magnitude_series', [frequency, magnitude])
                    dpg.set_item_label('magnitude_series', f"Magnitude (dB) - {frequency[-1]} Hz")

                    # Add the data to the table
                    with dpg.table_row(parent="main_table"):
                        dpg.add_text(str(frequency[-1]))
                        dpg.add_text(str(magnitude[-1]))
                        dpg.add_text(str(phase[-1]))

                except ValueError:
                    print("Invalid data: ", data)
                    continue
    
    # The plots have axis constraints, but now that the sweep is done, we can set it back to autofit
    dpg.set_axis_limits_auto('frequency_axis_1')
    dpg.set_axis_limits_auto('frequency_axis_2')
    dpg.set_axis_limits_auto('magnitude_axis')
    dpg.set_axis_limits_auto('phase_axis')

    # Send the character command 'r'
    command = "r\n"
    ser.write(command.encode())

def take_screenshot():
    # Just take a screenshot of the entire window
    im = pyscreenshot.grab()
    now = datetime.datetime.now()
    timestamp = now.strftime("%Y-%m-%d_%H-%M-%S")
    im.save(screenshot_path + "screenshot_{}.png".format(timestamp))
    print("Screenshot saved to", screenshot_path + "screenshot_{}.png".format(timestamp))

def table_to_csv():
    global frequency
    global magnitude
    global phase

    if len(frequency) > 0:
        now = datetime.datetime.now()
        timestamp = now.strftime("%Y-%m-%d_%H-%M-%S")

        # Check if the directory exists, create it if it does not exist
        if not os.path.exists(data_path):
            os.makedirs(data_path)

        # Create a pandas dataframe from the data
        df = pd.DataFrame({
            "Frequency (Hz)": frequency,
            "Magnitude (dB)": magnitude,
            "Phase (deg)": phase
        })
        # Save the dataframe to a csv file with the syntax "data_YYYY-MM-DD_HH-MM-SS.csv"
        df.to_csv(data_path + "data_{}.csv".format(timestamp), index=False)
        print("Data saved to", data_path + "data_{}.csv".format(timestamp))
    else:
        print("No data to save")




with dpg.window(label="Object Window", width=1450, height=1000, pos=(0, 0), tag="main_window"):
    # Theme #
    with dpg.theme(tag="mag_plot_theme"):
        with dpg.theme_component(dpg.mvLineSeries):
            dpg.add_theme_color(dpg.mvPlotCol_Line, (50, 100, 150), category=dpg.mvThemeCat_Plots)
            

    with dpg.theme(tag="phase_plot_theme"):
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

        with dpg.menu(label="Screenshot"):
            dpg.add_menu_item(label="Save Application Screenshot", callback=take_screenshot)

        with dpg.menu(label="Data"):
            dpg.add_menu_item(label="Export Raw Data", callback=table_to_csv)

        with dpg.menu(label="Help"):
            dpg.add_menu_item(label="Plotting Documentation", callback=plotting_documentation)
            dpg.add_menu_item(label="Github Repository", callback=github)
            dpg.add_menu_item(label="About", callback=about)

    # Connection Handler Child Window #
    with dpg.child_window(label="Connection Handler", width=320, height=130, pos=(6, 42), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Connection Handling")

        dpg.add_combo(label="Baud Rate", default_value=115200, items=[9600, 19200, 38400, 57600, 115200], width=230, tag='baud_rate')
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("The baud rate of the serial connection.")
        dpg.add_combo(label="Port Name", default_value="Select a port...", items=get_available_ports(), width=230, tag='port_name')
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("Check the device manager to see which port your device is connected to.")

        with dpg.group(horizontal=True, parent="connect_group"):
            dpg.add_button(label="Connect", callback=on_connect_button, width=111, tag='connect_button')
            dpg.add_button(label="Disconnect", callback=on_disconnect_button, width=111, tag='disconnect_button')
            dpg.add_button(label="Refresh", callback=print_me, width=65, tag='refresh_button')
            
        # Provide the status of the connection and allow it to be updated
        with dpg.group(horizontal=True, parent="status_group"):
            dpg.add_text("Connection Status: ")
            dpg.add_text("Disconnected", tag='connection_status')     

    # Data Child Window #
    with dpg.child_window(label="Object Window", width=320, height=560, pos=(6, 180), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Raw Data")
        
        with dpg.table(label="Table Data", tag="main_table", header_row=True, no_host_extendX=True,
                                        borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                        borders_outerV=True, row_background=True, hideable=False, reorderable=False,
                                        resizable=False, sortable=False, policy=dpg.mvTable_SizingFixedFit,
                                        scrollX=False, delay_search=True, scrollY=True):

                    c1 = dpg.add_table_column(label="Freq. (Hz)")
                    c2 = dpg.add_table_column(label="Mag. (dB)")
                    c3 = dpg.add_table_column(label="Phase (deg)")

    # Logger Child Window #
    with dpg.child_window(label="Object Window", width=320, height=210, pos=(6, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_button(label="Clear", callback=print_me)
            # Make this a logger textbox
            dpg.add_text(tag="logger")
        
    with dpg.child_window(label="Phase Plot Options", width=548, height=210, pos=(330, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Phase Plot Options")
        with dpg.group(horizontal=True, parent="phase_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="p_m0_checkbox")
            dpg.add_checkbox(label="M1", tag="p_m1_checkbox")
            dpg.add_checkbox(label="M2", tag="p_m2_checkbox")
            dpg.add_checkbox(label="Phase Cursors", default_value=False, callback=add_cursors, tag="phase_cursors_checkbox", parent="phase_plot")
        

    with dpg.child_window(label="Magnitude Plot Options", width=548, height=210, pos=(882, 744), menubar=True):
        with dpg.menu_bar():
            dpg.add_menu(label="Magnitude Plot Options")
            # Make the below horizontal
        with dpg.group(horizontal=True, parent="magnitude_checkbox_group"):
            dpg.add_checkbox(label="M0", tag="m_m0_checkbox")
            dpg.add_checkbox(label="M1", tag="m_m1_checkbox")
            dpg.add_checkbox(label="M2", tag="m_m2_checkbox")
            dpg.add_checkbox(label="Magnitude Cursors", default_value=False, callback=add_cursors, tag="magnitude_cursors_checkbox", parent="magnitude_plot")

    with dpg.plot(label="Phase Plot", height=350, width=1100, pos=(330, 42), tag="phase_plot"):
        # Optionally create legend
        dpg.add_plot_legend()

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_1")
        # Set the axis limits using set_axis_limits(axis, min, max)
        dpg.set_axis_limits(dpg.last_item(), 20, 30000)

        dpg.add_plot_axis(dpg.mvYAxis, label="Phase (deg)", tag="phase_axis")
        dpg.set_axis_limits(dpg.last_item(), -2, 2)

        # Set a placeholder for the phase data
        # DearPyGui uses the formatting of dpg.add_line_series(sindatax, sindatay, label="Sine Wave", parent="y_axis",tag="sine_series")
        # This is just a placeholder, and needs to use my actual data and variables from my code.
        dpg.add_line_series([0, 0], [0, 0], label="Phase", parent="phase_axis", tag="phase_series")

        dpg.bind_item_theme("phase_series", "phase_plot_theme")

    # Make a second plot for the Magnitude
    with dpg.plot(label="Magnitude Plot", height=350, width=1100, pos=(330, 391), tag="magnitude_plot"):
        dpg.add_plot_legend()

        dpg.add_plot_axis(dpg.mvXAxis, label="Frequency (Hz)", log_scale=True, tag="frequency_axis_2")
        dpg.set_axis_limits(dpg.last_item(), 20, 30000)

        dpg.add_plot_axis(dpg.mvYAxis, label="Magnitude (dB)", tag="magnitude_axis")
        dpg.set_axis_limits(dpg.last_item(), -2, 2)

        # Set a placeholder for the magnitude data
        dpg.add_line_series([0, 0], [0, 0], label="Magnitude", parent="magnitude_axis", tag="magnitude_series")

        dpg.bind_item_theme("magnitude_series", "mag_plot_theme")
        

# Create the viewport and start the Dear PyGui event loop
dpg.create_viewport(title="Filter Frequency Response Visualizer", width=1450, height=1000, resizable=False)
dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()

while dpg.is_dearpygui_running():
    # Run the get_data_from_serial function every 100ms
    dpg.render_dearpygui_frame()
# Once the event loop has finished, clean up

dpg.destroy_context()