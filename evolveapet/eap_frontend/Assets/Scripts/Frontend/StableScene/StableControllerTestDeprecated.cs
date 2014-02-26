//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//
//
///* this is untest code! here there be monsters!
// * 
// */
//namespace EvolveAPet {
//public class StableControllerTestDeprecated : MonoBehaviour {
//		public int numActiveStalls;
//		public bool[] areOccupied = new bool[6];
//		public bool[] areLocked = new bool[6];
//		public bool[] areUnlocked = new bool[6];
//	
//		public Transform stable0;
//		public Transform stable1;
//		public Transform stable2;
//		public Transform stable3;
//		public Transform stable4;
//		public Transform stable5;
// 
//		GameObject a0=null;
//		GameObject a1=null;
//		GameObject a2=null;
//		GameObject a3=null;
//		GameObject a4=null;
//		GameObject a5=null;
//
//		Animal an0=null;
//		Animal an1=null;
//		Animal an2=null;
//		Animal an3=null;
//		Animal an4=null;
//		Animal an5=null;
//
//
//		public Animal[] potentialAnimals;
//		public GameObject[] potentialGameObjects;
//		public Transform[] stableLocs;
//
//		//public List<GameObject> anObjects = new ArrayList<GameObject>();
//
//		public GUISkin myskin;
//
//
//	void OnGUI() {
//			GUI.skin = myskin;
//			//get screen coordinates of stable[i] sprite to set button locs appropriately
//			for (int i=0; i<6; i++) {
//						Vector3 loc = camera.WorldToScreenPoint (new Vector3(stableLocs [i].position.x,-stableLocs [i].position.y,1)); 
//						Vector3 newXY = loc + new Vector3 (-30, 30, 0);
//				
//						//misusing vectors: I am so sorry
//						Vector4 topButton = new Vector4 (newXY.x, newXY.y, 60, 30); //last two coords are height and length
//						Vector4 bottomButton = topButton + new Vector4 (0, 30, 0, 0);
//				
//						if (areUnlocked [i]) {
//								if (areOccupied [i]) {
//										if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "Make Active")) {
//												//do stuffs
//												Debug.LogWarning ("Make Active "+i+" pressed.");
//							
//										}
//						
//										if (GUI.Button (new Rect (bottomButton.x, bottomButton.y, bottomButton.z, bottomButton.w), "Release Animal")) {
//												Debug.LogWarning ("Release Button "+i+" pressed.");
//										}
//								} else {
//										if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "New Random Animal")) {
//												Debug.LogWarning ("rand animal button "+i+ " pressed.");
//										}
//								}
//						} else {
//								if (GUI.Button (new Rect (topButton.x, topButton.y, topButton.z, topButton.w), "Unlock")) {
//										Debug.LogWarning ("unlock "+i+" pressed");
//								}
//						}
//
//				}
//
//	}
//
//	// Use this for initialization
//	void Start () {
//			areUnlocked = new bool[]{true,true,true,true, false, false};
//			areOccupied = new bool[]{false,false,false,false,false,false};
//			potentialAnimals = new Animal[]{an0,an1,an3, an4, an5};
//			potentialGameObjects = new GameObject[]{a0,a1,a3,a4,a5};
//			stableLocs = new Transform[]{stable0,stable1,stable2,stable3,stable4,stable5};
//
//		//setup player's stable by instantiating user's animals
//		numActiveStalls = 3;
//
//		//populate player's stable
//		for (int i = 0; i< numActiveStalls; i++) {
//					StartCoroutine ("BuildAnimalAtIndex", i);
//
//			} 
//	}
//
//
//	
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	IEnumerator BuildAnimalAtIndex(int anIndex) {
//			if (potentialGameObjects[anIndex] != null) GameObject.Destroy(potentialGameObjects[anIndex]);
//			Resources.UnloadUnusedAssets();
//			
//			//Wait one frame for destroys to commit
//			yield return new WaitForSeconds(0f);
//
//			potentialAnimals [anIndex] = new Animal ();
//			potentialGameObjects [anIndex] = (GameObject)Instantiate (Resources.Load ("Prefabs/animal"));
//
//			potentialGameObjects [anIndex].GetComponent<PhysicalAnimal> ().animal = potentialAnimals [anIndex];
//
//			//build animal
//			potentialGameObjects[anIndex].GetComponent<PhysicalAnimal>().Build(potentialGameObjects[anIndex]);
//			potentialGameObjects [anIndex].transform.position = stableLocs[anIndex].position;
//			potentialGameObjects [anIndex].transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//
//			//set position in bool array tracking stable occupations to true
//			areOccupied [anIndex] = true;
//	}
//
//
//}
//}
