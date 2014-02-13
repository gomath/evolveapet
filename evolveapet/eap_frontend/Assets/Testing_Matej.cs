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

		/*
		for (int i=1; i<=20; i++) {
			testCreatingOfTetrads(i);
		}*/

		for (int i=3; i<10; i++) {
			testRandomBreeding (i);
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

	public static void testRandomBreeding(int testNumber){
		string path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Random_Breeding\\";
		string dst = "Random_breeding_" + testNumber + ".txt";
		
		System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
		file.AutoFlush = true;
		
		file.WriteLine ("Legend (column meanings): mother and father original chromosomes, crossover 1, crossover 2.");
		file.WriteLine ("Note: -...- means the genes has been splitted during cross over");
		
		Animal a1 = new Animal ();
		Genome g1 = a1.Genome;

		Animal a2 = new Animal ();
		Genome g2 = a2.Genome;

		/*Inlined from Animal.BreedMeRandomly*/
		Chromosome[,] tetrads1 = g1.CreateTetradsForBreeding ();
		Chromosome[,] tetrads2 = g2.CreateTetradsForBreeding ();
		
		Chromosome[] parent1 = new Chromosome[Global.NUM_OF_CHROMOSOMES];
		Chromosome[] parent2 = new Chromosome[Global.NUM_OF_CHROMOSOMES];

		int[,] chosen = new int[Global.NUM_OF_CHROMOSOMES, 2];

		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			int j = Global.rand.Next(4);
			chosen[i,0] = j;
			parent1[i] = tetrads1[i,j];
			
			j = Global.rand.Next(4);
			parent2[i] = tetrads2[i,j];
			chosen[i,1] = j;
		}
		
		Animal offspring = new Animal(parent1, parent2, a1, a2);


		for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
			String f = "{0,-9}";
			String d = "|  ";
			String d2 = "|| ";
			
			file.WriteLine("----------CHROMOSOME #" + i + "----------");
			file.WriteLine("Parent 1 split at gene #" + tetrads1[i,2].WhereHasBeenSplit);
			file.WriteLine("Parent 1 split at gene #" + tetrads2[i,2].WhereHasBeenSplit);
			file.Write(String.Format("{0,-20}"," ") + d);
			for(int j=0; j<4; j++){
				file.Write(String.Format (f,j) + ((j==3) ? d2 : d));
			}
			for(int j=0; j<4; j++){
				file.Write(String.Format (f,j) + ((j==3) ? d2 : d));
			}
			for(int j=0; j<2; j++){
				file.Write(String.Format(f,chosen[i,j]) + d);
			}

			file.WriteLine();

			for(int j=0; j<MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i]; j++){
				Gene g11 = tetrads1[i,0].Genes[j];
				Gene g12 = tetrads1[i,1].Genes[j];
				Gene g13 = tetrads1[i,2].Genes[j];
				Gene g14 = tetrads1[i,3].Genes[j];

				Gene g21 = tetrads2[i,0].Genes[j];
				Gene g22 = tetrads2[i,1].Genes[j];
				Gene g23 = tetrads2[i,2].Genes[j];
				Gene g24 = tetrads2[i,3].Genes[j];

				Gene r1 = offspring.Genome.MotherChromosomes[i].Genes[j];
				Gene r2 = offspring.Genome.FatherChromosomes[i].Genes[j];
				

				// Plain strings
				String s11 = g11.GetWholeNameEncoded();
				String s12 = g12.GetWholeNameEncoded();
				String s13 = g13.GetWholeNameEncoded(); 
				String s14 = g14.GetWholeNameEncoded();

				String s21 = g21.GetWholeNameEncoded();
				String s22 = g22.GetWholeNameEncoded();
				String s23 = g23.GetWholeNameEncoded(); 
				String s24 = g24.GetWholeNameEncoded();

				String s1 = r1.GetWholeNameEncoded();
				String s2 = r2.GetWholeNameEncoded();
				
				// Testing where has split occured and marking those lines
				if(j >= tetrads1[i,2].WhereHasBeenSplit){
					s13 = "-" + s13 + "-";
					s14 = "-" + s14 + "-";
				}

				if(j >= tetrads2[i,2].WhereHasBeenSplit){
					s23 = "-" + s23 + "-";
					s24 = "-" + s24 + "-";
				}



				// Formatted strings - for pretty output
				String sf0 = String.Format ("{0,-20}", "#" + j + ": " + MyDictionary.traitDict[i][j]) + d;
				String sf11 = String.Format (f, s11) + d;
				String sf12 = String.Format (f, s12) + d;
				String sf13 = String.Format (f, s13) + d;
				String sf14 = String.Format (f, s14) + d2;

				String sf21 = String.Format (f, s21) + d;
				String sf22 = String.Format (f, s22) + d;
				String sf23 = String.Format (f, s23) + d;
				String sf24 = String.Format (f, s24) + d2;

				String sf1 = String.Format (f, s1) + d;
				String sf2 = String.Format (f, s2) + d;
				file.WriteLine(sf0 + sf11 + sf12 + sf13 + sf14 + sf21 + sf22 + sf23 + sf24 + sf1 + sf2);
			}
			file.WriteLine();
		}
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
