using UnityEngine;
using System.Collections;

public class ChromosomeScreen : MonoBehaviour {

	float[] places = {-8f,-6f,-4f,-2f,0f,2f,4f};
	System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		int n;
		for (int i = 0; i<7; i++) {
			n = rand.Next(0, 6);
			for (int j = 0; j<7; j++) {
				n = (n+i)%7;
				if (places[n] != 99f) {
					GameObject c = (GameObject)Instantiate(Resources.Load ("Prefabs/chromosome pair "+i));
					c.transform.position = new Vector2(places[n],c.transform.position.y);
					places[n] = 99f;
					break;
				}
			}
		}
		ArrayRefresh();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ArrayRefresh() {
		places = new float[]{-8f,-6f,-4f,-2f,0f,2f,4f};
	}
}
