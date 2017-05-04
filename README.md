# CSCI3308-HoloPollster

HoloPollster is Windows Phone app that enables marketers and developers to create, launch and monitor incentivized surveys. Each time an individual takes a survey, they unlock a new feature or level in one of four unity games.  

## Repo
There are two repos - one for the HoloPollster Windows Phone app and one for the Unity games.  
### Unity Games repo  
https://github.com/Tarah-Peltz/Polling-App-Cross-Plat/tree/master  
This main directory contains 5 folders. /TestCases, contains screen shots of the Unity unit tests.
Each game has its own folder. Each game folder contains the game code and an .exe file:  
* /3D Pong  
* /CU_Themed_Pool  
* /Snake3D (contains errors, no .exe file)
* /Roll-a-ball_Demo  
### HoloPollster Windows Phone app repo
https://github.com/Tarah-Peltz/Polling-App-Cross-Plat  
The main directory contains three folders. /HoloPollster contains the app code and solution file. /AutoDoc contains the website for the auto-documentation. /TestCases contains TESTING.md, the testing document from project "part 4"  and a screenshot of the unit tests.

## Build/Run/Test  
* To launch a Unity game, download the folder containing the game (or clone the repo) and open the game's .exe file.  Either method is fine as long as the user has both the executable and the respective data folder. Without the latter, the .exe will not run. (Note: "Snake 3D" contains errors and does not have an executable file.)
* To launch the HoloPollster Windows Phone app, dowlond the HoloPollster folder (or clone the repo.) Open the HoloPollster.sln file in Visual Studios. Build the solution. Run the solution in a Windows Phone 8.1 emulator.  
