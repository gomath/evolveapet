using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class BoxBehaviour : MonoBehaviour {
	
	private PhysicalChromosome chromosome = null;
	private GameObject genderSign;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateChromosomeMirror(PhysicalChromosome ch){
			chromosome = ch;
			ShowMyChromosome();
	}

	void OnMouseDown(){
			ShowMyChromosome ();

			if (chromosome != null) {
				chromosome.PutMyTetradIntoCentre();
		}
	}

		public void ShowMyChromosome(){
			if (chromosome != null) {
				SendMessageUpwards("ShowMagnifiedChromosome",chromosome);
			}
		}

		public void SetGenderSign(int i){
			// Destroy old one
			if (genderSign != null) {
				GameObject.Destroy(genderSign);
			}

			genderSign = (GameObject)Instantiate (Resources.Load ("Prefabs/GenderSign/" + i));
			genderSign.transform.position = transform.FindChild ("number_join").position;
		}

}
}
