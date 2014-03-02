using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Common;

using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Collections;
using UnityEngine;

namespace EvolveAPet
{
	[Serializable]
    public class Animal
    {
        public int Generation { set; get; }
        public string Name { set; get; }
		public Genome Genome { set; get; }
		public BodyPart[] BodyPartArray { set; get; }
        // each animal will be given a genome and handed both its parents
        public bool Egg { get; set; }
		public Animal[] Parent { set; get;} 

		// Used in Genome screen - do not modify
		public int RemainingGuesses = 5;
		public bool cacheInitialized = false;
		public bool geneTherapyOnShape;
		// Guessing genes caches
		public string[][,] cacheGuessingStrings;
		public string[][] cacheCorrectGuesses;
		// Gene therapy caches
		public string[][,,] cacheRandomMutationNames;
		public Gene[][,,] cacheRandomMutations;
	


        private readonly int bodyPartNumber = Global.NUM_OF_CHROMOSOMES; // to allow easy changing of number of body parts

		/// <summary>
		/// Create a new animal from 2 set of genes and 2 parents.
		/// </summary> 
		/// <param name="chromA">Chrom a.</param>
		/// <param name="chromB">Chrom b.</param>
		/// <param name="parent1">Parent1.</param>
		/// <param name="parent2">Parent2.</param>
		public Animal(Chromosome[] chromA, Chromosome[] chromB, Animal parent1, Animal parent2)
        {
            //Egg = true; //Animals will alwyas be in egg form when created
            //Firstly, populate all the genes in the genome
			Genome = new Genome (chromA, chromB);
            Parent = new Animal[] { parent1, parent2 };

            // New animal has max. generations of your parents + 1
			//Generation = (parent1.Generation > parent2.Generation) ? parent1.Generation+1 : parent2.Generation+1;
			nameMeRandomly ();
			BodyPartArray = new BodyPart[bodyPartNumber]; 
            for (int n = 0; n < bodyPartNumber; n++){
				createBodyPart(n);
			}
			cullGrandparents ();
        }

		public static Animal deserialiseAnimal(string path){
			BinaryFormatter bf = new BinaryFormatter ();
			Animal a;
			FileStream inStream = new FileStream (path, FileMode.Open);
			a = bf.Deserialize(inStream) as Animal;
			return a;
				}

		public void cullGrandparents(){
			if (this.Parent [0] != null) {
							this.Parent [0].Parent [0] = null;
							this.Parent [0].Parent [1] = null;
						}
			if (this.Parent [1] != null) {
								this.Parent [1].Parent [0] = null;
								this.Parent [1].Parent [1] = null;
						}
		}
		public void serialiseAnimal(){
			string newFolder = Environment.CurrentDirectory + "/SavedAnimals";
			Directory.CreateDirectory (newFolder);

			string path = Environment.CurrentDirectory +"/SavedAnimals/" + this.Name + DateTime.Now.ToString("ddMM") + ".animal";
			BinaryFormatter bf = new BinaryFormatter();
			FileStream outStream = new FileStream(path,FileMode.OpenOrCreate);
			bf.Serialize (outStream,this);
			outStream.Close();
				}

		public Animal(){// Generates a completely random animal
			BodyPartArray = new BodyPart[bodyPartNumber]; 
			Generation = 1;
			Genome = new Genome ();
			for (int n=0; n<7; n++) {
				createBodyPart(n);			
			}
			nameMeRandomly ();
		}

       /* public void Mutate(int chromosomeNumber, int geneNumber, int genePairNumber, Gene newGene)
        {
            _genome.Mutate(chromosomeNumber, geneNumber, genePairNumber, newGene);
			BodyPartArray[chromosomeNumber] = createBodyPart(chromosomeNumber);
        }*/

        public void hatch()
        {
            Egg = false;
        }

