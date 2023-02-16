import dearpygui.dearpygui as dpg
import math
import random
import webbrowser
import serial
from serial.serialutil import SerialException
import time
import serial.tools.list_ports

dpg.create_context()

def print_me(sender):
    print(f"[{sender}] was clicked")

def get_available_ports():
    ports = []
    for port in serial.tools.list_ports.comports():
        ports.append(port.device)
    return ports

# Create the UI
def create_ui(portName_input, baud_rate_input, bytesize_input, stop_bits_input, parity_input, connect_button, disconnect_button, connection_status):

    def get_portName():
        return dpg.get_value(portName_input)

    def get_baud_rate():
        return int(dpg.get_value(baud_rate_input))

    def get_bytesize():
        return int(dpg.get_value(bytesize_input))

    def get_stop_bits():
        return int(dpg.get_value(stop_bits_input))

    def get_parity():
        return dpg.get_value(parity_input)

    def on_connect_button():
        global ser  # Make the ser object global so that it can be used in other functions
        try:
            ser = serial.Serial(
                port=get_portName(),
                baudrate=get_baud_rate(),
                bytesize=get_bytesize(),
                stopbits=get_stop_bits(),
                parity=get_parity(),
                timeout=0.5
            )
            print("Connected to serial port:", ser.name, "at", ser.baudrate, "baud", ser.bytesize, "data bits", ser.stopbits, "stop bits", ser.parity, "parity")
            dpg.set_value(connection_status, "Connected")
        except SerialException:
            print(f"Error connecting to device: Serial port {get_portName()} is not available")
            return 

    def on_disconnect_button():
        global ser  # Make sure the ser object is the same one used in on_connect_button
        if ser is not None and ser.is_open:
            ser.close()
            print("Disconnected from serial port:", ser.name)
            dpg.set_value(connection_status, "Disconnected")
        else:
            print("Serial port is not open, cannot disconnect")



    with dpg.window(label="Object Window", width=1450, height=1000, pos=(0, 0)):
        # Menu bar #
        with dpg.menu_bar():
            with dpg.menu(label="File"):
                dpg.add_menu_item(label="Exit", callback=print_me)

            with dpg.menu(label="Screenshot"):
                dpg.add_menu_item(label="Save Plot Bitmap", callback=print_me)

            with dpg.menu(label="Data"):
                dpg.add_menu_item(label="Import Data Points", callback=print_me)
                dpg.add_menu_item(label="Export Data Points", callback=print_me)

            with dpg.menu(label="Help"):
                dpg.add_menu_item(label="Plotting Documentation", callback=print_me)
                dpg.add_menu_item(label="Github Repository", callback=print_me)
                dpg.add_menu_item(label="About", callback=print_me)

        #region Child Windows
        # Connection Handler Child Window #
        with dpg.child_window(label="Connection Handler", width=320, height=200, pos=(6, 42), menubar=True):
            with dpg.menu_bar():
                dpg.add_menu(label="Connection Handling")

            dpg.add_input_text(label="Baud Rate", default_value=baud_rate_input, width=230, tag=baud_rate_input)
            dpg.add_input_text(label="Data Bits", default_value=bytesize_input, width=230, tag=bytesize_input)
            dpg.add_input_text(label="Stop Bits", default_value=stop_bits_input, width=230, tag=stop_bits_input)
           

            dpg.add_input_text(label="Parity", default_value=parity_input, width=230, tag=parity_input)

            # Use a function to find the available ports
            # TODO - Find a way to find the available ports
            dpg.add_combo(label="Port Name", default_value=portName_input, items=get_available_ports(), width=230, tag=portName_input)
            with dpg.tooltip(dpg.last_item()):
                dpg.add_text("Check the device manager to see which port your device is connected to.")

            with dpg.group(horizontal=True, parent="portName_input"):
                dpg.add_button(label="Connect", callback=on_connect_button, width=111, tag=connect_button)
                dpg.add_button(label="Disconnect", callback=on_disconnect_button, width=111, tag=disconnect_button)
                
            # Provide the status of the connection and allow it to be updated
            with dpg.group(horizontal=True, parent="portName_input"):
                dpg.add_text("Connection Status: ", parent="portName_input")
                dpg.add_text("Disconnected", tag=connection_status, parent="portName_input")     

        # Data Child Window #
        with dpg.child_window(label="Object Window", width=320, height=494, pos=(6, 246), menubar=True):
            with dpg.menu_bar():
                dpg.add_menu(label="Raw Data")
            
            with dpg.table(header_row=True, no_host_extendX=True,
                                    borders_innerH=True, borders_outerH=True, borders_innerV=True,
                                    borders_outerV=True, row_background=True, hideable=False, reorderable=True,
                                    resizable=False, sortable=True, policy=dpg.mvTable_SizingFixedFit,
                                    scrollX=False, delay_search=True, scrollY=True):

                        c1 = dpg.add_table_column(label="Index")
                        c2 = dpg.add_table_column(label="Magnitude")
                        c3 = dpg.add_table_column(label="Phase")

        # Logger Child Window #
        with dpg.child_window(label="Object Window", width=1424, height=210, pos=(6, 744), menubar=True):
            with dpg.menu_bar():
                dpg.add_menu(label="Activity Logger")
                dpg.add_button(label="Clear", callback=print_me)


        # Plot Child Window #
        with dpg.plot(label="Phase Series", height=345, width=1100, pos=(330, 40)):
            # optionally create legend
            dpg.add_plot_legend()

            # REQUIRED: create x and y axes
            dpg.add_plot_axis(dpg.mvXAxis, label="x_1")
            dpg.add_plot_axis(dpg.mvYAxis, label="y_1", tag="y_axis_1")

        with dpg.plot(label="Magnitude Series", height=345, width=1100, pos=(330, 390)):
            # optionally create legend
            dpg.add_plot_legend()

            # REQUIRED: create x and y axes
            dpg.add_plot_axis(dpg.mvXAxis, label="x_2")
            dpg.add_plot_axis(dpg.mvYAxis, label="y_2", tag="y_axis_2")

def main():
    # Populate portName with the available ports
    portName_input = get_available_ports()[0]
    baud_rate_input = 115200
    bytesize_input = 8 
    stop_bits_input = 1
    parity_input = "N"
    connect_button = dpg.generate_uuid()
    disconnect_button = dpg.generate_uuid()
    connection_status = dpg.generate_uuid()

    dpg.create_context()

    # Create the UI
    create_ui(portName_input, baud_rate_input, bytesize_input, stop_bits_input, parity_input, connect_button, disconnect_button, connection_status)

    # Create the viewport and start the Dear PyGui event loop
    dpg.create_viewport(title="Filter Frequency Response Visualizer", width=1450, height=1000, resizable=False)
    dpg.setup_dearpygui()
    dpg.show_viewport()
    dpg.start_dearpygui()

    # Once the event loop has finished, clean up
    dpg.destroy_context()


# Run the main() function __init__
if __name__ == '__main__':
    main()
