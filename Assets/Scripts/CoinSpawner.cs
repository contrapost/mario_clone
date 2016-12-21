using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {
	public GameObject coin;
    private GameObject coinSpawn;
	public AudioSource coinSound;
    public bool coinSpawned = false;
    Vector3 pos = new Vector3(0f, 2f, 0f);


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
            
            if (coinSpawned == false)
            {
                ScoreManager.score += 100;
                CoinManager.coinAmount += 1;
                coinSpawn = (GameObject) Instantiate(coin, transform.position + pos, transform.rotation);
				coinSound.Play ();
                coinSpawned = true;
                Destroy(coinSpawn, 0.3f);
                
            }
        }
    }
}
