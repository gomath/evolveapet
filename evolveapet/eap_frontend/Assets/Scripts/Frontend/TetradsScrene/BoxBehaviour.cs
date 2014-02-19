using UnityEngine;
using System.Collections;

namespace EvolveAPet{
public class BoxBehaviour : MonoBehaviour {
	
	private PhysicalChromosome chromosome = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateChromosomeMirror(PhysicalChromosome ch){
			chromosome = ch;
	}

	void OnMouseDown(){
		if (chromosome != null) {
			chromosome.transform.FindChild("centromere").FindChild("centromere colour").gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
			chromosome.PutMyTetradIntoCentre();
		}
	}

}
}
