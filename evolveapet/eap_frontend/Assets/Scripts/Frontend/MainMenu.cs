//C#
using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace EvolveAPet{
public class MainMenu : MonoBehaviour {
	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	Vector3 scale = new Vector3();
		bool showPopUp = false;
		string playerName = "Enter your name here";
	public GUISkin mySkin;

	void OnGUI () {

		GUI.skin = mySkin;

		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
		if (showPopUp){//If the pop up should be on screen, creates it
			GUI.Window(0, new Rect((Screen.width / 2) - 170, (Screen.height / 2) - 85, 300, 250), ShowGUI, "Hello!");
		}

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(660,82,150,40), "Play")) {
			//for now loads Animal Scene; should load stable scene

				Player currentPlayer = Player.playerInstance; //Checks if there is already an instantiated player
				if (currentPlayer==null){
					if (!File.Exists(Environment.CurrentDirectory + "/save.sav")){ //Checks if a save game exists
						
						showPopUp = true;
					}
					else{
						Player.loadGame(); 
						showPopUp = false;
					}//Loads player in from memory if there is a save

				}

				if (!showPopUp)Application.LoadLevel("RandomBreeding"); //TODO Change to stable

				
			}
		
		// Make the second button.
		if(GUI.Button(new Rect(570,272,200,40), "Import a friend's animal")) {
			//Brings up a file choosing interface, this 





		}

		// Make the third button.
		if(GUI.Button(new Rect(580,470,50,40), "Quit")) {
			//quit
			Application.Quit();
		}

		//restore matrix before return
		GUI.matrix = svMat;
	}
		void ShowGUI( int windowID){ //This is what is in the pop up window
			GUI.Label(new Rect(65, 40, 200, 50), "It appears that you do not have a save game. Welcome to Evolve a Pet!");

			playerName = GUI.TextField(new Rect(60, 130, 200, 20), playerName, 25); //Allows player to enter their name

			if (GUI.Button(new Rect(100, 170, 110, 50), "Get Started!"))
				
			{
				//Creates a new player with an empty stable and the given name
				showPopUp = false;
				Stable s = new Stable();
				Player player = new Player(s, playerName);
				Player.playerInstance = player;
				player.saveGame();
				Application.LoadLevel("RandomBreeding");

			}
		}
	}
}