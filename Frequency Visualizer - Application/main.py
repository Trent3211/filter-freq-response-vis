import dearpygui.dearpygui as dpg
import math
import random
import webbrowser
from math import sin, cos, tan


dpg.create_context()

    #region Variables and Data

sindatax = []
sindatay = []
for i in range(0, 100000):
    sindatax.append(i / 100)
    sindatay.append(0.5 + 0.5 * sin(i / 100))

tandatax = []
tandatay = []
for i in range(0, 100000):
    tandatax.append(i / 100)
    tandatay.append(0.5 + 0.5 * cos(i / 100)) 
#     #endregion

    #region Functions
def print_me(sender):
    # Write to the terminal
    print(f"[DEBUG] Object action: ", sender) 

def _hyperlink(text, address):
    b = dpg.add_button(label=text, callback=lambda:webbrowser.open(address))
    dpg.bind_item_theme(b, "__demo_hyperlinkTheme")


    #endregion

    #region Main Window
with dpg.window(label="Object Window", width=1450, height=1000, pos=(0, 0)):
    # Menu bar #
    with dpg.menu_bar():
        with dpg.menu(label="File"):
            dpg.add_menu_item(label="Exit", callback=print_me)

        with dpg.menu(label="Sweep"):
            dpg.add_menu_item(label="Begin sweep", callback=print_me)
            dpg.add_menu_item(label="Stop sweep", callback=print_me)
        
        with dpg.menu(label="Screenshot"):
            dpg.add_menu_item(label="Save Plot Bitmap", callback=print_me)

        with dpg.menu(label="Data"):
            dpg.add_menu_item(label="Import Data Points", callback=print_me)
            dpg.add_menu_item(label="Export Data Points", callback=print_me)            
        
        with dpg.menu(label="Help"):
            dpg.add_menu_item(label="Plotting Documentation", callback=print_me)
            dpg.add_menu_item(label="About", callback=print_me)

    #endregion
        
    #region Child Windows
    # Connection Handler Child Window #
    with dpg.child_window(label="Object Window", width=320, height=200, pos=(6, 42), menubar=True):
        with dpg.menu_bar():
             dpg.add_menu(label="Connection Handling")
        dpg.add_input_int(label="Baud Rate", default_value=9600, width=230)
        dpg.add_input_int(label="Data Bits", default_value=8, width=230)
        dpg.add_input_int(label="Stop Bits", default_value=1, width=230)
        dpg.add_input_int(label="Parity", default_value=0, width=230)

        # Use a function to find the available ports
        # TODO - Find a way to find the available ports
        dpg.add_combo(label="Port", items=["COM1", "COM2", "COM3", "COM4"], width=230)
        with dpg.tooltip(dpg.last_item()):
            dpg.add_text("Check the device manager to see which port your device is connected to.")
        

        with dpg.group(horizontal=True, parent="Port"):
            dpg.add_button(label="Connect", callback=print_me, width=111)
            dpg.add_button(label="Disconnect", callback=print_me, width=111)

        # Provide the status of the connection and allow it to be changed depending on the connection status
        # TODO - Find a way to change the status of the connection
        dpg.add_text("Connection Status: Disconnected", tag="Connection Status")

             

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

                    for i in range(100):
                        with dpg.table_row():
                            dpg.add_text(i)
                            # Make this to two decimal places
                            dpg.add_text(f"{i * .05847:.4f}") 
                            dpg.add_text(f"{i * .08475 * 100:.4f}")

    
    # Logger Child Window #
    with dpg.child_window(label="Object Window", width=1424, height=210, pos=(6, 744), menubar=True):
        with dpg.menu_bar():
             dpg.add_menu(label="Activity Logger")
             dpg.add_button(label="Clear Logger", callback=print_me)


             
    #endregion

    #region Plots
    with dpg.plot(label="Phase Series", height=346, width=1100, pos=(330, 42)):
    # optionally create legend
        dpg.add_plot_legend()

        # REQUIRED: create x and y axes
        dpg.add_plot_axis(dpg.mvXAxis, label="x_1")
        dpg.add_plot_axis(dpg.mvYAxis, label="y_1", tag="y_axis_1")
 
        # series belong to a y axis
        dpg.add_line_series(sindatax, sindatay, label="Phase", parent="y_axis_1")

    with dpg.plot(label="Magnitude Series", height=346, width=1100, pos=(330, 394)):
        # optionally create legend
        dpg.add_plot_legend()

        # REQUIRED: create x and y axes
        dpg.add_plot_axis(dpg.mvXAxis, label="x_2")
        dpg.add_plot_axis(dpg.mvYAxis, label="y_2", tag="y_axis_2")

        # series belong to a y axis
        dpg.add_line_series(tandatax, tandatay, label="Magnitude", parent="y_axis_2")
    
    #endregion

    #region Filter Frequency Response Visualizer
dpg.create_viewport(title=' Filter Frequency Response Visualizer', width=1450, height=1000) 
dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()
dpg.destroy_context()
    #endregion