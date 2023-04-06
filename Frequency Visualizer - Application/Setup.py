from setuptools import setup

setup(
    name='Filter Frequency Response Visualizer',
    version='0.1',
    description='Visualizing the frequency response of a filter',
    author='Team 18',
    packages=[''],
    install_requires=[
        'dearpygui',
        'pyserial',
        'pyscreenshot',
        'pygetwindow',
        'Pillow',
        'pandas',
        'numpy',
        'tk'
    ],
    entry_points={
        'console_scripts': [
        ]
    }
)

