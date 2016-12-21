using UnityEngine;
using System.Collections;

public class CoinSecretLevel : MonoBehaviour {

	public AudioSource coinSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			ScoreManager.score += 100;
			CoinManager.coinAmount += 1;
			coinSound.Play ();
			Destroy(gameObject, 0.3f);
		}
	}
}
