using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class GenomeViewController : MonoBehaviour {

	Genome g;
	Chromosome[,] chromosomes;
	ChromosomeScript[,] physicalChromosomes;
	

	// Use this for initialization
	void Start () {
			g = new Genome ();

			chromosomes = new Chromosome[Global.NUM_OF_CHROMOSOMES,2];
			physicalChromosomes = new ChromosomeScript[Global.NUM_OF_CHROMOSOMES,2];


			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				physicalChromosomes[i,0] = GetPhysicalChromosome(i,"m");
				physicalChromosomes[i,1] = GetPhysicalChromosome(i,"f");

				physicalChromosomes[i,0].Chromosome = g.FatherChromosomes[i];
				physicalChromosomes[i,1].Chromosome = g.MotherChromosomes[i];

				chromosomes[i,0] = physicalChromosomes[i,0].Chromosome;
				chromosomes[i,1] = physicalChromosomes[i,1].Chromosome;

			}
	}
	
	// Update is called once per frame
	void Update () {
		
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
	

	void BoxClicked(int boxNumber){
			Debug.Log ("Box " + boxNumber + " clicked.");
	}
}
}
