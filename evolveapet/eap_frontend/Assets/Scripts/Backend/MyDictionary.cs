/// <summary>
/// 
/// Lookup tables for 
/// 
/// 1. Coded and decoded names for the genes - geneDict
/// 4D array [i][j][k][l][m] geneDict should be interpreted as
/// 	- i: chromosome number (indexed from 0)
/// 	- j: gene number on that chromosome (indexed from 0)
/// 	- k: if 0 then array for names will be accessed
/// 		 if 1 then array for any additional information will be accessed
/// 	- l: pairs (coded string, real name corresponding to that string) associated with gene j on chromosome i
/// 	- m: if 0 then the result is coded name of the gene / additional info (displayed if gene unknown)
/// 		 if 1 then the result is decoded (real) name of the gene / additional info (displayed if gene known)
/// 
/// Note that number information is special and hence is not included in this dictionary. 
/// 
/// 2. Names of the chromosomes (to which bodypart it corresponds) for index [chromosomeNum]
/// chromosomeDict
/// 
/// 3. Gives EnumTrait for pair [chromosomeNum][geneNum_on_that_chromosome]
/// traitDict
/// 
/// </summary>
using System;
using System.Collections.Generic;

namespace EvolveAPet
{
	[Serializable]
	public class MyDictionary
	{
		public static int temp = 1;
		private readonly static String[][] auxColorName = {
			{"c","colour"}
		};
		private readonly static String[][] auxColourAdditional = {
			{"r","red"},
			{"b","blue"},
			{"y","yellow"},
		}; 

		private readonly static String[][] auxSizeName = {
			{"s","size"}
		};

		private readonly static String[][] auxPatternName = {
			{"b","blank"},
			{"d","dots"},
			{"s","stripes"}
		};

		private readonly static String[][] auxShapeName = {
			{"d","dino"}
		};

		private readonly static String[][] auxTeethShapeName = {
			{"h","herbivore"},
			{"c","carnivore"}
		};

		public static EnumBodyPart[] chromosomeDict = {
			EnumBodyPart.EARS,
			EnumBodyPart.EYES,
			EnumBodyPart.HEAD,
			EnumBodyPart.TORSO,
			EnumBodyPart.ARMS,
			EnumBodyPart.LEGS,
			EnumBodyPart.TAIL
		};
		public static String[][][][] geneDict = {
			// Chromosome order: ears, eyes, head, torso, arms, legs, tail
			//----------Chromosome 0: EARS (i = 0)----------
			{ 
				// #0 Gene order: colour, size, pattern, shape

				// Colour gene, j = 0
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information
				},
				// Size gene, j = 1
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Pattern gene, j = 2
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Shape gene, j = 3
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				}
			},
			//----------Chromosome 1: EYES (i = 1)----------
			{
				// #1 Gene order - size, colour, pattern, shape, number

				// Size gene, j = 0
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Colour gene, j = 1
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information		
				},

				// Pattern gene, j = 2
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Shape gene, j = 3
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Number gene, j = 4 (will be totally empty, but need to have placeholder for correct indexing)
				{}

			},
			//----------Chromosome 2: HEAD (i = 2)----------
			{
				// #2 Gene order - colour, teeth_shape, pattern, size, shape

				// Colour gene, j = 0
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Teeth_shape gene, j = 1
				{
					auxTeethShapeName,		// k = 0 : multiple names here (carnivore, herbivore)
					{}						// k = 1 : no additional information
				},
				// Pattern gene, j = 2
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Size gene, j = 3
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Shape gene, j = 4
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				}

			},
			//----------Chromosome 3: TORSO (i = 3)----------
			// torso has : {colour, size, pattern, shape}
			{
				// #3 Gene order - size, pattern, shape, colour

				// Size gene, j = 0
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Pattern gene, j = 1
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Shape gene, j = 2
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Colour gene, j = 3
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information		
				}
			},
			//----------Chromosome 4: ARMS (i = 4)----------
			// arms has : {colour, size, pattern, /*number,*/ shape}
			{
				// #4 Gene order - colour, size, shape, number, patter

				// Colour gene, j = 0
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Size gene, j = 1
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Shape gene, j = 2
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Number gene, j = 3 (will be totally empty, but need to have placeholder for correct indexing)
				{},
				// Pattern gene, j = 4
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				}
			}, 
			//----------Chromosome 5: LEGS (i = 5)----------
			{
				// #5 Gene order - shape, size, colour, number, pattern

				// Shape gene, j = 0
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Size gene, j = 1
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Colour gene, j = 2
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information	
				},
				// Number gene, j = 3 (will be totally empty, but need to have placeholder for correct indexing)
				{},
				// Pattern gene, j = 4
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},

			},
			//----------Chromosome 5: TAIL (i = 5)----------
			{
				// #6 Gene order - pattern, size, colour, shape

				// Pattern gene, j = 0
				{
					auxPatternName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				},
				// Size gene, j = 1
				{
					auxSizeName,			// k = 0
					{}						// k = 1 : no additional information
				},
				// Colour gene, j = 2
				{
					auxColorName, 			// k = 0
					auxColourAdditional		// k = 1 : additional information
				},
				// Shape gene, j = 3
				{
					auxShapeName,			// k = 0 : multiple names here
					{}						// k = 1 : no additional information
				}
			}	
		};

		public readonly static EnumTrait[][] traitDict = {
			// Chromosome order: ears, eyes, head, torso, arms, legs, tail
			//----------Chromosome 0: EARS----------
			{
				// #0 Gene order: colour, size, pattern, shape
				EnumTrait.COLOUR,
				EnumTrait.SIZE,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE
			},
			//----------Chromosome 1: EYES----------
			{
				// #1 Gene order - size, colour, pattern, shape, number
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE,
				EnumTrait.NUMBER
			},
			//----------Chromosome 2: HEAD----------
			{
				// #2 Gene order - colour, teeth_shape, pattern, size, shape
				EnumTrait.COLOUR,
				EnumTrait.TEETH_SHAPE,
				EnumTrait.PATTERN,
				EnumTrait.SIZE,
				EnumTrait.SHAPE
			},			
			//----------Chromosome 3: TORSO----------
			{
				// #3 Gene order - size, pattern, shape, colour
				EnumTrait.SIZE,
				EnumTrait.PATTERN,
				EnumTrait.SHAPE,
				EnumTrait.COLOUR
			},
			//----------Chromosome 4: ARMS----------
			{
				// #4 Gene order - colour, size, shape, number, pattern
				EnumTrait.COLOUR,
				EnumTrait.SIZE,
				EnumTrait.SHAPE,
				EnumTrait.NUMBER,
				EnumTrait.PATTERN

			},
			//----------Chromosome 5: LEGS----------
			{
				// #5 Gene order - shape, size, colour, number, pattern
				EnumTrait.SHAPE,
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.NUMBER,
				EnumTrait.PATTERN

			},
			//----------Chromosome 6: TAIL----------
			{
				// #6 Gene order - pattern, size, colour, shape
				EnumTrait.PATTERN,
				EnumTrait.SIZE,
				EnumTrait.COLOUR,
				EnumTrait.SHAPE
			}
		};

		public readonly static Dictionary<EnumTrait,int> numOfAdditionalInfo;
	
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
		}
	}
	

}

