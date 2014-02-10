using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{   [Serializable]
    public abstract class StdBodyPart : BodyPart{
		public readonly int pattern;
		public StdBodyPart(int[] rgbArray, int sizeNum, string shapeStr, int patternNumber): base(rgbArray, sizeNum, shapeStr){
			this.pattern = patternNumber;
		}
		public StdBodyPart(){}
	}
    



}
