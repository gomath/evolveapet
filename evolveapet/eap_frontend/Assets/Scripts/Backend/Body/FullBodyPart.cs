using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Common;

namespace EvolveAPet
{
    public class CollectionBodyPart : StandardBodyPart
    {
        public int Number { get; protected set; }
        public CollectionBodyPart(BodyPartType type, BodyPartShape shape, Color color, BodyPartSize size, BodyPartPattern pattern, int number)
            : base(type, shape, color, size, pattern)
        {

            Number = number;

        }

        //WARNING:is might contain aditional (unwanted) data
        public CollectionBodyPart(string serializedBodyPart)
            : base(serializedBodyPart)
        {


            // deserialize the body part
        }


        public override string Serialize()
        {
            throw new NotImplementedException("serialize simplebodypart");
        }



    }
}
