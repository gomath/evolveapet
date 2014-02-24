using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class GenomeViewController : MonoBehaviour {

	
	GameObject[] chromosomePairs;
	GameObject[,] physicalChromosomes;
	GameObject activePair;

	Player player;
	Animal animal;
	Genome g;
	Chromosome[,] frontEndChromosomes;
	Gene[,] genes;
	int activeChromosome;
	int numOfGenesOnActiveChromosome;
	String[,] geneNamesToDisplay;
	String[,] guessingStrings;
	String[] correctGuesses;
	


	QuickViewScript quickView;
	
	// GUI members
	public float originalWidth = 1098.0f;
	public float originalHeight = 618.0f;
	private float boxWidth = 200;
	private float boxWidth2 = 70;
	public float boxHeight = 30f;
	public float horizontalOffsetFromGene = 30f;
	public float verticalOffsetFromGene = 15f;
	public float horizontalGap = 15f;

	
	Vector3 scale = new Vector3 ();
	public	GUISkin myskin;

	public int toolbarInt = 0;
	public string[] toolbarStrings = new string[] {"Gene therapy","Guess genes"};
	bool displayGuessPopup;
	bool displayTherapyPopup;
	String popupText = "";
	String popupHeading = "";


	// Use this for initialization
	void Start () {
			quickView = transform.FindChild ("QuickView").GetComponent<QuickViewScript> ();
			transform.FindChild ("ControlAnchors").GetComponent<ControlAnchorsScript> ().showBreedButton = false;

			animal = new Animal ();
			//g = new Genome ();
			g = animal.Genome;
			player = new Player(new Stable(), "DefaultPlayer");
				// TODO remove afterwards
				// Randomizing which genes has been guessed correctly
				for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
					for(int j=0; j<6; j++){
						if(Global.rand.Next(3) == 0){
							player.guessedGenes[i,j] = true;
						} else {
							player.guessedGenes[i,j] = false;
						}
					}
			
			}

			geneNamesToDisplay = new String[5,2];
			// Adding initialization code in hope to get rid of crazy null pointers
			for (int i=0; i<5; i++) {
				geneNamesToDisplay[i,0] = "";
				geneNamesToDisplay[i,1] = "";
			}
			genes = new Gene[5,2];
			guessingStrings = new String[5,3];
			correctGuesses = new String[5];

			frontEndChromosomes = g.FrontEndChromosomes();
			physicalChromosomes = new GameObject[Global.NUM_OF_CHROMOSOMES,2];
			chromosomePairs = new GameObject[Global.NUM_OF_CHROMOSOMES];

			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				chromosomePairs[i] = transform.FindChild ("Chromosomes").FindChild ("ch_pair_"+i).gameObject;


				physicalChromosomes[i,0] = GetPhysicalChromosome(i,"m");
				physicalChromosomes[i,1] = GetPhysicalChromosome(i,"f");

				physicalChromosomes[i,0].GetComponent<ChromosomeScript>().Chromosome = frontEndChromosomes[0,i];
				physicalChromosomes[i,1].GetComponent<ChromosomeScript>().Chromosome = frontEndChromosomes[1,i];

			}

			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				for(int j=0; j<MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)i]; j++){
					SetGeneColor(i,j,false);
				}
			}

			ActivateChromosome (3);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Returns male or female physical chromosome at given location 
	/// </summary>
	/// <returns>The physical chromosome.</returns>
	/// <param name="chromosomeNum">Chromosome number.</param>
	/// <param name="mf">Mf.</param>
	GameObject GetPhysicalChromosome(int chromosomeNum, String mf){
		return transform.FindChild("Chromosomes").FindChild("ch_pair_" + chromosomeNum).FindChild("chromosome " + mf).gameObject;			
	}
	

	void BoxClicked(int boxNumber){
			Debug.Log ("Box " + boxNumber + " clicked.");
			ActivateChromosome (boxNumber);
    }

	void ActivateChromosome(int chromosomeNum){
			activeChromosome = chromosomeNum;
			numOfGenesOnActiveChromosome = MyDictionary.numOfGenesOnChromosome [(EnumBodyPart)activeChromosome]; //frontEndChromosomes [0, activeChromosome].NumOfGenes;
			CreateStringsForGuessing ();


			for (int i=0; i<numOfGenesOnActiveChromosome; i++) {
				genes[i,0] = g.FrontEndGetGene(activeChromosome,i,0);
				genes[i,1] = g.FrontEndGetGene(activeChromosome,i,1);
				if(player.guessedGenes[activeChromosome,i]){
					geneNamesToDisplay[i,0] = genes[i,0].GetWholeNameDecoded();
					geneNamesToDisplay[i,1] = genes[i,1].GetWholeNameDecoded();
				} else {
					geneNamesToDisplay[i,0] = genes[i,0].GetWholeNameEncoded();
					geneNamesToDisplay[i,1] = genes[i,1].GetWholeNameEncoded();
				}
			}

			if (activePair != null) {
				activePair.SetActive(false);
			}
			activePair = chromosomePairs[chromosomeNum]; 
			activePair.SetActive (true);
			quickView.SetActiveBox(chromosomeNum);   

	}

	/// <summary>
	/// Creates  random strings out of possible traits for guessing purposes 
	/// </summary>
	void CreateStringsForGuessing(){
		Gene temp;
		String s;
		String[] traitNamesForOneGene;
		for (int i=0; i<numOfGenesOnActiveChromosome; i++) {
			temp = g.FrontEndGetGene(activeChromosome,i,0);
			s = temp.TraitName();
			traitNamesForOneGene = g.GetDistinctRandomTraitNames(s,3);
			for(int j=0; j<3; j++){
				guessingStrings[i,j] = traitNamesForOneGene[j];
			}

			correctGuesses[i] = s;
			
		}

	}

	void SetGeneColor(int ch, int g, bool toggled){
			Color c;
			if (toggled) {
				c = GeneScript.toggleColor;
			} else {
				if(player.guessedGenes[ch,g])
					c = GeneScript.guessedColor;
				else
                    c = GeneScript.unknownColor;
			}

		
			physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
			physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
			physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
			physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;


			quickView.SetGeneColor (ch, g, c);
    }

	void ChangeColorWhenGuessed(int ch, int g){
			physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = GeneScript.guessedColor;
			physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = GeneScript.guessedColor;
			physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = GeneScript.guessedColor;
			physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = GeneScript.guessedColor;
			quickView.SetGeneColor (ch, g,GeneScript.guessedColor);
	}
        



	// Set of methods for colouring the genes corresponding to various body parts
	void ToggleEyes(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.EYES);
		SetColorToGenes (on, loci);
		//Debug.Log ("EYES");
	}
	void ToggleEars(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.EARS);
		SetColorToGenes (on, loci);   
		//Debug.Log ("EARS");
	}
	
	void ToggleHead(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.HEAD);
		SetColorToGenes (on, loci);
		//Debug.Log ("HEAD");
		
	}
	void ToggleTorso(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.TORSO);
		SetColorToGenes (on, loci); 
		//Debug.Log ("TORSO");
	}
	void ToggleArms(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.ARMS);
		SetColorToGenes (on, loci);
		//Debug.Log ("ARMS");
	}
	void ToggleLegs(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.LEGS);
		SetColorToGenes (on, loci);
		//Debug.Log ("LEGS");
	}
	void ToggleTail(bool on){
		Locus[] loci = g.FrontEndGetLociByBodyPart (EnumBodyPart.TAIL);
		SetColorToGenes (on, loci);
		//Debug.Log ("TAIL");
	}
	
	// Sets colour to all genes on given loci
	void SetColorToGenes(bool on, Locus[] loci){
		foreach(Locus l in loci){
			int ch = l.Chromosome;
			int g = l.GeneNumber;
			SetGeneColor(ch,g,on);
			
        }
    }

	// Action to take when the player correctly guesses.
	void CorrectGuessAction(int ch, int g){
		player.guessedGenes [ch, g] = true;
		ChangeColorWhenGuessed (ch,g);
	}

	void IncorrectGuessAction(){

	}
	

	void OnGUI(){
			//GUI.skin = mySkin;
			
			scale.x = Screen.width / originalWidth;
			scale.y = Screen.height / originalHeight;
			scale.z = 1;
			var svMat = GUI.matrix;
			
			// substitute matrix to scale if screen nonstandard
			GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
			Vector3 u, v;
			u = Camera.main.WorldToScreenPoint (transform.FindChild("ControlAnchors").FindChild("Toolbar").position);
			v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);

			if (!displayGuessPopup) {
				toolbarInt = GUI.Toolbar (new Rect (v.x, originalHeight - v.y, 200, 50), toolbarInt, toolbarStrings);
			}

			for (int i=0; i<numOfGenesOnActiveChromosome; i++) {
				//Debug.Log (activeChromosome + " " + i);
				// Left chromosome (male)
				u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome m").FindChild("gene " + i).position);
				v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);

				// TODO This line gives sometimes random Null Pointer Exceptions (tried to fix that by initializing geneNamesToDisplay to all "")
				GUI.Box(new Rect(v.x - boxWidth - horizontalOffsetFromGene,originalHeight - (v.y + verticalOffsetFromGene), boxWidth, boxHeight),geneNamesToDisplay[i,0]);

				// Right chromosome (female)
				u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome f").FindChild("gene " + i).position);
				v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
			
				GUI.Box(new Rect(v.x + horizontalOffsetFromGene,originalHeight - (v.y + verticalOffsetFromGene), boxWidth, boxHeight),geneNamesToDisplay[i,0]);
			}



			if (toolbarInt == 1) { // Guessing traits
				// TODO remove
				String correct = "Correct: ";

				for(int i=0; i<numOfGenesOnActiveChromosome; i++){
					u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome f").FindChild("gene " + i).position);
					v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);

					// TODO remove
					correct += correctGuesses[i] + ", ";
	

					for(int j=0; j<3; j++){
						if(displayGuessPopup){
							GUI.Box(new Rect(v.x + horizontalOffsetFromGene + boxWidth + horizontalGap + j*(boxWidth2+horizontalGap),originalHeight - (v.y + verticalOffsetFromGene), boxWidth2, boxHeight),guessingStrings[i,j] + "?");
						} else
						if(GUI.Button(new Rect(v.x + horizontalOffsetFromGene + boxWidth + horizontalGap + j*(boxWidth2+horizontalGap),originalHeight - (v.y + verticalOffsetFromGene), boxWidth2, boxHeight),guessingStrings[i,j] + "?")){

							bool correctGuess = guessingStrings[i,j] == correctGuesses[i];
							if(correctGuess){
								popupHeading = "Correct";
								popupText = "Congratulations, you have correctly guessed this trait.";
								CorrectGuessAction(activeChromosome,i);
							} else {
								popupHeading = "Incorrect";
								popupText = "I am sorry, but that was rather wrong. Remaining number of attempts is X.";
							}
							displayGuessPopup = true;
						}
					}
					if(displayGuessPopup){
						u = Camera.main.WorldToScreenPoint(transform.FindChild("ControlAnchors").FindChild("PopupAnchor").position);
						v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
						GUI.Window (0,new Rect(v.x,originalHeight-v.y,240,100),PopupOnGuess,popupHeading);
					}

				}
				// TODO remove
				GUI.Box(new Rect(0,0,300,30),correct);
			} else if(toolbarInt == 0){ // Gene therapy

			}


	}

		void PopupOnGuess(int id){
			GUI.TextArea (new Rect(20,20,200,50),popupText);
			if (GUI.Button (new Rect (20, 70, 80, 20), "Close")) {
				CreateStringsForGuessing();
				displayGuessPopup = false;
			}


		}

}

}
