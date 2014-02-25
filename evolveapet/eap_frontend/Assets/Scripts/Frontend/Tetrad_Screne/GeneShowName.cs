using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{

public class GeneShowName : MonoBehaviour {

	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;

	Vector3 scale = new Vector3 ();
	public	GUISkin myskin = (GUISkin)Resources.Load("GUI/Skin");
	
	public Gene gene { get; set; }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		GUI.skin = myskin;
		
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		
		// substitute matrix to scale if screen nonstandard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);

		Vector3 u = Camera.main.WorldToScreenPoint(transform.position);

		Vector3 v = new Vector3(originalWidth*u.x/Screen.width,originalHeight*u.y/Screen.height,1f);

		float y = Adjust(v.y);
		
			GUI.Box (new Rect (v.x + 30, originalHeight - y - 10, 60, 30), gene.GetWholeNameEncoded());

	}

		float Adjust(float y){
			float c1 = 5f;
			float c2 = 10f;
			int physicalChromosomeNum = Convert.ToInt32(transform.parent.name.Substring (transform.parent.name.Length - 2, 1));
			int physicalGeneNum = Convert.ToInt32(name.Substring (name.Length - 1, 1));
			switch (physicalChromosomeNum) {
			case 0: 
				switch(physicalGeneNum){
				case 0: break;
				case 1: break;
				case 2: y -= c1; break;
				case 3: break;
				}
				break;
			case 1:
				switch(physicalGeneNum){
				case 0: y -= c1; break;
				case 1: y += c1; break;
				case 2: y -= c2; break;
				case 3: y -= c1; break;
				case 4: break;
				}
				break;
			case 2:
				switch(physicalGeneNum){
				case 0: y -= c1; break;
				case 1: break;
				case 2: y += c1; break;
				case 3: break;
				case 4: y += c1; break;
				}
				break;
			case 3:
				switch(physicalGeneNum){
				case 0: break;
				case 1: break;
				case 2: break;
				case 3: break;
				}
				break;
			case 4:
				switch(physicalGeneNum){
				case 0: break;
				case 1: break;
				case 2: y -= c2; break;
				case 3: y += c1; break;
				case 4: break;
				}
				break;
			case 5:
				switch(physicalGeneNum){
				case 0: break;
				case 1: break;
				case 2: break;
				case 3: break;
				}
				break;
			case 6:
				switch(physicalGeneNum){
				case 0: y -= c1; break;
				case 1: y += c1; break;
				case 2: break;
				case 3: break;
				}
				break;
			}
			return y;
		}
}

}
