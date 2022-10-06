# Filter Frequency Response Visualizer
GitHub repo for SDSU Senior Design Team 18, Filter Frequency Response Visualizer, or FFRV

## SYSTEM DESCRIPTION

The FFRV system  will be a test box used for measuring the frequency response of a user-provided passive filter. Once started, the system will apply a frequency sweep across an inputted filter, then measure the filter output gain and phase shift using analog computations, inputted to an Arduino Due microcontroller. Thus, this FFRV system will calculate the Bode plots for a real-world filter. The purpose of the FFRV system is to allow students, hobbyists, and other casual filter-designers a rapid way to gain insight into simple filters, such as what frequencies they pass or reject. As such, the FFRV system will be designed to be straightforward to use. It shall have a box face with only a select few clearly-labeled inputs, and output a data file of the measurements to the user computer, connected over USB to the test box. The functional components in this system will include a procured waveform generator, a PCB for distributing power and performing analog analysis on filter magnitude and phase, an Arduino Due for converting the analog values to digital and controlling the waveform generator, and a NET application which will provide the user an interface to work with the system.


## USING GITHUB

### Basic Operation: 

Git is both local version control (local) and also cloud version control (remote). Version control is logging how changes are made to documents (think google docs history) so that way changes can be managed. This is useful in code, where you may have a working solution but you want to add features to it, so "committing" that version to a history log allows you to make changes, and always have a successful solution to return to. Basically, git is a way to log changes you make files, and then upload them to the cloud (have them stored virtually, rather than locally). That's the basic operation. There is more features that will be used, but at its most basic operation its changing files and then logging those changes. 

### Creating a commit: 

To use Git, you'll link your local to the remote. As you make changes to the whatever files you're editing (NET application, INO file, KiCAD schematic, LTSpice simulation, etc.) you'll want to mark good changes. You mark these changes with a "commit". Committing works in three phases. First, you add the files you want to a new commit (using command line argument, or CLI). Second, you'll "commit" these changes to your local git with a message that describes the changes that we're made from the previous commit to the current commit (there is a standardized way for creating commit messages), using the CLI. Third, you will "push" your local changes to the remote, again using the CLI. 

### Install Git:

__Create Git Repo Folder__
<br>
Purely for containment sakes, you may want to create a folder for holding all your GitHub repos. This is optional. If you already have a folder, then you have no need to create a new folder. I (Trent) would suggest creating a folder titled "GitHub-Repos" in your Documents folder on your computer. You can create that folder using the following CLI commands. (To open the terminal, type "Command Prompt" in the search function on your windows machine, then click on the application)
```
cd Documents
mkdir GitHub-Repos
cd GitHub-Repos
```
__Add the repository__
<br>
Go to the desired directory (if you followed the steps in "Create Git Repo Folder", you will already be in the desired folder) and clone the repository. The comand will look like the following
```
git clone https://github.com/<GitHub User Name>/filter-freq-response-vis.git <Optional: What you'd like the document to be named>
```
Sections wrapped by \< \> will want to be replaced by what is referred to in the section. For instance, I (Trent) would replace `<GitHub User Name>` with `Trent3211`. You should be able to now see a folder with the title you provided in the `<Optional: ...>` section, or it will be titled filter-freq-reponse-vis. You can view this in your file explorer, or by using the `dir` command in the Terminal (Command Prompt). To verify that the repository was properly imported, open the folder using the Windows File explorer. Check the folder for the README.md file. Its contents should match this file. 

### Command Line Arguments:

__0a) Go to repository:__ `cd <local file path for github repo> `
<br>
__0b) Pull any new changes:__ `git fetch`
<br>
__1) Add Files:__ `git add <file name1> <file name2> ... <file name x> `
<br>
-> NOTE: Use `git status` to see what changes you've made to files. Those that have been edited will appear in red. After you've added them, if you run `git status` again they should appear green.
<br>
__2) Create Log Message:__ `git commit `
<br>
-> NOTE: This will open up a default text editor. To set that text editor to Windows default text editor, see "Other Helpful CLI" 
<br>
__3) Push Changes:__ `git push -u origin`
<br>
-> NOTE: origin refers to the cloud repository, as it is the first instantiation of the github repo. 

### Other Helpful CLI:
