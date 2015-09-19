using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public int playerNum;
	public int state;	//0 moving, 1 left stick, 2 right stick, 3 both stick

	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	void ProcessInput()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{

		}
	}
}
