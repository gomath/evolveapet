using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
using UnityEngine;
using EvolveAPet;

namespace EvolveAPet
{   [Serializable]
    public abstract class FullBodyPart : StdBodyPart
    {
        public int Number { get; protected set; }
        public FullBodyPart(EnumBodyPart type, int shape, Color color, EnumSize size, EnumPattern pattern, int number)
            : base(type, shape, color, size, pattern)
        {

            Number = number;

        }


        
    }
}
