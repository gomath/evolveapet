using UnityEngine;
using System.Collections;
using System;

namespace EvolveAPet{
public class GenomeViewController : MonoBehaviour {

	
	GameObject[] chromosomePairs;
	GameObject[,] physicalChromosomes;
	GameObject activePair;

	Player player;
		int pointsForCorrectGuess = 30;
		int pointsForIncorrectGuess = 10;
		int costOfGeneTherapy = 3; //20;
	Animal animal;
	Genome g;
	Chromosome[,] frontEndChromosomes;
	Gene[,] genes;
	int activeChromosome;
	int numOfGenesOnActiveChromosome;
	String[,] geneNamesToDisplay;

	QuickViewScript quickView;
	
	// GUI members
	public float originalWidth = 1098.0f;
	public float originalHeight = 618.0f;
	private float boxWidth = 165;
	private float boxWidth2 = 70;
	public float boxHeight = 50f;
	public float boxHeight2 = 50f;
	public float horizontalOffsetFromGene = 30f;
	public float verticalOffsetFromGene = 15f;
	public float horizontalGap = 15f;
	public float horizontalGap2 = 5f;

	
	Vector3 scale = new Vector3 ();
	public	GUISkin myskin;

	// Toolbar GUI stuff
	public int toolbarInt = 0;
	public string[] toolbarStrings = new string[] {"Genome","Gene therapy","Guess gene"};
	public string toolbarMode;
	
	// General GUI Popup stuff
	bool displayGuessPopup;
	bool displayTherapyPopup;
	String popupText = "";
	String popupHeading = "";
	
	
	// Guessing traits stuff
	String[,] guessingStrings;
	String[] correctGuesses;
	
	// Gene therapy stuff
	String[,,] randomMutationsNames;
	Gene[,,] randomMutations; // [father-0/mother-1][genes][mutations]
	int numOfMutationChoices = 2;

