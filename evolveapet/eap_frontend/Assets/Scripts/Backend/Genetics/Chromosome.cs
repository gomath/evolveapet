using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Common;

namespace EvolveAPet
{
    public class Chromosome
    {

        public LinkedList<Gene> Gene { set; get; } // one for every bodypart caracteristic


        public Chromosome(LinkedList<Gene> genes)
        {
            Gene = genes;
        }


        public Chromosome(string serializedChromozome)
        {
            // deserializing a chromosome
        }

        public Gene GetGene(EnumTrait trait)
        {

            foreach (Gene gene in Gene)
            {
                if (Enum.Equals(gene.trait, trait))
                    return gene;
            }
            return null;


        }

        public void Mutate(Gene gene)
        {

            Gene.Find(GetGene(gene.trait)).Value = gene;

        }

        public string Serialize()
        {

            StringBuilder s = new StringBuilder();
            foreach (Gene gene in Gene)
            {
                s.Append(gene.Serialize());
            }

            return s.ToString();
        }



    }
}
