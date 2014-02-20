using UnityEngine;
using System.Collections;

namespace EvolveAPet{

public class PhysicalChromosome : MonoBehaviour {
	
	private Chromosome chromosome;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		SendMessageUpwards("ChromosomeClicked",this);

	}

	public void PutMyTetradIntoCentre(){
		SendMessageUpwards ("TranslateAllTetrads");
	}


}
}
