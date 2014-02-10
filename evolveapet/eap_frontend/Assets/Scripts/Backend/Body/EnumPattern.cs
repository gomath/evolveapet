using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{   [Serializable]
    public enum EnumPattern
    {
        // Do not change order, decoding of genes rely on this order
		NOPATTERN = 0,
        SPOTS = 1,
        STRIPES = 2,
        SPOTSTRIPES = 3
    }
}
