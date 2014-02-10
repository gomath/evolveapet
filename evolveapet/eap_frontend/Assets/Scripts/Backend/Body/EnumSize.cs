using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{   [Serializable]
    public enum EnumSize
    {
        // Do not change order. Decoding of size genes relies on this order.
		SMALL = 0,
        MEDIUM = 1,
        LARGE = 2
    }
}
