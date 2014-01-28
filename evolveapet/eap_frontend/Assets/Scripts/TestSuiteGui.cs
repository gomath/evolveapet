using UnityEngine;
using System.Collections;

public class TestSuiteGui : MonoBehaviour {

	private string[] parts = {"Body", "Head", "Tail", "Arms", "Legs", "Ears"};
	private int partSelected;
	private Color bodyColor = Color.white;
	private Color headColor = Color.white;
	private Color tailColor = Color.white;
	private Color armColor = Color.white;
	private Color legColor = Color.white;
	private Color earColor = Color.white;

	private string[] eyes = {"0","2","4"};
	private int numEye = 1;
	private int eyeSizeInt = 1;
	private Color eyeColor = Color.white;

	private string[] size = {"Small","Medium","Big"};
	private int bodySizeInt = 1;

	private int headSizeInt = 1;

	private int tailSizeInt = 1;

	private int armSizeInt = 1;

	private int legSizeInt = 1;

	private int earSizeInt = 1;

	private string[] teeth = {"Carnivore", "Vegetarian"};
	private int vege;

	private string[] legs = {"Quadrupedal", "Bipedal"};
	private int bipedalInt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	Color RGBSlider(Color rgb) {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Red", GUILayout.Width (40));
		GUI.color = Color.red;
		rgb.r = GUILayout.HorizontalSlider(rgb.r,0,1);
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
		GUILayout.BeginHorizontal();
		GUILayout.Label("Green", GUILayout.Width (40));
		GUI.color = Color.green;
		rgb.g = GUILayout.HorizontalSlider(rgb.g,0,1);
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
		GUILayout.BeginHorizontal();
		GUILayout.Label("Blue", GUILayout.Width (40));
		GUI.color = Color.blue;
		rgb.b = GUILayout.HorizontalSlider(rgb.b,0,1);
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
		return rgb;
	}


	void OnGUI()
	{
		// Wrap everything in the designated GUI Area on the left
		GUILayout.BeginArea (new Rect (0,0,200,Screen.height));

		//Label
		GUILayout.Box ("Animal colour");

		//Choose which part to colour
		partSelected = GUILayout.SelectionGrid(partSelected, parts, 3);
		
		// Place the appropriate colour sliders
		switch(partSelected) {
		case(0):
			bodyColor = RGBSlider(bodyColor);
			break;
		case(1):
			headColor = RGBSlider(headColor);
			break;
		case(2):
			tailColor = RGBSlider(tailColor);
			break;
		case(3):
			armColor = RGBSlider(armColor);
			break;
		case(4):
			legColor = RGBSlider(legColor);
			break;
		case(5):
			earColor = RGBSlider(earColor);
			break;
		}

		//Label
		GUILayout.Box ("Eye colour");
		
		// Place the eye color gene Toolbar
		eyeColor = RGBSlider (eyeColor);

		//Label
		GUILayout.Box ("Bipedal?");
		
		// Place the bipedal Toolbar
		bipedalInt = GUILayout.Toolbar (bipedalInt, legs);

		//Label
		GUILayout.Box ("Mouth type?");
		
		// Place the mouth type Toolbar
		vege = GUILayout.Toolbar (vege, teeth);

		//Label
		GUILayout.Box ("Number of eyes?");
		
		// Place the eye size Toolbar
		numEye = GUILayout.Toolbar (numEye, eyes);

		// End the Area
		GUILayout.EndArea();

		// Wrap everything in the designated GUI Area on the right
		GUILayout.BeginArea (new Rect (Screen.width-200,0,200,Screen.height));
		
		//Label
		GUILayout.Box ("Body size");
		
		// Place body size Toolbar
		bodySizeInt = GUILayout.Toolbar (bodySizeInt, size);

		//Label
		GUILayout.Box ("Head size");
		
		// Place the head size Toolbar
		headSizeInt = GUILayout.Toolbar (headSizeInt, size);

		//Label
		GUILayout.Box ("Tail size");
		
		// Place the tail size Toolbar
		tailSizeInt = GUILayout.Toolbar (tailSizeInt, size);

		//Label
		GUILayout.Box ("Arm size");
		
		// Place the arm size Toolbar
		armSizeInt = GUILayout.Toolbar (armSizeInt, size);

		//Label
		GUILayout.Box ("Leg size");
		
		// Place the leg size Toolbar
		legSizeInt = GUILayout.Toolbar (legSizeInt, size);

		//Label
		GUILayout.Box ("Ear size");
		
		// Place the ear size Toolbar
		earSizeInt = GUILayout.Toolbar (earSizeInt, size);

		//Label
		GUILayout.Box ("Eye size");
		
		// Place the eye size Toolbar
		eyeSizeInt = GUILayout.Toolbar (eyeSizeInt, size);
		
		// End the Area
		GUILayout.EndArea();

		//Create a build button, bottom right
		if (GUI.Button (new Rect(Screen.width-200,Screen.height-30,200, 30), "Build")) {
			StartCoroutine("Build");
		}
	}

