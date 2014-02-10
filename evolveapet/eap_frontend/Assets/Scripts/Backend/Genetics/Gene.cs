using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EvolveAPet
{
    /// <summary>
    /// Gene class - 
	/// Functionality so far
	/// 	1. Give it chromosome number and gene on that chromosome into Constructor, and appropriate random gene is constructed.
	/// 	2. Alter Path variable in display function and nice formatted gene is written into the specified file.
    /// </summary>
	[Serializable]
	public class Gene
    {
		private readonly EnumTrait _trait; // trait this gene codes for
		private bool _dominant; // 1 = dominant
		private int _chromosomeNum; // on which chromosome the gene is (1st index (i) into geneDict in MyDictionary, index to chromosomeDict in same file)
		private int _geneNum; // which gene on that chromosome gene is (2nd index (j) into geneDict in MyDictionary)
		private int _nameIndex; // 3rd index (k) into geneDict in MyDictionary
			private int _maxNumOfNames; // for generating random gene for mutation and gene therapy
		private int[] _additionalIndices; // any additional information connected to this gene, such as precise colour or further indices into geneDict in MyDictionary
			private int _numOfAdditionalInfo; // how many additional information this gene carries
			private bool[] _dominantAdditional; // used for colour, set to true if some additional information is dominant and false otherwise
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

		public bool[] DominantAdditional{
			get{ return _dominantAdditional; }
		}

		public bool IsDominant() {
			return _dominant;
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
			if (MyDictionary.geneDict [chromosomeNum] [geneNumber].GetLength (0) > 1) {
				_maxPossibleNumOfIndices = (MyDictionary.geneDict [chromosomeNum] [geneNumber] [1]).GetLength (0);
			} else {
				_maxPossibleNumOfIndices = 0;	
			}

			// Trait this gene codes for
			_trait = MyDictionary.traitDict[_chromosomeNum][_geneNum];

			// How many additional information in array this gene has
			_numOfAdditionalInfo = MyDictionary.numOfAdditionalInfo [_trait];

			// Generating random gene from options given by Dictionary
			System.Random rand = Global.rand;

			_nameIndex = rand.Next(_maxNumOfNames);
			if (rand.Next (2) == 0) {
				_dominant = false;
			} else {
				_dominant = true;
			}

			// Special treatement of genes coding for pattern
			if (_trait == EnumTrait.PATTERN) {
				if(_nameIndex == 0){
					_dominant = false; // blank pattern
				} else { 
					_dominant = true; // either dots or stripes pattern
				}
			}

			// Checking, whether gene has some additional information associated
			if (_numOfAdditionalInfo > 0) { 
				_additionalIndices = new int[_numOfAdditionalInfo];
				_dominantAdditional = new bool[_numOfAdditionalInfo];

				if(_trait == EnumTrait.NUMBER){
					switch(MyDictionary.chromosomeDict[_chromosomeNum]){
					case EnumBodyPart.EYES: // An animal can have between 1 and 3 eyes
						_additionalIndices[0] = (rand.Next(Global.EYES_LOWER_BOUND,Global.EYES_UPPER_BOUND));
						break;
					case EnumBodyPart.ARMS:
						_additionalIndices[0] = (rand.Next(Global.ARMS_LOWER_BOUND,Global.ARMS_UPPER_BOUND));
						break;
					}
				} else {
					for(int i=0; i<_numOfAdditionalInfo; i++){
						_additionalIndices[i] = rand.Next(_maxPossibleNumOfIndices);
						if(rand.Next(2) == 0){
							_dominantAdditional[i] = false;
						} else {
							_dominantAdditional[i] = true;
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

		// TODO - THIS METHOD (RANDOM MUTATIONS) IS UNTESTED AT ALL, TEST IT
		/// <summary>
		/// Returns n possible distinct random mutation of this gene.
		/// </summary>
		/// <returns>The mutations.</returns>
		/// <param name="n">N.</param>
		public Gene[] RandomMutations(int n){
			Gene[] res = new Gene[n];
			for (int i=0; i<n; i++) {

				// Recreating gene res[i] until it's not distinct from all genes in res[0..i-1]
				bool recreate;
				do{
					recreate = false;
					res[i] = new Gene(_chromosomeNum,_geneNum);
					// Checking whether this new gene is distinct to all previous genes in res array
					for(int j=0; j<i; j++){
						if(res[i].Equals(res[j])){
							recreate = true;
							break;
						}
					}
				} while(recreate);
			}
			return res;
		}

		/// <summary>
		/// Returns some number of random mutattion, appropraite to the trait this genes code for.
		/// </summary>
		/// <returns>The mutations.</returns>
		public Gene[] RandomMutations(){
			switch (_trait) {
			case EnumTrait.COLOUR: 		return RandomMutations(5); 
			case EnumTrait.NUMBER:  	if((EnumBodyPart)_chromosomeNum == EnumBodyPart.EYES){
											return RandomMutations(3); /* eyes */ 
										} else {
											return RandomMutations(2); /* arms */
										}
			case EnumTrait.PATTERN: 	return RandomMutations(2);
			case EnumTrait.SHAPE:		return RandomMutations(3);
			case EnumTrait.SIZE:		return RandomMutations(2);
			case EnumTrait.TEETH_SHAPE:	return RandomMutations(3);
			}
			return null;
		}

		/// <summary>
		/// Overriden operator for determining equality of two genes.
		/// </summary>
		/// <param name="g">The <see cref="EvolveAPet.Gene"/> to compare with the current <see cref="EvolveAPet.Gene"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="EvolveAPet.Gene"/> is equal to the current <see cref="EvolveAPet.Gene"/>;
		/// otherwise, <c>false</c>.</returns>
		public bool Equals (Gene g)
		{
			if(		_trait == g._trait
			   &&	_dominant == g._dominant
			   &&	_chromosomeNum == g._chromosomeNum
			   &&	_geneNum == g._geneNum
			   && 	_nameIndex == g._nameIndex
			   && 	_maxNumOfNames == g._maxNumOfNames
			   &&	_numOfAdditionalInfo == g._numOfAdditionalInfo
			   &&	_maxPossibleNumOfIndices == g._maxPossibleNumOfIndices)
			{
				for(int i=0; i<_numOfAdditionalInfo; i++){
					if(!(_additionalIndices[i] == g._additionalIndices[i]
					     &&	_dominantAdditional[i] == g._dominantAdditional[i])) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns encoded name of this gene.
		/// </summary>
		/// <returns>The name encoded.</returns>
		public String GetNameEncoded(){
			return MyDictionary.geneDict[_chromosomeNum][_geneNum][0][_nameIndex][0];
		}

		/// <summary>
		/// Return decoded name of this gene
		/// </summary>
		/// <returns>The name decoded.</returns>
		public String GetNameDecoded(){
			return MyDictionary.geneDict[_chromosomeNum][_geneNum][0][_nameIndex][1];
		}

		/// <summary>
		/// Returns array of all possible encoded names this gene can have. 
		/// </summary>
		/// <returns>The all possible names.</returns>
		public String[] getAllPossibleEncodedNames(){
			String[] res = new String[_maxNumOfNames];
			for (int i=0; i<_maxNumOfNames; i++) {
				res[i] = MyDictionary.geneDict[_chromosomeNum][_geneNum][0][i][0];		
			}
			return res;
		}

		/// <summary>
		/// Returns array of all possible decoded names this gene can have. 
		/// </summary>
		/// <returns>The all possible names.</returns>
		public String[] GetAllPossibleDecodedNames(){
			String[] res = new String[_maxNumOfNames];
			for (int i=0; i<_maxNumOfNames; i++) {
				res[i] = MyDictionary.geneDict[_chromosomeNum][_geneNum][0][i][1];		
			}
			return res;
		}

		/// <summary>
		/// Returns array of encoded names of additional information for this gene.
		/// </summary>
		/// <returns>The encoded additional info.</returns>
		public String[] GetEncodedAdditionalInfo(){
			String[] res = new String[_numOfAdditionalInfo];
			if (_trait == EnumTrait.NUMBER) {
				for(int i=0; i<_numOfAdditionalInfo; i++){
					res[i] = _additionalIndices[i]+"";
				}
			} else {
				for (int i=0; i<_numOfAdditionalInfo; i++) {
					res[i] = MyDictionary.geneDict[_chromosomeNum][_geneNum][1][_additionalIndices[i]][0];		 			
				}
			}
			return res;
		}
		/// <summary>
		/// Returns array of decoded names of additional information for this gene.
		/// </summary>
		/// <returns>The encoded additional info.</returns>
		public String[] GetDecodedAdditionalInfo(){
			String[] res = new String[_numOfAdditionalInfo];
			if (_trait == EnumTrait.NUMBER) {
				for(int i=0; i<_numOfAdditionalInfo; i++){
					res[i] = _additionalIndices[i]+"";
				}
			} else {
				for (int i=0; i<_numOfAdditionalInfo; i++) {
					res[i] = MyDictionary.geneDict[_chromosomeNum][_geneNum][1][_additionalIndices[i]][1];		 			
				}
			}
			return res;
		}

		/// <summary>
		/// Returns array of encoded names of all possible additional information associated with this gene.
		/// </summary>
		/// <returns>The all encoded possible additional info.</returns>
		public String[] GetAllEncodedPossibleAdditionalInfo(){
			if (_trait == EnumTrait.NUMBER) {
				return new String[0];
			}
			String[] res = new String[_maxPossibleNumOfIndices];
			for (int i=0; i<_maxPossibleNumOfIndices; i++) {
				res[i] = MyDictionary.geneDict[_chromosomeNum][_geneNum][1][i][0];		
			}
			return res;
		}

		/// <summary>
		/// Returns array of decoded names of all possible additional information associated with this gene.
		/// </summary>
		/// <returns>The all encoded possible additional info.</returns>
		public String[] GetAllDecodedPossibleAdditionalInfo(){
			if (_trait == EnumTrait.NUMBER) {
				return new String[0];
			}

			String[] res = new String[_maxPossibleNumOfIndices];
			for (int i=0; i<_maxPossibleNumOfIndices; i++) {
				res [i] = MyDictionary.geneDict [_chromosomeNum] [_geneNum] [1] [i] [1];		
			}
			return res;
		}

		/// <summary>
		/// Returns the whole encoded name. E.g. c(r,R);
		/// </summary>
		/// <returns>The whole name encoded.</returns>
		public String GetWholeNameEncoded(){
			String res = GetNameEncoded ();
			if (_dominant) {
				res = res.ToUpper();
			}
			String[] extra = GetEncodedAdditionalInfo ();
			if (_numOfAdditionalInfo > 0) {
				res += "(";
				for(int i=0; i<_numOfAdditionalInfo; i++){
					if(_dominantAdditional[i]){
						extra[i] = extra[i].ToUpper();
					}
					res += extra[i];
					if(i < _numOfAdditionalInfo - 1){
						res += ",";
					}
				}
				res += ")";
			}
			return res;
		}

		/// <summary>
		/// Returns the whole decoded name. E.g. color(red,Red);
		/// </summary>
		/// <returns>The whole name encoded.</returns>
		public String GetWholeNameDecoded(){
			String res = GetNameDecoded ();
			if (_dominant) {
				res = res.ToUpper();
			}
			String[] extra = GetDecodedAdditionalInfo ();
			if (_numOfAdditionalInfo > 0) {
				res += "(";
				for(int i=0; i<_numOfAdditionalInfo; i++){
					if(_dominantAdditional[i]){
						extra[i] = extra[i].ToUpper();
					}
					res += extra[i];
					if(i < _numOfAdditionalInfo - 1){
						res += ",";
					}
				}
				res += ")";
			}
			return res;
		}

		/// <summary>
		/// Displays all information associated with this gene. Write to the file.
		/// NOTE: Change path before use.
		/// </summary>
		public void display(int index){

			String path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Genome\\";
			String dst = "Gene_Output_" + index + ".txt";
			
			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;


			file.WriteLine("----------------GENE----------------");
			file.WriteLine ("Encoded name:    " + GetWholeNameEncoded());
			file.WriteLine ("Decoded name:    " + GetWholeNameDecoded());
			file.WriteLine ();
			
			file.WriteLine ("Chromosome num.: " + _chromosomeNum);
			file.WriteLine ("Gene num.:       " + _geneNum);
			file.WriteLine ();

			file.WriteLine("Trait: " + _trait);
			file.WriteLine ("Dominance:       " + _dominant);
			file.WriteLine ();
			
			file.WriteLine ("Name index:      " + _nameIndex);
			file.WriteLine ("Name:            " + GetNameEncoded() + " " + GetNameDecoded());
			file.WriteLine ();
			
			file.WriteLine ("Num. of extra:   " + _numOfAdditionalInfo);
			String[] decodedExtra = GetDecodedAdditionalInfo ();
			String[] encodedExtra = GetEncodedAdditionalInfo ();
			for (int i=0; i<_numOfAdditionalInfo; i++) {
				file.WriteLine("\t" + encodedExtra[i] + " " + decodedExtra[i] + " " + _dominantAdditional[i]);
			}
			file.WriteLine ();

			file.WriteLine ("Max. names:      " + _maxNumOfNames);
			String[] encodedAllNames = getAllPossibleEncodedNames ();
			String[] decodedAllNames = GetAllPossibleDecodedNames ();
			for (int i=0; i<_maxNumOfNames; i++) {
				file.WriteLine("\t" + encodedAllNames[i] + " " + decodedAllNames[i]);
			}
			file.WriteLine ();

			file.WriteLine ("Max. indices:    " + _maxPossibleNumOfIndices);
			String[] encodedAllExtra = GetAllEncodedPossibleAdditionalInfo ();
			String[] decodedAllExtra = GetAllDecodedPossibleAdditionalInfo ();
			if (_maxPossibleNumOfIndices > 0) {
				for(int i=0; i<_maxPossibleNumOfIndices; i++){
					file.WriteLine("\t" + encodedAllExtra[i] + " " + decodedAllExtra[i]);
				}
			}
			file.WriteLine("-------------------------------------");
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
