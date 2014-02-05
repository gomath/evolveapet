using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
   [Serializable]
	public class Chromosome {

        private EnumBodyPart _bodyPart;
        private Gene[] _gene { set; get; } // one for every bodypart trait
        private int _numOfGenes; // number of genes on this chromosome
        

		/// <summary>
		/// Creates new chromosome from array of genes and information about the coresponding body part.
		/// </summary>
		/// <param name="_genes">_genes.</param>
		/// <param name="bodyPart">Body part.</param>
        public Chromosome(Gene[] gene, EnumBodyPart bodyPart)
        {
            _gene = gene;
            _numOfGenes = _gene.Length;
            _bodyPart = bodyPart;
        }

		/// <summary>
		/// Gets the index in the EnumTrait of trait located on ith place on this chromosome.
		/// </summary>
		/// <returns>The trait index.</returns>
		/// <param name="gene">Gene.</param>
		public int GetTraitIndex(int i){
			if (i >= _numOfGenes) {
				return -1;
			} else {
				EnumTrait eTrait = _gene[i].Trait;
			}
		}

<<<<<<< HEAD
=======
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
>>>>>>> a810a69262c5608e92e523304919b285f62b00ca

        /// <summary>
        /// Mutate gene at particular location specified by geneNum on this chromosome to Gene.
        /// </summary>
        /// <param name="geneNum"></param>
        /// <param name="gene"></param>
        public static void Mutate(int geneNum, Gene gene)
        {
<<<<<<< HEAD
			if (geneNum < _numOfGenes) {
				gene[geneNum] = gene;
			}       
=======

            Gene.Find(GetGene(gene.trait)).Value = gene;

>>>>>>> a810a69262c5608e92e523304919b285f62b00ca
        }
        
        public static void Main(String[] args) {
            Console.WriteLine("Hello world!");
            Console.ReadLine();
        }
<<<<<<< HEAD
=======

		public static int getTraitPosition(int traitNo, int bodyPartNo){
		//NEED A METHOD TO WORK OUT WHERE THE GENE FOR A TRAIT IS LOCATED SINCE WE ARE PLACING THEM ACROSS THE GENE IN DIFFERENT POSITIONS - essentially lookup table.

			//RETURNS NULL IF TRAIT IS NOT FOUND!
			return 0;
		}

>>>>>>> 7a6120e0d0db09942d56765a977510939b880a5c
    }
}
