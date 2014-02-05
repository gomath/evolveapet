using System;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
   [Serializable]
	public class Gene
    {
		private readonly EnumTrait _trait; // trait this gene codes for
		private bool _dominance; // 1 = dominant
		private int _chromosomeNum; // on which chromosome the gene is (1st index (i) into geneDict in MyDictionary, index to chromosomeDict in same file)
		private int _geneNum; // which gene on that chromosome gene is (2nd index (j) into geneDict in MyDictionary)
		private int _nameIndex; // 3rd index (k) into geneDict in MyDictionary
			private int _maxNumOfNames; // for generating random gene for mutation and gene therapy
		private int[] _additionalIndices; // any additional information connected to this gene, such as precise colour or further indices into geneDict in MyDictionary
			private int _numOfAdditionalInfo; // how many additional information this gene carries
			private int[] _dominantAdditional; // used for colour, set to true if some additional information is dominant and false otherwise
			private int _maxPossibleNumOfIndices; // for generating random gene for mutation and gene therapy

		// GETTERS AND SETTERS
		public EnumTrait Trait{
			get{ return _trait; }
		}
		
		public int ChromosomeNum{
			get{ return _chromosomeNum; }
		}

		public int GeneNum{
			get{ return _geneNum; }
		}

		public int NameIndex{
			get{ return _nameIndex; }
		}

		public int[] AdditionalIndices{
			get{ return _additionalIndices; }
		}

		public bool IsDominant() {
			return _dominance;
		}

		/// <summary>
		/// Construct random gene that should be logically placed on this chromosome at given location.
		/// </summary>
		/// <param name="chromosomeNum">Chromosome number.</param>
		/// <param name="geneNumber">Gene number.</param>
		public Gene(int chromosomeNum, int geneNumber){
			// Obtaining which chromosome it is and which gene on the chromosome it is
			_chromosomeNum = chromosomeNum;
			_geneNum = geneNumber;

			// Maximum number of different names for this gene
			_maxNumOfNames = (MyDictionary.geneDict [chromosomeNum][geneNumber][0]).GetLength(0);
			// Maximum number of different additional informations for this gene
			_maxPossibleNumOfIndices = (MyDictionary.geneDict [chromosomeNum][geneNumber][1]).GetLength(0);

			// Trait this gene codes for
			_trait = MyDictionary.traitDict[_chromosomeNum][_geneNum];

			// How many additional information is array this gene has
			_numOfAdditionalInfo = MyDictionary.numOfAdditionalInfo [_trait];

			// Generating random gene from options given by Dictionary
			Random rand = new Random ();

			_nameIndex = rand.Next(_maxNumOfNames);
			if (rand.Next (2) == 0) {
				_dominance = false;
			} else {
				_dominance = true;
			}

			// Checking, whether gene has some additional information associated
			if (_numOfAdditionalInfo > 0) { 
				_additionalIndices = new int[_numOfAdditionalInfo];
				_dominantAdditional = new int[_numOfAdditionalInfo];

				if(_trait == EnumTrait.NUMBER){
					// TODO - generate random number in appropriate bounds
					int lowerBound = 0; // inclusive
					int upperBound = 3; // exclusive

					switch(MyDictionary.chromosomeDict[_chromosomeNum]){
					case EnumBodyPart.EYES: // TODO - set upper and lower bounds
						break;
					case EnumBodyPart.ARMS: // TODO - set upper and lower bounds
						break;
					case EnumBodyPart.LEGS: // TODO - set upper and lower bounds
						break;
					}

					// Setting random number in given range
					_additionalIndices[0] = rand.Next(lowerBound,upperBound);
				} else {
					for(int i=0; i<_numOfAdditionalInfo; i++){
						_additionalIndices[i] = rand.Next(_maxPossibleNumOfIndices);
						if(rand.Next(2) == 0){
							_additionalIndices[i] = false;
						} else {
							_additionalIndices[i] = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Returns true if any additional information are associated with this gene.
		/// </summary>
		/// <returns><c>true</c>, if additional was hased, <c>false</c> otherwise.</returns>
		public bool hasAdditional(){
			if (_additionalIndices == null) {
				return false;			
			}
			return true;
		}

		/*
		public void Mutate(Gene gene){
			this._trait = gene._trait;
			this._dominance = gene._dominance;
			this._symbol = gene._symbol;
			this._additional = gene._additional;
		}*/
    }
}
