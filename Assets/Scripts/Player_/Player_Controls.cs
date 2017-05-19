using UnityEngine;
using System.Collections;

public class Player_Controls : MonoBehaviour {

	public Rigidbody2D Body;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator  anim;

	bool crouched = false;

	// Jumping & groundCheck
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700;

	// Use this for initialization
	void Start () {

		Body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", Body.velocity.y);





	}


	void Update ()
	{

		// Player Movement Left or Right
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed",Mathf.Abs(move));
		Body.velocity = new Vector2(move * maxSpeed, Body.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();



		// If grounded & jump button ( (A) Controller) is press then jump
		if (grounded && Input.GetKeyDown (KeyCode.JoystickButton0))
		{
			
			anim.SetBool ("ground", false);
			Body.AddForce (new Vector2 (0, jumpForce));

		}


	// Crouchs/uncrouches the player

		if (Input.GetKeyDown (KeyCode.JoystickButton4)) {

			crouched = !crouched;
			anim.SetBool ("Crouch", crouched);
		}

	}		
	void Flip () {

		// Flips Player if not facing right
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
