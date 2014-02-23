using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 

namespace EvolveAPet
{
	[Serializable]
    public class Stable : LinkedList<Animal>
    {
		public int Size { set; get; }
		public int activeAnimalNumber{ set; get; }
		public Animal eggSlot { set; get; }


        public Stable()
        {

            Size = 0;


        }
		public Stable(SerializationInfo info, StreamingContext context) : base(info, context) {
			
		}

        public Stable(LinkedList<Animal> pets)
            : base(pets)
        {

            Size = pets.Count;


        }

        public void AddPet(Animal a)
        {

            if (Count < Size)
                AddFirst(a);
            else
                throw new ApplicationException("stable is full");

        }

        public void RemovePet(Animal a)
        {

            Remove(a);

        }

        public bool IsFull()
        {

            return Size >= Count;

        }

    }
}
