using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace EvolveAPet
{
    public class SimpleBodyPart
    {

        public BodyPartType Type { get; protected set; }
        public BodyPartShape Shape { get; protected set; }

        public SimpleBodyPart(BodyPartType type, BodyPartShape shape)
        {

            Type = type;
            Shape = shape;

        }
        //WARNING:is might contain aditional (unwanted) data
        public SimpleBodyPart(string serializedBodyPart)
        {

            // deserialize the bodypart

        }

        public virtual string Serialize()
        {
            throw new NotImplementedException("serialize simplebodypart");
        }




    }

}
