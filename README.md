
# Polling-App-Cross-Plat
A cross platform app created for individuals to take and create polls. Created for CSCI3308

## App Layout as Follows:##

__Shared Files:__

> _These will be a shared effort between Kristin and Tarah_
* __PollData.cs__ - This dictates the structure of each poll question. Contains the question, the answer structure (free response, radio              buttons, ect.) 
* __PollFormat.cs__ - This will contain The structure for a full poll. It can contain a List(this is a built in C# struct. Really nifty) of questions. It will inherit multiple instances of PollData.cs. We might need to research class inheritance for this. This will also contain a list of the answers to each of the questions. This will connect to the cloud.
* __Cloud.cs__ - This will contain a general class to upload a blob to a container in the cloud.

> Cloud structure - Each business or user will have a container with their relevant blobs. It will be named after the Username. To  check for unique usernames, we can add in the functionality to create a container if it doesn't exist and to scan through all the containers to see if the name already exists. The user password may be stored in the blob.
 * __Data.cs__ - This will gather poll results in a list that will upload to the cloud
 * __Fetch.cs__ - This fetches poll results from the cloud and prints them in a proper format
 * __Polls.cs__ - This is a list of all the polls. It fetches them from the cloud
 * __CreatePoll.cs__ - This will allow the businesses to create a poll by serving as an interface between the UI and PollFormat.cs
 
__iOS Files:__
* __main.xml__ - The front end file for iOS _(Kristin will handle this, as iOS is only testable on a Mac)_. This will eventually hold the login screen
* __homescreen.xml__ - This will show basic functionality like number of points, option to create a poll (if account is associated with a business) and the ability to take more polls. 
* __poll.xml__ - This will be the view model for a poll.
* __creation.xml__ - This will hold the UI for creating polls.
    
__Android Files:__ 
* __main.axml__ - This contains the front end for the first storyboard. At the moment, it will contain a demo poll, but will evolve to be the login screen as the login structure is developed. _(This will be a shared effort between Kristin and Tarah)_
* __homescreen.axml__ - This will show basic functionality like number of points, option to create a poll (if account is associated with a business) and the ability to take more polls. 
* __poll.axml__ - This will be the view model for a poll.
* __creation.axml__ - This will hold the UI for creating polls.
  
__Windows Files:__
* __main.xaml__ - This contains the front end for the first layout. At the moment, it will contain a demo poll, but will evolve to be the login screen as the login structure is developed. _(Tarah will handle this, as she has Windows experience)_
* __homescreen.xaml__ - This will show basic functionality like number of points, option to create a poll (if account is associated with a business) and the ability to take more polls. 
* __poll.xaml__ - This will be the view model for a poll.
* __creation.xaml__ - This will hold the UI for creating polls.
