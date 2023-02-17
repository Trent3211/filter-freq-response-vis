import dearpygui.dearpygui as dpg
import threading
import serial
import serial.tools.list_ports

# Initialize variables for the plot data
nsamples = 30000
data_y = [0.0] * nsamples
data_x = [0.0] * nsamples

# Create a global variable for the serial connection and connection status
ser = None
connected = False

# Define the function to read data from the Arduino and update the plot
def update_data():
    global ser, connected
    if not connected:
        return

    command = "s 20 30000 1 1\n"
    print(f"Sending command: {command}")
    ser.write(command.encode())

    print("Sweep started")
    while connected:
        # Read data from the serial port
        try:
            line = ser.readline().decode().strip()
        except UnicodeDecodeError:
            continue

        if not line:
            continue

        # Parse the data into frequency and voltage
        try:
            frequency, voltage = line.split(":")
            frequency = float(frequency)
            voltage = float(voltage)
            print(frequency, voltage)  # print parsed data
        except ValueError:
            continue

        # Add the new data point to the data lists
        data_x.append(frequency)
        data_y.append(voltage)

        # Set the series x and y to the last nsamples
        dpg.set_value('series_tag', [list(data_x[-nsamples:]), list(data_y[-nsamples:])])
        dpg.fit_axis_data('x_axis')
        dpg.fit_axis_data('y_axis')

        
def get_available_ports():
    ports = []
    for port in serial.tools.list_ports.comports():
        ports.append(port.device)
    return ports

# Define the function to connect to the Arduino
def connect_to_arduino(port, baudrate):
    global ser, connected
    try:
        ser = serial.Serial(port=port, baudrate=baudrate, timeout=1)
        print("Connected to serial port:", ser.name, "at", ser.baudrate, "baud")
        connected = True
        # Start the thread to update the plot
        thread = threading.Thread(target=update_data)
        thread.start()
    except serial.SerialException:
        print("Error connecting to device: Serial port", port, "is not available")
        ser = None

# Create the GUI
dpg.create_context()
with dpg.window(label='Tutorial', tag='win', width=1100, height=800):
    with dpg.plot(label='Line Series', height=500, width=1000):
        # Optionally create legend
        dpg.add_plot_legend()

        # REQUIRED: create x and y axes, set to auto scale.
        x_axis = dpg.add_plot_axis(dpg.mvXAxis, label='Frequency (Hz)', tag='x_axis')
        y_axis = dpg.add_plot_axis(dpg.mvYAxis, label='Voltage (V)', tag='y_axis')

        # Create a line series for the data
        dpg.add_line_series(x=list(data_x), y=list(data_y), 
                            label='Data', parent='y_axis', 
                            tag='series_tag')

    # Add the button to connect to the Arduino
    dpg.add_button(label='Connect', callback=lambda: connect_to_arduino(dpg.get_value('port'), dpg.get_value('baudrate')))
    dpg.add_combo(label='Port', items=get_available_ports(), default_value='COM6', tag='port')
    dpg.add_combo(label='Baudrate', items=[9600, 19200, 38400, 57600, 115200], default_value=115200, tag='baudrate')

# Create the viewport and start the Dear PyGui event loop
dpg.create_viewport(title="Filter Frequency Response Visualizer", width=1450, height=1000, resizable=False)
dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()

# Once the event loop has finished, clean up
dpg.destroy_context()