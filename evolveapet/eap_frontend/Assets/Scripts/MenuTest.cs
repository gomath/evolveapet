//C#
using UnityEngine;
using System.Collections;

public class MenuTest : MonoBehaviour {
	
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,320,130), "Main Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,300,20), "New Game")) {
			//will presumably call some function to change the scene to that of the main game
			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,300,20), "Load Saved Game")) {
			//will presumably call some function to change the scene to a load menu
			Application.LoadLevel(2);
		}

		// Make the third button.
		if(GUI.Button(new Rect(20,100,300,20), "Quit")) {
			//quit
			Application.Quit();
		}
	}
}
