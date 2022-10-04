# filter-freq-response-vis
GitHub repo for SDSU Senior Design Team 18, Filter Frequency Response Visualizer, or FFRV


## USING GITHUB
### Basic Operation: 
Git is both local version control (local) and also cloud version control (remote). Version control is logging how changes are made to documents (think google docs history) so that way changes can be managed. This is useful in code, where you may have a working solution but you want to add features to it, so "committing" that version to a history log allows you to make changes, and always have a successful solution to return to. Basically, git is a way to log changes you make files, and then upload them to the cloud (have them stored virtually, rather than locally). That's the basic operation. There is more features that will be used, but at its most basic operation its changing files and then logging those changes. 
### Creating a commit: 
To use Git, you'll link your local to the remote. As you make changes to the whatever files you're editing (NET application, INO file, KiCAD schematic, LTSpice simulation, etc.) you'll want to mark good changes. You mark these changes with a "commit". Committing works in three phases. First, you add the files you want to a new commit (using command line argument, or CLI). Second, you'll "commit" these changes to your local git with a message that describes the changes that we're made from the previous commit to the current commit (there is a standardized way for creating commit messages), using the CLI. Third, you will "push" your local changes to the remote, again using the CLI. 
### Install Git:
### Command Line Arguments:
__0a) Go to repository:__ cd \<local file path for github repo\> 
<br>
__0b) Pull any new changes:__ git pull 
<br>
__1) Add Files:__ git add \<file name1\> \<file name2\> ... \<file name x\> 
<br>
__2) Create Log Message:__ git commit 
<br>
-> NOTE: This will open up a default text editor. To set that text editor to Windows default text editor, see "Other Helpful CLI" 
<br>
__3) Push Changes:__ git push -u origin
<br>
-> NOTE: origin refers to the cloud repository, as it is the first instantiation of the github repo. 

### Other Helpful CLI:
