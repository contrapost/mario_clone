using UnityEngine;
using System.Collections;

public class PlayButtonScript : MonoBehaviour {

	public static bool gameLoaded = false;
	public static int gameLoadedTimes = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void LoadGame () {
			Application.LoadLevel("loadGame");
			gameLoaded = true;
			gameLoadedTimes++;
	}

	void Update() {
		if (gameLoaded)
			gameObject.SetActive (false);
	}
}
