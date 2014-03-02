using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization; 

namespace EvolveAPet
{
	[Serializable]
    public class Stable
    {
		public int Size { set; get; }//number of living animals
		public int NumberOfUnlockedSlots = 3;
		public int activeAnimalNumber{ set; get; }
		public Animal eggSlot { set; get; }
		public bool[] activeStableSlots = {true,true,true,false,false,false};//The stable slots that are unlocked 
		public bool[] livingAnimals = {false,false,false,false,false,false};//The stable slots which contain a living animal
		public Animal[] animalsInStable = new Animal[6];

		public Animal GetActiveAnimal(){
			return animalsInStable[activeAnimalNumber];		
		}

		public Stable()
        {


        }
		private void getsize(){
			int temp=0;
			int temp2 = 0;
			for (int n = 0; n<animalsInStable.Length;n++){
				if (animalsInStable[n] !=null){
					temp++;
				}
				if (activeStableSlots[n]==true){
					temp2++;
				}
			}
			Size = temp;
			NumberOfUnlockedSlots = temp2;

		}
		
        public Stable(Animal[] pets)
        {
			animalsInStable = pets;
			getsize ();
        }

  		public void unlockStableSlot(int stableSlot){
			activeStableSlots [stableSlot] = true;	
			//animalsInStable [stableSlot] = new Animal ();
			getsize ();
		}

		public void AddPet(Animal a, int stableSlot)
        {
			animalsInStable [stableSlot] = a;
			livingAnimals [stableSlot] = true;
			getsize ();
			
        }


        public void RemovePet(int stableslot)
        {
			animalsInStable [stableslot] = null;
			livingAnimals [stableslot] = false;
			getsize ();
		}

    }
}
