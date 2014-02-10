using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
using EvolveAPet;
using UnityEngine;


namespace EvolveAPet
{   [Serializable]
    public class Arms : StdBodyPart
    {
		public readonly int number;
		public bool isQuadrupedal;
		public Arms(int[] rgbArray, int sizeNum, string shapeStr, int patternNumber, int numberInput, bool quadrupedal): base(rgbArray, sizeNum, shapeStr, patternNumber){
			this.number = numberInput;
			this.isQuadrupedal = quadrupedal;
			
		}
		public Arms(){} // So C sharp stops moaning
    }
}
