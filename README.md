
# Polling-App-Cross-Plat
A cross platform app created for individuals to take and create polls. Created for CSCI3308

App Layout as Follows:
Shared Files: (These will be a shared effort between Kristin and Tarah)
    PollData.cs - This dictates the structure of each poll question. Contains the question, the answer structure (free response, radio              buttons, ect.) 
    PollFormat.cs - This will contain The structure for a full poll. It can contain a List(this is a built in C# struct. Really nifty) of         questions. It will inherit multiple instances of PollData.cs. We might need to research class inheritance for this. This will              also contain a list of the answers to each of the questions. This will connect to the cloud.
    Cloud.cs - This will contain a general class to upload a blob to a container in the cloud.
        Cloud structure - Each business or user will have a container with their relevant blobs. It will be named after the Username. To  
        check for unique usernames, we can add in the functionality to create a container if it doesn't exist and to scan through all the 
         containers to see if the name already exists. The user password may be stored in the blob. 
    Data.cs - This will gather poll results in a list that will upload to the cloud
    Fetch.cs - This fetches poll results from the cloud and prints them in a proper format
    Polls.cs - This is a list of all the polls. It fetches them from the cloud
    CreatePoll.cs - This will allow the businesses to create a poll by serving as an interface between the UI and PollFormat.cs
 iOS Files:
    main.xml - The front end file for iOS (Kristin will handle this, as iOS is only testable on a Mac). This will eventually hold the           login screen
    homescreen.xml - This will show basic functionality like number of points, option to create a poll (if account is associated with a         business) and the ability to take more polls. 
    poll.xml - This will be the view model for a poll.
    creation.xml - This will hold the UI for creating polls.
    
 Android Files: 
    main.axml - This contains the front end for the first storyboard. At the moment, it will contain a demo poll, but will evolve to be         the login screen as the login structure is developed. (This will be a shared effort between Kristin and Tarah)
   homescreen.axml - This will show basic functionality like number of points, option to create a poll (if account is associated with a         business) and the ability to take more polls. 
    poll.axml - This will be the view model for a poll.
    creation.axml - This will hold the UI for creating polls.
  
Windows Files:
    main.xaml - This contains the front end for the first layout. At the moment, it will contain a demo poll, but will evolve to be         the login screen as the login structure is developed. (Tarah will handle this, as she has Windows experience)
  homescreen.xaml - This will show basic functionality like number of points, option to create a poll (if account is associated with a         business) and the ability to take more polls. 
    poll.xaml - This will be the view model for a poll.
    creation.xaml - This will hold the UI for creating polls.
