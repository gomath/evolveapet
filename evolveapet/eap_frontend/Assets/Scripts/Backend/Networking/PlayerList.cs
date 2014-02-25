using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerList : MonoBehaviour
{		
		public static bool open = false;
		public Vector2 scrollPosition;

		private string[] PlayerNames{ get; set; }

		private List<PhotonPlayer> Players{ get; set; }
		// Use this for initialization
		void Start ()
		{
				GetPlayerList ();
				open = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	int index;
		void OnGUI ()
		{			
				GUILayout.BeginArea (new Rect ((float)(Screen.width / 2 - 50), (float)(Screen.height * 0.1), 200, 250));
				GUILayout.Label ("Player List");
				scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (200), GUILayout.Height (200));

		index = GUILayout.SelectionGrid (index, PlayerNames,1 );
				GUILayout.EndScrollView ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Breed")) {
					
			GameObject.Find ("TradingTab").GetComponent<TradingTab> ().RequestTrade (Players.ElementAt (index));
								CleanObject ();
					
							
						            
				}
				if (GUILayout.Button ("Refresh"))
						GetPlayerList ();
				if (GUILayout.Button ("Close")) {
						CleanObject ();
						

				}
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
		                     

		}
		

		void GetPlayerList ()
		{
				Players = NetworkManager.GetPlayerList ().ToList ();
				PlayerNames = Players.Select (p => p.name).ToArray ();

		}

		void CleanObject ()
		{
				open = false;
				Destroy (this.gameObject);
		}
}
