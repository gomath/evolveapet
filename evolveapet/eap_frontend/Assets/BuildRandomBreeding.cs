using UnityEngine;
using System.Collections;

namespace EvolveAPet{

public class BuildRandomBreeding : MonoBehaviour {

	
	// Use this for initialization
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
		
		//Create 1st new animal
		GameObject animal1 = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal1.GetComponent<PhysicalAnimal>().animal = new Animal();
			//animal1.transform.position = new Vector3 (10, 10, 10);
		animal1.GetComponent<PhysicalAnimal>().BuildAndSpecifyPos(10,10);
			//animal1.transform.localScale = new Vector3 (0.3f, 0.3f, 1);

	/*
		GameObject animal2 = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal2.GetComponent<PhysicalAnimal>().animal = new Animal();
		animal2.GetComponent<PhysicalAnimal>().Build();
			animal2.transform.position.Set (30,0,2);
	*/
		// Create 2nd new animal
	}
	
	void OnGUI() {
		if (GUI.Button (new Rect(Screen.width-200,Screen.height-30,200, 30), "Build")) {
			StartCoroutine("Build");
		}
	}
}
}
