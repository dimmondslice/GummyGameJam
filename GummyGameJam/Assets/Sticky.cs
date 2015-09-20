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
		stuckTo = touching;
		canStick = false;
		isStuck = true;

		if(transform.tag != "StickableItem")
			stuckToEnv = true;

		player.rb.velocity = Vector2.zero;
		player.rb.gravityScale = 0;
		player.rb.angularVelocity = 0f;

		//effect = (GameObject)Resources.Load("StickyEffect");
		//effect = Instantiate(effect, transform.position, transform.rotation) as GameObject;
		//effect.transform.parent = touching.root;

        GameObject pivot = (GameObject)Resources.Load("Pivot");
        pivot = Instantiate(pivot, transform.position, transform.rotation) as GameObject;
        player.pivot = pivot.transform;

        player.transform.parent = player.pivot;		//this is an important step
        player.pivot.parent = touching.root;
        player.offset = player.transform.localPosition;
	}
	public void Unstick()
	{
		player.transform.parent = null;		//this is an important step
        Destroy(player.pivot.gameObject);
        player.pivot = null;

		player.rb.gravityScale = 2;

		stuckTo = null;
		canStick = false;
		isStuck = false;
		stuckToEnv = false;

		Destroy(effect);

        player.transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
