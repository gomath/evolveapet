using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{   [Serializable]
    public class Arms : FullBodyPart
    {
    public Arms( int shape, Color color, EnumSize size, EnumPattern pattern, int number) : base(EnumBodyPart.ARMS, shape, color, size, pattern, number) { }
  
    }
}
