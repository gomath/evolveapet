using UnityEngine;
using System.Collections;

namespace EvolveAPet{

	public class BuildRandomBreeding : MonoBehaviour {

		public Transform father;
		public Transform mother;
		public Transform child;

		public GameObject aFather;
		public GameObject aMother;
		public GameObject aChild;
		
		// Use this for initialization
		void Start () {
			StartCoroutine("genFather");
			StartCoroutine("genMother");
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		void OnGUI() {
			if (GUI.Button (new Rect(0,0,200, 30), "Build")) {
				StartCoroutine("genFather");
			}
			if (GUI.Button (new Rect(Screen.width-200,0,200, 30), "Build")) {
				StartCoroutine("genMother");
			}
			if (GUI.Button (new Rect(Screen.width-200,Screen.height-30,200, 30), "Breed")) {
				StartCoroutine("genChild");
			}
		}

		IEnumerator genFather() {
			//Remove the previous animal
			if (aFather != null) GameObject.Destroy(aFather);
			Resources.UnloadUnusedAssets();
			
			//Wait one frame for destroys to commit
			yield return new WaitForSeconds(0f);
			
			aFather = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			aFather.GetComponent<PhysicalAnimal>().animal = new Animal();

			aFather.GetComponent<PhysicalAnimal>().Build(aFather);
			aFather.transform.position = father.position;
			aFather.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		}

		IEnumerator genMother() {
			//Remove the previous animal
			if (aMother != null) GameObject.Destroy(aMother);
			Resources.UnloadUnusedAssets();
			
			//Wait one frame for destroys to commit
			yield return new WaitForSeconds(0f);
			
			aMother = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			aMother.GetComponent<PhysicalAnimal>().animal = new Animal();

			aMother.GetComponent<PhysicalAnimal>().Build(aMother);
			aMother.transform.position = mother.position;
			aMother.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		}

		IEnumerator genChild() {
			//Remove the previous animal
			if (aChild != null) GameObject.Destroy(aChild);
			Resources.UnloadUnusedAssets();
			
			//Wait one frame for destroys to commit
			yield return new WaitForSeconds(0f);
			
			aChild = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			aChild.GetComponent<PhysicalAnimal>().animal = aFather.GetComponent<PhysicalAnimal>().animal.BreedMeRandomly(aMother.GetComponent<PhysicalAnimal>().animal);

			aChild.GetComponent<PhysicalAnimal>().Build(aChild);
			aChild.transform.position = child.position;
		}
	}
}
