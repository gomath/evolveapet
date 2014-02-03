using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class BodyPartShape
    {
        private readonly Enum _shape;
        public Enum Shape { get { return _shape; } }

        public BodyPartShape(BodyPartType type, int index)
        {

            switch (type)
            {
                case BodyPartType.HEAD: _shape = (HeadShape)index; break;
                case BodyPartType.TEETH: _shape = (TeethShape)index; break;
                case BodyPartType.ARMS: _shape = (ArmShape)index; break;
                case BodyPartType.EARS: _shape = (EarShape)index; break;
                case BodyPartType.EYES: _shape = (EyeShape)index; break;
                case BodyPartType.TORSO: _shape = (TorsoShape)index; break;
                case BodyPartType.TAIL: _shape = (TailShape)index; break;
                case BodyPartType.LEGS: _shape = (LegShape)index; break;
            }

        }

        public static bool IsCorrectIndex(BodyPartType type, int index)
        {
            switch (type)
            {
                case BodyPartType.HEAD: return Enum.IsDefined(typeof(HeadShape), index);
                case BodyPartType.TEETH: return Enum.IsDefined(typeof(TeethShape), index);
                case BodyPartType.ARMS: return Enum.IsDefined(typeof(ArmShape), index);
                case BodyPartType.EARS: return Enum.IsDefined(typeof(EarShape), index);
                case BodyPartType.EYES: return Enum.IsDefined(typeof(EyeShape), index);
                case BodyPartType.TORSO: return Enum.IsDefined(typeof(TorsoShape), index);
                case BodyPartType.TAIL: return Enum.IsDefined(typeof(TailShape), index);
                case BodyPartType.LEGS: return Enum.IsDefined(typeof(LegShape), index);

            }
            return false;

        }
    }
}
