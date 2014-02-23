using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class BoxBehaviour : MonoBehaviour {
	
	private PhysicalChromosome chromosome = null;
	private GameObject genderSign;

	public PhysicalChromosome PhysicalChromosome{
		get{return chromosome;}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Sets underlying chromosome to physical chromosome and show the correct magnified chromosome. 
	/// </summary>
	/// <param name="ch">Ch.</param>
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
		/// <summary>
		/// Show magnified chromosome corresponding to this box. 
		/// </summary>
		public void ShowMyChromosome(){
			if (chromosome != null) {
				SendMessageUpwards("ShowMagnifiedChromosome",chromosome);
			}
		}
		/// <summary>
		/// Set male/female/hermafrodite sign in this box. 
		/// </summary>
		/// <param name="i">The index.</param>
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
