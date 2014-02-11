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

			BodyPartArray = new BodyPart[bodyPartNumber]; 
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
			string teethShape = "";
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
			if (genePos != -1) {
				sizeNum = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}
			//Decodes Pattern
			genePos = Genome.MotherChromosomes[n].getTraitPosition(2);
			if (genePos != -1) {
				patternNumber = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}
			//Decodes Number
			genePos = Genome.MotherChromosomes[n].getTraitPosition(3);
			if (genePos != -1) {
				number = Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
			}

			//Decodes Shape
			genePos = Genome.MotherChromosomes[n].getTraitPosition(4);
			if (genePos != -1) {
				int shapeNo =Genome.DecodeTrait(motherChromosome.Genes[genePos],fatherChromosome.Genes[genePos]);
				shapeStr = MyDictionary.GetShape(shapeNo);
			}

			//If head, checks teeth shape
			if (n == 2) {
			genePos = motherChromosome.getTraitPosition(5);
				int teethShapeInt = Genome.DecodeTrait(motherChromosome.Genes[genePos], fatherChromosome.Genes[genePos]);
				teethShape = MyDictionary.GetShape(teethShapeInt);
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

		public void Build() {
			
			//Find transforms
			Transform tBody = GameObject.FindGameObjectWithTag("Body").transform;
			Transform tHead = GameObject.FindGameObjectWithTag("Head").transform;
			Transform tTail = GameObject.FindGameObjectWithTag("Tail").transform;
			Transform tFrontLeg = GameObject.FindGameObjectWithTag("Front Leg").transform;
			Transform tBackLeg = GameObject.FindGameObjectWithTag("Back Leg").transform;
			Transform tFrontArm = GameObject.FindGameObjectWithTag("Front Arm").transform;
			Transform tBackArm = GameObject.FindGameObjectWithTag("Back Arm").transform;
			Transform[] tEyes = {GameObject.FindGameObjectWithTag("Eye 0").transform, GameObject.FindGameObjectWithTag("Eye 1").transform, GameObject.FindGameObjectWithTag("Eye 2").transform};
			Transform tFrontEar = GameObject.FindGameObjectWithTag("Front Ear").transform;
			Transform tBackEar = GameObject.FindGameObjectWithTag("Back Ear").transform;
			
			//Build body, rotate if bipedal and scale/colour as needed
			Torso t = (Torso)BodyPartArray[3];
			GameObject body = (GameObject)Instantiate(Resources.Load ("Prefabs/"+t.shape+" body"));
			body.GetComponent<SpriteRenderer>().color = DecodeCol(t.colour);
			if (!((Arms)BodyPartArray[4]).isQuadrupedal) {
				body.transform.Rotate(new Vector3(0,0,-30));
			}
			body.transform.parent = tBody;
			Scale(tBody, t.size);
			
			//build head, color eyes, choose mouth and scale/colour as needed
			Head h = (Head)BodyPartArray[2];
			GameObject head = (GameObject)Instantiate(Resources.Load ("Prefabs/"+h.shape+" head"));
			head.GetComponent<SpriteRenderer>().color = DecodeCol (h.colour);
			head.transform.position = tHead.position = GameObject.Find ("head joint").transform.position;
			head.transform.parent = tHead;

			//build eyes
			Eyes e = (Eyes)BodyPartArray[1];
			for (int i = 0; i<e.number; i++) {
				GameObject eye = (GameObject)Instantiate (Resources.Load ("Prefabs/DINO eye"));
				eye.transform.position = tEyes[i].position = GameObject.Find("eye joint "+i).transform.position;
				eye.transform.parent = tEyes[i];
				Scale (tEyes[i], e.size);
				eye.GetComponent<SpriteRenderer>().color = DecodeCol (e.colour);
			}

			//build ears
			/*Ears ea = (Ears)BodyPartArray[0];
			GameObject ear = (GameObject)Instantiate(Resources.Load ("Prefabs/"+ea.shape+" front ear"));
			ear.GetComponent<SpriteRenderer>().color = DecodeCol (ea.colour);
			ear.transform.position = tFrontEar.position = GameObject.Find ("ear joint").transform.position;
			ear.transform.parent = tFrontEar;
			Scale(tFrontEar, ea.size);

			ear = (GameObject)Instantiate(Resources.Load ("Prefabs/"+ea.shape+" back ear"));
			ear.GetComponent<SpriteRenderer>().color = DecodeCol (ea.colour);
			ear.transform.position = tBackEar.position = GameObject.Find ("ear joint").transform.position;
			ear.transform.parent = tBackEar;
			Scale(tBackEar, ea.size);*/
			
			Scale(tHead, h.size);

			//build mouth
			GameObject.Find ("mouth").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/"+h.shape+" "+h.teethShape+" mouth");
			
			//build tail, scale/colour as needed
			Tail ta = (Tail)BodyPartArray[6];
			GameObject tail = (GameObject)Instantiate(Resources.Load ("Prefabs/"+ta.shape+" tail"));
			tail.GetComponent<SpriteRenderer>().color = DecodeCol (ta.colour);
			tail.transform.position = tTail.position = GameObject.Find ("tail joint").transform.position;
			tail.transform.parent = tTail;
			Scale(tTail, ta.size);
			
			//build legs, scale/colour as needed
			Legs l = (Legs)BodyPartArray[5];
			GameObject leg = (GameObject)Instantiate(Resources.Load ("Prefabs/"+l.shape+" front leg"));
			leg.GetComponent<SpriteRenderer>().color = DecodeCol(l.colour);
			leg.transform.position = tFrontLeg.position = GameObject.Find ("leg joint").transform.position;
			leg.transform.parent = tFrontLeg;
			Scale(tFrontLeg, l.size);
			
			leg = (GameObject)Instantiate(Resources.Load ("Prefabs/"+l.shape+" back leg"));
			leg.GetComponent<SpriteRenderer>().color = DecodeCol(l.colour);
			leg.transform.position = tBackLeg.position = GameObject.Find ("leg joint").transform.position;
			leg.transform.parent = tBackLeg;
			Scale(tBackLeg, l.size);

			//build arms, replace with legs if bipedal and scale/colour as needed
			Arms a = (Arms)BodyPartArray[4];
			string limb = a.isQuadrupedal ? "leg" : "arm";
			
			if (a.number != 0) {
				GameObject arm = (GameObject)Instantiate(Resources.Load ("Prefabs/"+a.shape+" front "+limb));
				arm.GetComponent<SpriteRenderer>().color = DecodeCol(a.colour);
				arm.transform.position = tFrontArm.position = GameObject.Find ("arm joint").transform.position;
				arm.transform.parent = tFrontArm;
				if (a.isQuadrupedal) {
					Scale(tFrontArm, l.size);
				} else {
					Scale(tFrontArm, a.size);
				}
				
				arm = (GameObject)Instantiate(Resources.Load ("Prefabs/"+a.shape+" front "+limb));
				arm.GetComponent<SpriteRenderer>().color = DecodeCol (a.colour);
				arm.transform.position = tBackArm.position = GameObject.Find ("arm joint").transform.position;
				arm.transform.parent = tBackArm;
				if (a.isQuadrupedal) {
					Scale(tBackArm, l.size);
				} else {
					Scale(tBackArm, a.size);
				}
			}
			
		}

		float[] sizes = {0.9f, 1.0f, 1.1f};
		
		//Parse the scale ints and scale accordingly
		void Scale (Transform t, int size) {
			t.localScale = new Vector3(sizes[size],sizes[size], 1);
		}

		Color DecodeCol (int[] c) {
			return new Color(c[0]/255f, c[1]/255f, c[2]/255f);
		}
    }
}
