using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
   [Serializable]
	public class Chromosome {

        private EnumBodyPart _bodyPart;
        private Gene[] _genes; // one for every bodypart trait
        private int _numOfGenes; // number of genes on this chromosome
        

		public int splitLocation{ get;set;}//This is needed for the front end, so that you can graphically display where the split in the chromosome has happened. This is useful because it makes
		//it more obvious that the chromosome has been split into two.

        // GETTERS AND SETTERS
        public EnumBodyPart BodyPart{
            get{
                return _bodyPart;
            }
        }

        public Gene[] Genes{
            get{
                return _genes;
            }
        }


		/// <summary>
		/// Creates new chromosome from array of genes and information about the coresponding body part.
		/// </summary>
		/// <param name="_genes">_genes.</param>
		/// <param name="bodyPart">Body part.</param>
        public Chromosome(int chromosomeNum)
        {
           //_bodyPart = 

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
				EnumTrait eTrait = _genes[i].Trait;
				return 0; // TODO - 0 is placeholder here
			}
		}
		
        /// <summary>
        /// Mutate gene at particular location specified by geneNum on this chromosome to Gene.
        /// </summary>
        /// <param name="geneNum"></param>
        /// <param name="gene"></param>
 
        public void Mutate(int geneNum, Gene gene){
		}

        
        public static void Main(String[] args) {
            Console.WriteLine("Hello world!");
            Console.ReadLine();
        }


		public static int getTraitPosition(int traitNo, int bodyPartNo){
				
			//Returns -1 if trait is not found on the gene. Should preferably be static. Simply a dictionary lookup
			return 0;
		}
    }
}
