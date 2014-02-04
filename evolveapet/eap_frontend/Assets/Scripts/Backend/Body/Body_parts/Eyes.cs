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
    public class Eyes : FullBodyPart
    {
        public Eyes( int shape, Color color, EnumSize size, EnumPattern pattern, int number) : base(EnumBodyPart.EYES, shape, color, size, pattern, number) { }
    }
}
