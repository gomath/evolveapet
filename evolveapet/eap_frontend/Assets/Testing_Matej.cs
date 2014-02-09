using UnityEngine;
using System.Collections;
using EvolveAPet;

public class Testing_Matej : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MyDictionary.Init (); // initializing maps (Dictionaries) in MyDictionary

		/*
		Gene g = new Gene (3, 2);
		g.display(0);
		g = new Gene (0, 0);
		g.display(1);
		g = new Gene (1, 2);
		g.display(2);
		g = new Gene (4, 1);
		g.display(3);
		g = new Gene (4, 3);
		g.display(4);
		*/

		for (int chr=0; chr<7; chr++) {
			for(int gene=0; gene < MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)chr]; gene++){
				Gene g = new Gene(chr,gene);
				g.display(10*chr + gene);
			}		
		}
		/*string[][][][][] a = MyDictionary.geneDict;
		for (int i=0; i<a.GetLength(0); i++) {
			for(j)
		}*/

		//MyDictionary.displayGeneDict ();
	}	
	
	// Update is called once per frame
	void Update () {
	
	}
}