	// Use this for initialization
	void Start () {
			quickView = transform.FindChild ("QuickView").GetComponent<QuickViewScript> ();
			transform.FindChild ("ControlAnchors").GetComponent<ControlAnchorsScript> ().showBreedButton = false;

			// GETTING REAL ANIMAL AND PLAYER
			player = Player.playerInstance;
			animal = player.Stable.GetActiveAnimal ();
			g = animal.Genome;

			if (!animal.cacheInitialized) {
				InitializeCaches();
			}

			geneNamesToDisplay = new String[5,2];
			// Adding initialization code in hope to get rid of crazy null pointers
			for (int i=0; i<5; i++) {
				geneNamesToDisplay[i,0] = "";
				geneNamesToDisplay[i,1] = "";
			}

			// Guessing genes
			genes = new Gene[5,2];

			/*
			guessingStrings = new String[5,3];
			correctGuesses = new String[5];

			// Random mutations
			randomMutations = new Gene[2, 5, numOfMutationChoices];
			randomMutationsNames = new string[2, 5, numOfMutationChoices];
			// Initializing 3D array of names (to avoid that weird null pointer exception error)
			for (int i=0; i<2; i++) {
				for(int j=0; j<5; j++){
					for(int k=0; k<numOfMutationChoices; k++){
						randomMutationsNames[i,j,k] = "";
					}
				}			
			}*/


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
	/// Sets active caches both for guessing and breeding purposes. 
	/// </summary>
	void SetActiveCaches(){
			SetActiveCachesForGuessing ();
			SetActiveCachesForGeneTherapy ();
	}
	
	/// <summary>
	/// Sets internal caches for holding guessing information to appropriate entry in caches in animal object. 
	/// </summary>
	void SetActiveCachesForGuessing(){
			guessingStrings = animal.cacheGuessingStrings [activeChromosome];
			correctGuesses = animal.cacheCorrectGuesses [activeChromosome];
	}
	
	/// <summary>
	/// Sets internal caches for holding gene therapy information to appropriate entry in caches in animal object 
	/// </summary>
	void SetActiveCachesForGeneTherapy(){
			randomMutations = animal.cacheRandomMutations [activeChromosome];
			randomMutationsNames = animal.cacheRandomMutationNames [activeChromosome];
	}

	/// <summary>
	/// Initialize the caches for this animal concerning genes to guess and gene therapy to apply 
	/// </summary>
	void InitializeCaches(){

			animal.cacheGuessingStrings = new String[Global.NUM_OF_CHROMOSOMES][,];
			animal.cacheCorrectGuesses = new String[Global.NUM_OF_CHROMOSOMES][];

			animal.cacheRandomMutationNames = new String[Global.NUM_OF_CHROMOSOMES][,,];
			animal.cacheRandomMutations = new Gene[Global.NUM_OF_CHROMOSOMES][,,];

			for (int i=0; i<Global.NUM_OF_CHROMOSOMES; i++) {
				animal.cacheGuessingStrings[i] = new String[5,3];
				animal.cacheCorrectGuesses[i] = new String[5];

				animal.cacheRandomMutationNames[i] = new String[2, 5, numOfMutationChoices];
				animal.cacheRandomMutations[i] = new Gene[2, 5, numOfMutationChoices];
			}

			RandomizeAllCaches ();

			animal.cacheInitialized = true;
	}
	
	void RandomizeGuessingCachesAndUpdateActive(){
			RandomizeGuessingCache ();
			SetActiveCachesForGuessing();
	}

	void RandomizeGeneTherapyCachesAndUpdateActive(){
			RandomizeGeneTherapyCache ();
			SetActiveCachesForGeneTherapy ();
	}
	
	/// <summary>
	/// Randomize all caches for all chromosomes stored within animal object. 
	/// </summary>
	void RandomizeAllCaches(){
			RandomizeGuessingCache ();
			RandomizeGeneTherapyCache ();
	}
	/// <summary>
	/// Randomizes caches for all genes stored in animal object used for gene guessing 
	/// </summary>
	void RandomizeGuessingCache(){
			for (int ch=0; ch<Global.NUM_OF_CHROMOSOMES; ch++) {
					Gene temp;
					String s;
					String[] traitNamesForOneGene;
					for (int i=0; i<MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)ch]; i++) {
							temp = g.FrontEndGetGene (ch, i, 0); 
							s = temp.TraitName ();
							traitNamesForOneGene = g.GetDistinctRandomTraitNames (s, 3);
							for (int j=0; j<3; j++) {
								animal.cacheGuessingStrings[ch][i,j] = traitNamesForOneGene [j];
							}
		
							animal.cacheCorrectGuesses[ch][i] = s;			
					}
			}
	}
	/// <summary>
	/// Randomizes caches of all genes stored in animal object used for gene therapy 
	/// </summary>
	void RandomizeGeneTherapyCache(){
			for (int ch = 0; ch<Global.NUM_OF_CHROMOSOMES; ch++) {
				int numOfGenes = MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)ch]; 
					Gene temp;
					Gene[] tempArray;
					for (int i=0; i<numOfGenes; i++) {
							// Mutating father gene
							temp = g.FrontEndGetGene (ch, i, 0);
							tempArray = temp.RandomMutations (numOfMutationChoices);
							for (int j=0; j<numOfMutationChoices; j++) {
									animal.cacheRandomMutations[ch][0, i, j] = tempArray [j];
							}
	
							// Mutating mother gene
							temp = g.FrontEndGetGene (ch, i, 1);
							tempArray = temp.RandomMutations (numOfMutationChoices);
							for (int j=0; j<numOfMutationChoices; j++) {
									animal.cacheRandomMutations[ch][1, i, j] = tempArray [j];
							}
					}

