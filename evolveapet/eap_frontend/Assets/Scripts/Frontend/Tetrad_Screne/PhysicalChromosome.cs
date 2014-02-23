using UnityEngine;
using System.Collections;

namespace EvolveAPet{

public class PhysicalChromosome : MonoBehaviour {
	
	private Chromosome chromosome;
	public int chromosomeNumber { get; set; }
	private Color[] geneColors;
	private Color COLOR_STRONG = Color.red;
	private Color COLOR_WEAK = Color.white;

	public Chromosome Chromosome{
		get{
			return chromosome;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		SendMessageUpwards("ChromosomeClicked",this);

	}
	/// <summary>
	/// Initializes underlying real chromosome and colour all genes according to points
    /// of split during cross over. 
	/// </summary>
	/// <param name="ch">Ch.</param>
	public void InitializeUnderlyingChromosome(Chromosome ch){
			chromosome = ch;
			geneColors = new Color[chromosome.NumOfGenes];

			for (int i=0; i<chromosome.NumOfGenes; i++) {
				if(ch.WhereHasBeenSplit >=0 && i>=ch.WhereHasBeenSplit){
					geneColors[i] = COLOR_STRONG;
					transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = COLOR_STRONG;	
				} else{
					geneColors[i] = COLOR_WEAK;
					transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = COLOR_WEAK;	

				}
			}
	}
	
	/// <summary>
	/// Recolors all the genes on this chromosomes according to the crossover split. 
	/// </summary>
	public void RecolorAllYourGenesAccordingToSplit(){
			for (int i=0; i<chromosome.NumOfGenes; i++) {
				if(chromosome.WhereHasBeenSplit >=0 && i>=chromosome.WhereHasBeenSplit){
					transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = COLOR_STRONG;	
				} else{
					transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = COLOR_WEAK;	
                    
                }
			}

	}
	
	/// <summary>
	/// Reset color on target gene to its original one. 
	/// </summary>
	/// <param name="gene">Gene.</param>
	public void ResetColorAtGene(int gene){
			transform.FindChild("gene " + gene).GetComponent<SpriteRenderer>().color = geneColors[gene];	
	}
	/// <summary>
	/// Center whole screen according to tetrad corresponding to this chromosome. 
	/// </summary>
	public void PutMyTetradIntoCentre(){
		SendMessageUpwards ("TranslateAllTetrads");
	}


}
}
