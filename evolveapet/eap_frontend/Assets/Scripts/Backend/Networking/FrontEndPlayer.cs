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
				NetworkManager.SetPlayerData ();
		}

}
