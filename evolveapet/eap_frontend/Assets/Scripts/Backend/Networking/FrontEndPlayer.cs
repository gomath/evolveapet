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
				
				Player.Stable.Size = 5;
				Player.Stable.AddPet (new Animal ());
				Player.Stable.AddPet (new Animal ());
		GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal.GetComponent<PhysicalAnimal> ().animal = FrontEndPlayer.Player.Stable[0];
		animal.GetComponent<PhysicalAnimal>().Build(animal);
		animal.transform.Translate (new Vector2 (6, 3));


		 animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));
		animal.GetComponent<PhysicalAnimal> ().animal = FrontEndPlayer.Player.Stable[1];
		animal.GetComponent<PhysicalAnimal>().Build(animal);
		animal.transform.Translate (new Vector2 (-6, 3));

				NetworkManager.SetPlayerData ();
		}

}
