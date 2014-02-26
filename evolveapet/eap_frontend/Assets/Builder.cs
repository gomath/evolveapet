using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolveAPet {
	public class Builder : MonoBehaviour {
		// Use this for initialization
		Player player = Player.playerInstance;

		void Start () {
			StartCoroutine("Build");
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		IEnumerator Build() {
			//Remove the previous animal
			GameObject.Destroy(GameObject.FindGameObjectWithTag("Animal"));
			Resources.UnloadUnusedAssets();
			
			//Wait one frame for destroys to commit
			yield return new WaitForSeconds(0f);
			int activeAnimalNo = Player.playerInstance.Stable.activeAnimalNumber;
			//Create new animal
			GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			animal.GetComponent<PhysicalAnimal> ().animal = Player.playerInstance.Stable.animalsInStable [activeAnimalNo];
			animal.GetComponent<PhysicalAnimal>().Build(animal);
			LinkedList<Animal> list= new LinkedList<Animal> ();
			list.AddFirst (animal.GetComponent<PhysicalAnimal> ().animal);
		}

		void OnGUI() {
			if (GUI.Button (new Rect(Screen.width-200,Screen.height-30,200, 30), "Build")) {
				StartCoroutine("Build");
			}
			string dailyChallengeStr = player.getDailyChallengeString ();
			string completeBtnStr;
			string countdown;

			float center = Screen.width * 0.5f -200;
			GUI.Label (new Rect (center, Screen.height * 0.05f, 400, 50), dailyChallengeStr);
			if (player.currentDailyChallenge != -1) {//If player already has a daily challenge
				
				if (GUI.Button (new Rect (center, Screen.height * 0.1f +20, 400, 30), "Complete Daily Challenge!")) {
					player.completeDailyChallenge ();
								}
			} else if (player.currentDailyChallenge == -1 && DateTime.Compare (DateTime.Today, player.dailyChallengeSetDate) != 0) {
				if (GUI.Button (new Rect (center, Screen.height * 0.1f + 20, 400, 30), "New Daily Challenge")) {
					player.newDailyChallenge(); // If it is not the day of the last completed daily challenge, then make a button to get a new one.
										//Assumes player cannot travel backwards in time
								}
						} else {
				
				TimeSpan ts = player.dailyChallengeSetDate.AddDays(1).Subtract(DateTime.Now);//Gets how many hours till 12 AM until day after daily challenge
				countdown = string.Format("{0} Hours, {1} Minutes, and {2} Seconds until new daily challenge!", ts.Hours, ts.Minutes, ts.Seconds);
				//Counts down
				GUI.Label (new Rect (center, Screen.height * 0.1f +20, 410, 30), countdown);
			}
		}
	}
}