					// Creating string[][][] array of names
					for (int i=0; i<numOfGenes; i++) {
							for (int j=0; j<numOfMutationChoices; j++) {
									if (player.guessedGenes [ch, i]) {
											animal.cacheRandomMutationNames[ch] [0, i, j] = animal.cacheRandomMutations[ch] [0, i, j].GetWholeNameDecoded ();
											animal.cacheRandomMutationNames[ch] [1, i, j] = animal.cacheRandomMutations[ch] [1, i, j].GetWholeNameDecoded ();
									} else {
											animal.cacheRandomMutationNames[ch] [0, i, j] = animal.cacheRandomMutations[ch] [0, i, j].GetWholeNameEncoded ();
											animal.cacheRandomMutationNames[ch] [1, i, j] = animal.cacheRandomMutations[ch] [1, i, j].GetWholeNameEncoded ();
									}
							}			
					}
			}
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
			//Debug.Log ("Box " + boxNumber + " clicked.");
			ActivateChromosome (boxNumber);
    }
	
	/// <summary>
	/// Called when different chromosome selected via clicking the box or using arrows. 
	/// </summary>
	/// <param name="chromosomeNum">Chromosome number.</param>
	void ActivateChromosome(int chromosomeNum){
			activeChromosome = chromosomeNum;
			numOfGenesOnActiveChromosome = MyDictionary.numOfGenesOnChromosome [(EnumBodyPart)activeChromosome]; //frontEndChromosomes [0, activeChromosome].NumOfGenes;

			SetActiveCachesForGuessing ();
			SetActiveCachesForGeneTherapy ();
			//CreateStringsForGuessing ();
			//CreateRandomMutations();

			UpdateNamesAndGenes ();

			if (activePair != null) {
				activePair.SetActive(false);
			}
			activePair = chromosomePairs[chromosomeNum]; 
			activePair.SetActive (true);
			quickView.SetActiveBox(chromosomeNum);   

	}
	/// <summary>
	/// Update gene names displayed next to chromosome 
	/// </summary>
	void UpdateNamesAndGenes(){
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
	
	/// <summary>
	/// Creating mutated genes to display on the screen 
	/// </summary>
	void CreateRandomMutations(){
			Gene temp;
			Gene[] tempArray;
			for (int i=0; i<numOfGenesOnActiveChromosome; i++) {
				// Mutating father gene
				temp = g.FrontEndGetGene(activeChromosome,i,0);
				tempArray = temp.RandomMutations(numOfMutationChoices);
				for(int j=0; j<numOfMutationChoices; j++){
					randomMutations[0,i,j] = tempArray[j];
				}


				// Mutating mother gene
				temp = g.FrontEndGetGene(activeChromosome,i,1);
				tempArray = temp.RandomMutations(numOfMutationChoices);
				for(int j=0; j<numOfMutationChoices; j++){
					randomMutations[1,i,j] = tempArray[j];
				}
			}

			// Creating string[][][] array of names
			for (int i=0; i<numOfGenesOnActiveChromosome; i++) {
				for(int j=0; j<numOfMutationChoices; j++){
					if(player.guessedGenes[activeChromosome,i]){
						randomMutationsNames[0,i,j] = randomMutations[0,i,j].GetWholeNameDecoded();
						randomMutationsNames[1,i,j] = randomMutations[1,i,j].GetWholeNameDecoded();
					} else {
						randomMutationsNames[0,i,j] = randomMutations[0,i,j].GetWholeNameEncoded();
						randomMutationsNames[1,i,j] = randomMutations[1,i,j].GetWholeNameEncoded();
					}
				}			
			}
	}

	void SetGeneColor(int ch, int g, bool toggled){
			Color c;
			Color temp;
			if(player.guessedGenes[ch,g])
				c = GeneScript.guessedColor;
			else
                c = GeneScript.unknownColor;

			if (toggled) {
				temp = physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor;
				c = Color.black; //new Color(temp.r, temp.g, temp.b, 255);
				physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
				temp = physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor;
				c = Color.black; //new Color(temp.r, temp.g, temp.b, 255);
				physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
			} else {
				physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				physicalChromosomes[ch,0].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
				physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<GeneScript>().actualColor = c;
				physicalChromosomes[ch,1].transform.FindChild("gene " + g).GetComponent<SpriteRenderer>().color = c;
			}


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
		player.Points += pointsForCorrectGuess;
		ChangeColorWhenGuessed (ch,g);
		UpdateNamesAndGenes ();
		RandomizeGuessingCachesAndUpdateActive ();
		RandomizeGeneTherapyCachesAndUpdateActive (); // to show decoded names of a options for gene therapy after guess

	}
	// Action to take when the player guesses incorrectly
	void IncorrectGuessAction(){
		animal.RemainingGuesses--;
		player.Points += pointsForIncorrectGuess;
		RandomizeGuessingCachesAndUpdateActive ();

	}
	
	// Action to take after successful mutation
	void RandomMutationAction(EnumBodyPart changedBodyPart){
		player.Points -= costOfGeneTherapy;
		UpdateNamesAndGenes();
		animal.createBodyPart((int)changedBodyPart);
		RandomizeGeneTherapyCachesAndUpdateActive ();
	}
	
	void OnGUI(){
			GUI.skin = myskin;
			
			scale.x = Screen.width / originalWidth;
			scale.y = Screen.height / originalHeight;
			scale.z = 1;
			var svMat = GUI.matrix;
			
			// substitute matrix to scale if screen nonstandard
			GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
			Vector3 u, v;

			u = Camera.main.WorldToScreenPoint (transform.FindChild("ControlAnchors").FindChild("Toolbar").position);
			v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);

			if (!displayGuessPopup && ! displayTherapyPopup) {
					String geneTherapyString = "Gene therapy";

					if(animal.RemainingGuesses == 0){
						if(player.Points >= costOfGeneTherapy){
							toolbarStrings = new string[] {"Genome",geneTherapyString};
							toolbarMode = "GENE_THERAPY";
						} else {
							toolbarStrings = new string[]{"Genome"};
							toolbarMode = "NONE";
						}
					} else {
						if(player.Points >= costOfGeneTherapy){
							toolbarStrings = new string[] {"Genome",geneTherapyString,"Guess genes"};
							toolbarMode = "BOTH";
						} else {
							toolbarStrings = new string[] {"Genome","Guess genes"};
							toolbarMode = "GUESSING";
						}
					}
					
					toolbarInt = GUI.SelectionGrid(new Rect (v.x, originalHeight - v.y, 300, 120), toolbarInt, toolbarStrings,2);
				
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
			
				GUI.Box(new Rect(v.x + horizontalOffsetFromGene,originalHeight - (v.y + verticalOffsetFromGene), boxWidth, boxHeight),geneNamesToDisplay[i,1]);
			}



			if ((toolbarInt == 2 && toolbarMode == "BOTH") || (toolbarInt == 1) && toolbarMode == "GUESSING") { // Guessing traits
				// TODO remove
				String correct = "Correct: ";

				for(int i=0; i<numOfGenesOnActiveChromosome; i++){
					u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome f").FindChild("gene " + i).position);
					v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);

					// TODO remove
					correct += correctGuesses[i] + ", ";
	

					for(int j=0; j<3; j++){
					if(!player.guessedGenes[activeChromosome,i]){
						if(displayGuessPopup){
							GUI.Box(new Rect(v.x + horizontalOffsetFromGene + boxWidth + horizontalGap + j*(boxWidth2+horizontalGap),originalHeight - (v.y + verticalOffsetFromGene), boxWidth2, boxHeight),guessingStrings[i,j] + "?");
						} else
						if(GUI.Button(new Rect(v.x + horizontalOffsetFromGene + boxWidth + horizontalGap + j*(boxWidth2+horizontalGap),originalHeight - (v.y + verticalOffsetFromGene), boxWidth2, boxHeight2),guessingStrings[i,j] + "?")){
							bool correctGuess = guessingStrings[i,j] == correctGuesses[i];
							if(correctGuess){
								popupHeading = "Correct";
								popupText = "Congratulations, you have correctly guessed this trait, earning " + pointsForCorrectGuess + " points. ";
								popupText += "You have still " + animal.RemainingGuesses + " guesses left for this animal."; 
								CorrectGuessAction(activeChromosome,i);
							} else {
								IncorrectGuessAction();
								popupHeading = "Incorrect";
								popupText = "Don't worry, you still get " + pointsForIncorrectGuess + " points. " +"\nRemaining number of attempts is " + animal.RemainingGuesses + ". ";
								if (animal.RemainingGuesses == 0){
									popupText += "Try breeding your animal or load another animal to guess!";
								}
							}
							displayGuessPopup = true;
						}
					}
					}
					if(displayGuessPopup){
						u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
						v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
						GUI.Window (0,new Rect(v.x,originalHeight-v.y,240,180),PopupOnGuess,popupHeading);
					}

				}
				// TODO remove
				//GUI.Box(new Rect(0,0,300,30),correct);

			} else if(toolbarInt == 1){ // Gene therapy - always first (if exists)

				for(int i=0; i<numOfGenesOnActiveChromosome; i++){
					for(int j = 0; j<numOfMutationChoices; j++){
						for(int k=0; k<2; k++){
							Rect buttonLocation;
							if(k == 0){
								u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome m").FindChild("gene " + i).position);
								v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
								buttonLocation = new Rect(v.x - horizontalOffsetFromGene - boxWidth - horizontalGap2 - boxWidth - j*(boxWidth+horizontalGap2),originalHeight - (v.y + verticalOffsetFromGene), boxWidth, boxHeight2);
							} else /*k == 1*/ {
								u = Camera.main.WorldToScreenPoint(activePair.transform.FindChild("chromosome f").FindChild("gene " + i).position);
								v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
								buttonLocation = new Rect(v.x + horizontalOffsetFromGene + boxWidth + horizontalGap2 + j*(boxWidth+horizontalGap2),originalHeight - (v.y + verticalOffsetFromGene), boxWidth, boxHeight2);
							}

							if(GUI.Button (buttonLocation,randomMutationsNames[k,i,j])){
								Gene temp = g.FrontEndGetGene(activeChromosome,i,k);
								String tempName = (player.guessedGenes[activeChromosome,i]) ? temp.GetWholeNameDecoded() : temp.GetWholeNameEncoded();

								if (temp.TraitName().Equals("shape")){ 
								    animal.geneTherapyOnShape = true;
								}


								temp.Mutate(randomMutations[k,i,j]);
								RandomMutationAction((EnumBodyPart)temp.ChromosomeNum);

								popupHeading = "Gene Therapy Successful";
								popupText = "You have successfuly applied Gene therapy to mutate gene \"" + tempName + "\" to gene \"" + randomMutationsNames[k,i,j] + "\", costing you " + costOfGeneTherapy + ". ";   
								popupText += "Return to the stable to view to see the changes in phenotype of the pet.";

								displayTherapyPopup = true;
							}
						}
					}
					if(displayTherapyPopup){
						u = Camera.main.WorldToScreenPoint(transform.FindChild("PopupAnchor").position);
						v = new Vector3 (originalWidth * u.x / Screen.width, originalHeight * u.y / Screen.height, 1f);
						GUI.Window (1,new Rect(v.x,originalHeight-v.y,240,300),PopupOnTherapy,popupHeading);
					}
				}

			}
	}

		void PopupOnGuess(int id){
			GUI.Box (new Rect(20,20,200,100),popupText);
			if (GUI.Button (new Rect (20, 130, 80, 40), "Close")) {
				CreateStringsForGuessing();

				displayGuessPopup = false;
			}
		}

		void PopupOnTherapy(int id){
			GUI.Box (new Rect(20,20,200,200),popupText);
			if (GUI.Button (new Rect (20, 250, 80, 40), "Close")) {

				displayTherapyPopup = false;
			}
		}

}

}
