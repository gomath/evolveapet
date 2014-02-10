using UnityEngine;
using System;
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

		/*
		for (int chr=0; chr<7; chr++) {
			for(int gene=0; gene < MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)chr]; gene++){
				Gene g = new Gene(chr,gene);
				g.display(10*chr + gene);
			}		
		}*/

		//Chromosome ch = new Chromosome (0);
		//ch.Display ();

		/*
		for (int i=20; i<30; i++) {

			Genome genome = new Genome ();
			genome.Display (i);
		}
		*/
		for (int i=1; i<=20; i++) {
			testRandomMutations (i);
		}
		/*string[][][][][] a = MyDictionary.geneDict;
		for (int i=0; i<a.GetLength(0); i++) {
			for(j)
		}*/

		//MyDictionary.displayGeneDict ();
	}	

	public static void testRandomMutations(int testNumber){
		string path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Test_of_mutations\\";
		string dst = "Random_mutations_test_" + testNumber + ".txt";
		
		System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
		file.AutoFlush = true;

		file.WriteLine ("Legend: original random genes and then its distinct random mutations)");

		Genome genome = new Genome();
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			file.WriteLine("----------CHROMOSOME #" + i + "----------");
			for(int j=0; j<MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i]; j++){
				Gene g = genome.MotherChromosomes[i].Genes[j];
				Gene[] gMutated = g.RandomMutations();

				file.Write(String.Format("{0,-20}","#" + j + ": " + MyDictionary.traitDict[i][j]) + "\t" + String.Format("{0,-15}",g.GetWholeNameEncoded()));
				for(int k=0; k<gMutated.Length; k++){
					file.Write(String.Format("{0,-15}",gMutated[k].GetWholeNameEncoded()));
				}
				file.WriteLine();
			}
		}
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
