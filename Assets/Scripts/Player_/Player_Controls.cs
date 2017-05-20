using UnityEngine;
using System.Collections;

public class Player_Controls : MonoBehaviour {

	GameObject standCollider;
	GameObject crouchCollider;

	public Rigidbody2D Body;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator  anim;

	float ceillingRadius = 0.2f;
	bool ceiling = false;
	public Transform ceilingCheck;

	bool crouched = false;

	// Jumping & groundCheck
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700;

	// Use this for initialization
	void Start () {

		standCollider = transform.FindChild("standCollider").gameObject;
		crouchCollider = transform.FindChild ("crouchCollider").gameObject;


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

		//tempVector2 = new Vector3 (Screen.width * 0.5f, 0, Screen.height * 0.5f);
		//tempVector = Input.mousePosition;




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

		if (Input.GetKeyDown (KeyCode.JoystickButton1)) {

			crouched = !crouched;
			anim.SetBool ("Crouch", crouched);

//			Body.velocity / maxSpeed 2f;
			// Disables Standing collider box
			standCollider.SetActive (false);
			crouchCollider.SetActive (true);


		}

		if (Input.GetKeyUp (KeyCode.JoystickButton1)) {
			
			crouched = !crouched;
			anim.SetBool ("Crouch", crouched);

			// disables crouched collisions & enables standing collider
			standCollider.SetActive (true);
			crouchCollider.SetActive (false);

			if (!crouched && anim.GetBool ("Crouch")) 
			{
				if (Physics2D.OverlapCircle (ceilingCheck.position,ceillingRadius,whatIsGround))
					
				{
					crouched = true;
					anim.SetBool ("Crouch", crouched);
				}



				}


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
