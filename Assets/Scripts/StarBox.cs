using UnityEngine;
using System.Collections;

public class StarBox : MonoBehaviour {

	public GameObject starPrefab;
	public AudioSource starSound;
	public bool powerupSpawned = false;
	Vector3 pos = new Vector3(0f, 1f, 0f);
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
			ScoreManager.score += 100;
			if (powerupSpawned == false)
			{
				Instantiate(starPrefab, transform.position + pos, transform.rotation);
				starSound.Play();
				powerupSpawned = true;
				animator.SetTrigger("Collision");
			}
			
		}
	}
}
