using UnityEngine;
using System.Collections;

public class StarMovement : MonoBehaviour {

	public float moveSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector2(moveSpeed, 0));
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Tube") {
			Destroy(gameObject);
		}
		if (coll.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}
	
	void OnCollisionEnter2D(Collider2D coll)
	{

	}
}

