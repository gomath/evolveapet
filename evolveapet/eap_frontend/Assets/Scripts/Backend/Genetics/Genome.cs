using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EvolveAPet
{
    [Serializable]
	public class Genome
    {
		// elements at index i of both arrays together logically codes for one trait
		private Chromosome[] _motherChromosome;
		private Chromosome[] _fatherChromosome;
		private int _numOfChromosomes;

		// GETTERS AND SETTERS
		public Chromosome[] MotherChromosomes{
			get { return _motherChromosome; }
		}

		public Chromosome[] FatherChromosomes{
			get { return _fatherChromosome; }
		}

		public int NumOfChromosomes{
			get { return _numOfChromosomes; }
		}

		/// <summary>
		/// Creates random genome. 
		/// </summary>
		public Genome(){
			_numOfChromosomes = Global.NUM_OF_CHROMOSOMES;
			_motherChromosome = new Chromosome[_numOfChromosomes];
			_fatherChromosome = new Chromosome[_numOfChromosomes];
			for (int i=0; i<_numOfChromosomes; i++) {
				_motherChromosome[i] = new Chromosome(i);
				_fatherChromosome[i] = new Chromosome(i);
			}
		}
		
		/// <summary>
		/// Creates genome from two chromosome arrays. 
		/// </summary>		
		public Genome(Chromosome [] motherC, Chromosome[] fatherC){
			_numOfChromosomes = Global.NUM_OF_CHROMOSOMES;
			_motherChromosome = motherC;
			_fatherChromosome = fatherC;
		
		}

		/// <summary>
		/// Load Global.mapBtF and Global.mapFtB if they don't exist 
		/// </summary>
		private void LoadMapIfNotExist(){
			// Checking whether maps exist
			if (Global.mapBtF == null || Global.mapFtB == null) {
				Global.LoadMap();
			}	
		}

		// TODO - untested
		/// <summary>
		/// FRONTEND METHOD
		/// Given frontEnd chromosome number, frontEnd gene number and integer 0 (mother) or 1 (father), returns the
		/// corresponding gene in the backend genome. 
		/// 
		/// </summary>
		/// <returns>The end get gene.</returns>
		/// <param name="frontendChromosome">Frontend chromosome.</param>
		/// <param name="frontEndGeneNumber">Front end gene number.</param>
		/// <param name="mother">Mother.</param>
		public Gene FrontEndGetGene(int frontendChromosome, int frontEndGeneNumber, int parent){
			LoadMapIfNotExist ();

			Locus l = new Locus (frontendChromosome, frontEndGeneNumber);
			Locus backendLocus = Global.mapFtB[l];
				int backEndChrom = backendLocus.Chromosome;
				int backEndGene = backendLocus.GeneNumber;

			Chromosome[] ch = (parent == 0) ? _motherChromosome : _fatherChromosome;
			return ch[backEndChrom].Genes[backEndGene];
		}

		//TODO - untested
		/// <summary>
		/// FRONTEND METHOD
		/// Given body part, returns the array of frontend Loci (positions, i.e. chromosome number and gene number) which codes for this body part.
		/// </summary>
		/// <returns>The end get loci by body part.</returns>
		/// <param name="bodyPart">Body part.</param>
		public Locus[] FrontEndGetLociByBodyPart(EnumBodyPart bodyPart){
			LoadMapIfNotExist ();

			int backendChromosomeNumber = (int)bodyPart;
			int numOfGenes = MyDictionary.numOfGenesOnChromosome [bodyPart];

			Locus[] result = new Locus[numOfGenes];
			for (int i=0; i<numOfGenes; i++) {
				result[i] = Global.mapBtF[new Locus(backendChromosomeNumber,i)];
			}

			return result;
		}

		// TODO - untested
		/// <summary>
		/// Returns front-end represantation of chromosomes.
		/// </summary>
		/// <returns>The ent chromosomes.</returns>
		private Chromosome[,] FrontEndChromosomes(){
			LoadMapIfNotExist ();

			Chromosome[,] result = new Chromosome[2,Global.NUM_OF_CHROMOSOMES];
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				// Obtain number of genes on chromosome i
				int numOfGenesOnTheChromosome = MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i];

				Gene[] motherGenes = new Gene[numOfGenesOnTheChromosome];
				Gene[] fatherGenes = new Gene[numOfGenesOnTheChromosome];
				// Create correct Gene[] arrays that will contain appropriate Genes that are located on frontend chromosome i
				for(int j=0; j<numOfGenesOnTheChromosome; j++){
					Locus l = Global.mapFtB[new Locus(i,j)];
					motherGenes[j] = new Gene(_motherChromosome[l.Chromosome].Genes[l.GeneNumber]);
					fatherGenes[j] = new Gene(_fatherChromosome[l.Chromosome].Genes[l.GeneNumber]);
				}

				// Create the front end chromosomes
				result[0,i] = new Chromosome(motherGenes);
				result[1,i] = new Chromosome(fatherGenes);
			}

			return result;
		}

		// TODO - untested
		/// <summary>
		/// FRONTEND METHOD
		/// Returns the 2D array of chromosome Chromosome[i][j]. Index i specifies the chromosome (i.e. the body part) and the
		/// second index j (1 <= j <=4) specifies the actual chromosome withing the tetrad.
		/// </summary>
		/// <returns>The tetrads for breeding.</returns>
		public Chromosome[,] FrontEndCreateTetradsForBreeding(){
			Chromosome[,] frontEndChromosomes = FrontEndChromosomes();

			Chromosome[,] res = new Chromosome[Global.NUM_OF_CHROMOSOMES,4];
			
			Chromosome fatherChrom, motherChrom;
			Chromosome[] crossOver;
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				fatherChrom = frontEndChromosomes[1,i];		
				motherChrom = frontEndChromosomes[0,i];
				crossOver = CrossOver(motherChrom, fatherChrom); // crossOver[0] originally mothers chromosome 
				
				res[i,0] = new Chromosome (motherChrom);
				res[i,1] = new Chromosome (fatherChrom);
				res[i,2] = new Chromosome (crossOver[0]);
				res[i,3] = new Chromosome (crossOver[1]);
			}
			return res;
		}

		/// <summary>
		/// Writes this genome in concise form into given file.
		/// </summary>
		/*public void Display(int testIndex){
			String path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Complete_Genomes\\";
			String dst = "Genome_" +  testIndex + ".txt";
			
			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;

			file.WriteLine("Legend: each line is in for #gene number, mother gene, father gene and phenotype");
			for (int i=0; i<_numOfChromosomes; i++) {
				file.WriteLine("----------CHROMOSOME #" + i + "----------");
				for(int j=0; j<_fatherChromosome[i].NumOfGenes; j++){
					Gene motherGene = _motherChromosome[i].Genes[j];
					Gene fatherGene = _fatherChromosome[i].Genes[j];

					String f = "{0,-20}";
					//String motherString = String.Format(f,motherGene.GetWholeNameEncoded()) + String.Format(f,"(" + motherGene.GetWholeNameDecoded() + ")");
					//String fatherString = String.Format(f,fatherGene.GetWholeNameEncoded()) + String.Format(f,"(" + fatherGene.GetWholeNameDecoded() + ")");
					String simpleMotherString = String.Format(f,motherGene.GetWholeNameEncoded());
					String simpleFatherString = String.Format(f,fatherGene.GetWholeNameEncoded());
					int index = GetTrait(i,motherGene.Trait);
					String manifestation = AuxDisplayAttribute(motherGene.Trait,index);

					file.Write("#" + j + ":\t" + simpleMotherString + "\t" + simpleFatherString + "\t" + manifestation);
					if(i == MyDictionary.GetIndexOfBodyPart(EnumBodyPart.ARMS) && motherGene.Trait == EnumTrait.NUMBER){
						if(IsQuadrupedal()){
							file.Write ("\tQUADRUPEDAL");
						} else {
							file.Write ("\tBIPEDAL");
						}
					}
					file.WriteLine();
				}
				file.WriteLine();
			}


			file.Close ();
		}*/

		/// <summary>
		/// Auxiliary function for displaying. Given trait and index to that trait, returns textual representation of the attribute.
		/// </summary>
		/// <returns>The display attribute.</returns>
		/// <param name="trait">Trait.</param> 
		/// <param name="index">Index.</param>
		private String AuxDisplayAttribute(EnumTrait trait, int index){
			switch (trait) {
			case EnumTrait.COLOUR:
				int red = (index & 0xFF0000) >> 16;
				int green = (index & 0xFF00) >> 8;
				int blue = index & 0xFF;
				return "R: " + red + " G: " + green + " B: " + blue;
			case EnumTrait.NUMBER:
				return index + "";
			case EnumTrait.PATTERN:
				return (EnumPattern)index + "";
			case EnumTrait.SHAPE:
				return MyDictionary.GetShape(index);
			case EnumTrait.SIZE:
				return (EnumSize)index + "";
			case EnumTrait.TEETH_SHAPE:
				return MyDictionary.GetShape(index);
			}
			return null;
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
			int split = Global.rand.Next(c1.NumOfGenes + 1); // returns [0,NUM_OF_CHROMOSOMES], both inclusive

			// Creating deep fresh clones of both chromosomes
			Chromosome[] res = new Chromosome[2];
				res [0] = new Chromosome (c1);
				res [1] = new Chromosome (c2);
				
			// Marking where the chromosome has been split (mainly for testing purposes)
			res [0].WhereHasBeenSplit = split;
			res [1].WhereHasBeenSplit = split; 

			// Crossover part
			Gene temp;
			for (int i=split; i<c1.NumOfGenes; i++) {
				temp = res[0].Genes[i];
				res[0].Genes[i] = res[1].Genes[i];
				res[1].Genes[i] = temp;
			}

			return res;
		}

		/// <summary>
		/// Returns the 2D array of chromosome Chromosome[i][j]. Index i specifies the chromosome (i.e. the body part) and the
		/// second index j (1 <= j <=4) specifies the actual chromosome withing the tetrad.
		/// </summary>
		/// <returns>The tetrads for breeding.</returns>
		public Chromosome[,] CreateTetradsForBreeding(){
			Chromosome[,] res = new Chromosome[Global.NUM_OF_CHROMOSOMES,4];

			Chromosome fatherChrom, motherChrom;
			Chromosome[] crossOver;
			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				fatherChrom = _fatherChromosome[i];
				motherChrom = _motherChromosome[i];
				crossOver = CrossOver(motherChrom, fatherChrom); // crossOver[0] originally mothers chromosome 

				res[i,0] = new Chromosome (motherChrom);
				res[i,1] = new Chromosome (fatherChrom);
				res[i,2] = new Chromosome (crossOver[0]);
				res[i,3] = new Chromosome (crossOver[1]);
			}
			return res;
		}

		/// <summary>
		/// Given two genes, it returns the index into appropriate enumeration corresponding to the trait these two genes
		/// are coding for. In case of colour, it returns the color RGY (Red, Green, Yellow) as a single integer.
		/// </summary>
		/// <returns>The trait.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		public int DecodeTrait(Gene g1, Gene g2){
			if (g1.Trait != g2.Trait) {
				return -1; 	// error
			}

			switch(g1.Trait){
			case EnumTrait.COLOUR:  	return DecodeColour(g1,g2); 
			case EnumTrait.NUMBER:  	return DecodeNumber(g1,g2);  
			case EnumTrait.PATTERN: 	return DecodePattern(g1,g2); 
			case EnumTrait.SHAPE:   	return DecodeShape(g1,g2); 
			case EnumTrait.SIZE:    	return DecodeSize(g1,g2); 
			case EnumTrait.TEETH_SHAPE: return DecodeShape(g1,g2); 
			}

			return 0;
		}

		/// <summary>
		/// Rules for decoding colour are as follows:
		/// 
		/// Schema c1(x1,y1) c2(x2,y2)
		/// 	- c1, c2 -> can be recessive or dominant
		/// 	- x1,y1,x2,y2 -> can be recessive or dominant and are taken from set (red, green, blue)
		/// 
		/// If exactly 1 gene is recessive, ignore that one and work only with dominant one.
		/// If both gene are dominant / both recessive, work with both.
		/// 
		/// You are left with 2 or 4 attributes out of x1,y1,x2,y1, each of them dominant or recessive and coding for particular colour.
		/// 
		/// Establish the ratio of (red, green, blue) as follows.
		/// 1 unit for colour c if corresponding attribute x or y is recessive.
		/// 2 unites for colour c if corresponding attribute x or y is dominant.
		/// 
		/// You are left with ratio (r,g,b).
		///  
		/// Set colour component of MAX(r,g,b) to 255 and set rest of the component accordingly using the ratio.
		/// Code the colour into 1 integer as follows
		/// 
		/// COLOUR = RED << 16 | GREEN << 8 | BLUE
		/// 
		/// </summary>
		/// <returns>The colour.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		private int DecodeColour(Gene g1, Gene g2){
			int chromosome = g1.ChromosomeNum;
			int gene = g1.GeneNum;

			String[] colour;
			bool[] dominant;
			int l; // length of array colour and dominant

			if ((g1.IsDominant () && g2.IsDominant ()) || (!g1.IsDominant () && !g2.IsDominant ())) {
				// if both dominant or both recessive
				colour = new String[4];
				dominant = new bool[4];
				l = 4;
				colour[0] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g1.AdditionalIndices[0],2);
				colour[1] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g1.AdditionalIndices[1],2);
				colour[2] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g2.AdditionalIndices[0],2);
				colour[3] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g2.AdditionalIndices[1],2);

				dominant[0] = g1.DominantAdditional[0];
				dominant[1] = g1.DominantAdditional[1];
				dominant[2] = g2.DominantAdditional[0];
				dominant[3] = g2.DominantAdditional[1];
			} else {
				// exactly one is dominant
				Gene g;
				if(g1.IsDominant()){
					g = g1;
				} else {
					g = g2;
				}

				colour = new String[2];
				dominant = new bool[2];
				l = 2;
				colour[0] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g.AdditionalIndices[0],2);
				colour[1] = MyDictionary.GetFromGeneDict(chromosome,gene,1,g.AdditionalIndices[1],2);
				dominant[0] = g.DominantAdditional[0];
				dominant[1] = g.DominantAdditional[1];
 			}

			int[] counts = new int[3]{0,0,0}; // counts for red, green and blue
			int addDominant = 2;
			int addRecessive = 1;

			for (int i=0; i<l; i++) {
				int index = 0;
				switch(colour[i]){
					case "RED":		index = 0; break;
					case "GREEN": 	index = 1; break;
					case "BLUE": 	index = 2; break;
				}
				counts[index] += dominant[i] ? addDominant : addRecessive;
			}

			int maxIndex = max (counts);
			int ratio = (int) (255 / counts[maxIndex]);

			if (counts [maxIndex] * ratio > 255) {
				ratio--;
			}
			
			byte red = 		(byte) 	(counts [0] * ratio);
			byte green = 	(byte)	(counts [1] * ratio);
			byte blue = 	(byte) 	(counts [2] * ratio);

			int finalColour = 0;
			finalColour = (red << 16) | (green << 8) | blue;
	
			return finalColour;
		}

		/// <summary>
		/// Returns the index to maximum element of the array.
		/// </summary>
		/// <param name="a">The alpha component.</param>
		private int max(int[] a){
			if (a.Length == 0) {
				return -1;
			}
			int maxIndex = 0;
			for (int i=1; i<a.Length; i++) {
				if(a[i] > a[maxIndex]){
					maxIndex = i;
				}
			}
			return maxIndex;
		}

		/// <summary>
		/// If exactly one gene dominant, take the number associated with that gene.
		/// If both dominant or both recessive -> return arithmetic average (truncated).
		/// </summary>
		/// <returns>The number.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		private int DecodeNumber(Gene g1, Gene g2){
			int n1 = g1.AdditionalIndices [0];
			int n2 = g2.AdditionalIndices [0];
			if (g1.IsDominant ()) {
				if(g2.IsDominant ()){
					// both dominant
					return (n1 + n2) / 2;
				} else {
					return n1; // return number stored in g1
				}
			} else {
				if(g2.IsDominant()){
					return n2; // return number stored in g2
				} else {
					// both recessive
					return (n1 + n2) / 2;
				}
			}
		}

		/// <summary>
		/// Returns true if the animal is quadrupedal and false otherwise.
		/// 	N(1) + n(x) => bipedal with arm
		/// 	N(0) + n(x) => bipedal with no arms
		///		N(1) + N(1) => bipedal with arm
		///		N(0) + N(0) => bipedal without arms
		///		N(1) + N(0) => bipedal with or without arms (can be both)
		///		n(x) + n(x) => quadrupedal
		/// </summary>
		/// <returns><c>true</c> if this instance is quadrupedal; otherwise, <c>false</c>.</returns>
		public bool IsQuadrupedal(){
			int chromosome = Array.FindIndex (MyDictionary.chromosomeDict, x => x == EnumBodyPart.ARMS);
			int gene = _fatherChromosome [chromosome].getTraitPosition ((int)EnumTrait.NUMBER);

			Gene g1 = _motherChromosome [chromosome].Genes [gene];
			Gene g2 = _fatherChromosome [chromosome].Genes [gene];

			if (g1.IsDominant () || g2.IsDominant ()) {
				// bipedal animal
				return false;
			} else {
				// quadrupedal animal
				return true;
			}
		}

		/// <summary>
		/// Codominant gene.
		/// Recessive gene -> BLANK
		/// Dominant gene -> DOTS or STRIPES
		///  
		/// </summary>
		/// <returns>The pattern.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		private int DecodePattern(Gene g1, Gene g2){
			if (!g1.IsDominant () && !g2.IsDominant ()) {
				// both recessive, final phenotype is BLANK
				return 0;
			} else {
				if(g1.IsDominant() && g2.IsDominant()){
					// both dominant
					String pattern1 = MyDictionary.GetFromGeneDict(g1.ChromosomeNum, g1.GeneNum, 0, g1.NameIndex, 2);
					String pattern2 = MyDictionary.GetFromGeneDict(g2.ChromosomeNum, g2.GeneNum, 0, g2.NameIndex, 2);

					if(pattern1 == "DOTS"){
						if(pattern2 == "DOTS"){
							return 1; // DOTS phenotype
						} else {
							return 3; // Mixed phenotype
						}
					} else /*pattern1 = STRIPES*/ {
						if(pattern2 == "DOTS"){
							return 3; // Mixed phenotype
						} else {
							return 2; // STRIPES phenotype
						}
					}
				} else {
					// exactly one of them is dominant
					String pattern;
					if(g1.IsDominant()){
						pattern = MyDictionary.GetFromGeneDict(g1.ChromosomeNum, g1.GeneNum, 0, g1.NameIndex, 2);
					} else {
						pattern = MyDictionary.GetFromGeneDict(g2.ChromosomeNum, g2.GeneNum, 0, g2.NameIndex, 2);
					}

					if(pattern == "DOTS"){
						return 1; // DOTS phenotype
					} else {
						return 2; // STRIPES phenotype
					}
				}
			}
		}

		/// <summary>
		/// Returns index into MyDictionary.EnumShapes, pointing to appropriate shape. 
		/// If both genes are dominant or both recessive, one shape is chosen at random.
		/// Otherwise, dominant gene takes over.
		/// 
		/// </summary>
		/// <returns>The shape.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		private int DecodeShape(Gene g1, Gene g2){
			String shape1 = MyDictionary.GetFromGeneDict (g1.ChromosomeNum, g1.GeneNum, 0, g1.NameIndex, 2);
			String shape2 = MyDictionary.GetFromGeneDict (g2.ChromosomeNum, g2.GeneNum, 0, g2.NameIndex, 2);

			int index1 = MyDictionary.GetIndexInEnumShapes (shape1);
			int index2 = MyDictionary.GetIndexInEnumShapes (shape2);

			int random = Global.rand.Next (2);
			if (g1.IsDominant ()) {
				if(g2.IsDominant()){
					// both dominant
					if(random == 0){
						return index1;
					} else {
						return index2;
					}
				} else {
					// g1 only dominant
					return index1;
				}
			} else {
				if(g2.IsDominant()){
					// g2 only dominant
					return index2;
				} else {
					// both recessive
					if(random == 0){
						return index1;
					} else {
						return index2;
					}
				}
			}
		}

		/// <summary>
		/// Two recessive genes -> SMALL
		/// Exactly one dominant -> MEDIUM
		/// Two dominant -> LARGE
		/// 
		/// </summary>
		/// <returns>The size.</returns>
		/// <param name="g1">G1.</param>
		/// <param name="g2">G2.</param>
		private int DecodeSize(Gene g1, Gene g2){
			if (g1.IsDominant ()) {
				if(g2.IsDominant ()){
					return 2; // LARGE
				} else {
					return 1; // MEDIUM
				}
			} else {
				if(g2.IsDominant()){
					return 1; // MEDIUM
				} else {
					return 0; // SMALL
				}
			}
		}


        /// <summary>
        /// Given chromosome number and the gene number on that chromosome, returns the index into appropriate enum corresponding to that trait
        /// of colour information as a single integer
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="trait"></param>
        /// <returns></returns>
		public int GetTrait(int chromosomeNum, int traitNum) { 
			int geneNum = _fatherChromosome [chromosomeNum].getTraitPosition (traitNum);
			Gene g1 = _motherChromosome [chromosomeNum].Genes [geneNum];
			Gene g2 = _fatherChromosome [chromosomeNum].Genes [geneNum];

			return DecodeTrait(g1,g2);
        }
		
		/// <summary>
		/// Same as GetTrait with traitNum parameter, but here, explicit EnumTrait can be specified. (for convenience for front-end). 
		/// </summary>
		/// <returns>The trait.</returns>
		/// <param name="chromosomeNum">Chromosome number.</param>
		/// <param name="trait">Trait.</param>
		public int GetTrait(int chromosomeNum, EnumTrait trait){
			return GetTrait (chromosomeNum, (int)trait);
		}

	}
}
