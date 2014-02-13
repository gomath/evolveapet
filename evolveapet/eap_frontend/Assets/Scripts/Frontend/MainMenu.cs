//C#
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	void OnGUI () {
		// Make a background box
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(475,50,75,20), "New Game")) {
			//for now loads Animal Scene; should load stable scene
			Application.LoadLevel("RandomBreeding");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(400,150,120,20), "Load Saved Game")) {
			//will presumably call some function to change the scene to a load menu
			Application.LoadLevel("test suite");
		}

		// Make the third button.
		if(GUI.Button(new Rect(390,260,50,20), "Quit")) {
			//quit
			Application.Quit();
		}
	}
}
