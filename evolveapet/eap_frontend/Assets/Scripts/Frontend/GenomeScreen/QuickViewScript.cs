using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class QuickViewScript : MonoBehaviour {

	Transform[,] miniPhysicalChromosomes;
	Transform[] boxes;
	
	void Start () {
		miniPhysicalChromosomes = new Transform[Global.NUM_OF_CHROMOSOMES,2];
		boxes = new Transform[Global.NUM_OF_CHROMOSOMES];

			for(int i=0; i<Global.NUM_OF_CHROMOSOMES; i++){
				boxes[i] = transform.FindChild("Box" + i);
				miniPhysicalChromosomes[i,0] = GetMiniPhysicalChromosome(i,"m"); 
				miniPhysicalChromosomes[i,1] = GetMiniPhysicalChromosome(i,"f"); 
			}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Returns male or female physical mini chromosome at given location
	/// </summary>
	/// <returns>The mini physical chromosome.</returns>
	/// <param name="chromosomeNum">Chromosome number.</param>
	/// <param name="mf">Mf.</param>
	Transform GetMiniPhysicalChromosome(int ch, String mf){
		return transform.FindChild ("Box" + ch).FindChild ("ch_pair_" + ch).FindChild ("chromosome " + mf);	
	}


}
}
