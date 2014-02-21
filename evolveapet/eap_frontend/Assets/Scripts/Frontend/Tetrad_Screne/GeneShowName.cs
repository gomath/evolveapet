using UnityEngine;
using System.Collections;

namespace EvolveAPet{

public class GeneShowName : MonoBehaviour {
	
	public Gene gene { get; set; }
		bool toggle = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
		//GUI.Box(new Rect(v.x + 20,v.y,50,20),"Tralala");
		float y = Adjust(v.y);
		
			if (GUI.Button (new Rect (v.x + 20, Screen.height - y - 10, 50, 20), gene.GetWholeNameEncoded())) {
				if (toggle){
					GetComponent<SpriteRenderer> ().color = Color.yellow;
				} else{
					GetComponent<SpriteRenderer> ().color = Color.magenta;
				}
				toggle = !toggle;
			}

			GUI.Box(new Rect(0,0,50,20),"0,0");
			GUI.Box(new Rect(50,100,50,20),"50,100");

	}

		float Adjust(float y){
			float c1 = 5f;
			float c2 = 10f;
			switch (gene.ChromosomeNum) {
			case 0: 
				switch(gene.GeneNum){
				case 0: break;
				case 1: break;
				case 2: y -= c1; break;
				case 3: break;
				}
				break;
			case 1:
				switch(gene.GeneNum){
				case 0: y -= c1; break;
				case 1: y += c1; break;
				case 2: y -= c2; break;
				case 3: y -= c1; break;
				case 4: break;
				}
				break;
			case 2:
				switch(gene.GeneNum){
				case 0: y -= c1; break;
				case 1: break;
				case 2: y += c1; break;
				case 3: break;
				case 4: y += c1; break;
				}
				break;
			case 3:
				switch(gene.GeneNum){
				case 0: break;
				case 1: break;
				case 2: break;
				case 3: break;
				}
				break;
			case 4:
				switch(gene.GeneNum){
				case 0: break;
				case 1: break;
				case 2: y -= c2; break;
				case 3: y += c1; break;
				case 4: break;
				}
				break;
			case 5:
				switch(gene.GeneNum){
				case 0: break;
				case 1: break;
				case 2: break;
				case 3: break;
				}
				break;
			case 6:
				switch(gene.GeneNum){
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
