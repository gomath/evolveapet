using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvolveAPet;

namespace EvolveAPet
{
    [Serializable]
    public abstract class BodyPart
    {
		public readonly int[] colour;
		public readonly int size;
		public readonly string shape;
		public BodyPart (int[] rgbArray, int sizeNum, string shapeStr){
			colour = rgbArray;
			size = sizeNum;
			shape = shapeStr;

		}
		public BodyPart(){}// So C sharp stops moaning
	}

}
