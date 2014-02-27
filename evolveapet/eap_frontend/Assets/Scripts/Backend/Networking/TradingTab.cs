using UnityEngine;
using System.Collections;
using EvolveAPet;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.IO;

public class TradingTab : MonoBehaviour
{

		public PhotonPlayer TradingPlayer{ get; set; }

		private Chromosome[] selected_chromozomes = null;
		private Animal selected_animal;
		bool trading_window = false;
		float busy_timer = 0;

		void OnGUI ()
		{
				if (busy_timer > 0) {
						busy_timer -= Time.deltaTime;
						GUI.Window (1, new Rect ((float)(Screen.width / 2 - 50), (float)(Screen.height * 0.05), 200, 50), BusyWindow, "Failed");
				}
				
				if (trading_window)
						
						GUI.ModalWindow (0, new Rect (Screen.width / 2 - 50, (float)(Screen.height * 0.1), 300, 80), GUITradeRequest, "Trade Request");
			
		}

		void GUITradeRequest (int windowID)
		{
				GUILayout.Label (TradingPlayer.name + " wants to breed with your selected pet");
				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Accept")) {
						
						AcceptTrade ();
						
				}
				if (GUILayout.Button ("Decline")) {
						if (TradingPlayer != null)
								PhotonView.Get (this).RPC ("CleanObject", TradingPlayer);
						CleanObject ();
				}

				GUILayout.EndHorizontal ();
				


		}
		
		[RPC]
		public void DisplayTradeRequest (PhotonPlayer p)
		{		
				if (TradingPlayer == null) {
						PhotonView.Get (this).RPC ("Accepted", p, PhotonNetwork.player);
						TradingPlayer = p;
						trading_window = true;
				} else {
						if (TradingPlayer != null)
								PhotonView.Get (this).RPC ("Busy", p);
				}
				

		}

		void AcceptTrade ()
		{
				
				trading_window = false;
				Debug.Log ("accepted");
				if (TradingPlayer != null) {
						PhotonView.Get (this).RPC ("DisplayTradeTab", TradingPlayer);
						DisplayTradeTab ();
				} else
						Busy ();
		}
		

		//NEEDs revision
		[RPC]
		void DisplayTradeTab ()
		{		
				
				
				//GUI STUFF, chromozome choosing etc...
				StartCoroutine (WaitForChromozomesAndBreed ());

		
		}
		
		IEnumerator WaitForChromozomesAndBreed ()
		{
				while (selected_chromozomes == null) {
						yield return new WaitForSeconds (1);

				}
				
				FrontEndPlayer.Player.Stable.activeAnimalNumber = ((int)(Random.value * 100)) % FrontEndPlayer.Player.Stable.Size;
				selected_animal = FrontEndPlayer.Player.Stable .animalsInStable [FrontEndPlayer.Player.Stable.activeAnimalNumber];
				byte[] animal_bytes = serialize (selected_animal);
				if (TradingPlayer != null)						
						PhotonView.Get (this).RPC ("Breed", TradingPlayer, animal_bytes);
				else
						Busy ();
		}

		[RPC]
		void Breed (byte[] mate_bytes)
		{
				StartCoroutine (WaitForChromozomesToBreed (mate_bytes));
		}

		//NEEDS revision
		IEnumerator WaitForChromozomesToBreed (byte[] mate_bytes)
		{
				while (selected_chromozomes == null) {
						yield return new WaitForSeconds (1);
			
				}
				Debug.Log ("Breed");
				Animal mate = (Animal)deserialize (mate_bytes);
				FrontEndPlayer.Player.Stable.AddPet (selected_animal.BreedMeRandomly (mate), FrontEndPlayer.Player.Stable.Size);
				// do whatever else is needed
				//Create new animal
				GameObject animal = (GameObject)Instantiate (Resources.Load ("Prefabs/animal"));
				animal.GetComponent<PhysicalAnimal> ().animal = FrontEndPlayer.Player.Stable.animalsInStable [FrontEndPlayer.Player.Stable.Size - 1];
				animal.GetComponent<PhysicalAnimal> ().Build (animal);
				animal.transform.Translate (new Vector2 (-7 + (FrontEndPlayer.Player.Stable.Size - 3) * 4, -3));
				CleanObject ();
		}
	
		[RPC]
		void CleanObject ()
		{
				trading_window = false;
				selected_animal = null;
				TradingPlayer = null;
				
		}

		public void  RequestTrade (PhotonPlayer p)
		{
				if (TradingPlayer == null) {
						PhotonView.Get (this).RPC ("DisplayTradeRequest", p, PhotonNetwork.player);
						Debug.Log ("Sent over the network");
				}
		
		}

		[RPC]
		void Accepted (PhotonPlayer p)
		{
				if (TradingPlayer == null) {
						TradingPlayer = p;		
				} else {
						PhotonView.Get (this).RPC ("CleanObject", p);
				}
		
		}

		[RPC]
		void Busy ()
		{	

				Debug.Log ("busy");
				busy_timer = 3;
				CleanObject ();
		

		}

		void BusyWindow (int id)
		{
		
				GUILayout.Label ("Player is busy!");
		
		
		
		}

		byte[] serialize (object obj)
		{

				if (obj == null)
						return null;
				BinaryFormatter bf = new BinaryFormatter ();
				MemoryStream ms = new MemoryStream ();
				bf.Serialize (ms, obj);
				return ms.ToArray ();

		}

		object deserialize (byte[] arrBytes)
		{

				MemoryStream memStream = new MemoryStream ();
				BinaryFormatter binForm = new BinaryFormatter ();
				memStream.Write (arrBytes, 0, arrBytes.Length);
				memStream.Seek (0, SeekOrigin.Begin);
				object obj = binForm.Deserialize (memStream);
				return obj;

		}
	
}

