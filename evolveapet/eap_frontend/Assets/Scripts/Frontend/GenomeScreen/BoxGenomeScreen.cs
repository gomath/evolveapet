using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class BoxGenomeScreen : MonoBehaviour {

	int boxNumber;
	// Use this for initialization
	void Start () {
		boxNumber = Convert.ToInt32(name.Substring(name.Length - 1, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		SendMessageUpwards ("BoxClicked", boxNumber);
	}
}
}
