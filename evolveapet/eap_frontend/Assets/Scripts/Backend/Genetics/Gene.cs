using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Common;

namespace EvolveAPet
{
    public class Gene //: AGene
    {
        private readonly EnumTrait _trait;
        private readonly bool _type;
        private readonly char _symbol; // consider Dominant if uppercase
        private readonly int _data; // additional information needed for that 1 caracteristic
        // the students do not know this exist, this should be used only for our internal porpuses
        public bool Type { get { return _type; } }
        public char Symbol { get { return _symbol; } }
        public int Data { get { return _data; } }
        public EnumTrait trait { get { return _trait; } }
        public bool IsKnown { get; private set; }

        public Gene(char symbol, EnumTrait trait, int data = 0)
        {
            _trait = trait;
            _symbol = symbol;
            _data = data;
            _type = char.IsUpper(symbol);
            IsKnown = false;
        }

        public void Discovered()
        {
            IsKnown = true;
        }


        public string Serialize()
        {
            throw new NotImplementedException("serialize gene");
        }

        public Gene(string serialGene)
        {

            // deserializing a gene

        }

    }
}
