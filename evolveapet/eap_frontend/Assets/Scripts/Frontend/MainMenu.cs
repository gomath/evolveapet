//C#
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	Vector3 scale = new Vector3();

	public GUISkin mySkin;

	void OnGUI () {

		GUI.skin = mySkin;

		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;

		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(660,82,150,40), "Play")) {
			//for now loads Animal Scene; should load stable scene

			//Loads the player in from memory
			Player currentPlayer;


			Application.LoadLevel("RandomBreeding");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(570,272,200,40), "Import a friend's animal!")) {
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
}
