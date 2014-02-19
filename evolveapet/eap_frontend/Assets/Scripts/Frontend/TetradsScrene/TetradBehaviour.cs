using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{

public class TetradBehaviour : MonoBehaviour {

	private GameObject activeChromosome;
	private GameObject myBox;

	// Use this for initialization
	void Start () {
		short boxNum = Convert.ToInt16(name.Substring(name.Length-1,1));
		myBox = GameObject.Find ("Box" + boxNum);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChromosomeClicked(PhysicalChromosome chromosome){

		GameObject centromere = chromosome.transform.FindChild("centromere").FindChild("centromere colour").gameObject;

		if (centromere.GetComponent<SpriteRenderer> ().color != Color.green) {
			centromere.GetComponent<SpriteRenderer> ().color = Color.green;
		} else {
			centromere.GetComponent<SpriteRenderer> ().color = Color.red;
		}

		myBox.GetComponent<BoxBehaviour>().CreateChromosomeMirror(chromosome);
	}

	void TranslateAllTetrads(){
		float offset = transform.localPosition.y;
		SendMessageUpwards ("TranslateAllTetradsByOffset", offset);
	}
}
}
