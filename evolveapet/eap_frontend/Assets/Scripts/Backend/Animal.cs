using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Common;
using System.Collections;
using UnityEngine;

namespace EvolveAPet
{
    public class Animal : MonoBehaviour
    {
        private readonly Animal[] _parents;
        public int Generation { set; get; }
        public string Name { set; get; }
		public Genome Genome { set; get; }
		public Animal[] Parent { get { return _parents; } }
        public BodyPart[] BodyPartArray { get; private set; }
        // each animal will be given a genome and handed both its parents
        public bool Egg { get; set; }
        private readonly int bodyPartNumber = Global.NUM_OF_CHROMOSOMES; // to allow easy changing of number of body parts

		public Animal(Chromosome[] chromA, Chromosome[] chromB, Animal parent1, Animal parent2)
        {
            Egg = true; //Animals will alwyas be in egg form when created
            //Firstly, populate all the genes in the genome
            Genome = new Genome(chromA, chromB);
            _parents = new Animal[] { parent1, parent2 };
            BodyPartArray = new BodyPart[bodyPartNumber]; 
            for (int n = 0; n < bodyPartNumber; n++)
            {
           //BodyPartArray[n] = createBodyPart((EnumBodyPart)n);
            }
			/*if (BodyPartArray[4].Number ==2 && BodyPartArray[5].Number==4){ // if the creature has 2 arms and 4 legs
				throw new TooManyLimbsException("Quadrupedal animal with arms being created");
			}*/
        }

		public Animal(){// Generates a completely random animal
			
			Chromosome[] chromosomeArrayA = new Chromosome[bodyPartNumber];
			Chromosome[] chromosomeArrayB = new Chromosome[bodyPartNumber];
			int genePos;
			Gene newGene; // mother and father genes
			/*6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			Traits not used for that specific body part are generated but not used
			
			7 body parts in this order:
			0.Ears
			1.Eyes
			2.Head
			3.Torso
			4.Arms
			5.Legs
			6.Tail
			*/

			for (int n = 0; n<bodyPartNumber;n++){// iterate through body parts
				Chromosome currentChromosomeA = new Chromosome (n); // creates a new chromosome for each body part
				Chromosome currentChromosomeB = new Chromosome (n); // creates a new chromosome for each body part




				for (int t=0;t<6;t++){ // iterate through all traits

						genePos = currentChromosomeA.getTraitPosition(t); // finds where the gene coding for trait t and body part n is located on the gene
						//Use random constructor of Gene to generate two random genes.
				
						if (genePos !=-1){
							newGene = new Gene(n,genePos);
							currentChromosomeA.Genes[genePos] = newGene;
							newGene = new Gene(n,genePos);
							currentChromosomeB.Genes[genePos] = newGene;
						}						

					}
				chromosomeArrayA[n] = currentChromosomeA;
				chromosomeArrayB[n] = currentChromosomeA;


				}
			Genome = new Genome (chromosomeArrayA, chromosomeArrayB);
			for (int n=0; n<7; n++) {
				createBodyPart(n);			
			}

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
        private void createBodyPart(int n){
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
			bool isCarnivore = false;
			bool isQuadrupedal= false; //Note, this is not technically a trait
			Chromosome motherChromosome = Genome.MotherChromosomes [n];
			Chromosome fatherChromosome = Genome.MotherChromosomes [n];

			//initialise the relevant bits of information for that chromosome.
			//Decodes Colour
			int genePos; //The position of the gene for the current trait
			genePos = Genome.MotherChromosomes[n].getTraitPosition(0);
			if (genePos != 1) {
				int colour =Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
				// COLOUR = RED << 16 | GREEN << 8 | BLUE
				rgbArray[0] = (colour & 0x00FF0000) >> 16;
				rgbArray[1] = (colour & 0x0000FF00)>>8;
				rgbArray[2] = (colour & 0x000000FF);
			}
			//Decodes Size
			genePos = Genome.MotherChromosomes[n].getTraitPosition(1);
			if (genePos != 1) {
				sizeNum = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}
			//Decodes Pattern
			genePos = Genome.MotherChromosomes[n].getTraitPosition(2);
			if (genePos != 1) {
				patternNumber = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}
			//Decodes Number
			genePos = Genome.MotherChromosomes[n].getTraitPosition(3);
			if (genePos != 1) {
				number = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}

			//Decodes Shape
			genePos = Genome.MotherChromosomes[n].getTraitPosition(4);
			if (genePos != 1) {
				int shapeNo =Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
				shapeStr = MyDictionary.GetShape(shapeNo);
			}

			//If head, checks teeth shape
			if (n == 2) {
			genePos = motherChromosome.getTraitPosition(5);
				if(Genome.DecodeTrait(motherChromosome.Genes[genePos], fatherChromosome.Genes[genePos]) == 1) {
					isCarnivore =true;
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
				BodyPartArray[2] = new Head(rgbArray,sizeNum,shapeStr,patternNumber,isCarnivore);
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

		/*
		 * Below is Unity specific code
		 * Author: Tom Lefley
		 * 
		 */

		private Vector2 lastPos;
		private float delta;
		private bool touched;

		void Start() {
			StartCoroutine("twitch");
			StartCoroutine("blink");
		}

		void Update() {
			tickle ();
		}

		void setTouched(bool b) {
			touched = b;
		}

		void tickle() {
			if ( Input.GetMouseButtonDown(0) ) {
				lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			} else if ( Input.GetMouseButton(0) ) {
				delta += Mathf.Abs(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition),lastPos));
				lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			} else if ( Input.GetMouseButtonUp(0) ) {
				delta = 0f;
				touched = false;
			}

			if ((delta > 20f) && touched) {
				GetComponent<Animator>().SetTrigger("Tickle");
				delta = 0f;
				touched = false;
			}
		}

		IEnumerator twitch() {
			for(;;) {
				GetComponent<Animator>().SetTrigger("Twitch");
				yield return new WaitForSeconds(UnityEngine.Random.Range (10f, 20f));
			}	
		}

		IEnumerator blink() {
			for(;;) {
				GetComponent<Animator>().SetTrigger("Blink");
				yield return new WaitForSeconds(UnityEngine.Random.Range (2f, 5f));
			}	
		}
    }
}
