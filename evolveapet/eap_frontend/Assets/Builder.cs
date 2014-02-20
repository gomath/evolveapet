using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.05f, 1000, 30), dailyChallengeStr);
			if (player.currentDailyChallenge!=-1){
				
				if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * 0.1f, 300, 30), "Complete Daily Challenge!")) {
					player.completeDailyChallenge ();
				}
			}
			GUI.Label (new Rect (Screen.width * 0.42f, Screen.height * 0.3f, 300, 30), "Your points: " + player.Points);
		}
	}
}
