using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace EvolveAPet
{
    public class Animal
    {
        private readonly Animal[] _parents;
        private readonly Genome _genome;
        public int Generation { set; get; }
        public string Name { set; get; }
        public Genome Genome { get { return _genome; } }
        public Animal[] Parent { get { return _parent; } }
        public BodyPart[] BodyPartArray { get; private set; }
        // each animal will be given a genome and handed both its parents
        public bool Egg { get; set; }
        private readonly int bodyPartNumber = 7; // to allow easy changing of number of body parts
		private readonly trait traitNumber = 6; // to allow easy changing of number of traits
        public Animal(Chromosome[] chromA, Chomosome[] chromB, Animal parent1, Animal parent2)
        {
            Egg = true; //Animals will alwyas be in egg form when created
            //Firstly, populate all the genes in the genome
            _genome = new Genome(chromA, chromB);
            Parent = new Animal[] { parent1, parent2 };
            BodyPartArray = new BodyPart[bodyPartNumber]; 
            for (int n = 0; n < bodyPartNumber; n++)
            {
           BodyPartArray[n] = createBodyPart(n);
            }
			if (BodyPartArray[4].Number ==2 && BodyPartArray[5].Number==4){ // if the creature has 2 arms and 4 legs
				throw new TooManyLimbsException("Quadrupedal animal with arms being created");
			}
        }
		public Animal(){// Generates a completely random animal
			Random rnd = new Random();
			int randomNumber;
			bool isDominant;
			int genePos;
			Chromosome[] chromA = new Chromosome[bodyPartNumber];
			Chromosome[] chromB = new Chromosome[bodyPartNumber];
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

		//Generate Pattern for all Body Parts except eyes, and generate shape and size for everything
			int maxNumberInEnum;
			for (int n = 0; n<7;n++){// iterate through body parts
				for (int t=1;t<5;t++){ // iterate through all traits excluding teethShape and colour. colour needs to be treated separately

					//Need to work out the maximum number of enums for each trait, this is clunky but needed.
					switch(t){
						
						case t==1:
							maxNumberInEnum=2; // 0 to 3
							break;
						case t==2:
							maxNumberInEnum = EnumPattern.GetValues.Max(); //WARNING NOT SURE HOW THE ENUMS ARE GOING TO BE IMPLEMENTED
							break;
					case t==3:
						maxNumberInEnum=2;
						break;
					case t==4:
						maxNumberInEnum = EnumShape.GetValues.Max();//WARNING NOT SURE HOW THIS ENUM IS GOING TO BE IMPLEMENTED
					}	

					if ((n!=1 && t!= 2) && t!=3){ //ignores eyes with pattern and ignores number

						genePos = Chromosome.getTraitPosition(t,n); // finds where the gene coding for trait t and body part n is located on the gene


						randomNumber = rnd.Next(0, ((int)maxNumberInEnum+1)); // generates a value between 0 and the maximum size of the enum
						isDominant = flipCoin(); // generates true or false
						//WARNING: STRING FOR GENE CONSTRUCTOR IS NOT WELL DEFINED
						newGene = new Gene(isDominant, t, randomNumber);
						chromA[genePos] = newGene;


						randomNumber = rnd.Next(0, ((int)maxNumberInEnum+1)); // generates a value between 0 and the maximum size of the enum
						isDominant = flipCoin();
						newGene	= new Gene(isDominant,t,randomNumber);
						chromB[genePos] = newGene;
					}
				}
			}

		//Generate Colour for all body parts - needs to be treated separately


		//Generate Teeth Shape for head
			bool veg = flipCoin ();

		//Generate Number of Arms and Legs

		//Generate Number of Eyes

		
		
		}

		private bool flipCoin(){ // to reduce code needed in random animal
			Random rnd = new Random();
			int result;
			result = rnd.Next (0, 2);
			if (result == 0) return false;
			else return true;
		}

        public void Mutate(int chromosomeNumber, int geneNumber, int genePairNumber, Gene newGene)
        {
            _genome.Mutate(chromosomeNumber, geneNumber, genePairNumber, newGene);
			BodyPartArray[chromosomeNumber] = createBodyPart(chromosomeNumber);
        }
        public void hatch()
        {
            Egg = false;
        }
        private bodyPart createBodyPart(EnumBodyPart e){
			/* e is an enum pointing to body parts in this order:
			0.Ears
			1.Eyes
			2.Head
			3.Torso
			4.Arms
			5.Legs
			6.Tail
			*/
            StdBodyPart bPart = new StdBodyPart();
            int[] traitPos = new int[6]; 
			/*An array with all the 6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			Traits not used for that specific body part are simply represented as null.
			*/
			for (int n =0; n< traitNumber;n++){
			traitPos[n] = _genome.getTraitIndex(n);
			}
            bPart.Colour = _genome.getTrait(e,traitPos[0]); 
            bPart.Shape =_genome.getTrait(e,traitPos[4]);                         
            bPart.Size =_genome.getTrait(e,traitPos[1]); 
            if (e!=1) bPart.Pattern =_genome.getTrait(e,traitPos[2]); // eyes don't have a pattern
            if (e==2){ //body part is head, need to deal with teeth
				(Head)bPart.TeethShape = _genome.getTrait(e,traitPos[5]);
			}
			if (e ==1 || e ==4 || e==5){ //these are all FullBodyParts (eyes, arms, legs in that order)
				(FullBodyPart)bPart.Number = _genome.getTrait(e,traitPos[3]);
			}
            return bPart;
        }
		

    }
}
