1. First Steps

Tutorials will give a broad overview and working knowledge of DPG. Tutorials do not cover every detail so refer to the documentation on each topic to learn more.
1.1. Installing

Python 3.6 (64 bit) or above is required.

pip install dearpygui

1.2. First Run

Confirm the pip install by running the code block below.

Code:

import dearpygui.dearpygui as dpg

dpg.create_context()
dpg.create_viewport(title='Custom Title', width=600, height=300)

with dpg.window(label="Example Window"):
    dpg.add_text("Hello, world")
    dpg.add_button(label="Save")
    dpg.add_input_text(label="string", default_value="Quick brown fox")
    dpg.add_slider_float(label="float", default_value=0.273, max_value=1)

dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()
dpg.destroy_context()

Result:
https://raw.githubusercontent.com/hoffstadt/DearPyGui/assets/readme/first_app.gif
1.3. Demo

DPG has a complete built-in demo/showcase. It is a good idea to look into this demo. The code for this can be found in the repo in the `demo.py`_ file

Code:

import dearpygui.dearpygui as dpg
import dearpygui.demo as demo

dpg.create_context()
dpg.create_viewport(title='Custom Title', width=600, height=600)

demo.show_demo()

dpg.setup_dearpygui()
dpg.show_viewport()
dpg.start_dearpygui()
dpg.destroy_context()

Result:
https://raw.githubusercontent.com/hoffstadt/DearPyGui/assets/readme/demo.gif

Note

The main script must always:

    Create the context create_context

    Create the viewport create_viewport

    Setup dearpygui setup_dearpygui

    Show the viewport show_viewport

    Start dearpygui start_dearpygui

    Clean up the context destroy_context

------------------------------------


What & Why
What is DPG

DPG is a simple, bloat-free, and powerful Python GUI framework.

DPG is built with Dear ImGui and other extensions in order to create a unique retained mode API, as opposed to Dear ImGui’s immediate mode paradigm.

Under the hood, DPG uses the immediate mode paradigm allowing for extremely dynamic interfaces. Similar to PyQt, DPG does not use native widgets but instead draws the widgets using your computer’s graphics card (using Directx11, Metal, and Vulkan rendering APIs).

In the same manner Dear ImGui provides a simple way to create tools for game developers, DPG provides a simple way for python developers to create quick and powerful GUIs for scripts.
Why use DPG

When compared with other Python GUI libraries DPG is unique with:

    GPU rendering

    Multithreaded

    Highly customizable

    Built-in developer tools: theme inspection, resource inspection, runtime metrics

    70+ widgets with hundreds of widget combinations

    Detailed documentation, examples and support

