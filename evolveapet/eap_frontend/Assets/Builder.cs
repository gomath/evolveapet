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
			string dailyChallengeStr = player.getDailyChallengeString ();
			string completeBtnStr;
			string countdown;
			float center = Screen.width * 0.5f -200;
			GUI.Label (new Rect (center, Screen.height * 0.05f, 400, 50), dailyChallengeStr);
			TimeSpan ts = player.dailyChallengeSetDate.AddDays(1).Subtract(DateTime.Now);
			countdown = string.Format("{0} Hours, {1} Minutes, and {2} Seconds until new daily challenge!", ts.Hours, ts.Minutes, ts.Seconds);
			if (player.currentDailyChallenge == -1) {
				//If player has no current daily challenge, then there are two possibilities. Either he needs a new one, or he needs to wait.
				if (DateTime.Compare (DateTime.Today, player.dailyChallengeSetDate) != 0){
					if (GUI.Button (new Rect (center, Screen.height * 0.1f + 20, 400, 30), "New Daily Challenge")) {
						player.newDailyChallenge(); // If it is not the day of the last completed daily challenge, then make a button to get a new one.
						//Assumes player cannot travel backwards in time
					}
				}else{
					if (!(ts.Seconds <=0 && ts.Hours<=0 && ts.Minutes<=0)){
						GUI.Label (new Rect (center, Screen.height * 0.1f, 400, 50), countdown);}
				}				    
			}else{
				if (GUI.Button (new Rect (center, Screen.height * 0.1f +20, 400, 30), "Complete Daily Challenge!")) {
					player.completeDailyChallenge();
				}
			}
			
			
		}
	}
}
