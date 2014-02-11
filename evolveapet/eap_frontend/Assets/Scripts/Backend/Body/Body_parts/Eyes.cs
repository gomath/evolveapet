using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
using UnityEngine;
using EvolveAPet;


namespace EvolveAPet
{
    [Serializable]
    public class Eyes : BodyPart
    {
		public readonly int number;
		public Eyes(int[] rgbArray, int sizeNum, string shapeStr, int numberInput) : base(rgbArray, sizeNum, shapeStr){
		
			this.number = numberInput;
    
		}
		public Eyes(){}
	}
}

