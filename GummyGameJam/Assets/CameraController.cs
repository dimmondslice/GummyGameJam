using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour 
{
	public Transform player1;
	public List<Transform> entityList;
	// Use this for initialization
	void Start () {
		entityList.Add(player1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 focalPoint = new Vector2();
		foreach(Transform t in entityList)
		{
			focalPoint += new Vector2(t.position.x, t.position.y);
		}
		//now normalize that vector by the number of entities
		focalPoint /= entityList.Count;
		//now lerp there
		Vector3 lerpSpot = new Vector3(focalPoint.x, focalPoint.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position,lerpSpot , .1f);
	}
}
