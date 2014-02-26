using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization; 

namespace EvolveAPet
{
	[Serializable]
    public class Stable
    {
		public int Size { set; get; }
		public int activeAnimalNumber{ set; get; }
		public Animal eggSlot { set; get; }
		public bool[] activeStableSlots = {true,true,true,false,false,false};
		public Animal[] animalsInStable = new Animal[6];

        public Stable()
        {


        }
		private void getsize(){
			int temp=0;
			for (int n = 0; n<animalsInStable.Length;n++){
				if (animalsInStable[n] !=null) temp++;
			}
			Size = temp;

		}
		
        public Stable(Animal[] pets)
        {
			animalsInStable = pets;
			getsize ();
        }

  		public void unlockStableSlot(int stableSlot){
			activeStableSlots [stableSlot] = true;		
		}

		public void AddPet(Animal a, int stableSlot)
        {
			animalsInStable [stableSlot] = a;
			getsize ();
        }


        public void RemovePet(int stableslot)
        {
			animalsInStable [stableslot] = null;
			getsize ();
		}

    }
}
