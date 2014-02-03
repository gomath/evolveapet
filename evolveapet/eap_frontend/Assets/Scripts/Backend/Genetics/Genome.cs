using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace EvolveAPet
{
    public class Genome
    {

        private readonly LinkedList<Chromosome> _motherChromosome, _fatherChromosome; // one for every bodypart

        public LinkedList<Chromosome> MotherChromosome { get { return _motherChromosome; } }

        public LinkedList<Chromosome> FatherChromosome { get { return _fatherChromosome; } }



        public Genome(LinkedList<Chromosome> motherChromosome, LinkedList<Chromosome> fatherChromosome)
        {

            _motherChromosome = motherChromosome;
            _fatherChromosome = fatherChromosome;

        }

        public Genome(string[] serializedGenome)
        {

            // deserializing a genome

        }



        public Genome AutoCrossOver(Genome g)
        {
            // here we have our Algorithm if player does not want to choose
            throw new NotImplementedException("CrossOver");

        }

        public LinkedList<string> CrossOver()
        {

            //this is the partial algorithm for our chromosome breeding if the user wants to choose
            // chromosomes are encoded

            throw new NotImplementedException("ChromosomeSet");
        
        }

        public LinkedList<object> Mutate(Gene gene, BodyPartType index, ChromosomePair cp)
        {

            if (Enum.Equals(cp, ChromosomePair.FATHER))
                _fatherChromosome.ElementAt((int)index).Mutate(gene);
            else
                _motherChromosome.ElementAt((int)index).Mutate(gene);
            return Decode(index);

        }

        public LinkedList<object> Decode(BodyPartType type)
        {
            // this returns a list of enum/int that we can feed in the constructor
            throw new NotImplementedException("Decode Chromosome pair to bodypart");

        }


        public string[] Serialize()
        {

            StringBuilder sm = new StringBuilder();
            foreach (Chromosome chromosome in _motherChromosome)
            {
                sm.Append(chromosome.Serialize());
            }

            StringBuilder sf = new StringBuilder();
            foreach (Chromosome chromosome in _fatherChromosome)
            {
                sf.Append(chromosome.Serialize());
            }

            string[] result = new string[2];
            result[0] = sm.ToString();
            result[1] = sm.ToString();
            return result;
        }


    }
}
