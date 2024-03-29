﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EvolveAPet
{
   [Serializable]
	public class Chromosome {

		// DO NOT REMEMBER TO UPDATE COPY CONSTRUCTOR IF YOU CHANGE MEMBERS
        private EnumBodyPart _bodyPart;
		private int _chromosomeNumber;
        private Gene[] _genes; // one for every bodypart trait
        private int _numOfGenes; // number of genes on this chromosome
		private int _whereHasBeenSplit;
        
		// NOTE - splitLocation variable is the same as _whereHasBeenSplit, which is used during cross over. Use _whereHasBeenSplit
		//public int splitLocation{ get;set;}//This is needed for the front end, so that you can graphically display where the split in the chromosome has happened. This is useful because it makes
		//it more obvious that the chromosome has been split into two.

        // GETTERS AND SETTERS
        public EnumBodyPart BodyPart{
            get{
                return _bodyPart;
            }
        }

		public int ChromosomeNumber{
			get{
				return _chromosomeNumber;
			}
		}

        public Gene[] Genes{
            get{
                return _genes;
            }
        }

		public int NumOfGenes{
			get{
				return _numOfGenes;
			}
		}

		public int WhereHasBeenSplit{
			get{
				return _whereHasBeenSplit;
			}
			set{
				_whereHasBeenSplit = value;
			}
		}


		/// <summary>
		/// Given chromosome number, returns corresponding randomized chromosome.
		/// </summary>
		/// <param name="_genes">_genes.</param>
		/// <param name="bodyPart">Body part.</param>
        public Chromosome(int chromosomeNum)
        {
			MyDictionary.Init ();

			_chromosomeNumber = chromosomeNum;
			_bodyPart = (EnumBodyPart)chromosomeNum;
			_numOfGenes = MyDictionary.numOfGenesOnChromosome[_bodyPart];
			// Generating genes
			_genes = new Gene[_numOfGenes];
			for (int i=0; i<_numOfGenes; i++) {
				_genes[i] = new Gene(_chromosomeNumber,i);
			}

			_whereHasBeenSplit = -1;
        }


		/// <summary>
		/// Create deep copy clone of given chromosome. 
		/// </summary>
		/// <param name="ch">Ch.</param>
		public Chromosome(Chromosome ch){
			_bodyPart = ch._bodyPart;
			_chromosomeNumber = ch._chromosomeNumber;
			_numOfGenes = ch._numOfGenes;
			_genes = new Gene[_numOfGenes];
			for(int i=0; i<_numOfGenes; i++){
				_genes[i] = new Gene(ch._genes[i]);
			}

			_whereHasBeenSplit = ch._whereHasBeenSplit;
		}

		/// <summary>
		/// Creates new chromosome from array of genes. Used e.g. by FRONT-END when creating front-end representation of chromosomes.
		/// </summary>
		/// <param name="g">The green component.</param>
		public Chromosome(Gene[] g){
			_bodyPart = EnumBodyPart.NONE;
			_chromosomeNumber = -1;
			_numOfGenes = g.Length;
			_genes = g;
			_whereHasBeenSplit = -1; // hasn't been split yet
		}

		/// <summary>
		/// Create new (backend) chromosome from array of genes corresponding to chromosomeNumer
		/// </summary>
		/// <param name="g">The green component.</param>
		/// <param name="bodyPart">Body part.</param>
		public Chromosome(Gene[] g,int chromosomeNum){
			_bodyPart = (EnumBodyPart)chromosomeNum;

			_chromosomeNumber = chromosomeNum;
			_genes = g;
			_numOfGenes = MyDictionary.numOfGenesOnChromosome [(EnumBodyPart)_chromosomeNumber];
			_whereHasBeenSplit = -1;
		}


		/// <summary>
		/// Writes this chromosome (in consice form) into the specified file.
		/// </summary>
		public void Display(){
			String path = "E:\\Mato\\Cambridge\\2nd year\\Group_Project\\Software (under git)\\evolveapet\\evolveapet\\eap_frontend\\Assets\\Scripts\\Backend\\OutputOfTests\\Chromosomes\\";
			String dst = "Chromosome_" + _chromosomeNumber + ".txt";

			System.IO.StreamWriter file = new System.IO.StreamWriter(path+dst);
			file.AutoFlush = true;

			file.WriteLine ("CHROMOSOME #" + _chromosomeNumber);
			for(int i=0; i<_numOfGenes; i++){
				file.WriteLine("#" + i + ": \t\t" + _genes[i].GetWholeNameEncoded() + " (" + _genes[i].GetWholeNameDecoded() + ")");
			}

			file.Close ();

		}
		
		/// <summary>
		/// Given traitNumber (index into EnumTrait, returns the index of this trait on this chromosome.
		/// </summary>
		/// <returns>The trait position.</returns>
		/// <param name="traitNo">Trait no.</param>
		/// <param name="chromosomeNum">Chromosome number.</param>
		public int getTraitPosition(int traitNo){
			/*6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			*/
			for (int n = 0; n < _numOfGenes; n++) { //Searches through the dictionary until it finds the specified trait. 
				//This could simply be hardcoded, but a linear search won't take any time at all on 7 items.
				if ((int)(_genes[n].Trait) == traitNo) return n;
			}

			//Returns -1 if trait is not found on the gene.
			return -1;
		}
    }
}
