using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{
    [Serializable]
    public class Torso : StdBodyPart
    {
        public Torso( int shape, Color color, EnumSize size, EnumPattern pattern) : base(EnumBodyPart.TORSO, shape, color, size, pattern) { }
    }
}
