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
		public readonly string teethShape;
		public Head(int[] rgbArray, int sizeNum, string shapeStr, int patternNum, string teeth): base(rgbArray, sizeNum, shapeStr, patternNum) {
			this.teethShape = teeth;
			
		}
		public Head(){}
	}
}
