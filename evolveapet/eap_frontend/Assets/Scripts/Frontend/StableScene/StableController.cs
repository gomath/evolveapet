﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/* this is untest code! here there be monsters!
 * 
 */
namespace EvolveAPet {
public class StableController : MonoBehaviour {
		public int numActiveStalls;
		public bool[] areOccupied = new bool[6];
		public bool[] areUnlocked = new bool[6];
	
		public Transform stable0;
		public Transform stable1;
		public Transform stable2;
		public Transform stable3;
		public Transform stable4;
		public Transform stable5;

		public GameObject padlock0;
		public GameObject padlock1;
		public GameObject padlock2;
		public GameObject padlock3;
		public GameObject padlock4;
		public GameObject padlock5;

 
		GameObject a0=null;
		GameObject a1=null;
		GameObject a2=null;
		GameObject a3=null;
		GameObject a4=null;
		GameObject a5=null;

		int pointsForUnlock  = 10;
		int pointsForNewAnimal = 5;

		public Animal[] potentialAnimals;
		public GameObject[] potentialGameObjects;
		public Transform[] stableLocs;
		public GameObject[] padlocks;


	void OnGUI() {
			for (int i=0; i<6; i++) {
						//get screen coordinates of stable[i] sprite to set button locs appropriately
						//Screen coordinates have the origin in the bottom left, GUI coordinates have orogin at the top left
						Vector3 rawPos = stableLocs [i].position;

						Vector3 loc = camera.WorldToScreenPoint (new Vector3(rawPos.x, -rawPos.y,1)); 
						Vector3 newXY = loc + new Vector3 (-60, 60, 0);

						//compute relative positions for the buttons
						Vector4 topButton = new Vector4 (newXY.x, newXY.y, 120, 20); //last two coords are height and length
						Vector4 bottomButton = topButton + new Vector4 (0, 30, 0, 0);

						if (areUnlocked [i]) {
								if (areOccupied [i]) {
										if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "Make Active")) {
												
												Debug.LogWarning ("Make Active pressed");
												Player.playerInstance._stable.activeAnimalNumber = i;
										}
					
										if (GUI.Button (new Rect (bottomButton.x, bottomButton.y, bottomButton.z, bottomButton.w), "Release Animal")) {
												Debug.LogWarning ("Release Button pressed.");
												if(numActiveStalls > 1) {
													//release into wild
													areOccupied[i] = false;
													//potentialAniamls[i].releaseIntoWild();
												} else {
													//nope.
												}
												
										}
								} else {
										if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "New Random Animal")) {
												Debug.LogWarning ("rand animal button pressed.");
												if(Player.playerInstance.Points > pointsForUnlock) {
													Player.playerInstance.Points -= pointsForNewAnimal;
													Animal an = new Animal();
													Player.playerInstance._stable.animalsInStable[i] = an;
													potentialAnimals[i] = an;
													StartCoroutine("BuildAnimalAtIndex",i);

												}
												
						
										}
								}
						} else {
								if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "Unlock")) {
										Debug.LogWarning ("unlock pressed");
										if(Player.playerInstance.Points > pointsForUnlock) {
											Player.playerInstance.Points -= pointsForUnlock;
											areUnlocked[i] = true; 
											padlocks[i].GetComponent<SpriteRenderer>().enabled = false; //fancy animations later
										}
								}
						}
				}

	}

	// Use this for initialization
	void Start () {
			potentialAnimals = Player.playerInstance._stable.animalsInStable;
			potentialGameObjects = new GameObject[]{a0,a1,a2,a3,a4,a5};
			stableLocs = new Transform[]{stable0,stable1,stable2,stable3,stable4,stable5};
			//CHANGE THIS
			areUnlocked = Player.playerInstance._stable.activeStableSlots;

			areOccupied = Player.playerInstance._stable.livingAnimals;
			padlocks = new GameObject[]{padlock0,padlock1,padlock2, padlock3, padlock4,padlock5};

		//setup player's stable by instantiating user's animals
		numActiveStalls = Player.playerInstance._stable.Size;

		//populate player's stable
		for (int i = 0; i< numActiveStalls; i++) {
					StartCoroutine ("BuildAnimalAtIndex", i);

		} 

		if (Player.playerInstance._stable.eggSlot != null) {
				//Player.playerInstance.eggslot.hatch();
		}
	}


	
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator BuildAnimalAtIndex(int anIndex) {
			if (potentialGameObjects[anIndex] != null) GameObject.Destroy(potentialGameObjects[anIndex]);
			Resources.UnloadUnusedAssets();
			
			//Wait one frame for destroys to commit
			yield return new WaitForSeconds(0f);

//			potentialAnimals [anIndex] = Player.playerInstance._stable.ElementAt(anIndex);
			potentialGameObjects [anIndex] = (GameObject)Instantiate (Resources.Load ("Prefabs/animal"));

			potentialGameObjects [anIndex].GetComponent<PhysicalAnimal> ().animal = potentialAnimals [anIndex];

			//build animal
			potentialGameObjects[anIndex].GetComponent<PhysicalAnimal>().Build(potentialGameObjects[anIndex]);
			potentialGameObjects [anIndex].transform.position = stableLocs [anIndex].position;
			potentialGameObjects [anIndex].transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			//set position in bool array tracking stable occupations to true
			areOccupied [anIndex] = true;
	}


}
}
