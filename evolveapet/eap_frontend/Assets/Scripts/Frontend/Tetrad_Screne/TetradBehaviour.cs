using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{

public class TetradBehaviour : MonoBehaviour {

	private PhysicalChromosome activeChromosome;
		private int tetradNum;
		private int chromosomeNum;
	private GameObject myBox;

	// Use this for initialization
	void Start () {
		tetradNum = Convert.ToInt32(name.Substring(name.Length-1,1));
		myBox = GameObject.Find ("Box" + tetradNum);
		
		// Setting chromosome numbers
			transform.FindChild ("chromosome pair " + tetradNum + "A").FindChild ("chromosome m").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 0;
			transform.FindChild ("chromosome pair " + tetradNum + "A").FindChild ("chromosome f").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 1;
			transform.FindChild ("chromosome pair " + tetradNum + "B").FindChild ("chromosome m").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 2;
			transform.FindChild ("chromosome pair " + tetradNum + "B").FindChild ("chromosome f").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 3;
		// Randomly select one chromosome at the beginning
		string AB = "A";
		string mf = "m";
		int r = Global.rand.Next (4);
			switch(r){
			case 0: AB = "A"; mf = "m"; chromosomeNum = 0; break; 
			case 1: AB = "A"; mf = "f"; chromosomeNum = 1; break;
			case 2: AB = "B"; mf = "m"; chromosomeNum = 2; break;
			case 3: AB = "B"; mf = "f"; chromosomeNum = 3; break;
			}

		activeChromosome = transform.FindChild ("chromosome pair " + tetradNum + AB).FindChild("chromosome " + mf).gameObject.GetComponent<PhysicalChromosome>();
		ChromosomeClicked (activeChromosome);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChromosomeClicked(PhysicalChromosome chromosome){

		if (activeChromosome != null) {
			// Setting old centromere colour to white
			activeChromosome.transform.FindChild("centromere").FindChild("centromere colour").gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}
		// Setting new centrome colour to green
		activeChromosome = chromosome;
		activeChromosome.transform.FindChild ("centromere").FindChild ("centromere colour").gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		myBox.GetComponent<BoxBehaviour>().CreateChromosomeMirror(chromosome);
		myBox.GetComponent<BoxBehaviour> ().SetGenderSign (activeChromosome.chromosomeNumber);
	}

	void TranslateAllTetrads(){
		float offset = transform.localPosition.y;
		SendMessageUpwards ("TranslateAllTetradsByOffset", offset);
	}
}
}
