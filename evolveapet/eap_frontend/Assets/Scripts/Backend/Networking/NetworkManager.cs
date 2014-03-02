using UnityEngine;
using System.Collections;
using EvolveAPet;

public class NetworkManager : MonoBehaviour
{
		

		void Start ()
		{

				//PhotonNetwork.offlineMode = true;
		if(!PhotonNetwork.connected)
				Connect ();


				
		}

		void OnFailedToConnectToPhoton (DisconnectCause cause)
		{
				Debug.Log (cause.ToString ());
				PhotonNetwork.offlineMode = true;
				Connect ();
		}

		void Connect ()
		{
		
				
				if (PhotonNetwork.offlineMode)
						OfflineConnect ();
				else
						PhotonNetwork.ConnectUsingSettings ("Alpha version");

		SetPlayerData ();
		}

	
		void OnJoinedLobby ()
		{
				PhotonNetwork.JoinRandomRoom ();

		}

		public static PhotonPlayer[] GetPlayerList ()
		{


				return PhotonNetwork.playerList;

		}
	
		void OnJoinedRoom ()
		{	
				Debug.Log (PhotonNetwork.room.name);
		if (GameObject.Find ("TradingTab(Clone)") == null)
			PhotonNetwork.Instantiate ("TradingTab",Vector2.zero,Quaternion.identity,0);
				
				
		}

		public void SetPlayerData ()
		{		

		PhotonNetwork.player.name = Player.playerInstance.UserName;
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

		


}
