using UnityEngine;
using System.Collections;

public class PowerUpBlock : MonoBehaviour {
    public GameObject powerupPrefab;
	public AudioSource powerUpSound;
    public bool powerupSpawned = false;
    Vector3 pos = new Vector3(0f, 1f, 0f);


    // Use this for initialization
    void Start()
    {
       
    }
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ScoreManager.score += 100;
            if (powerupSpawned == false)
            {
                Instantiate(powerupPrefab, transform.position + pos, transform.rotation);
				powerUpSound.Play();
                powerupSpawned = true;
            }

        }
    }
}
