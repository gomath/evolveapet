using UnityEngine;
using System.Collections;

namespace EvolveAPet {

public class TetradsScroll : MonoBehaviour {

	GameObject[] tetrads;
	public float scrollingSensitivity = 300;
	public float scrollForce = 0;
	public float dumpingFactor = 2;
	public float scrollingTreshold = 0.01f;

	// Use this for initialization
	void Start () {
		tetrads = new GameObject[Global.NUM_OF_CHROMOSOMES];
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			tetrads[i] = GameObject.Find("Tetrad_" + i);
		}
	}
	
	// Update is called once per frame
	void Update () {

		float actualMaxY = tetrads [0].transform.localPosition.y;
		float actualMinY = tetrads [Global.NUM_OF_CHROMOSOMES-1].transform.localPosition.y; 
		
			float scroll = -Input.GetAxis ("Mouse ScrollWheel");
			if((scroll > scrollingTreshold  && actualMinY > -1) ||
			   (scroll < -scrollingTreshold && actualMaxY < 1) 
			   || (scrollForce > scrollingTreshold && actualMinY > -1) 
			   || (scrollForce < -scrollingTreshold && actualMaxY < 1)) {
				if((scrollForce > scrollingTreshold && actualMinY > -1) ||(scrollForce < scrollingTreshold && actualMaxY < 1)){
					scrollForce = 0;
				}
				return;
			}

			if (Mathf.Abs (scroll) > scrollingTreshold) {
				scrollForce += scroll;
				foreach (GameObject tetrad in tetrads) {
					tetrad.transform.Translate (new Vector2(0,scroll * Time.deltaTime * scrollingSensitivity));
				}
			} else if(Mathf.Abs(scrollForce) > scrollingTreshold) {
				foreach (GameObject tetrad in tetrads) {
					tetrad.transform.Translate (new Vector2(0,scrollForce * Time.deltaTime * scrollingSensitivity));
				}

				// Dumping scrolling
				scrollForce /= dumpingFactor;
			}
		
	}

	public void TranslateAllTetradsByOffset(float offset){
		foreach (GameObject tetrad in tetrads) {
			tetrad.transform.Translate (new Vector2(0,-offset));
		}

		
	}
}

}
