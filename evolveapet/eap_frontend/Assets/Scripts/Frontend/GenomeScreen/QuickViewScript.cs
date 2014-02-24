using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class QuickViewScript : MonoBehaviour {

	Transform[,] miniPhysicalChromosomes;
	Transform[] boxes;
		Color SET_COLOR = new Color32(49,77,121,255);
		Color UNSET_COLOR = new Color32(111,150,210,255);
	
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

	public void SetActiveBox(int n){
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				boxes[i].FindChild ("Background").GetComponent<SpriteRenderer>().color = UNSET_COLOR;
			}
			boxes [n].FindChild ("Background").GetComponent<SpriteRenderer> ().color = SET_COLOR;
	}

	public void SetGeneColor(int ch, int g, Color c){

				miniPhysicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				miniPhysicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
				miniPhysicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				miniPhysicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
    }
        
        
    }
}
