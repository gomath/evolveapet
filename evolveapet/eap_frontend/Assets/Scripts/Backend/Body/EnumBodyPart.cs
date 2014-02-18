using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{   [Serializable]
    public enum EnumBodyPart
    {

        EARS = 0,
        EYES = 1,
        HEAD = 2,
        TORSO = 3,
        ARMS = 4,
        LEGS = 5,
        TAIL = 6,
		NONE = 7 // don't remove, it is there for purposes of front-end chromosomes
    }
}
