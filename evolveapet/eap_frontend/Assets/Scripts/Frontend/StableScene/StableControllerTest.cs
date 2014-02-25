using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/* this is untest code! here there be monsters!
 * 
 */
namespace EvolveAPet {
public class StableControllerTest : MonoBehaviour {
		public int numActiveStalls;
		public bool[] stallStatii = new bool[6];
		public Animal activeAnimal;
	
		public Transform stable0;
		public Transform stable1;
		public Transform stable2;
		public Transform stable3;
		public Transform stable4;
		public Transform stable5;
 
		GameObject a0=null;
		GameObject a1=null;
		GameObject a2=null;
		GameObject a3=null;
		GameObject a4=null;
		GameObject a5=null;

		Animal an0=null;
		Animal an1=null;
		Animal an2=null;
		Animal an3=null;
		Animal an4=null;
		Animal an5=null;


		public Animal[] potentialAnimals;
		public GameObject[] potentialGameObjects;
		public Transform[] stableLocs;

		//public List<GameObject> anObjects = new ArrayList<GameObject>();


	void OnGUI() {

	}

	// Use this for initialization
	void Start () {
			potentialAnimals = new Animal[]{an0,an1,an3, an4, an5};
			potentialGameObjects = new GameObject[]{a0,a1,a3,a4,a5};
			stableLocs = new Transform[]{stable0,stable1,stable2,stable3,stable4,stable5};

		//setup player's stable by instantiating user's animals
		numActiveStalls = 3;

		//populate player's stable
		for (int i = 0; i< numActiveStalls; i++) {
					StartCoroutine ("BuildAnimalAtIndex", i);

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

			potentialAnimals [anIndex] = new Animal ();
			potentialGameObjects [anIndex] = (GameObject)Instantiate (Resources.Load ("Prefabs/animal"));

			potentialGameObjects [anIndex].GetComponent<PhysicalAnimal> ().animal = potentialAnimals [anIndex];

			//build animal
			potentialGameObjects[anIndex].GetComponent<PhysicalAnimal>().Build(potentialGameObjects[anIndex]);
			potentialGameObjects [anIndex].transform.position = stableLocs[anIndex].position;
			potentialGameObjects [anIndex].transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			//set position in bool array tracking stable occupations to true
			stallStatii [anIndex] = true;
	}


}
}
