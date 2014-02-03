using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EvolveAPet;


namespace EvolveAPet
{
    [Serializable]
    public class Head : StdBodyPart
    {
        private readonly Shape _teethShape;
        public string TeethShape { get { return _teethShape.ShapeName; } }

        public Head(int shape, Color color, EnumSize size, EnumPattern pattern, bool carnivour = false)
            : base(EnumBodyPart.HEAD, shape, color, size, pattern)
        {
             //since teeth is not considered a bodypart, -1 is somehting we are sure is not a valid item in our bodyparts
            _teethShape = new Shape((EnumBodyPart)(-1),Convert.ToInt32(carnivour));

        }

       

    }
}
