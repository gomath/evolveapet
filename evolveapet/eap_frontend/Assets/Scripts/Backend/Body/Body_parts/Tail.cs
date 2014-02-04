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
    public class Tail : StdBodyPart
    {
        public Tail( int shape, Color color, EnumSize size, EnumPattern pattern) : base(EnumBodyPart.TAIL, shape, color, size, pattern) { }

    }
}
