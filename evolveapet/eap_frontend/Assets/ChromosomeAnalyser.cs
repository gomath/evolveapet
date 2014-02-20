using UnityEngine;
using System.Collections;

namespace EvolveAPet{
	public class ChromosomeAnalyser : MonoBehaviour {

		public Genome genome { set; get; }

		public GameObject activeChromosome = null;
		public GameObject prevChromosome = null;
		public GameObject activeGene = null;

		public float translateBy = 0.001f;
		private bool trackDown = true;
		private bool trackUp = true;

		// Use this for initialization
		void Start () {
			genome = new Genome();
			PopulateGenes();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void PopulateGenes() {
			for (int i = 0; i<7; i++) {
				GameObject pChromo = GameObject.Find("chromosome pair "+i+"(Clone)");
				int k = 4;
				if (i == 1 || i == 2 || i == 4) k = 5;
				for (int j = 0; j<k; j++) {
					pChromo.transform.FindChild("chromosome m").FindChild("gene "+j).GetComponent<PhysicalGene>().gene = genome.FrontEndGetGene(i, j, 1);
					pChromo.transform.FindChild("chromosome f").FindChild("gene "+j).GetComponent<PhysicalGene>().gene = genome.FrontEndGetGene(i, j, 0);
				}
			}
		}

		public void SetActiveGene(GameObject g) {
			activeGene = g;
		}

		public void SetActiveChromosome(GameObject g) {
			if (!(trackUp&&trackDown)) return;
			if (activeChromosome != null) {
				if (g == activeChromosome) return;
				prevChromosome = activeChromosome;
				StartCoroutine("TrackUp");
			}
			activeChromosome = g;
			StartCoroutine ("TrackDown");
		}

		IEnumerator TrackUp() {
			trackUp = false;
			float start = prevChromosome.transform.position.y;
			while(prevChromosome.transform.position.y<start + translateBy) {
				prevChromosome.transform.Translate(10*Vector2.up*Time.deltaTime);
				if (prevChromosome.transform.position.y>start + translateBy) prevChromosome.transform.position = new Vector2(prevChromosome.transform.position.x, start + translateBy);
				yield return new WaitForSeconds(0f);
			}
			trackUp = true;
		}

		IEnumerator TrackDown() {
			trackDown = false;
			float start = activeChromosome.transform.position.y;
			while(activeChromosome.transform.position.y>start - translateBy) {
				activeChromosome.transform.Translate(-10*Vector2.up*Time.deltaTime);
				if (activeChromosome.transform.position.y<start - translateBy) activeChromosome.transform.position = new Vector2(activeChromosome.transform.position.x, start - translateBy);
				yield return new WaitForSeconds(0f);
			}
			trackDown = true;
		}

		void OnGUI() {
			float originalWidth = 876.0f;  // define here the original resolution
			float originalHeight = 493.0f; // you used to create the GUI contents 
			Vector3 scale;

			scale.x = Screen.width/originalWidth; // calculate hor scale
			scale.y = Screen.height/originalHeight; // calculate vert scale
			scale.z = 1;
			Matrix4x4 svMat = GUI.matrix; // save current matrix
			// substitute matrix - only scale is altered from standard
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

			if (trackDown && activeChromosome != null) {
				Vector2 pos = Camera.main.WorldToScreenPoint(activeChromosome.transform.position);
				GUILayout.BeginArea (new Rect (Screen.width*(pos.x+20)/originalWidth,2*originalHeight/3f,200,200));
				GUILayout.Box("Test");
				GUILayout.EndArea();
			}

			// restore matrix before returning
			GUI.matrix = svMat; // restore matrix
		}
	}
}
