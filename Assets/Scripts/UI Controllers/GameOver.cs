using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public AudioSource gameOverMusic;

	// Use this for initialization
	void Start () {
		gameOverMusic.Play ();

	}
	
	// Update is called once per frame
	void Update () {

		Invoke ("LoadMain", 4.0f);

	}

	void LoadMain(){
		Application.LoadLevel("main");
		PlayButtonScript.gameLoaded = false;
		LivesManager.live = 3;
		PlayerPrefs.SetInt("CurrentScore", 0); 
	}
}
