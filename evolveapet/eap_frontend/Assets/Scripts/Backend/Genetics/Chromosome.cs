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


        /// <summary>
        /// Mutate gene at particular location specified by geneNum on this chromosome to Gene.
        /// </summary>
        /// <param name="geneNum"></param>
        /// <param name="gene"></param>
        public static void Mutate(int geneNum, Gene gene)
        {
			if (geneNum < _numOfGenes) {
				gene[geneNum] = gene;
			}       
        }
        
        public static void Main(String[] args) {
            Console.WriteLine("Hello world!");
            Console.ReadLine();
        }
    }
}
