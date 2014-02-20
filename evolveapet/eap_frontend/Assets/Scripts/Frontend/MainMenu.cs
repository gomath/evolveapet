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

		GUI.color = Color.blue;

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(700,82,150,40), "New Game")) {
			//for now loads Animal Scene; should load stable scene
			Application.LoadLevel("RandomBreeding");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(640,272,120,40), "Load Saved Game")) {
			//will presumably call some function to change the scene to a load menu
			Application.LoadLevel("test suite");
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
