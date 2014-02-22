using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Note - has to execute TetradsViewController first before executing this script!  
/// </summary>

namespace EvolveAPet{

public class TetradBehaviour : MonoBehaviour {

	private PhysicalChromosome activeChromosome;
		private int tetradNum;
		private int chromosomeNum;
	private GameObject myBox;

	public Chromosome UnderlyingChromosome{
		get{
			return activeChromosome.Chromosome;
		}
	}

	// Use this for initialization
	void Start () {
		tetradNum = Convert.ToInt32(name.Substring(name.Length-1,1));
		myBox = GameObject.Find ("Box" + tetradNum);
		
		// Setting chromosome numbers
			transform.FindChild ("chromosome pair " + tetradNum + "A").FindChild ("chromosome m").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 0;
			transform.FindChild ("chromosome pair " + tetradNum + "A").FindChild ("chromosome f").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 1;
			transform.FindChild ("chromosome pair " + tetradNum + "B").FindChild ("chromosome m").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 2;
			transform.FindChild ("chromosome pair " + tetradNum + "B").FindChild ("chromosome f").gameObject.GetComponent<PhysicalChromosome> ().chromosomeNumber = 3;
		// Randomly select one chromosome at the beginning
		string AB = "A";
		string mf = "m";
		int r = Global.rand.Next (4);
			switch(r){
			case 0: AB = "A"; mf = "m"; chromosomeNum = 0; break; 
			case 1: AB = "A"; mf = "f"; chromosomeNum = 1; break;
			case 2: AB = "B"; mf = "m"; chromosomeNum = 2; break;
			case 3: AB = "B"; mf = "f"; chromosomeNum = 3; break;
			}
		activeChromosome = transform.FindChild ("chromosome pair " + tetradNum + AB).FindChild("chromosome " + mf).gameObject.GetComponent<PhysicalChromosome>();
		ChromosomeClicked (activeChromosome);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChromosomeClicked(PhysicalChromosome chromosome){

		if (activeChromosome != null) {
			// Setting old centromere colour to white
			activeChromosome.transform.FindChild("centromere").FindChild("centromere colour").gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}
		// Setting new centrome colour to green
		activeChromosome = chromosome;
		activeChromosome.transform.FindChild ("centromere").FindChild ("centromere colour").gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		myBox.GetComponent<BoxBehaviour>().CreateChromosomeMirror(chromosome);
		myBox.GetComponent<BoxBehaviour> ().SetGenderSign (activeChromosome.chromosomeNumber);
	}

	void TranslateAllTetrads(){
		float offset = transform.localPosition.y;
		SendMessageUpwards ("TranslateAllTetradsByOffset", offset);
	}
	

	String GetGeneNamesInTetrad(int chromosomeNum,PhysicalChromosome[,] ch){
		String res = "";
		String f = "{0,-10}";
		for(int i=MyDictionary.numOfGenesOnChromosome[(EnumBodyPart)tetradNum]-1; i>=0; i--){
				int split = ch[tetradNum,chromosomeNum].Chromosome.WhereHasBeenSplit;
				String add = "";
				if(i >= split && split != -1){
					add = "-" + ch[tetradNum,chromosomeNum].Chromosome.Genes[i].GetWholeNameEncoded() + "-";
				} else {
					add = ch[tetradNum,chromosomeNum].Chromosome.Genes[i].GetWholeNameEncoded();
				}
				res += String.Format(f,add);
				res += "\n";
		}
		
		return res;
	}

	float originalWidth = 1098.0f;
	float originalHeight = 618.0f;
	
	Vector3 scale = new Vector3 ();
	public	GUISkin myskin;
	void OnGUI(){

			PhysicalChromosome[,] temp = transform.parent.parent.GetComponent<TetradsViewController> ().chromosomes;


			scale.x = Screen.width / originalWidth;
			scale.y = Screen.height / originalHeight;
			scale.z = 1;
			var svMat = GUI.matrix;
			
			// substitute matrix to scale if screen nonstandard
			GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
			Vector3 u = Camera.main.WorldToScreenPoint(transform.FindChild("description_anchor").position);
			Vector3 v = new Vector3(originalWidth*u.x/Screen.width,originalHeight*u.y/Screen.height,1f);
			int h = 100;
			int w = 60;

			string geneNames = GetGeneNamesInTetrad (0, temp);
			GUI.TextArea(new Rect (v.x, originalHeight - v.y, w, h), geneNames);
			geneNames = GetGeneNamesInTetrad (1, temp);
			GUI.TextArea(new Rect (v.x + w, originalHeight - v.y, w, h), geneNames);
			geneNames = GetGeneNamesInTetrad (2, temp);
			GUI.TextArea(new Rect (v.x, originalHeight - v.y + h, w, h), geneNames);
			geneNames = GetGeneNamesInTetrad (3, temp);
			GUI.TextArea(new Rect (v.x + w, originalHeight - v.y + h, w, h), geneNames);
			
	}
}
}
