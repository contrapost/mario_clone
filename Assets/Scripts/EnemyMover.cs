using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour{
    public float moveSpeed = 0f;
    Animator animator;
	public float upForce = 1000;
    public BoxCollider2D colliderBox;
	public EdgeCollider2D colliderEdge;
//    private bool triggered = false;
	public AudioSource marioCick;
	public EdgeCollider2D colliderEdgeCamera;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		if (coll.gameObject.tag == "Tube" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Koopa")
        {
            moveSpeed = moveSpeed * -1;
        }

		if (coll.gameObject.tag == "Koopa" && KoopaMovement.koopaIsDead) 
		{
			colliderBox.enabled = false;
			colliderEdge.enabled = false;
 			transform.localScale = new Vector2(transform.localScale.x,
			                                   -1 * transform.localScale.y);
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce);
			Invoke("Die", 3.3f);
		}

		if (coll.gameObject.tag == "CameraCollider") {
			Physics2D.IgnoreCollision(colliderEdgeCamera, colliderBox);
		}

		if (coll.gameObject.tag == "Player" && MarioController.marioIsInvisible) {
			marioCick.Play();
			ScoreManager.score += 100;
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Rigidbody2D>().gravityScale = 0f;
			transform.localScale = new Vector2(transform.localScale.x,
			                                   -1 * transform.localScale.y);
			moveSpeed = 0;
			Invoke("Die", 0.3f);
		}
     
        
      
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
			marioCick.Play();
            ScoreManager.score += 100;
            animator.SetTrigger("Died");
			GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce);
            moveSpeed = 0;
			Invoke("Die", 0.3f);
        }

        if (coll.gameObject.tag == "MainCamera")
        {

			moveSpeed = 0.02f;
        }

    }

	void Die() {
		Destroy(gameObject);
	}

}

