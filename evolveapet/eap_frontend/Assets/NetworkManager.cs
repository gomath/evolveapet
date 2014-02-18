using UnityEngine;
using System.Collections;
using EvolveAPet;
public class NetworkManager : MonoBehaviour
{
	public PhotonPlayer[] PlayerList{ get; set;}

	float timer;
	public const float TICK_TIME = 10;
	void Start ()
	{
		timer = TICK_TIME;

		PhotonNetwork.offlineMode = true;
		Connect ();
	}
	
	void Connect ()
	{
		
		
		if (PhotonNetwork.offlineMode)
			OfflineConnect ();
		else
			PhotonNetwork.ConnectUsingSettings ("Alpha version");

		
	}

	void Update(){

		if (timer > 0) {

			timer -= Time.deltaTime;
		} else {timer=TICK_TIME;
			GetPlayerList();
		}
	}
	
	void OnGUI ()
	{	


			
		
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		GUILayout.Label (PhotonNetwork.insideLobby.ToString ());

	}
	
	void OnJoinedLobby ()
	{
		PhotonNetwork.JoinRandomRoom ();
		GetPlayerList ();
	}

	void GetPlayerList(){


		PlayerList=PhotonNetwork.otherPlayers;
		if (PlayerList != null)
			Debug.Log (PlayerList.Length);

	}
	
	void OnJoinedRoom ()
	{	
		Debug.Log (PhotonNetwork.room.name);
		SetPlayerData ();
				
	}
	void SetPlayerData(){
		//PhotonNetwork.player.name = ...
	}
	void OnPhotonRandomJoinFailed ()
	{
		Debug.Log ("first one");
		PhotonNetwork.CreateRoom ("only one");
		
		
	}
	
	void OfflineConnect ()
	{
		
		PhotonNetwork.JoinRandomRoom ();
	}

	//ALL  THIS IS THE TRADING TAB


	PhotonView pv;

	//THIS IS DONE ON START
	//on start pv= PhotonView.Get (this);

	PhotonPlayer trading_player;
	Animal selected_animal;
	// this is called on trade button press
	void RequestTrade (PhotonPlayer p)
	{

		trading_player = p;
		pv.RPC("DisplayTradeRequest",p,PhotonNetwork.player);
		Debug.Log ("Sent over the network");
	
	}
	[RPC]
	void DisplayTradeRequest(PhotonPlayer p){
		//GUI stuff
		// if accepted
		trading_player = p;
		DisplayTradeTab ();
	}
	
	[RPC]
	void DisplayTradeTab ()
	{		
		Debug.Log ("accepted");
		pv.RPC("DisplayTradeTab",trading_player);
		//GUI STUFF, chromozome choosing etc...
		selected_animal = null ;//The animal he chose
		pv.RPC("Breed",trading_player,selected_animal);

	}
	[RPC]
	void Breed(Animal mate){
		//take into account case where one player did not pick the animal on time
		while (selected_animal==null) {
					
				}
		selected_animal.BreedMeRandomly (mate);
		// do whatever else is needed
		selected_animal = null;
		trading_player = null;
	}

	
}
