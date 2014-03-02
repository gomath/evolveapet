using UnityEngine;
using System.Collections;

namespace EvolveAPet {
	public class MainMenuAnimalBuilder : MonoBehaviour {

		public Transform move;

		// Use this for initialization
		void Start () {
			GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			animal.GetComponent<PhysicalAnimal>().animal = new Animal();
			animal.GetComponent<PhysicalAnimal>().Build(animal);
			animal.transform.position = move.position;
			animal.transform.localScale = new Vector3(0.75f,0.75f,1);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
