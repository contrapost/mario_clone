using UnityEngine;
using System.Collections;

public class QuestionBlockDestroyer : MonoBehaviour {
    Animator animator;
	public AnimationCurve curve;
    
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
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
			StartCoroutine("sample");
			animator.SetTrigger("Collision");
		}
	}


	void OnCollisionEnter2D(Collision2D coll) {
		// collision below?
		if (coll.contacts[0].point.y < transform.position.y) {

			}
	}
}
