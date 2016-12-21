using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {
	public GameObject mario;
	public float normalSpeed = 6f;
	public float sprintSpeed = 10f;
	[Range(0,1)] public float sliding = 0.9f;
	private bool facingRight = true;
	Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
	public CircleCollider2D collider1;
	public BoxCollider2D collider2;
	public static bool marioIsDead = false;
	public static bool marioIsBig = false;
	public static bool marioIsInvisible = false;
	bool MarioIsHittenWhenBig = false;
	float move;
    private bool goal = false;
    public BoxCollider2D flag;
	int flagscore;
	public static bool win = false;
	private bool IsSitting = false;

	//Sounds
	public AudioSource jumpSmall;
	public AudioSource jumpBig;
	public AudioSource eatingPowerUp;	
	public AudioSource deathSound;
	public AudioSource stageClaer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		TimeManager.restTime = 380;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!marioIsDead) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		} else {
			grounded = true;
		}

		anim.SetBool ("Ground", grounded);



		if (PlayButtonScript.gameLoaded) {
			move = Input.GetAxis ("Horizontal");
		}
        if (!goal)
        {
			if (!marioIsBig) {anim.SetFloat("speed", Mathf.Abs(move));}
			else {anim.SetFloat("speedBig", Mathf.Abs(move));}
            


			if (move != 0) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * sprintSpeed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetBool("Running", true);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * normalSpeed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetBool("Running", false);
            }
			} else { GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * sliding, GetComponent<Rigidbody2D>().velocity.y);}
        }
			
			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}

		if (Input.GetKey (KeyCode.DownArrow)) {
			anim.SetBool ("IsSitting", true);
			IsSitting = true;
		} else {
			anim.SetBool ("IsSitting", false);
			IsSitting = false;
		}
        
		if (gameObject.transform.position.y < -1.5f && !marioIsDead) {
			gameObject.SetActive(false);
			LivesManager.live--;
			if (LivesManager.live > 0) {
				Application.LoadLevel ("loadGame");
			} else {
				Application.LoadLevel("gameOver");
			}
		}

		/*Inngang i "secret level"
		if ((gameObject.transform.position.x > 23.3f && gameObject.transform.position.x < 24f) && IsSitting) {
			Application.LoadLevel("secret");
		}*/
	}

	void Update() {
		if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))){
			if (marioIsBig) {jumpBig.Play();}
			else {jumpSmall.Play();}
			anim.SetBool("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}

	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void BecomeSmaller() {
		anim.SetBool("MarioIsBig", false);
		marioIsBig = false;
		MarioIsHittenWhenBig = false;
		anim.SetBool("MarioIsHittenWhenBig", false);
	}

	void BecomeVisible() {
		marioIsInvisible = false;
		anim.SetBool("MarioIsInvisible", false);
		Debug.Log ("Visible"  + " " + Time.time);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
		if ((coll.gameObject.tag == "Enemy" || (coll.gameObject.tag == "Koopa" && !KoopaMovement.koopaIsDead)) && !marioIsInvisible) {
			if (!marioIsBig) {
				deathSound.Play();
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				anim.SetTrigger ("MarioDead");
				marioIsDead = true;
				Debug.Log("IsDead" + marioIsDead);
				collider1.enabled = false;
				collider2.enabled = false;
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 800);
				LivesManager.live--;
				Invoke ("Die", 2.0f);
				PlayerPrefs.SetInt("CurrentScore", ScoreManager.score);  
				PlayerPrefs.Save();
			} else if (marioIsBig){
				Debug.Log("IsBig" + marioIsBig);
				MarioIsHittenWhenBig = true;
				anim.SetBool("MarioIsHittenWhenBig", MarioIsHittenWhenBig);
				GameObject.FindGameObjectWithTag("GroundCheck").transform.position = 
					new Vector2 (gameObject.transform.position.x, 
					             gameObject.transform.position.y - 0.5f);
				collider1.GetComponent<CircleCollider2D>().radius = 0.32f;
				collider1.GetComponent<CircleCollider2D>().offset = new Vector2 (collider1.GetComponent<CircleCollider2D>().offset.x, -0.18f);
				collider2.GetComponent<BoxCollider2D>().size = new Vector2 (0.57f, 0.49f);
				collider2.GetComponent<BoxCollider2D>().offset = new Vector2(collider2.GetComponent<BoxCollider2D>().offset.x, 0.25f);
				Invoke("BecomeSmaller", 2.0f);
			} 
		} 

        if (coll.gameObject.tag == "Flag")
        {
			stageClaer.Play ();
			flagscore = (int) (coll.gameObject.transform.position.y);
            goal = true;
			win = true;
            GetComponent<Rigidbody2D>().Sleep();
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2f);

        }
        if (goal && coll.gameObject.tag == "FlagBottom")
        {
            //GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + new Vector2(5f, 0) * Time.deltaTime);
			Time.timeScale = 0.5f;
			GetComponent<Rigidbody2D>().velocity = new Vector2(22f, 0);
            GetComponent<Rigidbody2D>().gravityScale = 6f;
            GetComponent<Rigidbody2D>().WakeUp();
            Physics2D.IgnoreCollision(flag, collider1);
            Physics2D.IgnoreCollision(flag, collider2);
			ScoreManager.score += TimeManager.restTime * 10;
			ScoreManager.score += flagscore * 100;
            if (ScoreManager.score > TopScore.topScore)
            {
                PlayerPrefs.SetInt("HighScore", ScoreManager.score);
                PlayerPrefs.Save();
            }
			marioIsBig = false;
			win = false;
			Time.timeScale = 1;
			Invoke("LoadMain", 4.0f);
        }

        if (coll.gameObject.tag == "Castle")
        {
            gameObject.SetActive(false);
        }

		if (coll.gameObject.tag == "Star" && !MarioIsHittenWhenBig) {
			marioIsInvisible = true;
			anim.SetBool("MarioIsInvisible", true);
			Invoke("BecomeVisible", 10.0f);
		}
        
		if (coll.gameObject.name == "secretTubeIn") {
			if(IsSitting)Application.LoadLevel("secret");
		}
    }

	void LoadMain() {
		PlayerPrefs.SetInt("CurrentScore", 0); 
		Application.LoadLevel("main");
		PlayButtonScript.gameLoaded = false;
	}

	void Die(){
		gameObject.SetActive(false);
		if (LivesManager.live > 0) {
			Application.LoadLevel ("loadGame");
		} else {
			Application.LoadLevel("gameOver");
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag == "Mushroom") {eatingPowerUp.Play();}
		if (coll.gameObject.tag == "Mushroom" && !marioIsBig)
        {
			marioIsBig = true;
			GetComponent<Rigidbody2D>().gravityScale = 0;
			GameObject.FindGameObjectWithTag("GroundCheck").transform.position = 
				new Vector2 (GameObject.FindGameObjectWithTag("GroundCheck").transform.position.x, 
				             GameObject.FindGameObjectWithTag("GroundCheck").transform.position.y - 0.5f);
			GetComponent<Rigidbody2D>().transform.position = 
				new Vector2 (GetComponent<Rigidbody2D>().transform.position.x, 
				             GetComponent<Rigidbody2D>().transform.position.y + 0.5f);

			collider1.GetComponent<CircleCollider2D>().radius = 0.48f;
			collider1.GetComponent<CircleCollider2D>().offset = new Vector2 (collider1.GetComponent<CircleCollider2D>().offset.x, -0.52f);
			collider2.GetComponent<BoxCollider2D>().size = new Vector2 (0.96f, 1.2f);
			collider2.GetComponent<BoxCollider2D>().offset = new Vector2(collider2.GetComponent<BoxCollider2D>().offset.x, 0.4f);
			anim.SetTrigger("1up");
			GetComponent<Rigidbody2D>().gravityScale = 6;
			anim.SetBool("MarioIsBig", true);
        }

		/*Utgang fra "secret level"
		if (coll.gameObject.tag == "secretTubeOut") {
			Application.LoadLevel("main");
		}*/
    }


}
