
using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
		if (MarioController.marioIsDead) {
			deathSound.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Invoke ("LoadMain", 4.0f);
		KoopaMovement.isKilledByKoopa = false;
		KoopaMovement.koopaIsDead = false;
	}

	void LoadMain(){
		Application.LoadLevel("main");
	}
}
