using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {


	public AnimationCurve curve;
	public GameObject debries;
	public AudioSource debriesSound;

	// Use this for initialization
	void Start () {

	}

	IEnumerator sample() { 
		// start position
		Vector2 pos = transform.position;
		
		// go through curve time
		for (float t=0; t < curve.keys[curve.length-1].time; t+=Time.deltaTime) {
			// move a bit
			transform.position = new Vector2(pos.x, pos.y + curve.Evaluate(t));
			
			// come back next Update
			yield return null;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if(!MarioController.marioIsBig) {StartCoroutine("sample");}
			else {
				Instantiate(debries, new Vector2(transform.position.x + 0.25f, transform.position.y  + 0.255f), 
				            transform.rotation);
				Destroy(gameObject);
				debriesSound.Play();
				GameObject.FindGameObjectWithTag("debris1").GetComponent<Rigidbody2D>().velocity = 
					new Vector2(-2, 4);
				GameObject.FindGameObjectWithTag("debris2").GetComponent<Rigidbody2D>().velocity = 
					new Vector2(2, 4);
				GameObject.FindGameObjectWithTag("debris3").GetComponent<Rigidbody2D>().velocity = 
					new Vector2(2, -4);
				GameObject.FindGameObjectWithTag("debris4").GetComponent<Rigidbody2D>().velocity = 
					new Vector2(-2, -4);
				Destroy(GameObject.FindGameObjectWithTag("debris1"), 0.5f);
				Destroy(GameObject.FindGameObjectWithTag("debris2"), 0.5f);
				Destroy(GameObject.FindGameObjectWithTag("debris3"), 0.5f);
				Destroy(GameObject.FindGameObjectWithTag("debris4"), 0.5f);
			}
		}
	}
}
