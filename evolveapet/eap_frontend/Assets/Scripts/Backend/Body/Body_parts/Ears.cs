using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{   
    [Serializable]
    public class Ears : StdBodyPart
    {
        public Ears(int shape, Color color, EnumSize size, EnumPattern pattern) : base(EnumBodyPart.EARS, shape, color, size, pattern) { }
    }
}
