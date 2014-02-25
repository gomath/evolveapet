using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet {

public class TetradsViewController : MonoBehaviour {
	
	Genome g; // TODO remove at the end
	public PhysicalChromosome[,] chromosomes;
	GameObject[,] magnifiedChromosomes;
	public GameObject activeChromosome;
	public static Color CHOSEN_COLOR = Color.green;
	public static Color NUMBER_COLOR = new Color32(111,150,210,255);

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
			g = new Genome();
			Chromosome[,] ch = g.CreateTetradsForBreeding();
			
			chromosomes[i,0].InitializeUnderlyingChromosome(ch[i,1]);
			chromosomes[i,1].InitializeUnderlyingChromosome(ch[i,0]);
			chromosomes[i,2].InitializeUnderlyingChromosome(ch[i,3]);
			chromosomes[i,3].InitializeUnderlyingChromosome(ch[i,2]);

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
	
	/// <summary>
	/// Reset all gene colours of magnified chromosome to corresponding chromosome in source tetrads. 
	/// </summary>
	void ResetMagnifiedChromosome(){
			String s = activeChromosome.name;
			int chromosomeNum = Convert.ToInt32 (s.Substring (s.Length - 2, 1));
			PhysicalChromosome ch = transform.FindChild ("QuickView").FindChild ("Box" + chromosomeNum).GetComponent<BoxBehaviour> ().PhysicalChromosome;
			ShowMagnifiedChromosome(ch);
	}
	
	/// <summary>
	/// Set color of currently chosen box to white and keep rest light blue. 
	/// </summary>
	/// <param name="boxName">Box name.</param>
	void ColourCurrentBox(String boxName){
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			Transform box = transform.FindChild("QuickView").FindChild ("Box"+i);
			if(box.name != boxName){
					box.FindChild("num"+(i+1)).GetComponent<SpriteRenderer>().color =  NUMBER_COLOR;
			} else {
					box.FindChild("num"+(i+1)).GetComponent<SpriteRenderer>().color = Color.white;
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

	
	// Set of methods for colouring the genes corresponding to various body parts
	void ToggleEyes(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.EYES);
			SetColorToGenes (on, loci);
			//Debug.Log ("EYES");
	}
	void ToggleEars(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.EARS);
			SetColorToGenes (on, loci);   
			//Debug.Log ("EARS");
	}

	void ToggleHead(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.HEAD);
		SetColorToGenes (on, loci);
		//Debug.Log ("HEAD");
    
	}
	void ToggleTorso(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.TORSO);
			SetColorToGenes (on, loci); 
			//Debug.Log ("TORSO");
	}
	void ToggleArms(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.ARMS);
			SetColorToGenes (on, loci);
			//Debug.Log ("ARMS");
	}
	void ToggleLegs(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.LEGS);
			SetColorToGenes (on, loci);
			//Debug.Log ("LEGS");
	}
	void ToggleTail(bool on){
			Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.TAIL);
			SetColorToGenes (on, loci);
			//Debug.Log ("TAIL");
	}
        
    // Sets colour to all genes on given loci
	void SetColorToGenes(bool on, Locus[] loci){
		foreach(Locus l in loci){
			int ch = l.Chromosome;
			int g = l.GeneNumber;
			for(int i=0; i<4; i++){
				PhysicalChromosome temp = chromosomes[ch,i];
				if(!on){
					temp.ResetColorAtGene(g);
				} else {
					Transform gene = temp.transform.FindChild("gene " + g);
					Color c = gene.GetComponent<SpriteRenderer>().color;
					gene.GetComponent<SpriteRenderer>().color = new Color(c.r,c.g,c.b,1);
				}

			}
		}
	}

	void OnGUI(){
		if (chosen) {
			GUI.Box(new Rect(0,0,100,40),"Chosen");
		}
	}

	
}

}
