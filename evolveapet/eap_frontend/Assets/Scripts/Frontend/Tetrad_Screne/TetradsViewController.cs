using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet {

public class TetradsViewController : MonoBehaviour {
	
	PhysicalChromosome[,] chromosomes;
	GameObject[,] magnifiedChromosomes;
	GameObject activeChromosome;
		
	bool chosen = false; // TODO - remove at the end

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
			
			//Animal a = new Animal();
			//Genome g = a.Genome;
				Genome g = new Genome();
			Chromosome[,] ch = g.CreateTetradsForBreeding();
			
			chromosomes[i,0].InitializeUnderlyingChromosome(ch[i,1]);
			chromosomes[i,1].InitializeUnderlyingChromosome(ch[i,0]);
			chromosomes[i,2].InitializeUnderlyingChromosome(ch[i,2]);
			chromosomes[i,3].InitializeUnderlyingChromosome(ch[i,3]);

			/*
			// Initialize random chromosomes and their underlying genes
			for(int j =0; j<4; j++){
				chromosomes[i,j].InitializeUnderlyingChromosome(new Chromosome(i));
			}*/
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Called from boxbehaviour script. 
	/// </summary>
	/// <param name="ch">Ch.</param>
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
				Color col;
				if(i>=ch.Chromosome.WhereHasBeenSplit){
					col = Color.red;
				} else {
					col = Color.white;
				}
				activeChromosome.transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = col;// ch.transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color;
				 
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


	/// <summary>
	/// Called when choosing is finished and button clicked. 
	/// </summary>
	void TetradsChosen(){
			Debug.Log ("Tetrads chosen");
		Chromosome[] temp = new Chromosome[Global.NUM_OF_CHROMOSOMES];
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			temp[i] = transform.FindChild("Tetrads").FindChild("Tetrad_" + i).GetComponent<TetradBehaviour>().UnderlyingChromosome;
		}
		
		Chromosome[] backendChromosomes = Global.FrontEndToBackendChromosomes (temp);
		
		chosen = true;
	}

	void OnGUI(){
		if (chosen) {
			GUI.Box(new Rect(0,0,100,40),"Chosen");
		}
	}
}

}
