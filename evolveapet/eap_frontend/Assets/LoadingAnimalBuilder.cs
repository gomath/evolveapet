using UnityEngine;
using System.Collections;

namespace EvolveAPet{
	public class LoadingAnimalBuilder : MonoBehaviour {

		public GameObject animal;

		float originalWidth = 1098.0f;
		float originalHeight = 618.0f;
		Vector3 scale = new Vector3();
		
		public GUISkin mySkin;
		
		// Use this for initialization
		void Start () {
			animal.GetComponent<PhysicalAnimal>().animal = Player.playerInstance.animalForBreeding1;
			animal.GetComponent<PhysicalAnimal>().Build(animal);
			animal.transform.FindChild("animal skeleton").GetComponent<Animator>().SetTrigger("Loading");
			animal.transform.localScale = new Vector3(-1,1,1);
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

			GUI.skin.label.fontSize = 30;
			GUI.Label(new Rect((originalWidth/2)-500,50,1000,50),"Waiting for your partner to select tetrads...");
			GUI.skin.label.fontSize = 0;
		}
	}
}
