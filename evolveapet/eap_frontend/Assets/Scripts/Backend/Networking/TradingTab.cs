using UnityEngine;
using System.Collections;
using EvolveAPet;

public class TradingTab : MonoBehaviour
{

		public PhotonPlayer TradingPlayer{ get; set; }

		private Animal selected_animal;
		//private const float THINKING_TIME = 20;
		//private float timer;
		//The timer was causing some unwanted events
		bool trading_window = false;
	float busy_timer=0;
		void OnGUI ()
	{if (busy_timer > 0) {
			busy_timer-=Time.deltaTime;
			GUI.Window (1, new Rect ((float)(Screen.width / 2 - 50), (float)(Screen.height * 0.05), 200, 50), BusyWindow, "Failed");
		}
				//	if (timer > 0)
				if (trading_window)
						
						GUI.ModalWindow (0, new Rect (Screen.width / 2 - 50, (float)(Screen.height * 0.1), 300, 80), GUITradeRequest, "Trade Request");
			
		}

		void GUITradeRequest (int windowID)
		{
				GUILayout.Label (TradingPlayer.name + " wants to breed with your selected pet");
				//		GUILayout.Label ("Time left to make a decision: " + (int)timer);
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Accept")) {
						
						AcceptTrade ();
						
				}
				if (GUILayout.Button ("Decline")) {
						PhotonView.Get (this).RPC ("CleanObject", TradingPlayer);
						CleanObject ();
				}

				GUILayout.EndHorizontal ();
				//timer -= Time.deltaTime;


		}
		
		[RPC]
		public void DisplayTradeRequest (PhotonPlayer p)
		{		
				if (TradingPlayer == null) {
						TradingPlayer = p;
						trading_window = true;
				} else {
			PhotonView.Get (this).RPC("Busy",p);
				}
				//timer = THINKING_TIME;

		}

		void AcceptTrade ()
		{
				//timer = 0;
				trading_window = false;
				Debug.Log ("accepted");
				PhotonView.Get (this).RPC ("DisplayTradeTab", TradingPlayer);
				DisplayTradeTab ();
		}
		

		//NEEDs revision
		[RPC]
		void DisplayTradeTab ()
		{		
				
				
				//GUI STUFF, chromozome choosing etc...
				selected_animal = FrontEndPlayer.Player.Stable[FrontEndPlayer.Player.Stable.activeAnimalNumber];
				PhotonView.Get (this).RPC ("Breed", TradingPlayer, selected_animal);
		
		}

		//NEEDS revision
		[RPC]
		void Breed (Animal mate)
		{
				Debug.Log ("Breed");

				FrontEndPlayer.Player.Stable.AddPet (selected_animal.BreedMeRandomly (mate));
				// do whatever else is needed
				
				CleanObject ();
		}
		
		[RPC]
		void CleanObject ()
		{
				trading_window = false;
				//timer = 0;
				selected_animal = null;
				TradingPlayer = null;
				
		}

		public void  RequestTrade (PhotonPlayer p)
		{


				
						TradingPlayer = p;
						PhotonView.Get (this).RPC ("DisplayTradeRequest", p, PhotonNetwork.player);
						Debug.Log ("Sent over the network");
		
		}
	[RPC]
	void Busy()
	{	

		Debug.Log ("busy");
		busy_timer = 3;
		CleanObject ();
		

	}

	void BusyWindow (int id)
	{
		
		GUILayout.Label ("Player is busy!");
		
		
		
	}
	
}

