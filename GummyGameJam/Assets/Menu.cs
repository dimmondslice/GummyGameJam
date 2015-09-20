using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("JumpP1"))
			Application.LoadLevel("Scene1");
			GetComponent<AudioSource>().Play ();
	}
}
