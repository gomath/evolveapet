/// <summary>
/// 
/// DO NOT MODIFY THIS CLASS. GENE CLASS HEAVILY DEPENDS ON IT.
/// 
/// Contents of the class 
/// 0. Some auxiliary String[][] arrys used further in geneDict (it saves space for now).
/// 
/// 1. geneDict Coded and decoded names for the genes
/// 5D array [i][j][k][l][m] geneDict should be interpreted as
/// 	- i: chromosome number (indexed from 0)
/// 	- j: gene number on that chromosome (indexed from 0)
/// 	- k: if 0 then array for names will be accessed
/// 		 if 1 then array for any additional information will be accessed
/// 	- l: pairs (coded string, real name corresponding to that string) associated with gene j on chromosome i
/// 	- m: if 0 then the result is coded name of the gene / additional info (displayed if gene unknown)
/// 		 if 1 then the result is decoded (real) name of the gene / additional info (displayed if gene known)
/// 
/// 
/// 2. chromosomeDict - Names of the chromosomes (to which bodypart it corresponds) for index [chromosomeNum]
/// 
/// 3. traitDict - gives EnumTrait for pair [chromosomeNum][geneNum_on_that_chromosome]
/// 
/// 5. numOfAdditionalInfo - given trait, this Dictionary returns number of additional info associated with that trait.
/// 
/// 6. numOfGenesOnChromosome - given body part, this Dictionary returns number of genes on the chromosome corresponding to that body part.
/// 
/// NOTE: You have to call Init() in order to get numOfAdditionalInfo and numOfGenesOnChromosome working.
/// 
/// 7. displayGeneDict - convenient method for outputting whole geneDict dictionary (in formatted manner) into the file.
/// NOTE: have to change path to your own.
/// 
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveAPet
{
	[Serializable]
	public class MyDictionary
	{
		private readonly static String[][] auxColorName = new String[][] {
			new String[] {"c","colour"}
		};
		private readonly static String[][] auxColourAdditional = new String[][]{
			new String[] {"r","red"},
			new String[] {"b","blue"},
			new String[] {"y","yellow"},
		}; 

		private readonly static String[][] auxNumberName = new String[][]{
			new String[] {"n","number"}
		};

		private readonly static String[][] auxSizeName = new String[][]{
			new String[] {"s","size"}
		};

		private readonly static String[][] auxPatternName = new String[][]{
			new String[] {"b","blank"},
			new String[] {"d","dots"},
			new String[] {"s","stripes"}
		};

		private readonly static String[][] auxShapeName = new String[][]{
			new String[] {"d","dino"}
		};

		private readonly static String[][] auxTeethShapeName = new String[][]{
			new String[] {"h","herbivore"},
			new String[] {"c","carnivore"}
		};

		public static EnumBodyPart[] chromosomeDict = new EnumBodyPart[]{
			EnumBodyPart.EARS,
			EnumBodyPart.EYES,
			EnumBodyPart.HEAD,
			EnumBodyPart.TORSO,
			EnumBodyPart.ARMS,
			EnumBodyPart.LEGS,
			EnumBodyPart.TAIL
		};

		// TODO - REMOVE
		public static String[][] tmpString = new String[][]{};

		public static String[][][][][] geneDict = new String[7][][][][]{
			// Chromosome order: ears, eyes, head, torso, arms, legs, tail
			//----------Chromosome 0: EARS (i = 0)----------
			new String[4][][][]
			{ 
				// #0 Gene order: colour, size, pattern, shape

				// Colour gene, j = 0
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information
				},
				// Size gene, j = 1
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Pattern gene, j = 2
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				},
				// Shape gene, j = 3
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				}
			},
			//----------Chromosome 1: EYES (i = 1)----------
			new String[4][][][]
			{
				// #1 Gene order - size, colour, pattern, shape, number

				// Size gene, j = 0
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Colour gene, j = 1
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information		
				},

				// Pattern gene, j = 2
				new String[1][][]
				{
					auxPatternName		// k = 0 : multiple names here
				},
				// Shape gene, j = 3
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				}
				// Number gene j = 4, EMPTY

			},
			//----------Chromosome 2: HEAD (i = 2)----------
			new String[5][][][]
			{
				// #2 Gene order - colour, teeth_shape, pattern, size, shape

				// Colour gene, j = 0
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Teeth_shape gene, j = 1
				new String[1][][]
				{
					auxTeethShapeName		// k = 0 : multiple names here (carnivore, herbivore)
				},
				// Pattern gene, j = 2
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				},
				// Size gene, j = 3
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Shape gene, j = 4
				new String[1][][]
				{
					auxShapeName		// k = 0 : multiple names here
				}

			},
			//----------Chromosome 3: TORSO (i = 3)----------
			new String[4][][][]
			{
				// #3 Gene order - size, pattern, shape, colour

				// Size gene, j = 0
				new String[1][][]
				{
					auxSizeName				// k = 0
				},
				// Pattern gene, j = 1
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				},
				// Shape gene, j = 2
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				},
				// Colour gene, j = 3
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information		
				}
			},
			//----------Chromosome 4: ARMS (i = 4)----------
			new String[5][][][]
			{
				// #4 Gene order - colour, size, shape, number, patter

				// Colour gene, j = 0
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Size gene, j = 1
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Shape gene, j = 2
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				},
				// Number gene, j = 3
				new String[1][][]
				{
					auxNumberName
				},
				// Pattern gene, j = 4
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				}
			}, 
			//----------Chromosome 5: LEGS (i = 5)----------
			new String[5][][][]
			{
				// #5 Gene order - shape, size, colour, number, pattern

				// Shape gene, j = 0
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				},
				// Size gene, j = 1
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Colour gene, j = 2
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Number gene, j = 3 
				new String[1][][]
				{
					auxNumberName
				},
				// Pattern gene, j = 4
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				},

			},
			//----------Chromosome 5: TAIL (i = 5)----------
			new String[4][][][]
			{
				// #6 Gene order - pattern, size, colour, shape

				// Pattern gene, j = 0
				new String[1][][]
				{
					auxPatternName			// k = 0 : multiple names here
				},
				// Size gene, j = 1
				new String[1][][]
				{
					auxSizeName			// k = 0
				},
				// Colour gene, j = 2
				new String[2][][]
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information
				},
				// Shape gene, j = 3
				new String[1][][]
				{
					auxShapeName			// k = 0 : multiple names here
				}
			}	
		};

		/// <summary>
		/// Outputs formatted geneDict into the specified file.
		/// NOTE: Format path variable before use.
		/// </summary>
		public static void displayGeneDict(){
			String path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\";
			String dst = "GeneDict_Output.txt";

			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;

			file.WriteLine("OVERALL STRUCTURE OF GENOME");

			String[][][][][] a = geneDict;
			for (int i=0; i<a.GetLength(0); i++) {
				file.WriteLine("------------------------");
				file.WriteLine("Chromosome #" + i + " (" + chromosomeDict[i] + ")");
				int numOfGenes = a[i].GetLength(0);
				for(int j=0; j<numOfGenes; j++){
					file.WriteLine(getTabs(1) + "\r\nGene #" + j + " (" + traitDict[i][j] + ")");

					bool hasExtra = a[i][j].GetLength(0) > 1;

					int numOfNames = a[i][j][0].GetLength(0);
					file.WriteLine(getTabs(2) + "Num. of names: " + numOfNames);
					for(int l=0; l<numOfNames; l++){
						String nameEncoded = a[i][j][0][l][0];
						String nameDecoded = a[i][j][0][l][1];
						file.WriteLine(getTabs(3) + nameEncoded + " " + nameDecoded);
					}

					if(hasExtra){
						int numOfExtra = a[i][j][1].GetLength(0);
						file.WriteLine(getTabs(2) + "Num. of extra: " + numOfExtra);
						for(int l=0; l<numOfExtra; l++){
							String extraEncoded = a[i][j][1][l][0];
							String extraDecoded = a[i][j][1][l][1];
							file.WriteLine(getTabs(3) + extraEncoded + " " + extraDecoded);
						}
					} else {
						file.WriteLine(getTabs (2) + "Num. of extra: 0");
					}
				}
			}
			file.Close();
		}

		/// <summary>
		/// Gets string containin n tabs.
		/// </summary>
		/// <returns>The tabs.</returns>
		/// <param name="n">N.</param>
		private static String getTabs(int n){
			String res = "";
			for (int i=0; i<n; i++) {
				res += "\t";
			}
			return res;
		}

		/// <summary>
		/// Stores appropriate trait at each [chromosome][gene] location. 
		/// </summary>
		public readonly static EnumTrait[][] traitDict = new EnumTrait[][]{
			// Chromosome order: ears, eyes, head, torso, arms, legs, tail
			//----------Chromosome 0: EARS----------
			new EnumTrait[]
			{
				// #0 Gene order: colour, size, pattern, shape
				EnumTrait.COLOUR,
				EnumTrait.SIZE,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE
			},
			//----------Chromosome 1: EYES----------
			new EnumTrait[]
			{
				// #1 Gene order - size, colour, pattern, shape, number
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE,
				EnumTrait.NUMBER
			},
			//----------Chromosome 2: HEAD----------
			new EnumTrait[]
			{
				// #2 Gene order - colour, teeth_shape, pattern, size, shape
				EnumTrait.COLOUR,
				EnumTrait.TEETH_SHAPE,
				EnumTrait.PATTERN,
				EnumTrait.SIZE,
				EnumTrait.SHAPE
			},			
			//----------Chromosome 3: TORSO----------
			new EnumTrait[]
			{
				// #3 Gene order - size, pattern, shape, colour
				EnumTrait.SIZE,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE,
				EnumTrait.COLOUR
			},
			//----------Chromosome 4: ARMS----------
			new EnumTrait[]
			{
				// #4 Gene order - colour, size, shape, number, pattern
				EnumTrait.COLOUR,
				EnumTrait.SIZE,
				EnumTrait.SHAPE,
				EnumTrait.NUMBER,
				EnumTrait.PATTERN

			},
			//----------Chromosome 5: LEGS----------
			new EnumTrait[]
			{
				// #5 Gene order - shape, size, colour, number, pattern
				EnumTrait.SHAPE,
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.NUMBER,
				EnumTrait.PATTERN

			},
			//----------Chromosome 6: TAIL----------
			new EnumTrait[]
			{
				// #6 Gene order - pattern, size, colour, shape
				EnumTrait.PATTERN,
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.SHAPE
			}
		};

		// Given trait, reutrns how many additional information is associated with that trait
		public readonly static Dictionary<EnumTrait,int> numOfAdditionalInfo = new Dictionary<EnumTrait, int> ();

		// Given body part that codes for the chromosome, returns number of genes on that chromosome
		public readonly static Dictionary<EnumBodyPart,int> numOfGenesOnChromosome  = new Dictionary<EnumBodyPart, int> ();
	
		/// <summary>
		/// Has to be called at least once in order to initialize Dictionary
		/// </summary>
		public static void Init(){
			// Initializing Dictionary numOfAdditionalInfo
			numOfAdditionalInfo.Add (EnumTrait.COLOUR,2);
			numOfAdditionalInfo.Add (EnumTrait.SIZE,0);	
			numOfAdditionalInfo.Add (EnumTrait.PATTERN,0);	
			numOfAdditionalInfo.Add (EnumTrait.NUMBER,1);	
			numOfAdditionalInfo.Add (EnumTrait.SHAPE,0);	
			numOfAdditionalInfo.Add (EnumTrait.TEETH_SHAPE,0);	

			// Initializeng Dictionary numOfGenesOnChromosome
			numOfGenesOnChromosome.Add(EnumBodyPart.EARS,4); // colour, size, pattern, shape
			numOfGenesOnChromosome.Add(EnumBodyPart.EYES,5); // size, colour, pattern, shape, number
			numOfGenesOnChromosome.Add(EnumBodyPart.HEAD,5); // colour, teeth_shape, pattern, size, shape
			numOfGenesOnChromosome.Add(EnumBodyPart.TORSO,4); //  size, pattern, shape, colour
			numOfGenesOnChromosome.Add(EnumBodyPart.ARMS,5); // colour, size, shape, number, pattern
			numOfGenesOnChromosome.Add(EnumBodyPart.LEGS,5); // shape, size, colour, number, pattern
			numOfGenesOnChromosome.Add(EnumBodyPart.TAIL,4); //  pattern, size, colour, shape
		}
	}
	

}

