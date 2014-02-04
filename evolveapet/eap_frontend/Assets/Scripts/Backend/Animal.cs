using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Common;
using UnityEngine;

namespace EvolveAPet
{
    public class Animal : MonoBehaviour
    {
        private readonly Animal[] _parents;
        private readonly Genome _genome;
        public int Generation { set; get; }
        public string Name { set; get; }
        public Genome Genome { get { return _genome; } }
		public Animal[] Parent { get { return _parents; } }
        public BodyPart[] BodyPartArray { get; private set; }
        // each animal will be given a genome and handed both its parents
        public bool Egg { get; set; }
        private readonly int bodyPartNumber = 7; // to allow easy changing of number of body parts
		private readonly int traitNumber = 6; // to allow easy changing of number of traits

		public Animal(LinkedList<Chromosome> chromA, LinkedList<Chromosome> chromB, Animal parent1, Animal parent2)
        {
            Egg = true; //Animals will alwyas be in egg form when created
            //Firstly, populate all the genes in the genome
            _genome = new Genome(chromA, chromB);
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
        /*public void Mutate(int chromosomeNumber, int geneNumber, int genePairNumber, Gene newGene)
        {
            _genome.Mutate(chromosomeNumber, geneNumber, genePairNumber, newGene);
        }*/
        public void hatch()
        {
            Egg = false;
        }
        /*private BodyPart createBodyPart(EnumBodyPart e){
			/* e is an enum pointing to body parts in this order:
			0.Ears
			1.Eyes
			2.Head
			3.Torso
			4.Arms
			5.Legs
			6.Tail
			*/
            //StdBodyPart bPart = new StdBodyPart();
            //int[] traitPos = new int[6]; 
			/*An array with all the 6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			Traits not used for that specific body part are simply represented as null.
			*/
			/*for (int n =0; n< traitNumber;n++){
			traitPos[n] = _genome.getTraitIndex(n);
			}
            bPart.Colour = _genome.getTrait(e,traitPos[0]); 
            bPart.Shape =_genome.getTrait(e,traitPos[4]);                         
            bPart.Size =_genome.getTrait(e,traitPos[1]); 
            bPart.Pattern =_genome.getTrait(e,traitPos[2]);
            if (e==2){ //body part is head, need to deal with teeth
				(Head)bPart.TeethShape = _genome.getTrait(e,traitPos[5]);
			}
			if (e ==1 || e ==4 || e==5){ //these are all FullBodyParts (eyes, arms, legs in that order)
				(FullBodyPart)bPart.Number = _genome.getTrait(e,traitPos[3]);
			}
            return bPart;
        }*/
    }
}
