using UnityEngine;
using System.Collections;

public class platformLerp : MonoBehaviour 
{
	public Vector3 start;
	public Vector3 End;

	private Vector3 target;
	// Use this for initialization
	void Start () {
		target = start;
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - target).magnitude < .1f)
		{
			if(target == start)
				target = End;
			else target = start;
		}
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
	}
}
