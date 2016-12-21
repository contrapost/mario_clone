using UnityEngine;
using System.Collections;

public class KoopaMovement : MonoBehaviour {


	public GameObject mario;
	public float speed = 0;
	Vector2 dir = Vector2.right;
//	public float upForce = 800;
	public static bool koopaIsDead = false;
	public static bool isKilledByKoopa = false;
	public EdgeCollider2D edgeColl;
	public CircleCollider2D circleColl;
	public AudioSource marioCick;
	public EdgeCollider2D colliderEdgeCamera;

	void Start() {
		transform.localScale = new Vector2(-1 * transform.localScale.x,
		                                   transform.localScale.y);
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = dir * speed;

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Tube" || coll.gameObject.tag == "Enemy") {
			transform.localScale = new Vector2(-1 * transform.localScale.x,
			                                   transform.localScale.y);
			

			dir = new Vector2(-1 * dir.x, dir.y);
		}


		if (coll.gameObject.tag == "Player" && koopaIsDead) {
				if(mario.GetComponent<Rigidbody2D>().transform.position.x < gameObject.transform.position.x) {
					dir = Vector2.right;
					speed = 6.0f;
				} else {
					dir = -1 * Vector2.right;
					speed = 6.0f;
				}
			}

		if (coll.gameObject.tag == "CameraCollider") {
		Physics2D.IgnoreCollision(colliderEdgeCamera, circleColl);
		}

		if (coll.gameObject.tag == "Player" && MarioController.marioIsInvisible) {
			marioCick.Play();
			ScoreManager.score += 100;
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Rigidbody2D>().gravityScale = 0f;
			transform.localScale = new Vector2(transform.localScale.x,
			                                   -1 * transform.localScale.y);
			speed = 0;
			Invoke("Die", 0.3f);
		}
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") 
		{
			marioCick.Play();
			GetComponent<Animator>().SetTrigger("Died");
			koopaIsDead = true;
			Debug.Log ("KOOPPPAPAPP" + koopaIsDead);
//			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce);
			speed = 0.0f;
			GetComponent<Collider2D>().sharedMaterial.friction = 0.0f;
			GetComponent<Rigidbody2D>().mass = 1.0f;
			edgeColl.enabled = false;
			circleColl.GetComponent<CircleCollider2D>().radius = 0.4f;
			circleColl.GetComponent<CircleCollider2D>().offset = new Vector2 (0, -0.34f);
		}

		if (coll.gameObject.tag == "MainCamera")
		{
			speed = 3.0f;
		}

	}
}
