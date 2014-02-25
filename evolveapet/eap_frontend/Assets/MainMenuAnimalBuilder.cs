using UnityEngine;
using System.Collections;

namespace EvolveAPet {
	public class MainMenuAnimalBuilder : MonoBehaviour {

		public Transform move;

		// Use this for initialization
		void Start () {
			GameObject animal = GameObject.Find("animal");
			animal.GetComponent<PhysicalAnimal>().animal = new Animal();
			animal.GetComponent<PhysicalAnimal>().Build(animal);
			animal.transform.position = move.position;
			animal.transform.localScale = new Vector2(0.75f,0.75f);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
