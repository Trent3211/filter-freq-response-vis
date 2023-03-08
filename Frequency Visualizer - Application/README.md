# FFRV - Filter Frequency Response Visualizer

FFRV is a Python application for testing and analyzing the performance of a device under test (DUT) using a Due microcontroller. 

## Installation

To install the FFRV application, please ensure Python is installed as well as all required dependencies using the Setup.py script:


```python setup.py install```

## Running the Application

To run the FFRV application, execute the ffrv.py script using Python:


```python ffrv.py```

The application will launch and present a user interface for connecting to and testing a DUT using the Due microcontroller. The Connect/Disconnect buttons can be used to establish and terminate a connection to the Due, and the Sweep option in the top object window menu can be used to initiate a sweep of the DUT's response.

During testing, the application will present a real-time plot and table of the raw and running average data from the Due. Error testing conditions have been used for debugging and monitoring for invalid data. Currently, all debug notifications and information is printed in the user's executed environment (cmd, VScode terminal, etc).

## License

FFRV is licensed under the MIT License. See the LICENSE file for more information.

## Acknowledgments

The FFRV application was developed by Jarrod Rowson and as part of Filter Frequency Response Visualizer for Team 18 at San Diego State University. Special thanks to [Dr. Ashrafi].
