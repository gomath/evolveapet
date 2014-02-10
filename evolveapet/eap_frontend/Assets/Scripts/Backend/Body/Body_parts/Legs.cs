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
    public class Legs : StdBodyPart
    {		public Legs(int[] rgbArray, int sizeNum, string shapeStr, int patternNumber): base(rgbArray, sizeNum, shapeStr,patternNumber){
		}
		public Legs(){}
    }
}
