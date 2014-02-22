using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace EvolveAPet
{
	[Serializable]
    public class Player
    {

        public string NickName { set; get; }
        public string UserName { set; get; }
        public int Points { get; set; }
		public Stable _stable{get; set;}
		public int currentDailyChallenge;
		public DateTime dailyChallengeSetDate;
		public String[] allDailyChallenges = {"is the biggest","is the smallest","is the reddest","is the greenest", "is the bluest",  
			 "has spots on as many body parts as possible", "has stripes on as many body parts as possible"}; // add new challenges at end, do not change order
        public Stable Stable { get { return _stable; } }
		public bool[,] guessedGenes; // All the genes in the animal. If the array member at [i][j] is true, that means the the jth gene on the ith chromosome has been guessed
		public static Player playerInstance = null; // I should have thought of this earlier



		public Player(Stable s, string username)
        {
            Points = 0;
            UserName = username;
            NickName = username;
            _stable = s;
			guessedGenes = new bool[7, 6];//This wastes some space, but easy. 
			newDailyChallenge ();
			playerInstance = this;
        }
		
		public static void loadGame(){
			
			BinaryFormatter bf = new BinaryFormatter ();
			Player a;
			FileStream inStream = new FileStream (Environment.CurrentDirectory + "save.sav", FileMode.Open);
			a = bf.Deserialize(inStream) as Player;
			Player.playerInstance = a;
			
		}
		
		public void saveGame(){
			string path = Environment.CurrentDirectory + "save.sav";
			BinaryFormatter bf = new BinaryFormatter();
			FileStream outStream = new FileStream(path,FileMode.OpenOrCreate);
			bf.Serialize (outStream,this);
			outStream.Close();
		}

        // probably we want to serialize him to to save the game ...

		public void guessGene(int i, int j){

			guessedGenes [i,j] = true;
		
		}
		public String getDailyChallengeString(){
			String str = "Today's challenge is : Breed an animal which ";

			if (currentDailyChallenge == -1){
				str = "You have already completed a daily challenge today!";
			}
			else str += allDailyChallenges [currentDailyChallenge] + ".";
			return str;

		}
		public void newDailyChallenge(){
			if(DateTime.Compare(DateTime.Today, dailyChallengeSetDate) == 0) //Daily challenge was set today
			{
				currentDailyChallenge = -1;
			}

			else{		
				Random r = new Random();
			currentDailyChallenge = r.Next(0, allDailyChallenges.Length);
			dailyChallengeSetDate = DateTime.Today;
			}

		}
		public void completeDailyChallenge(){ // the int is the number of points in the 
			if (currentDailyChallenge != -1) {
						 // give no points in the event that there is no daily challenge
			int points = 0 ;
			int activeAnimalNo = Stable.activeAnimalNumber;
			Animal activeAnimal = Stable.ElementAt(activeAnimalNo);

			/*6 traits filled in this order :
			0.Colour
			1.Size
			2.Pattern
			3.Number
			4.Shape
			5.Teeth_Shape
			*/

			for (int n=0; n<Global.NUM_OF_CHROMOSOMES; n++) { // iterate through all the chromosomes
				if (currentDailyChallenge ==0 || currentDailyChallenge == 1){ // biggest or smallest
					int size = activeAnimal.Genome.GetTrait(n,1);
					if ((currentDailyChallenge ==0 && size == 2) || (currentDailyChallenge ==1 && size ==0)) points++;
				}
				if (currentDailyChallenge ==2|| currentDailyChallenge==3||currentDailyChallenge==4){ //reddest, greenest, bluest
					int colour = activeAnimal.Genome.GetTrait(n,0);
					// COLOUR = RED << 16 | GREEN << 8 | BLUE
					int red = (colour & 0x00FF0000) >> 16;
					int green = (colour & 0x0000FF00)>>8;
					int blue = (colour & 0x000000FF);
					if (currentDailyChallenge==2 && red>200) points++;
					if (currentDailyChallenge==3 && green>200) points++;
					if (currentDailyChallenge==4&& blue>200) points++;
				}
				if (currentDailyChallenge==5 || currentDailyChallenge==6){//most spots or stripes
					int patternNo = activeAnimal.Genome.GetTrait(n,2);
					if (currentDailyChallenge==5 && patternNo ==1) points++;
					if (currentDailyChallenge==6 && patternNo ==2) points++;
					if (patternNo==3) points++;
				}
			}

			Points = points;
			}
		}
        //Will be adding function for the KnownTraits but not boolean to boolean.
    }
}
