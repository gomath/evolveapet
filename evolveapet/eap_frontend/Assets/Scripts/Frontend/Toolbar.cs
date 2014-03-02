using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
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

		public Texture helpT;

		private bool showBreedingPopup = false;
		private bool showCantBreedPopup = false;
		private bool showLocalBreedingPopupPart = false;
		private Animal[] animals = new Animal[6]{new Animal(),new Animal(),new Animal(),new Animal(),new Animal(),new Animal()};
		private bool[] animalAlive = new bool[6]{true,true,true,true,true,true};
		private int stableSize = 6;
		private bool[] localBreedingToggleSelected = new bool[]{false,false,false,false,false,false};
		private string popupHeading = "Breed your perfect pet";
		private float popupButtonHeight = 40f;
		private float popupWindowHeight = 110f;
		private float toggleInPopupHeight = 30f;

		public Vector2 scrollPosition;
		
		private string[] PlayerNames{ get; set; }
		
		private List<PhotonPlayer> Players{ get; set; }

		bool networkPopup = false;

		// Use this for initialization
		void Start () {
/*			if (currentPlayer == null) {
				Player.playerInstance = new Player(new Stable(),"TestPlayer");			
			}
*/
			GetPlayerList ();

			currentPlayer = Player.playerInstance; //Checks if there is already an instantiated player
			animals = currentPlayer.Stable.animalsInStable;
			animalAlive = currentPlayer.Stable.livingAnimals;

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
					if (Player.playerInstance.Stable.NumberOfUnlockedSlots - Player.playerInstance._stable.Size >0) {
						showBreedingPopup = true;
					} else {
						showCantBreedPopup = true;
					}
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
					Player.playerInstance.saveGame();

				}
			}

			GUI.skin.button.fontSize = (50);
			help = GUILayout.Toggle(help,"?","button",GUILayout.Width(75));
			GUI.skin.button.fontSize = (0);

			GUILayout.EndHorizontal();
			GUILayout.EndArea();

			if(showBreedingPopup){
				Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
				Vector3 v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
				GUI.Window (2,new Rect(v.x,originalHeight-v.y,300,popupWindowHeight),PopupOnBreeding,popupHeading);
			}

			if (showCantBreedPopup) {
				Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
				Vector3 v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
				popupHeading = "Stable too small.";
				GUI.Window (2,new Rect(v.x,originalHeight-v.y,300,180),PopupWhenCantBreed,popupHeading);		
			}
			if (networkPopup) {
				Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
				Vector3 v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
				GUI.Window (2,new Rect(v.x,originalHeight-v.y,300,300),BreedOverNetwork,"Player List");
			}

			GUI.depth = -100;
			if(help) GUI.Label(new Rect (0,0,originalWidth,originalHeight),helpT);

		}

		int index = 0;
		void BreedOverNetwork(int id)
		{

			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (200), GUILayout.Height (200));

			index = GUILayout.SelectionGrid (index, PlayerNames,1 ,GUILayout.Height(popupButtonHeight * PlayerNames.Length));
			GUILayout.EndScrollView ();
			GUILayout.BeginHorizontal ();
			if (index != 0) {
				if (GUILayout.Button ("Breed")) {
					GameObject.Find ("TradingTab(Clone)").GetComponent<TradingTab> ().RequestTrade (Players.ElementAt (index));
					networkPopup = false;
					
					
					
				}
			}
			if (GUILayout.Button ("Refresh"))
				GetPlayerList ();
			if (GUILayout.Button ("Close")) {
				networkPopup = false;
				
				
			}
			GUILayout.EndHorizontal ();

				}

		void GetPlayerList ()
		{
			Players = NetworkManager.GetPlayerList ().ToList ();
			PlayerNames = Players.Select (p => p.name).ToArray ();
			
		}

		void PopupOnBreeding(int id){
			GUILayout.BeginVertical ();
				GUILayout.BeginHorizontal();
					if (GUILayout.Button ("Breed locally",GUILayout.Height(popupButtonHeight))) {
						showLocalBreedingPopupPart = true;
					}
					if (GUILayout.Button ("Breed with a friend",GUILayout.Height(popupButtonHeight))) {
				networkPopup=true;
				showBreedingPopup=false;

					}
				GUILayout.EndHorizontal ();
				
				if (showLocalBreedingPopupPart) {
					popupWindowHeight = 30f + 2 * popupButtonHeight + currentPlayer.Stable.Size * toggleInPopupHeight;//300f;
					int numOfAnimalsSelected = 0;
					int currentAnimalToAdd = 0;
					int[] animalIndex = new int[2];

					for(int i=0; i<stableSize; i++){
						if(animalAlive[i]){
							localBreedingToggleSelected[i] = GUILayout.Toggle(localBreedingToggleSelected[i],animals[i].Name);
							if(localBreedingToggleSelected[i]){
								numOfAnimalsSelected ++;
								
								animalIndex[currentAnimalToAdd] = i;
								currentAnimalToAdd = (currentAnimalToAdd + 1) % 2;
							}
						}
					}
					
					if(numOfAnimalsSelected == 2){
						popupWindowHeight = popupWindowHeight + popupButtonHeight;
						if(GUILayout.Button("Breed",GUILayout.Height(popupButtonHeight))){
							currentPlayer.animalForBreeding1 = animals[animalIndex[0]];
							currentPlayer.animalForBreeding2 = animals[animalIndex[1]];
							currentPlayer.remainingAnimalsToBreed = 2;
							currentPlayer.animalToChooseForBreeding = 1;
						currentPlayer.network_breeding=false;
							Application.LoadLevel("CreateTetradsScrene");
							//GUILayout.Box ("Breeding scene");	
						}
					}
					
				}
				

				if (GUILayout.Button ("Close",GUILayout.Height(popupButtonHeight))) {
					showBreedingPopup = false;
					showLocalBreedingPopupPart = false;
					popupWindowHeight = 110f;
				}

			GUILayout.EndVertical ();
		}

		void PopupWhenCantBreed(int id){
			GUILayout.BeginVertical ();
			GameObject.Find ("Main Camera").GetComponent<StableController> ().buttonShow = false;;
			GUILayout.Label("You can't breed because you don't have enough slots in your stable. Try unlocking a stable slot or releasing some of your animals to wild.");

			if (GUILayout.Button ("Close",GUILayout.Height(popupButtonHeight))) {
				showCantBreedPopup = false;
				GameObject.Find ("Main Camera").GetComponent<StableController> ().buttonShow = true;

			}
			GUILayout.EndVertical ();

		}
	}
}
