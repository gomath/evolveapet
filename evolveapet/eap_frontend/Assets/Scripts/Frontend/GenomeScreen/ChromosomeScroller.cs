using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class ChromosomeScroller : MonoBehaviour {

		int activeChromosome;
	
	// Use this for initialization
	void Start () {
		activeChromosome = 3;
	}
	
	// Update is called once per frame
	void Update () {
			int move = 0;
			if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				move = 1;
			} else if(Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				move = -1;
			}

			if(move == 1 && activeChromosome < Global.NUM_OF_CHROMOSOMES - 1){
				activeChromosome++;
				SendMessageUpwards("BoxClicked",activeChromosome);
			} else
			if(move == -1 && activeChromosome > 0){
				activeChromosome--;
				SendMessageUpwards("BoxClicked",activeChromosome);
			}
	}

	/// <summary>
	/// Returns male or female physical chromosome at given location 
	/// </summary>
	/// <returns>The physical chromosome.</returns>
	/// <param name="chromosomeNum">Chromosome number.</param>
	/// <param name="mf">Mf.</param>
	ChromosomeScript GetPhysicalChromosome(int chromosomeNum, String mf){
		return transform.FindChild("Chromosomes").FindChild("ch_pair_" + chromosomeNum).FindChild("chromosome " + mf).GetComponent<ChromosomeScript>();			
	}

}
}