	IEnumerator Build() {
		//Remove the previous animal
		GameObject.Destroy(GameObject.FindGameObjectWithTag("Animal"));
		Resources.UnloadUnusedAssets();

		//Wait one frame for destroys to commit
		yield return new WaitForSeconds(0f);

		//Create new animal
		GameObject animal = (GameObject)Instantiate(Resources.Load ("Prefabs/animal"));

		//Build body, rotate if bipedal and scale/colour as needed
		GameObject body = (GameObject)Instantiate(Resources.Load ("Prefabs/dino body"));
		Scale(body.transform, bodySizeInt);
		body.GetComponent<SpriteRenderer>().color = bodyColor;
		if (bipedalInt == 1) {
			body.transform.Rotate(new Vector3(0,0,-30));
		}
		body.transform.parent = animal.transform;

		//build head, color eyes, choose mouth and scale/colour as needed
		GameObject head = (GameObject)Instantiate(Resources.Load ("Prefabs/dino head"));
		Scale(head.transform, headSizeInt);
		head.GetComponent<SpriteRenderer>().color = headColor;
		head.transform.position = GameObject.Find ("head joint").transform.position;
		head.transform.parent = animal.transform;

		for (int i = 0; i<numEye; i++) {
			GameObject eye = (GameObject)Instantiate (Resources.Load ("Prefabs/dino eye"));
			eye.transform.position = GameObject.Find("eye joint "+i).transform.position;
			eye.transform.parent = head.transform;
			Scale (eye.transform, eyeSizeInt);
			eye.GetComponent<SpriteRenderer>().color = eyeColor;
		}

		GameObject.Find ("mouth").GetComponent<SpriteRenderer>().sprite = (vege == 1) ? Resources.Load<Sprite>("Sprites/dino vmouth") : Resources.Load<Sprite>("Sprites/dino mouth");

		//build tail, scale/colour as needed
		GameObject tail = (GameObject)Instantiate(Resources.Load ("Prefabs/dino tail"));
		Scale(tail.transform, tailSizeInt);
		tail.GetComponent<SpriteRenderer>().color = tailColor;
		tail.transform.position = GameObject.Find ("tail joint").transform.position;
		tail.transform.parent = animal.transform;

		//build arms, replace with legs if bipedal and scale/colour as needed
		GameObject arm = (bipedalInt==0) ? (GameObject)Instantiate(Resources.Load ("Prefabs/dino front leg")) : (GameObject)Instantiate(Resources.Load ("Prefabs/dino front arm"));
		Scale(arm.transform, armSizeInt);
		arm.GetComponent<SpriteRenderer>().color = armColor;
		arm.transform.position = GameObject.Find ("arm joint").transform.position;
		arm.transform.parent = animal.transform;

		arm = (bipedalInt==0) ? (GameObject)Instantiate(Resources.Load ("Prefabs/dino front leg")) : (GameObject)Instantiate(Resources.Load ("Prefabs/dino front arm"));
		Scale(arm.transform, armSizeInt);
		arm.GetComponent<SpriteRenderer>().color = armColor;
		arm.transform.position = GameObject.Find ("arm joint").transform.position;
		arm.transform.parent = animal.transform;

		//build legs, scale/colour as needed
		GameObject leg = (GameObject)Instantiate(Resources.Load ("Prefabs/dino front leg"));
		Scale(leg.transform, legSizeInt);
		leg.GetComponent<SpriteRenderer>().color = legColor;
		leg.transform.position = GameObject.Find ("leg joint").transform.position;
		leg.transform.parent = animal.transform;

		leg = (GameObject)Instantiate(Resources.Load ("Prefabs/dino back leg"));
		Scale(leg.transform, legSizeInt);
		leg.GetComponent<SpriteRenderer>().color = legColor;
		leg.transform.position = GameObject.Find ("leg joint").transform.position;
		leg.transform.parent = animal.transform;
	}

	//Parse the scale ints and scale accordingly
	void Scale (Transform t, int size) {
		float f = (size+1)/2F;
		t.localScale = new Vector2(f,f);
	}
}
