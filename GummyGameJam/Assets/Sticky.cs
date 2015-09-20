using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour 
{
	public bool canStick;
	public bool isStuck;
	public Transform touching;
	public Transform stuckTo;
	public bool stuckToEnv;

	private GameObject effect;

	public PlayerController player;

	public void Start()
	{
		player = GetComponentInParent<PlayerController>();
	}
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(!isStuck)
			canStick = true;
		touching = other.transform;
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		canStick = false;
		touching = null;
	}

	public void StickToThatThing()
	{
		transform.root.parent = touching.root;		//this is an important step

		stuckTo = touching;
		canStick = false;
		isStuck = true;

		if(transform.tag != "StickableItem")
			stuckToEnv = true;

		player.rb.velocity = Vector2.zero;
		player.rb.gravityScale = 0;
		player.rb.angularVelocity = 0f;


		effect = (GameObject)Resources.Load("StickyEffect");
		effect = Instantiate(effect, transform.position, transform.rotation) as GameObject;
		effect.transform.parent = touching.root;
	}
	public void Unstick()
	{
		player.transform.parent = null;		//this is an important step

		player.rb.gravityScale = 1;

		stuckTo = null;
		canStick = false;
		isStuck = false;
		stuckToEnv = false;

		Destroy(effect);
	}
}
