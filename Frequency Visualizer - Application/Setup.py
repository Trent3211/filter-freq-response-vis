from setuptools import setup, find_packages

setup(
    name='Filter Frequency Response Visualizer',
    version='1.0.0',
    description='Visualizing the frequency response of a filter',
    author='Team 18',
    author_email='jrowson7843@sdsu.edu',
    packages=find_packages(),
    install_requires=[
        'dearpygui',
        'pyserial',
        'pyscreenshot',
        'pygetwindow',
        'Pillow',
        'pandas',
        'numpy'
    ],
)
