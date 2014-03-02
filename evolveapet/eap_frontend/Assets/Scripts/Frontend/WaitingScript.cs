using UnityEngine;
using System.Collections;

public class WaitingScript : MonoBehaviour {

	public GUISkin mySkin;
	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	Vector3 scale = new Vector3();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.skin = mySkin;
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
	}
}
