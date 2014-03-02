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

		void Awake(){
		DontDestroyOnLoad (gameObject);
		}

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
		
		[RPC]
		void DisplayTradeTab ()
		{		
				

				CallTeradScene ();
				StartCoroutine (WaitForChromozomesAndBreed ());

		
		}

		void CallTeradScene ()
		{
				selected_animal = Player.playerInstance.Stable.GetActiveAnimal ();
				Player.playerInstance.chromosomes1 = null;
				Player.playerInstance.animalForBreeding1 = selected_animal;
				Player.playerInstance.animalToChooseForBreeding = 1;
				Player.playerInstance.network_breeding = true;
				Application.LoadLevel ("CreateTetradsScrene");
		}

		bool CheckIfSet ()
		{
				if (Player.playerInstance.chromosomes1 != null) {
						selected_chromozomes = Player.playerInstance.chromosomes1;

						return false;
				}

				return true;
		}
		
		IEnumerator WaitForChromozomesAndBreed ()
		{
				
				while (CheckIfSet()) {
						
						yield return new WaitForSeconds (0);

				}
				Debug.Log ("Tetrads choosed");
				byte[] animal_bytes = serialize (selected_animal);
				byte[] chromozomes_bytes = serialize (selected_chromozomes);
				if (TradingPlayer != null)						
						PhotonView.Get (this).RPC ("Breed", TradingPlayer, animal_bytes, chromozomes_bytes);
				else
						Busy ();
		}

		[RPC]
		void Breed (byte[] mate_bytes, byte[] chromozomes_bytes)
		{
				Debug.Log ("Breeding started");
				StartCoroutine (WaitForChromozomesToBreed (mate_bytes, chromozomes_bytes));
		}
		
		IEnumerator WaitForChromozomesToBreed (byte[] mate_bytes, byte[] chromozomes_bytes)
		{
				while (CheckIfSet()) {
						
						yield return new WaitForSeconds (1);
			
				}
				Debug.Log ("Breed");
				Animal mate = (Animal)deserialize (mate_bytes);
				Chromosome[] chromozomes = (Chromosome[])deserialize (chromozomes_bytes);
				Animal child = new Animal (selected_chromozomes, chromozomes, selected_animal, mate);
				Player.playerInstance.Stable.eggSlot = child;
				CleanObject ();
		}
	
		[RPC]
		void CleanObject ()
		{
				Debug.Log ("Cleanup started");
				selected_chromozomes = null;
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

