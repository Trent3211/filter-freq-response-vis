import tkinter as tk
import os

splash_image = "resources/splash_screen.png"

# Create a new window for the splash screen and center it
root = tk.Tk()

# Add a border and drop shadow to the window
root.attributes('-alpha', 0.0)
root.overrideredirect(True)
root.configure(bg='white')
root.attributes('-topmost', True)
root.geometry("+{}+{}".format(root.winfo_screenwidth()//2-350, root.winfo_screenheight()//2-146))
root.lift()

# Add a border to the splash screen
border_width = 2
border_color = 'black'
root.configure(highlightbackground=border_color, highlightcolor=border_color, highlightthickness=border_width)

# Add an image to the splash screen
image = tk.PhotoImage(file=splash_image)
label = tk.Label(root, image=image, bg='white', bd=0)
label.pack(fill='both', expand=True)

# Add a status bar to the bottom of the window
statusbar = tk.Label(root, text='Loading...', bd=1, relief='sunken', anchor='w')
statusbar.pack(side='bottom', fill='x')

# Define a function to update the status bar
def update_status(message):
    statusbar.config(text=message)
    statusbar.update_idletasks()

# Define a function to close the splash screen and open the ffrv.py file
def close_splash():
    # Update the status bar
    update_status('Opening the FFRV application...')

    # Create a fade-out effect
    alpha = 1.0
    while alpha > 0.0:
        alpha -= 0.05
        root.attributes('-alpha', alpha)
        root.update()
        root.after(50)

    # Update the status bar
    update_status('Done.')

    root.destroy()
    os.system("python ffrv.py")

# Set a timer to close the splash screen after 15 seconds
root.after(15000, close_splash)

# Update the status bar
update_status('Loading...')

# Define a function to gradually fade in the window
def fade_in():
    alpha = 0.0
    while alpha < 1.0:
        alpha += 0.05
        root.attributes('-alpha', alpha)
        root.update()
        root.after(50)

# Call the fade_in function after a short delay
root.after(100, fade_in)

# Start the mainloop for the splash screen window
root.mainloop()
