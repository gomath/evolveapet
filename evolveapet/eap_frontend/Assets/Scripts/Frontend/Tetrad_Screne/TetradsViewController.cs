using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet {

public class TetradsViewController : MonoBehaviour {
	
	PhysicalChromosome[,] chromosomes;
	GameObject[,] magnifiedChromosomes;
	GameObject activeChromosome;

	// Use this for initialization
	void Start () {
		// Initializing magnified chromosomes
		magnifiedChromosomes = new GameObject[Global.NUM_OF_CHROMOSOMES,2];
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			magnifiedChromosomes[i,0] = transform.FindChild("MagnifiedChromosomes").FindChild("ch_"+i+"m").gameObject;
			magnifiedChromosomes[i,1] = transform.FindChild("MagnifiedChromosomes").FindChild("ch_"+i+"f").gameObject;
		}

		// Initializing individual chromosomes
		chromosomes = new PhysicalChromosome[Global.NUM_OF_CHROMOSOMES,4];
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			chromosomes[i,0] = transform.FindChild("Tetrads").FindChild("Tetrad_" + i).FindChild("chromosome pair " + i + "A").FindChild("chromosome m").gameObject.GetComponent<PhysicalChromosome>();
			chromosomes[i,1] = transform.FindChild("Tetrads").FindChild("Tetrad_" + i).FindChild("chromosome pair " + i + "A").FindChild("chromosome f").gameObject.GetComponent<PhysicalChromosome>();
			chromosomes[i,2] = transform.FindChild("Tetrads").FindChild("Tetrad_" + i).FindChild("chromosome pair " + i + "B").FindChild("chromosome m").gameObject.GetComponent<PhysicalChromosome>();
			chromosomes[i,3] = transform.FindChild("Tetrads").FindChild("Tetrad_" + i).FindChild("chromosome pair " + i + "B").FindChild("chromosome f").gameObject.GetComponent<PhysicalChromosome>();
			
			// Initialize random chromosomes and their underlying genes
			for(int j =0; j<4; j++){
				chromosomes[i,j].InitializeUnderlyingChromosome(new Chromosome(i));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowMagnifiedChromosome(PhysicalChromosome ch){
			if (activeChromosome != null) {
				activeChromosome.SetActive(false);			
			}

			string p = ch.transform.parent.name;
			int tetradNumber = Convert.ToInt32(p.Substring(p.Length-2,1));
			string n = ch.transform.name;
			int mf = (n.Substring (n.Length - 1, 1) == "m") ? 0 : 1;

			activeChromosome = magnifiedChromosomes [tetradNumber, mf];
			activeChromosome.SetActive (true);

			// Color active magnified chromosome according to its source physical chromosome
			for (int i=0; i<ch.Chromosome.NumOfGenes; i++) {
				activeChromosome.transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = ch.transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color;
			}

			// Initializing underlying genes
			for (int i=0; i<ch.Chromosome.NumOfGenes; i++) {
				GameObject temp = activeChromosome.transform.FindChild("gene " + i).gameObject;
				if(temp.GetComponent<GeneShowName>() == null){
					temp.AddComponent<GeneShowName>();
					temp.GetComponent<GeneShowName>().gene = ch.Chromosome.Genes[i];
				}
			}

	}
}

}
