using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public int playerNum;
	public int state;	//0 moving, 1 left stick, 2 right stick, 3 both stick
	public bool stuck = false;
	
	public Sticky leftSticky;
	public Sticky rightSticky;

	public Rigidbody2D rb{get;set;}
	private Sticky stuckSticky;		//the sticky that is currently stuck
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
		ProcessStickyInput();
		if(!stuck)
			MovementEngine();
		else
		{
			StickyMovementEngine();
		}
	}
	void MovementEngine()
	{
		//rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, );
		if(Mathf.Abs(rb.velocity.x) < Mathf.Abs(maxSpeed))
			rb.AddForce(new Vector2(Input.GetAxis("HorizontalP"+playerNum)* moveForce,0), ForceMode2D.Force);
		else
		{
			//rb.AddTorque(-1 * Mathf.Sign(rb.velocity.x));	//make gummy rotate
			rb.velocity = new Vector3(maxSpeed * Mathf.Sign(rb.velocity.x), rb.velocity.y);	//cap the speed
		}
		if(onGround && Input.GetButtonDown("JumpP"+playerNum))
		{
			rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
		}
	}
	void UpdateOnGround()
	{
		Debug.DrawLine(transform.position, transform.position + new Vector3(0, -.55f,0f), Color.red);
		if(Physics2D.Raycast(transform.position, Vector2.down, .55f))
		   onGround = true;
		else onGround = false;
	}

	void ProcessStickyInput()
	{
		if (stuck && (Input.GetButtonUp("LeftStickyP"+playerNum) || Input.GetButtonUp("RightStickyP"+playerNum)))
		{
			stuck = false;
			stuckSticky.Unstick();
			stuckSticky = null;
		}
		else if(Input.GetButton("LeftStickyP"+playerNum) && leftSticky.canStick && !stuck)
		{
			if(stuckSticky) stuckSticky.Unstick();
			leftSticky.StickToThatThing();
			stuck = true;
			stuckSticky = leftSticky;
		}
		else if(Input.GetButton("RightStickyP"+playerNum) && rightSticky.canStick && !stuck)
		{
			if(stuckSticky) stuckSticky.Unstick();
			rightSticky.StickToThatThing();
			stuck = true;
			stuckSticky = rightSticky;
		}
	}
	void StickyMovementEngine()
	{
		transform.RotateAround(stuckSticky.transform.position, Vector3.forward, Input.GetAxis("HorizontalP"+playerNum) * 3);
	}
}
