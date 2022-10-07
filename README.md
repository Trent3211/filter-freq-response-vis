# Filter Frequency Response Visualizer
GitHub repo for SDSU Senior Design Team 18, Filter Frequency Response Visualizer, or FFRV

## SYSTEM DESCRIPTION

The FFRV system  will be a test box used for measuring the frequency response of a user-provided passive filter. Once started, the system will apply a frequency sweep across an inputted filter, then measure the filter output gain and phase shift using analog computations, inputted to an Arduino Due microcontroller. Thus, this FFRV system will calculate the Bode plots for a real-world filter. The purpose of the FFRV system is to allow students, hobbyists, and other casual filter-designers a rapid way to gain insight into simple filters, such as what frequencies they pass or reject. As such, the FFRV system will be designed to be straightforward to use. It shall have a box face with only a select few clearly-labeled inputs, and output a data file of the measurements to the user computer, connected over USB to the test box. The functional components in this system will include a procured waveform generator, a PCB for distributing power and performing analog analysis on filter magnitude and phase, an Arduino Due for converting the analog values to digital and controlling the waveform generator, and a NET application which will provide the user an interface to work with the system.


## USING GITHUB

### Basic Operation: 

Git is both local version control (local) and also cloud version control (remote). Version control is logging how changes are made to documents (think google docs history) so that way changes can be managed. This is useful in code, where you may have a working solution but you want to add features to it, so "committing" that version to a history log allows you to make changes, and always have a successful solution to return to. Basically, git is a way to log changes you make files, and then upload them to the cloud (have them stored virtually, rather than locally). That's the basic operation. There is more features that will be used, but at its most basic operation its changing files and then logging those changes. 

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
git clone https://github.com/Trent3211/filter-freq-response-vis.git <Optional: What you'd like the document to be named>
```
Sections wrapped by \< \> will want to be replaced by what is referred to in the section. You should be able to now see a folder with the title you provided in the `<Optional: ...>` section, or it will be titled filter-freq-reponse-vis. You can view this in your file explorer, or by using the `dir` command in the Terminal (Command Prompt). To verify that the repository was properly imported, open the folder using the Windows File explorer. Check the folder for the README.md file. Its contents should match this file. 

### Creating a commit: 

To use Git, you'll link your local to the remote. As you make changes to the whatever files you're editing (NET application, INO file, KiCAD schematic, LTSpice simulation, etc.) you'll want to mark good changes. You mark these changes with a "commit". Committing works in three phases. First, you add the files you want to a new commit (using command line interface arguments, or CLI). Second, you'll "commit" these changes to your local git with a message that describes the changes that we're made from the previous commit to the current commit (there is a standardized way for creating commit messages), using the CLI. Third, you will "push" your local changes to the remote, again using the CLI. The steps and their associated CLI arguments are shown below:
<br>

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
-> NOTE: This will open the default text editor, "Vim". Vim slightly terrifies me, so I would recommend switching your default editor to Notepad. To set it to that text editor, see "Other Helpful CLI". See notes on creating an effective commit message in the section below
<br>
__3) Push Changes:__ `git push -u origin`
<br>
-> NOTE: origin refers to the cloud repository, as it is the first instantiation of the github repo. 
<br>
<br>
_Commit Message Best Practice_
<br>
There are two parts to a commit message: the subject header and a description. A subject header should encapsulate what was changed in the commit using 50 characters or less. If there is a lot more detail necessary to encapsulate the full picture, that's what the description is used for. Best practice for the subject header is to use the imperative tense. Examples include `Test new data`, or `Rewire ground and power planes`. A subject header should be formatted such that someone reading could read it as "If applied, this commit will ...", followed by the subject header. Thus, the following examples would read "This commit will: Test new data" or "This commit will: Rewire ground and power planes". 
<br>
After writing the subject header, two lines breaks will separate the subject header from the description. The description may add more detail not included in the subject header, reasoning for why the commit was needed, include details about testing and verification steps, or detail errors that were encountered with the version in the commit that will need to be fixed in future commits. Wrap each line of the description after 72 characters, otherwise it will be hard to read using the CLI. More detail on commit messages can be found here [https://gist.github.com/robertpainsi/b632364184e70900af4ab688decf6f53]. A good commit message may look like the following (the sections preceded by # are autogenerated by Git. They can be left in, they will not be included when viewing the message in the future):
```
Rewire ground and power planes

