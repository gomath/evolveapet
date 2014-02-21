using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EvolveAPet {
	public class Builder : MonoBehaviour {
		// Use this for initialization
		Player player;

		void Start () {
			StartCoroutine("Build");
			player = new Player (new Stable(), "test");
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
			
			//Create new animal
			GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			animal.GetComponent<PhysicalAnimal>().animal = new Animal();
			animal.GetComponent<PhysicalAnimal>().Build(animal);
			LinkedList<Animal> list= new LinkedList<Animal> ();
			list.AddFirst (animal.GetComponent<PhysicalAnimal> ().animal);
			player._stable = new Stable (list);
			player._stable.activeAnimalNumber = 0;
		}

		void OnGUI() {
			if (GUI.Button (new Rect(Screen.width-200,Screen.height-30,200, 30), "Build")) {
				StartCoroutine("Build");
			}
			string dailyChallengeStr = player.getDailyChallengeString ();
			string completeBtnStr;
			string countdown;
			GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.05f, 1000, 30), dailyChallengeStr);
			if (player.currentDailyChallenge != -1) {//If player already has a daily challenge
				
								if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.1f, 300, 30), "Complete Daily Challenge!")) {
										player.completeDailyChallenge ();
								}
						} else if (player.currentDailyChallenge == -1 && DateTime.Compare (DateTime.Today, player.dailyChallengeSetDate) != 0) {
								if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.1f, 300, 30), "New Daily Challenge")) {
										player.newDailyChallenge(); // If it is not the day of the last completed daily challenge, then make a button to get a new one.
										//Assumes player cannot travel backwards in time
								}
						} else {
				
				TimeSpan ts = player.dailyChallengeSetDate.AddDays(1).Subtract(DateTime.Now);//Gets how many hours till 12 AM until day after daily challenge
				countdown = string.Format("{0} Hours, {1} Minutes, {2} Seconds until new daily challenge!", ts.Hours, ts.Minutes, ts.Seconds);
				//Counts down
				GUI.Label (new Rect (Screen.width * 0.3f, Screen.height * 0.1f, 700, 30), countdown);
			}
		}
	}
}
