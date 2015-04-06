using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Hero : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 0.5f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

	private Hero m_hero;
	private Rigidbody2D m_Rigidbody2D;
	private Animator m_Anim; 

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_GroundedRadius = 0.1f; // Radius of the overlap circle to determine if grounded
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool m_isClimb = false;
	// Use this for initialization
	void Start () {
	
	}

	private void Awake()
	{
		m_hero = GetComponent<Hero>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();
		m_GroundCheck = transform.Find("GroundChecker");

	}
	
	// Update is called once per frame
	void Update () {
		float move = CrossPlatformInputManager.GetAxis("Horizontal");
		m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
		m_Anim.SetFloat("speed", Mathf.Abs(move));

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

		if (m_isClimb) {
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
			m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, v * m_MaxSpeed);
		}
		m_Anim.SetBool ("isClimb", m_isClimb);
		

//		float v = CrossPlatformInputManager.GetAxis ("Vertical");
//		if (v > 0) {
//			m_Anim.SetBool ("isGround", false);
//		} else if (v < 0){
//			m_Anim.SetBool ("isGround", true);
//		}
	}

	private void FixedUpdate()
	{
		m_Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}

//		Debug.Log ("m_Grounded=" + m_Grounded + " vSpeed=" + m_Rigidbody2D.velocity.y);

		m_Anim.SetBool("isGround", m_Grounded);
		m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("OnTriggerEnter2D");

		if (other.gameObject.tag == "Tree") {
			m_isClimb = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("OnTriggerExit2D");
		
		if (other.gameObject.tag == "Tree") {
			m_isClimb = false;
		}
	}
}
