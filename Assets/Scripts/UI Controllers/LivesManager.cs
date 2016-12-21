using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesManager : MonoBehaviour {

	public static int live = 3;
	
	Text text;
	
	// Use this for initialization
	void Awake () {
		
		text = GetComponent<Text> ();

	}
	
	
	// Update is called once per frame
	void Update () {
		text.text = " x " + live;
	}
}
