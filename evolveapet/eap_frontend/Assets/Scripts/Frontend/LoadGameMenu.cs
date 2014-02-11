using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LoadGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//will need a list of dates that are the dates
	//
	//ArrayList<DateTime> saveDates;

	
	void OnGUI () {

		//would like to fetch a list of dates of the saved games a user has
		List<DateTime> saveDates = new List<DateTime> ();
		saveDates.Add(new DateTime(6, 3, 2014, 11, 11, 11));
		saveDates.Add (new DateTime (19, 10, 2013, 12, 12, 12));
		saveDates.Add (new DateTime (3, 8, 2013, 7, 7, 7));

		//Collections.sort (saveDates);

		// Make a background box
		GUI.Box(new Rect(10,10,600,400), "Saved Games");

		int h = 40;
		foreach (DateTime date in saveDates) {
			if(GUI.Button(new Rect(20,h,30,30), "Game Saved on "+date)) {
				Application.LoadLevel(1);
			}
			h+=40;

		}

		
//		//s Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
//		if(GUI.Button(new Rect(20,40,300,20), "New Game")) {
//			//will presumably call some function to change the scene to that of the main game
//			Application.LoadLevel(1);
//		}
//		
//		// Make the second button.
//		if(GUI.Button(new Rect(20,70,300,20), "Load Saved Game")) {
//			//will presumably call some function to change the scene to a load menu
//			Application.LoadLevel(2);
//		}
//		
//		// Make the third button.
//		if(GUI.Button(new Rect(20,100,300,20), "Quit")) {
//			//quit
//			Application.Quit();
//		}
	}
}
