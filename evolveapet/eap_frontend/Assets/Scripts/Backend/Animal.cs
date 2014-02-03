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
        private readonly int bodyPartNumber = 7;

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
        }
        public void Mutate(int chromosomeNumber, int geneNumber, int genePairNumber, Gene newGene)
        {
            _genome.Mutate(chromosomeNumber, geneNumber, genePairNumber, newGene);
        }
        public void hatch()
        {
            Egg = false;
        }
        private bodyPart createBodyPart(EnumBodyPart e){
            StdBodyPart bPart = new StdBodyPart();
            int traitPos;
            traitPos =  _genome.getTraitIndex(0);// queries the genome for the position of the gene coding for colour
            bPart.Colour = _genome.getTrait(e,traitPos); 
            traitPos =  _genome.getTraitIndex(4); //position of shape trait
            bPart.Shape =_genome.getTrait(e,traitPos);                         
            traitPos =  _genome.getTraitIndex(1);//size
            bPart.Size =_genome.getTrait(e,traitPos); 
            traitPos =  _genome.getTraitIndex(2);//pattern
            bPart.Pattern =;
            switch (e){
                case 1://EYES
                    break;
                case 3://HEAD
                case 4://ARMS
                    break;
                case 5://LEGS
                    break;
            }
            return bPart;
        }
    }
}
