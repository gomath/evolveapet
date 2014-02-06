using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
    [Serializable]
	public class Genome
    {
		// elements at index i of both arrays together logically codes for one trait
		private Chromosome[] _motherChromosome { set; get; }
		private Chromosome[] _fatherChromosome { set; get; }
		private int _numOfChromosomes;
		
        /// <summary>
        /// Given two haploid sets of chromosomes, create a full genome.
        /// </summary>
        /// <param name="mother"></param>
        /// <param name="father"></param>
        public Genome(Chromosome[] mother, Chromosome[] father) {
			_motherChromosome = mother;
			_fatherChromosome = father;
			_numOfChromosomes = mother.Length;
        }

		/// <summary>
		/// Choose random part of each chromosome (same locations on each of them) and swap those parts. 
		/// Return array of 4 chromosome, 2 being the original chromosome passed as arguments and the rest 
		/// being created from both mother and father chromosome by cross-over.
		/// </summary>
		/// <returns>Chromosome[4]</returns>
		/// <param name="c1">C1.</param>
		/// <param name="c2">C2.</param>
        private Chromosome[] CrossOver(Chromosome c1, Chromosome c2){
			return null;
		}

		/// <summary>
		/// Returns the 2D array of chromosome Chromosome[i][j]. Index i specifies the chromosome (i.e. the body part) and the
		/// second index j (1 <= j <=4) specifies the actual chromosome withing the tetrad.
		/// </summary>
		/// <returns>The tetrads for breeding.</returns>
		public Chromosome[] CreateTetradsForBreeding(){
			return null;
		}

		/// <summary>
		/// Given two genes, it returns the index into appropriate enumeration corresponding to the trait these two genes
		/// are coding for. In case of colour, it returns the color RGY (Red, Green, Yellow) as a single integer.
		/// </summary>
		/// <returns>The trait.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		public int DecodeTrait(Gene g1, Gene g2){
			return 0;
		}

        /// <summary>
        /// Given chromosome number and the gene number on that chromosome, returns the index into appropriate enum corresponding to that trait
        /// of colour information as a single integer
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="trait"></param>
        /// <returns></returns>
        public int GetTrait(int chromosome, int trait) { 
			return 0;
        }
		
        /// <summary>
        /// Returns index to the EnumTrait for the particular gene on the given chromosome.
        /// </summary>
        /// <param name="chromose"></param>
        /// <param name="trait"></param>
        /// <returns></returns>

        /*public int GetTraitIndex(int chromosome, int gene) { 
>>>>>>> 922712992245d00a890e6e7e4807b4d13e578647
			if (chromosome >= _numOfChromosomes) {
				return -1;			
			} else{
				Chromosome temp = _fatherChromosome[chromosome];
<<<<<<< HEAD
			}
			return -1;
		}

=======

				//for(int i=0; if<temp.nu)
			}
		}*/
        /*public LinkedList<object> Mutate(Gene gene, BodyPartType index, ChromosomePair cp)
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
>>>>>>> 922712992245d00a890e6e7e4807b4d13e578647


		/// <summary>
		/// Replace given gene on given chromosome of mother (sex = 0) or father (sex = 1) by given gene.
		/// </summary>
		/// <param name="chromosomeNum">Chromosome number.</param>
		/// <param name="geneNum">Gene number.</param>
		/// <param name="sex">Sex.</param>
		/// <param name="gene">Gene.</param>
		public void Mutate(int chromosomeNum, int geneNum, int sex, Gene gene){

		}

        
    }
}*/
	}
}
