using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public int playerNum;
	public int state;	//0 moving, 1 left stick, 2 right stick, 3 both stick
	public bool stuck = false;
	

	public Rigidbody2D rb;
	public float maxSpeed;
	public float moveForce;
	public float jumpForce;
	public bool onGround;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () 
	{
		UpdateOnGround();
		if(!stuck)
			MovementEngine();
	}
	void MovementEngine()
	{
		//rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, );
		if(Mathf.Abs(rb.velocity.x) < Mathf.Abs(maxSpeed))
			rb.AddForce(new Vector2(Input.GetAxis("Horizontal")* moveForce,0), ForceMode2D.Force);
		else
		{
			rb.AddTorque(-1 * Mathf.Sign(rb.velocity.x));	//make gummy rotate
			rb.velocity = new Vector3(maxSpeed * Mathf.Sign(rb.velocity.x), rb.velocity.y);	//cap the speed
		}
		if(onGround && Input.GetButtonDown("Jump"))
		{
			rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
		}
	}
	void UpdateOnGround()
	{
		if(Physics2D.Raycast(transform.position, Vector2.down, .55f))
		   onGround = true;
		else onGround = false;
	}
}
