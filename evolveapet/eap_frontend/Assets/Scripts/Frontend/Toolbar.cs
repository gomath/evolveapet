using UnityEngine;
using System.Collections;

namespace EvolveAPet{
	public class Toolbar : MonoBehaviour {

		float originalWidth = 1098.0f;
		float originalHeight = 618.0f;
		Vector3 scale = new Vector3();

		public GUISkin mySkin;

		Player currentPlayer;

		public int level;

		public Texture coins;

		bool help;

		private bool showBreedingPopup = false;
		private bool showLocalBreedingPopupPart = false;
		private Animal[] animals = new Animal[6]{new Animal(),new Animal(),new Animal(),new Animal(),new Animal(),new Animal()};
		private bool[] animalAlive = new bool[6]{true,true,true,true,true,true};
		private int stableSize = 6;
		private bool[] localBreedingToggleSelected = new bool[]{false,false,false,false,false,false};
		private string popupHeading = "Breed your perfect pet";
		private float popupButtonHeight = 40f;
		private float popuWindowHeight = 110f;

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
				GUI.skin.label.fontSize = (30);
				GUILayout.Label (currentPlayer.Points.ToString(), GUILayout.Width (200));
				GUI.skin.label.fontSize = (0);
			} else {
				GUILayout.Label("Wow, such NickName", GUILayout.Width (200));
				GUI.skin.label.fontSize = (30);
				GUILayout.Label ("9000", GUILayout.Width (200));
				GUI.skin.label.fontSize = (0);
			}

			GUI.color = Color.white;

			GUILayout.EndVertical();

			if (level == 0) {	
				if (GUILayout.Button("Animal")){
					Application.LoadLevel("Animal");
					Player.autoSave();

				}
				if (GUILayout.Button("Genome")){
					Application.LoadLevel("GenomeScene");
					Player.autoSave();

				}
				if (GUILayout.Button("Breed")) {
					showBreedingPopup = true;
					Player.autoSave();

				}
				if (GUILayout.Button("Exit")){
					Application.LoadLevel("MainMenu");
					Player.autoSave();
				}


			}

			if (level == 1) {
				if (GUILayout.Button("Stable")){ 
					Application.LoadLevel("Stable");
					Player.autoSave();

				}
				if (GUILayout.Button("Genome")){ 
					Application.LoadLevel("GenomeScene");
					Player.autoSave();

				}
				/*if (GUILayout.Button("Breed")) {
					showBreedingPopup = true;
				}*/
				if (GUILayout.Button("Exit")){
					Application.LoadLevel("MainMenu");
					Player.autoSave();
				}
			}

			if (level == 2) {
				if (GUILayout.Button("Stable")){ 
					Application.LoadLevel("Stable");
						Player.autoSave();

					}
				if (GUILayout.Button("Animal")){
					Application.LoadLevel("Animal");
					Player.autoSave();

				}
				/*if (GUILayout.Button("Breed")){
					showBreedingPopup = true;
				};*/
				if (GUILayout.Button("Exit")){
					Application.LoadLevel("MainMenu");
					Player.autoSave();

				}
			}

			GUI.skin.button.fontSize = (50);
			GUILayout.Toggle(help,"?","button",GUILayout.Width(75));
			GUI.skin.button.fontSize = (0);

			GUILayout.EndHorizontal();
			GUILayout.EndArea();

			if(showBreedingPopup){
				Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
				Vector3 v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
				GUI.Window (2,new Rect(v.x,originalHeight-v.y,300,popuWindowHeight),PopupOnBreeding,popupHeading);
			}
		}

		void PopupOnBreeding(int id){
			GUILayout.BeginVertical ();
				GUILayout.BeginHorizontal();
					if (GUILayout.Button ("Breed locally",GUILayout.Height(popupButtonHeight))) {
						showLocalBreedingPopupPart = true;
					}
					if (GUILayout.Button ("Breed with a friend",GUILayout.Height(popupButtonHeight))) {
								
					}
				GUILayout.EndHorizontal ();
				
				if (showLocalBreedingPopupPart) {
					popuWindowHeight = 300f;
					int numOfAnimalsSelected = 0;
					for(int i=0; i<stableSize; i++){
						if(animalAlive[i]){
							localBreedingToggleSelected[i] = GUILayout.Toggle(localBreedingToggleSelected[i],i+"");
							if(localBreedingToggleSelected[i]){
								numOfAnimalsSelected ++;
							}
						}
					}
					
					if(numOfAnimalsSelected == 2){
						popuWindowHeight = 340f;
						if(GUILayout.Button("Breed",GUILayout.Height(popupButtonHeight))){
							GUILayout.Box ("Breeding scene");	
						}
					}
					
				}
				

				if (GUILayout.Button ("Close",GUILayout.Height(popupButtonHeight))) {
					showBreedingPopup = false;
					showLocalBreedingPopupPart = false;
					popuWindowHeight = 110f;
				}

			GUILayout.EndVertical ();

		}
	}
}
