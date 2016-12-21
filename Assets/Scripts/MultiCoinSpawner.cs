using UnityEngine;
using System.Collections;

public class MultiCoinSpawner : MonoBehaviour {
	public GameObject coin;
    private GameObject coinSpawn;
	public AudioSource coinSound;
    private int coinsSpawned = 0;
    public bool coinSpawned = false;
    Vector3 pos = new Vector3(0f, 2f, 0f);
	Animator animator;

    // Use this for initialization
    void Start()
    {
		animator = GetComponent<Animator>();
    }
	// Update is called once per frame
	void Update () {
     
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
            if (coinSpawned == false && coinsSpawned < 11)
            {
                ScoreManager.score += 100;
                CoinManager.coinAmount += 1;
                coinSpawn = (GameObject) Instantiate(coin, transform.position + pos, transform.rotation);
				coinSound.Play();
                coinsSpawned += 1;
                Destroy(coinSpawn, 0.3f);
                if (coinsSpawned == 10)
                {
                    coinSpawned = true;
					animator.SetTrigger("Collision");
                }
                
            }
        }
    }
}
