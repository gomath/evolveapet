using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Common;


namespace EvolveAPet
{
    public class StandardBodyPart : SimpleBodyPart
    {
        public Color Color { get; protected set; }
        public BodyPartSize Size { get; protected set; }
        public BodyPartPattern Pattern { get; protected set; }

        public StandardBodyPart(BodyPartType type, BodyPartShape shape, Color color, BodyPartSize size, BodyPartPattern pattern)
            : base(type, shape)
        {

            Color = color;
            Size = size;


        }
        //WARNING:is might contain aditional (unwanted) data
        public StandardBodyPart(string serializedBodyPart)
            : base(serializedBodyPart)
        {


            // deserialize the body part
        }

        public override string Serialize()
        {
            throw new NotImplementedException("serialize standardbodypart");
        }




    }
}
