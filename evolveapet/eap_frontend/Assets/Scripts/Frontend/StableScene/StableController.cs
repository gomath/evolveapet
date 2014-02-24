using UnityEngine;
using System.Collections;
using System.Linq;


/* this is untest code! here there be monsters!
 * 
 */
namespace EvolveAPet {
public class StableController : MonoBehaviour {
		int numActiveStalls;
		bool[] stallStatii = new bool[6];
		public Transform stable0;
		public Transform stable1;
		public Transform stable2;
		public Transform stable3;
		public Transform stable4;
		public Transform stable5;
 
		public GameObject a0;
		public GameObject a1;
		public GameObject a2;
		public GameObject a3;
		public GameObject a4;
		public GameObject a5;

		public Animal an0;
		public Animal an1;
		public Animal an2;
		public Animal an3;
		public Animal an4;
		public Animal an5;


	void OnGUI() {

	}

	// Use this for initialization
	void Start () {
		//setup player's stable by instantiating user's animals
		numActiveStalls = Player.playerInstance._stable.Size;

		//if no created animal GameObjects
		if (stallStatii.All (b => !b)) {

			for (int i = 0; i< numActiveStalls; i++) {
					StartCoroutine ("BuildAnimalAtIndex", i);

			} 

		}else {
			for (int i=0; i<5; i++) {
					if (numActiveStalls >= i-1) {
							StartCoroutine ("BuildAnimalAtIndex", i);
					}
			}
		}
	}


	
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator BuildAnimalAtIndex(int anIndex) {

			/*
			 * commented out because I suck at reflection; going to try keeping dictionaries of GameObjects and Animals...
			
			//set local ref to player's animal
			((Animal)this.GetType ().GetField ("an" + anIndex)) = Player.playerInstance._stable.ElementAt (anIndex);

			//make GameObject from animal
			this.GetType().GetField("a"+anIndex) = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
			((GameObject)this.GetType ().GetField ("a" + anIndex).GetValue()).GetComponent<PhysicalAnimal> ().animal = (Animal)this.GetType ().GetField ("a" + anIndex).GetValue();

			((GameObject)this.GetType ().GetField ("a" + anIndex).GetValue ()).GetComponent<PhysicalAnimal>().Build (((GameObject)this.GetType ().GetField ("a" + anIndex).GetValue ()));
	
			((GameObject)this.GetType ().GetField ("a" + anIndex).GetValue ()).transform.position = this.GetType ().GetField ("stable" + anIndex).GetValue ();
			((GameObject)this.GetType ().GetField ("a" + anIndex).GetValue()).transform.localScale = new Vector3(0.5f,0.5f,0.5f);
			*/
			yield return new WaitForSeconds(0f);
	}


}
}
