using UnityEngine;
using System.Collections;

namespace EvolveAPet{
public class BreedButtonScript : MonoBehaviour {
	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	
	Vector3 scale = new Vector3 ();
	public	GUISkin myskin;

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
		Vector3 u = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 v = new Vector3(originalWidth*u.x/Screen.width,originalHeight*u.y/Screen.height,1f);
		
			if (GUI.Button (new Rect (v.x + 30, originalHeight - v.y, 110, 40), "Finish")){
				SendMessageUpwards("TetradsChosen");
			}



		
	}
}
}
