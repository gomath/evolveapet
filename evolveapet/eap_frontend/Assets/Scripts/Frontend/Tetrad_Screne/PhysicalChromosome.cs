using UnityEngine;
using System.Collections;

namespace EvolveAPet{

public class PhysicalChromosome : MonoBehaviour {
	
	private Chromosome chromosome;
	public int chromosomeNumber { get; set; }
	
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

	public void InitializeUnderlyingChromosome(Chromosome ch){
			chromosome = ch;
			for (int i=0; i<chromosome.NumOfGenes; i++) {
				int r = Global.rand.Next(4);
				Color c = Color.white;
				switch(r){
					case 0: c = Color.white; break;
					case 1: c = Color.black; break;
					case 2: c = Color.blue; break;
					case 3: c = Color.red; break;
				}
				transform.FindChild("gene " + i).GetComponent<SpriteRenderer>().color = c;				
			}

	}

	public void PutMyTetradIntoCentre(){
		SendMessageUpwards ("TranslateAllTetrads");
	}


}
}