        public void createBodyPart(int n){
			/* e is an enum pointing to body parts in this order:
			0.Ears
			1.Eyes
			2.Head
			3.Torso
			4.Arms
			5.Legs
			6.Tail
			*/

			//all the possible bits of information
			/*6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			*/
			int[] rgbArray = new int[3];
			int sizeNum = Int16.MinValue;
			int patternNumber = Int16.MinValue;
			int number = Int16.MinValue;
			string shapeStr= "";
			string teethShape = "";
			bool isQuadrupedal= false; //Note, this is not technically a trait
			Chromosome motherChromosome = Genome.MotherChromosomes [n];
			Chromosome fatherChromosome = Genome.FatherChromosomes [n];

			//initialise the relevant bits of information for that chromosome.
			//Decodes Colour
			int genePos; //The position of the gene for the current trait
			genePos = Genome.MotherChromosomes[n].getTraitPosition(0);
			if (genePos != -1) {
				int colour = Genome.GetTrait(n,0);
				// COLOUR = RED << 16 | GREEN << 8 | BLUE
				rgbArray[0] = (colour & 0x00FF0000) >> 16;
				rgbArray[1] = (colour & 0x0000FF00)>>8;
				rgbArray[2] = (colour & 0x000000FF);
			}
			//Decodes Size
			genePos = Genome.MotherChromosomes[n].getTraitPosition(1);
			if (genePos != -1) {
				sizeNum = Genome.GetTrait(n,1);
			}
			//Decodes Pattern
			genePos = Genome.MotherChromosomes[n].getTraitPosition(2);
			if (genePos != -1) {
				patternNumber=Genome.GetTrait(n,2);
			}
			//Decodes Number
			genePos = Genome.MotherChromosomes[n].getTraitPosition(3);
			if (genePos != -1) {
				number = Genome.GetTrait(n,3);

				// handling quadrupedality and 0 arms - quadrupedal animal has ALWAYS 1 arm
				if(n == (int)EnumBodyPart.ARMS){
					if(Genome.IsQuadrupedal()){
						number = 1;
					}
				}
			}

			//Decodes Shape
			genePos = Genome.MotherChromosomes[n].getTraitPosition(4);
			if (genePos != -1) {
				if (geneTherapyOnShape || BodyPartArray[n]==null){
				int shapeNo = Genome.GetTrait(n,4);
				shapeStr = MyDictionary.GetShape(shapeNo);
				}else shapeStr = BodyPartArray[n].shape; 
				geneTherapyOnShape =false;
			}

			//If head, checks teeth shape
			if (n == 2) {
				genePos = Genome.MotherChromosomes[n].getTraitPosition(5);
				
				Gene g1 = motherChromosome.Genes [genePos];
				Gene g2 = fatherChromosome.Genes [genePos];
				string gene1Trait = g1.GetNameEncoded();
				string gene2Trait = g2.GetNameEncoded();
				//Need to fudge the fact that carnivore is dominant over herbivore
				if (gene1Trait.ToLower().Equals("h") && gene2Trait.ToLower().Equals("h")){// both herbivore shaped teeth


					teethShape=MyDictionary.GetShape(1);
				}else{
					teethShape = MyDictionary.GetShape(0);
				}

			}

			if (n == 4) {
								isQuadrupedal = Genome.IsQuadrupedal (); // If body part is arms, checks if it is quadrupedal
						}

			switch (n) {

			case 0: //ears
				BodyPartArray[0] = new Ears(rgbArray, sizeNum, shapeStr, patternNumber);
				break;
			case 1: //eyes
				BodyPartArray[1] = new Eyes(rgbArray,sizeNum,shapeStr,number);
				break;
			case 2: //head
				BodyPartArray[2] = new Head(rgbArray,sizeNum,shapeStr,patternNumber,teethShape);
				break;
			case 3://torso
				BodyPartArray[3] = new Torso(rgbArray, sizeNum, shapeStr,patternNumber);
				break;
			case 4://arms
				BodyPartArray[4] = new Arms(rgbArray,sizeNum,shapeStr,patternNumber,number,isQuadrupedal);
				break;
			case 5://legs
				BodyPartArray[5] = new Legs(rgbArray, sizeNum, shapeStr, patternNumber);
				break;
			case 6://tail
				BodyPartArray[6] = new Tail(rgbArray, sizeNum, shapeStr, patternNumber);
				break;
			}
        }

		/// <summary>
		/// Given an animal, randomly breeds this animal with that given and returns offspring. 
		/// </summary>
		/// <returns>The me randomly.</returns>
		/// <param name="a">The alpha component.</param>
		public Animal BreedMeRandomly(Animal a){
			Chromosome[,] tetrads1 = this.Genome.CreateTetradsForBreeding ();
			Chromosome[,] tetrads2 = a.Genome.CreateTetradsForBreeding ();

			Chromosome[] parent1 = new Chromosome[Global.NUM_OF_CHROMOSOMES];
			Chromosome[] parent2 = new Chromosome[Global.NUM_OF_CHROMOSOMES];

			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				int j = Global.rand.Next(4);
				parent1[i] = tetrads1[i,j];

				j = Global.rand.Next(4);
				parent2[i] = tetrads2[i,j];
			}

			Animal offspring = new Animal(parent1, parent2, this, a);
			return offspring;
		}

		public void nameMeRandomly() {
			int pr = Global.rand.Next (12);
			int mi = Global.rand.Next (13);
			int po = Global.rand.Next (12);
			string pre = Global.prefixes [pr];
			string mid = Global.names [mi];
			string post = Global.suffixes [po];
			Name = (pre + mid + post);
			if (Name.Length>16){
				Name = (mid + post);
			}

		}
	}
	
}