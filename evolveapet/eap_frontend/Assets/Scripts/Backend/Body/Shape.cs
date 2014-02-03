using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
    public class Shape
    {
        private readonly Enum _shape;
        public string ShapeName { get { return Enum.GetName(_shape.GetType(), _shape); } }

        public Shape(EnumBodyPart type,int index)
        {

            switch (type)
            {
                case EnumBodyPart.HEAD: _shape = (EnumHeadShape)index; break;
                case EnumBodyPart.ARMS: _shape = (EnumArmShape)index; break;
                case EnumBodyPart.EARS: _shape = (EnumEarShape)index; break;
                case EnumBodyPart.EYES: _shape = (EnumEyeShape)index; break;
                case EnumBodyPart.TORSO: _shape = (EnumTorsoShape)index; break;
                case EnumBodyPart.TAIL: _shape = (EnumTailShape)index; break;
                case EnumBodyPart.LEGS: _shape = (EnumLegShape)index; break;
                default:_shape= (EnumTeeth)index; break;
            }

        }

        public static bool IsCorrectIndex(EnumBodyPart type, int index)
        {
            switch (type)
            {
                case EnumBodyPart.HEAD: return Enum.IsDefined(typeof(EnumHeadShape), index);
                case EnumBodyPart.ARMS: return Enum.IsDefined(typeof(EnumArmShape), index);
                case EnumBodyPart.EARS: return Enum.IsDefined(typeof(EnumEarShape), index);
                case EnumBodyPart.EYES: return Enum.IsDefined(typeof(EnumEyeShape), index);
                case EnumBodyPart.TORSO: return Enum.IsDefined(typeof(EnumTorsoShape), index);
                case EnumBodyPart.TAIL: return Enum.IsDefined(typeof(EnumTailShape), index);
                case EnumBodyPart.LEGS: return Enum.IsDefined(typeof(EnumLegShape), index);
    
            }
            return false;

        }

        private  enum EnumHeadShape
        {
            dino,
            wolf

        }
        private  enum EnumArmShape
        {
            dino,
            wolf

        }
        private  enum EnumEarShape
        {
            dino,
            wolf

        }
        private  enum EnumTorsoShape
        {
            dino,
            wolf

        }
        private  enum EnumTailShape
        {
            dino,
            wolf

        }
        private  enum EnumEyeShape
        {
            grumpy,
            angry

        }
        private  enum EnumLegShape
        {
            dino,
            wolf

        }
        private  enum EnumTeeth
        {
            carnivour,
            herbivour

        }
        

    }
}
