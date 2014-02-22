using UnityEngine;
using System.Collections;

public class Toolbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int toolbarInt = 0;
	public float barHeight = 0.1f;
	public string[] toolbarStrings = new string[] {"Toolbar1", "Toolbar2", "Toolbar3"};
	void OnGUI() {
		toolbarInt = GUI.Toolbar(new Rect(0, Screen.height-(barHeight*Screen.height), Screen.width, barHeight*Screen.height), toolbarInt, toolbarStrings);
	}
}
