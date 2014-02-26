using UnityEngine;
using System.Collections;
using EvolveAPet;


public class FrontEndPlayer : MonoBehaviour
{
		public static Player Player{ get; private set; }
		// Use this for initialization
		void Start ()
		{
				Player = new Player (new Stable (), (Random.value * 1000).ToString () );
		Player.Stable.unlockStableSlot (3);
		Player.Stable.unlockStableSlot (4);
				Player.Stable.AddPet (new Animal (),0);
				Player.Stable.AddPet (new Animal (),1);

		GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal.GetComponent<PhysicalAnimal> ().animal = FrontEndPlayer.Player.Stable.animalsInStable[0];
		animal.GetComponent<PhysicalAnimal>().Build(animal);
		animal.transform.Translate (new Vector2 (6, 3));


		 animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal.GetComponent<PhysicalAnimal> ().animal = FrontEndPlayer.Player.Stable.animalsInStable[1];
		animal.GetComponent<PhysicalAnimal>().Build(animal);
		animal.transform.Translate (new Vector2 (-6, 3));

				NetworkManager.SetPlayerData ();
		}
}

