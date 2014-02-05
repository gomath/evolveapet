using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{   [Serializable]
    public abstract class StdBodyPart : BodyPart
    {
        public Color color { get; protected set; }
        public EnumSize Size { get; protected set; }
        public EnumPattern Pattern { get; protected set; }
		public EnumTrait Shape { get; protected set; }
        public StdBodyPart(EnumBodyPart type, int shape, Color color, EnumSize size, EnumPattern pattern)
            : base(type, shape)
        {

            this.color = color;
            Size = size;


        }
        
        
    }
}
