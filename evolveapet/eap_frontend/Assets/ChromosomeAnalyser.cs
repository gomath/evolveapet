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
					Gene g1 = genome.FrontEndGetGene(i, j, 1);
					pChromo.transform.FindChild("chromosome m").FindChild("gene "+j).GetComponent<PhysicalGene>().gene = g1;
					Gene g2 = genome.FrontEndGetGene(i, j, 0);
					pChromo.transform.FindChild("chromosome f").FindChild("gene "+j).GetComponent<PhysicalGene>().gene = g2;
				}
			}
		}

		public void SetActiveGene(GameObject g) {
			activeGene = g;
		}

		public void SetActiveChromosome(GameObject g) {
			if (!(trackUp&&trackDown)) return;
			if (activeChromosome != null) {
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
				yield return new WaitForSeconds(0f);
			}
			trackUp = true;
		}

		IEnumerator TrackDown() {
			trackDown = false;
			float start = activeChromosome.transform.position.y;
			while(activeChromosome.transform.position.y>start - translateBy) {
				activeChromosome.transform.Translate(-10*Vector2.up*Time.deltaTime);
				yield return new WaitForSeconds(0f);
			}
			trackDown = true;
		}
	}
}
