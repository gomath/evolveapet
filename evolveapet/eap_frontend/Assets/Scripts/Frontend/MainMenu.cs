//C#
using UnityEngine;
using System.Collections;
using System.IO;
using System;
//using UnityEditor;
namespace EvolveAPet{
public class MainMenu : MonoBehaviour {
	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	Vector3 scale = new Vector3();
	bool showPopUp = false;
	bool showErrorMessage = false;		
	string playerName = "Enter your name here";
	Player currentPlayer;
	protected string importPath = "";
	protected FileBrowser m_fileBrowser;
	protected Texture2D m_fileImage;
	protected Texture2D m_directoryImage;
	public GUISkin mySkin;
	bool exception;
	

		void Start(){


			
		}

		void OnGUI () {

			if (Player.playerInstance==null){
				
				if (!File.Exists(Environment.CurrentDirectory + "/save.sav")){ //Checks if a save game exists
					
				}
				else{
					Player.loadGame(); 
					showPopUp = false;
				}//Loads player in from memory if there is a save
			}
			if (m_fileBrowser != null) {
				m_fileBrowser.OnGUI();
			}
		currentPlayer = Player.playerInstance; //Checks if there is already an instantiated player
		m_fileImage = (Texture2D) Resources.Load("document icon");
		m_directoryImage = (Texture2D)Resources.Load ("folder icon");
		GUI.skin = mySkin;
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);


		if (showPopUp){//If the pop up should be on screen, creates it
			GUI.Window(0, new Rect((originalWidth / 2) - 170, (originalHeight / 2) - 85, 300, 250), ShowGUI, "Hello!");
		}
		if (showErrorMessage){//If the error message should be on screen, creates it
			GUI.Window(1, new Rect((originalWidth / 2) - 170, (originalHeight / 2) - 85, 300, 250), ErrorGUI, "Oh No!");
		}
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		//if(GUI.Button(new Rect(660,82,150,40), "Play")) {
		if(GUI.Button(new Rect(80,270,150,40), "Play")) {
			//for now loads Animal Scene; should load stable scene

				if (currentPlayer==null){
					if (!File.Exists(Environment.CurrentDirectory + "/save.sav")){ //Checks if a save game exists
						
						showPopUp = true;
						showErrorMessage =false;
					}
					else{
						showPopUp = false;
					}

				}

				if (!showPopUp)Application.LoadLevel("Stable"); 

				
			}
		
		// Make the second button.
		//if(GUI.Button(new Rect(570,272,200,40), "Import a friend's animal")) {
		if(GUI.Button(new Rect(250,270,200,40), "Import a friend's animal")) {
			//Brings up a file choosing interface, then loads the animal into memory.
			//If the stable's egg slot is full then throws an error message

				if (currentPlayer ==null || currentPlayer.Stable.Size == currentPlayer.Stable.NumberOfUnlockedSlots){
					showErrorMessage = true;
					showPopUp =false;
				}
				else{
					m_fileBrowser = new FileBrowser(
						new Rect(1100, 300, 600, 500),
						"Import an animal",
						FileSelectedCallback
						);
					
					m_fileBrowser.DirectoryImage = m_directoryImage;
					m_fileBrowser.FileImage = m_fileImage;
					m_fileBrowser.SelectionPattern = "*.animal";
				

				}


		}

		// Make the third button.
		//if(GUI.Button(new Rect(580,470,60,40), "Quit")) {
		if(GUI.Button(new Rect(470,270,60,40), "Quit")) {
			//quit
			Player.playerInstance.saveGame();

			Application.Quit();
		}

		//restore matrix before return
		GUI.matrix = svMat;
	}

		protected void FileSelectedCallback(string path) {
			m_fileBrowser = null;
			importPath = "";
			if (path!=null) importPath = path;
			if (!importPath.Equals("")){
				try{
					Animal newAnimal = Animal.deserialiseAnimal(importPath);
					currentPlayer.Stable.eggSlot = newAnimal;
				}
				catch(Exception e){
					exception = true;
					showErrorMessage =true;
				}
			}
		}
		
		
		void ErrorGUI(int windowID){
			if (currentPlayer == null) {

								GUI.Label (new Rect (65, 40, 200, 80), "It appears that you do not have a save game.\n Press play to get started!");

						}
			else if (exception ==true) {
				GUI.Label(new Rect(65, 40, 200, 80), "Something went wrong while opening the file! Is this a saved animal?");
						}
			else if (currentPlayer.Stable.Size == currentPlayer.Stable.NumberOfUnlockedSlots) {

				GUI.Label(new Rect(65, 40, 200, 80), "Your stable is full, please make some room!");
				
			}
			if (GUI.Button(new Rect(100, 170, 110, 50), "Close"))
				
			{
				showErrorMessage = false;
				exception = false;
			}


		}

		void ShowGUI( int windowID){ //This is what is in the pop up window
			GUI.Label(new Rect(65, 40, 200, 80), "Welcome to Evolve a Pet!\n");

			playerName = GUI.TextField(new Rect(60, 130, 200, 40), playerName, 25); //Allows player to enter their name

			if (GUI.Button(new Rect(100, 170, 110, 50), "Get Started!"))
				
			{
				//Creates a new player with an empty stable and the given name
				showPopUp = false;
				Stable s = new Stable();
				Player player = new Player(playerName);
				player.saveGame();
				Application.LoadLevel("Stable");

			}
		}
	}
}