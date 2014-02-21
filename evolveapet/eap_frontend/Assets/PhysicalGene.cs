using UnityEngine;
using System.Collections;

namespace EvolveAPet{
	public class PhysicalGene : MonoBehaviour {

		public Gene gene {set; get;}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnMouseDown() {
			//SendMessageUpwards("SetActiveGene", gameObject);
			//SendMessageUpwards("SetActiveChromosome",transform.parent.gameObject);
		}


	}
}
