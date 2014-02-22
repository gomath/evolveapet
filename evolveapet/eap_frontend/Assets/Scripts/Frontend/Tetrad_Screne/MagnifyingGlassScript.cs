using UnityEngine;
using System.Collections;

public class MagnifyingGlassScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ClearGlass ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearGlass(){
		// Just inactivate all magnifying chromosomes
		foreach (Transform t in transform) {
			t.gameObject.SetActive(false);
		}
	}
}
