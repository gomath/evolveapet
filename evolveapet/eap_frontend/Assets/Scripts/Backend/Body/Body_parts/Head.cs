using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EvolveAPet
{
    [Serializable]
    public class Head : StdBodyPart
    {
		public readonly bool isCarnivorous;
		public Head(int[] rgbArray, int sizeNum, string shapeStr, int patternNum, bool carnivore): base(rgbArray, sizeNum, shapeStr, patternNum) {
			this.isCarnivorous = carnivore;
			
		}
		public Head(){}
	}
}
