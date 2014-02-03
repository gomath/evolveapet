using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvolveAPet;

namespace EvolveAPet
{
    [Serializable]
    public abstract class BodyPart
    {
        private readonly Shape _shape;
        public EnumBodyPart Type { get; protected set; }

        public string ShapeName { get { return _shape.ShapeName; } }
        public BodyPart(EnumBodyPart type, int shape)
        {

            Type = type;
            if (Shape.IsCorrectIndex(type, shape))
                _shape = new Shape(type, shape);
            else
                throw new ApplicationException("wrong ShAPE");

        }





    }

}
