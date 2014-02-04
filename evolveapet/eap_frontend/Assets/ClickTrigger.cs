using UnityEngine;
using System.Collections;

public class ClickTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver () {
		gameObject.SendMessageUpwards("setTouched", true);
	}

	void OnMouseExit() {
		gameObject.SendMessageUpwards("setTouched", false);
	}
}
