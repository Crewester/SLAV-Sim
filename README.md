TO DOWNLOAD THE PROJECT PLEASE FOLLOW THIS LINK TO A GOOGLE DRIVE THAT CONTAINS THE UNITY PROJECT https://drive.google.com/file/d/1Bz9hCifMS7bW2E1rXOCAvf7ARb0Jz-45/view?usp=drive_link  (As the project is too large to store the assets on GitHub, but the code is here to review before downloading the project)
About 
This project was completed alongside a paper release and allows the user to simulate a self-learning autonomous vehicle on low to mid-range consumer hardware. This project is related to a research paper that you can read for free here:
https://www.mdpi.com/1424-8220/23/20/8649?utm_campaign=releaseissue_sensorsutm_medium=emailutm_source=releaseissueutm_term=doilink91
Installation
Getting started:
To be able to run this project a user will need to install Unity 2021.1.15f1. this can either be done through the Unity hub which can be downloaded here: https://unity.com/download or done by just downloading the 2021.1.15f1 version of unity here: https://unity.com/releases/editor/archive. 
With Unity downloaded you will need to download the Unity project. This project is too large to store on GitHub so has been placed on google drive (the code is included in the project on google drive so no need to download it from GitHub).
With the project downloaded and unzipped you can open it using the Unity editor. You may get a warning that some packages are missing if this occurs you will need to start in safe mode and add the ML agents package, currently the project uses ML agents version 1.0.8 which will need to be added to the project and will not work with later versions. 
Python is also needed to run all of the machine learning so make sure you have python 3.7 installed on your machine, there is a virtual environment included in the project and all required Python packages are included. 
A guide to use the ML agent’s software can be found on the ML agents GitHub page: https://github.com/Unity-Technologies/ml-agents/tree/release_10 
A video tutorial is also available here: 
https://www.youtube.com/watch?v=zPFU30tbyKs&ab_channel=CodeMonkey 

Quick start Training 
Load up the unity environment that you would like to train the agent in and open the virtual environment inside the project.
To start a simulation, use the command “mlagents-learn”. This will then start to listen on port 5004 for a unity environment and will display a message informing you to start the Unity environment. Press play on the Unity environment. The agent will begin to move and training data and statistics will be displayed in the virtual environment.
For more complex training you can pass additional commands in the learn command As documented on the ML-agents GitHub page you can pass additional parameters in this command to train the agent with specific parameters.
To increase learning speed multiple instances of the track and the vehicles can be run at the same time contributing to the learning of the same model. To achieve this simply copy the track and vehicles and place them in a different location before beginning learning. 
Quick Start Testing
Once training is concluded a file will be written in the results folder inside the project. This folder will contain a .nn file. This file can be used to run inference within the simulator as it will control the vehicle but will not continue to learn. This is a good way to benchmark the current state of the model. SLAV-sim contains some benchmarking tools that can be used to quantify the performance of a model that has already been trained.
To start a test you will need to place the model inside the model section of the behaviour parameter component of the self-learning vehicle (please note that the observations will need to match the observations at the time of training), then press play (without running the training command).
 
Using different observation methods.
There are two methods of observing the environment built into SLAV-sim. First is using raycasts to detect the distance between the middle and side of the road and other obstacles. Second is to use a camera at the front of the vehicle. 
In the assets of the project there are several vehicles to choose from either with the camera or the raycast components active. Simply drag one of these prefabs from the asset menu at the bottom of the screen and place it on the road. delete any other vehicles that do not use the observation method that is being used.
Using algorithms not included in ML agents.
The simulator can be run as a gym environment to do this simply build the environment into a stand-alone package by going to file > build. This will produce an executable file that can be used as a gym environment.
Displaying training data using Tensorboard.
To display the training data, you can use Tensorboard in the virtual environment and use the command “Tensorboard –logdir results”. This command will host a webpage that can be viewed in any browser by going to “http://localhost:6006/”. 
