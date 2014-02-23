using UnityEngine;
using System.Collections;

namespace EvolveAPet{
public class ControlAnchorsScript : MonoBehaviour {

	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	
	Vector3 scale = new Vector3 ();
	public	GUISkin myskin;

	bool[] toggle = {false,false,false,false,false,false,false};
	bool[] newToggle = {false,false,false,false,false,false,false};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//GUI.skin = mySkin;
		
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		
		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
		Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("BreedButtonAnchor").position);
		Vector3 v = new Vector3(originalWidth*u.x/Screen.width,originalHeight*u.y/Screen.height,1f);
		
			if (GUI.Button (new Rect (v.x + 30, originalHeight - v.y, 110, 40), "Finish")){
				SendMessageUpwards("TetradsChosen");
			}


			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				string anchor = "";
				string function = "";
				string label = "";
				switch(i){
				case 0: anchor = "ToggleEarsAnchor"; function = "ToggleEars"; label = "Ears"; break;
				case 1: anchor = "ToggleEyesAnchor"; function = "ToggleEyes"; label = "Eyes"; break;
				case 2: anchor = "ToggleHeadAnchor"; function = "ToggleHead"; label = "Head"; break;
				case 3: anchor = "ToggleTorsoAnchor"; function = "ToggleTorso"; label = "Torso"; break;
				case 4: anchor = "ToggleArmsAnchor"; function = "ToggleArms"; label = "Arms"; break;
				case 5: anchor = "ToggleLegsAnchor"; function = "ToggleLegs"; label = "Legs"; break;
				case 6: anchor = "ToggleTailAnchor"; function = "ToggleTail"; label = "Tail"; break;

				}

				u = Camera.main.WorldToScreenPoint(transform.FindChild(anchor).position);
				v = new Vector3(originalWidth*u.x/Screen.width,originalHeight*u.y/Screen.height,1f);
				newToggle[i] = GUI.Toggle (new Rect (v.x,originalHeight-v.y,50,30), toggle[i], label);
				if (newToggle[i] != toggle[i]){
					toggle[i]= newToggle[i];
				    SendMessageUpwards(function,toggle[i]);	
					SendMessageUpwards("ResetMagnifiedChromosome");
				}
			}	
	}
}

}
