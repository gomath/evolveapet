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
		/*
		for (int i=1; i<=20; i++) {
			testRandomMutations (i);
		}*/

		for (int i=1; i<=20; i++) {
			testCreatingOfTetrads(i);
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

	public static void testCreatingOfTetrads(int testNumber){
		string path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Test_of_tetrads_creation\\";
		string dst = "Creating_Tetrads_Test_" + testNumber + ".txt";
		
		System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
		file.AutoFlush = true;
		
		file.WriteLine ("Legend (column meanings): mother and father original chromosomes, crossover 1, crossover 2.");
		file.WriteLine ("Note: -...- means the genes has been splitted during cross over");
		
		Genome genome = new Genome();
		Chromosome[,] tetrad = genome.CreateTetradsForBreeding();
		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			file.WriteLine("----------CHROMOSOME #" + i + "----------");
			file.WriteLine("Split at gene #" + tetrad[i,2].WhereHasBeenSplit);

			for(int j=0; j<MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i]; j++){
				Gene motherOriginal = tetrad[i,0].Genes[j];
				Gene fatherOriginal = tetrad[i,1].Genes[j];
				Gene crossOver1 = tetrad[i,2].Genes[j];
				Gene crossOver2 = tetrad[i,3].Genes[j];

				String f = "{0,-15}";
				// Plain strings
				String s1 = motherOriginal.GetWholeNameEncoded();
				String s2 = fatherOriginal.GetWholeNameEncoded();
				String s3 = crossOver1.GetWholeNameEncoded(); // originally mother chromosome
				String s4 = crossOver2.GetWholeNameEncoded(); // originally father chromosome

				// Testing where has split occured and marking those lines
				if(j >= tetrad[i,2].WhereHasBeenSplit){
					s3 = "-" + s3 + "-";
					s4 = "-" + s4 + "-";
				}
				// Formatted strings - for pretty output
				String sf0 = String.Format ("{0,-20}", "#" + j + ": " + MyDictionary.traitDict[i][j]) + "|     ";
				String sf1 = String.Format (f, s1) + "|     ";
				String sf2 = String.Format (f, s2) + "|     ";
				String sf3 = String.Format (f, s3) + "|     ";
				String sf4 = String.Format (f, s4) + "|     ";
				 
				file.WriteLine(sf0 + sf1 + sf2 + sf3 + sf4);
			}
			file.WriteLine();
		}
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