A previous commit had the ground and power planes connected to the same 
netlist. However, there are still some serious ratnests that may 
introduce noise. An issue has been added to the GitHub to address it.
# Please enter the commit message for your changes. Lines starting
# with '#' will be ignored, and an empty message aborts the commit.
#
# On branch fake-branch
# Your branch is up to date with 'origin/fake-branch'.
#
# Changes to be committed:
#       new file:   fake-file
```

### Creating and merging a branch

As we set up issues (think spots of development in achieving our long term milestones) in GitHub and try to solve them, we'll want to create new branches to address them. Branching keeps the main tree clean as solutions develop, so that they are only added into the `main` branch when they are finished being developed and have been reviewed by other members of the team. The naming convention for a branch is the issue number, followed by an underscore, and a 1 to 3 word description of what changes the branch encapsulates/will encapsulate. For example, a good name for a branch that addresses issue 0 of team members getting to know each other would be `0_starbucks_meeting`. Be sure to always branch from the main branch, rather than form other branches. This will keep the logging and version control easier to follow. The steps for creating a branch locally and publishing to the remote are shown below:
<br>
<br>
__0a) Update to the remote:__ 
<br>
Run `git fetch` to update your local repository with changes with those made in the remote (the online repository that we all access). Now run `git status` to see how many commits above or behind you are. If you have not made any changes that haven't yet been committed, run `git pull`. You will now be up-to-date with the remote and ready to proceed. 
<br>
__0b) Set to `main` branch:__ 
<br> Run `git status` using the CLI to make sure you are on the main branch. The first line of the text output should detail which branch you are on. If you are not on the main branch, run `git checkout main`. The output in this case should now be:
```
Switched to branch 'main'
Your branch is up to date with 'origin/main'.
```
__1) Create a new local branch:__ 
<br>
To create a new branch, there are two sets of commands you can choose. The first create a new branch off of the current branch, followed by a checkout command to switch to it. First run `git branch <Branch-Name>`. You can verify the branch was properly created by running `git branch`, which will list all available branches. Next, switch to the branch by running `git checkout <Branch-Name>`. The second method for creating a new branch both creates and switches to a branch in one command. This is `git checkout -b <Branch-Name>`. Only one of these two methods may be used to create and switch to a new branch--not both.
<br>
__2) Push branch to remote:__ 
<br>
In order to set up the branch so that it is remotely tracked, run `git push -u origin <Branch-Name>`. Now, when commits are made and pushed, they will be pushed to this branch in the remote. You are now free to add/change files and create commits
<br>
__3) Pull Request:__ 
<br>
The final part in a branches lifecycle is its death, which happens by merging it into the `main` branch. A merge into the `main` branch is called a pull request. It's poorly named, but there's no changing what GitHub refers to it as so so be it. You can find the list of branches on GitHub website by clicking "\<\> Code" when in the repositor folder. Click on the "Branches" tab. When you click on this, you will see a list of branches. Click on the "Pull Request" button next to the desired branch. Add a comment providing a paragraph description of what has been changed, and add reviewers for who should review the changes before its merged into `main`. Publish once you are ready.
<br>
### Other Helpful CLI:
`git checkout <branch name>`
<br>
`git branch`
Lists all branches currently visible on the local machine. (Other branches in the remote will be accessible using a `git checkout`, but may not be visible using git branch)
`git log` 
See all commits in the repository (if you've made new commits, these should show up here. It zhould also be synced with the remote if you've run `git fetch` and `git pull`)
