# RWPackager
This application was made to package Train Simulator 2020 scenarios and other content in a fast, simple way. 

I've found the TS Utilities quite exhausting to use due to its sluggish speed, and manual packaging to be tedious and prone to error. That's why this was made! I've had it lying on my PC for personal use only for a couple of months, but I've brushed it up now and it's out to the wild.

# How to Run
1. (Not required but highly recommended) There is an empty .txt file in the included download called "railworksdirectory.txt." Simply put in the name of your RailWorks directory (up to the RailWorks folder). If a valid directory has been put in, when selecting your game directory in the application, the file chooser will start in the directory, making things a bit easier for you. If it is empty, it will default to somewhere like the Desktop. The .txt file should look something like this once you've done that...
![step1](http://www.cynxs-stuff.com/RWP1.png)

2. Time to launch the application! Run RWPackager.jar and it should open to something similar to this:
![step2](http://www.cynxs-stuff.com/RWP2.png)

3. Click on "Select Scenario/Asset." It will open up a file chooser. If you're done step 1, it should open directly to your RailWorks directory. You'll need to supply a **directory** as to what scenario/asset/whatever TS content you'd like to package.
* If you're packing a scenario, this step is relatively easy. If you are running TS2020 in Borderless or Windowed mode, simply go to the Build menu and click Open. If you are running in fullscreen mode, you will need to take the tedious route and manually navigate to the scenario folder.
![step3](http://www.cynxs-stuff.com/RWP3.png)
* If you're packaging any other TS content, such as decals, assets, models, etc. you will need to manually navigate to the Assets directory.

4. Once you've navigated to the directory of choice, right click on the folder name (see image) and click **Copy address as text.** Once put in the file chooser, all contents of this folder will be packaged up. This also means that if there are any folders within this directory, they will also be packaged.
![step4](http://www.cynxs-stuff.com/RWP4.png)

5. Once that's been done, the main window will now display the name of the folder you'll be packaging up.
![step5](http://www.cynxs-stuff.com/RWP5.png)
Now click "Package To" and select the folder of your choice. This is where the Assets/Content folder will be copied over to.

6. Click "Create." Should all work, the window will close and a new Assets or Content folder should be present. Check to make sure everything has moved over correctly (it should have done if you've followed these steps correctly). To package something else, relaunch the application (hopefully this will be fixed in an update so you can package multiple items in a single session).

# Notes
* Once the Assets/Content folder has been created, to install it into TS, all you need to do is copy the folder into your RailWorks directory and overwrite if prompted.
* This will copy ALL content in the directory you've specified when doing Step 3. This means that if you're copying assets (such as files for a reskin), you must manually remove copyrighted files after packaging, such as GeoPcDx files.

# Contact
If you need to ask me about something, such as an issue with the application, you can open up a thread here on GitHub or send me a message on Facebook/through http://www.cynxs-stuff.com/contact.html

Thank you for downloading, hope it treats you well! :)
- S
