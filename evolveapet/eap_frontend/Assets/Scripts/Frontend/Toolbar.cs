using UnityEngine;
using System.Collections;

namespace EvolveAPet{
	public class Toolbar : MonoBehaviour {

		float originalWidth = 1098.0f;
		float originalHeight = 618.0f;
		Vector3 scale = new Vector3();

		public GUISkin mySkin;

		Player currentPlayer;

		public int selected;

		string[] buttons = {"Stable","Animal","Genome","Breed"};

		public Texture coins;

		// Use this for initialization
		void Start () {
			currentPlayer = Player.playerInstance; //Checks if there is already an instantiated player
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnGUI() {
			GUI.skin = mySkin;
			
			scale.x = Screen.width / originalWidth;
			scale.y = Screen.height / originalHeight;
			scale.z = 1;
			var svMat = GUI.matrix;
			// substitute matrix to scale if screen nonstandard
			GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);

			GUILayout.BeginArea(new Rect(5,originalHeight-75,originalWidth-5,75));
			GUILayout.BeginHorizontal();

			GUILayout.Label(coins);

			GUILayout.BeginVertical();

			GUI.color = Color.black;
			if (currentPlayer != null) {
				GUILayout.Label(currentPlayer.NickName,  GUILayout.Width (200));
				//GUILayout.Label (currentPlayer.Points.ToString);
			} else {
				GUILayout.Label("Wow, such NickName", GUILayout.Width (200));
				GUI.skin.label.fontSize = (30);
				GUILayout.Label ("9000", GUILayout.Width (200));
				GUI.skin.label.fontSize = (0);
				//GUILayout.Space(10);
			}

			GUI.color = Color.white;

			GUILayout.EndVertical();


			GUILayout.Toolbar(selected, buttons);

			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
	}
}
