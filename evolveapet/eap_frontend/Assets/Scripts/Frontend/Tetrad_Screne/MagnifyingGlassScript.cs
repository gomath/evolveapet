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

	/// <summary>
	/// Disable all chromosome under magnifyin glass and set colour of all numbers in all boxes to defualt light blue. 
	/// </summary>
	public void ClearGlass(){
		// Just inactivate all magnifying chromosomes
		foreach (Transform t in transform) {
			t.gameObject.SetActive(false);
		}
		SendMessageUpwards("ColourCurrentBox","random");
	}
}
