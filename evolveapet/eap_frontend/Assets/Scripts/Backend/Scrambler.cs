using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace EvolveAPet
{
	public class Scrambler{
		public static Dictionary<Locus,Locus> Scramble(){
			MyDictionary.Init();

			// Creating picture of the current genome
			Locus[][] genome = new Locus[Global.NUM_OF_CHROMOSOMES][];
			List<Locus> linearGenome = new List<Locus> ();
			int totalGenes = 0;

			// Creating linear genome
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				int n = MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i];
				for(int j=0; j<n; j++){
					linearGenome.Add(new Locus(i,j));
					totalGenes++;
				}
			}

			// Random scrambling
			int numOfSwaps = 1000;
			for(int i=0; i<numOfSwaps; i++){
				int a = Global.rand.Next(totalGenes);
				int b = Global.rand.Next(totalGenes);

				// swap two random genes
				Locus temp = linearGenome[a];
				linearGenome[a] = linearGenome[b];
				linearGenome[b] = temp;

			}

			// Creating full genome of Loci that are swapped -> creating bijectional Map from old to new positions
			Dictionary<Locus,Locus> map = new Dictionary<Locus, Locus> (new LocusEqualityComparer());
			int k = 0;
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				int n = MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i];
				genome[i] = new Locus[n];
				for(int j=0; j<n; j++){
					map.Add(new Locus(i,j), linearGenome[k++]);
				}
			}

			return map;
		}

		/*
		public static void TestScramblerCreateMap(int testCase){
			string path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Scrambler\\";
			string dst = "Scrambler_" + testCase + ".txt";
			
			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;
			//file.WriteLine ("Hello world.");

			Dictionary<Locus,Locus> map = Scramble ();
			Dictionary<Locus,Locus>.KeyCollection keys = map.Keys;

			foreach(Locus key in keys){
				Locus value = map[key];
				file.WriteLine(key.Chromosome + " " + key.GeneNumber + ", " + value.Chromosome +" " + value.GeneNumber);
			}
			SerializeMe(map,Global.mapName);

			file.Close();
		}
		*/

		public static void TestScramblerUseMap(int testCase, Dictionary<Locus,Locus> map){
			string path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Scrambler\\";
			string dst = "Scrambler_Loaded_" + testCase + ".txt";
			
			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;

			Dictionary<Locus,Locus>.KeyCollection keys = map.Keys;
			
			foreach(Locus key in keys){
				Locus value = map[key];
				file.WriteLine(key.Chromosome + " " + key.GeneNumber + ", " + value.Chromosome +" " + value.GeneNumber);
			}

			file.Close();
		}

		public static void SerializeMe(Dictionary<Locus,Locus> map, String path){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream outStream = new FileStream(path,FileMode.OpenOrCreate);
			bf.Serialize (outStream,map);

			outStream.Close();

		}


	}
}

