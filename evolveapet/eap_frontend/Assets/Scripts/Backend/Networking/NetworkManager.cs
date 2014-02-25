using UnityEngine;
using System.Collections;
using EvolveAPet;
//not tested for connection drop
public class NetworkManager : MonoBehaviour
{
		

		void Start ()
		{
				//PhotonNetwork.offlineMode = true;
	
				Connect ();
		}
	
		void Connect ()
		{
		
		
				if (PhotonNetwork.offlineMode)
						OfflineConnect ();
				else
						PhotonNetwork.ConnectUsingSettings ("Alpha version");

		
		}
	
		void OnGUI ()
		{	
				
				GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
				GUILayout.Label (PhotonNetwork.insideLobby.ToString ());
				GUILayout.Label (FrontEndPlayer.Player.Stable.Count.ToString ());
				if (GUILayout.Button ("Player List")) {
						if (!PlayerList.open)
								Instantiate (Resources.Load ("Prefabs/Networking/PlayerList"), Vector2.zero, Quaternion.identity);
				}

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
				
				
		}

		public static void SetPlayerData ()
		{		

				PhotonNetwork.player.name = FrontEndPlayer.Player.UserName;
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
