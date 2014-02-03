using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{   [Serializable]
    public abstract class StdBodyPart : BodyPart
    {
        public Color Color { get; protected set; }
        public EnumSize Size { get; protected set; }
        public EnumPattern Pattern { get; protected set; }

        public StdBodyPart(EnumBodyPart type, int shape, Color color, EnumSize size, EnumPattern pattern)
            : base(type, shape)
        {

            Color = color;
            Size = size;


        }
        
        
    }
}
