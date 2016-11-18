using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Rigidbody ball;
	public GameplayController GameplayController;

	private bool InMotion = false;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "club_dummy") //club_dummy
		{
			Debug.Log ("HIT");
		}
	}

	void OnTriggerEnter (Collider col)
	{

		if(col.gameObject.tag == "FlagHole") //Ball goes inside of hole
		{
			Debug.Log ("Hole in One!!");
			ball.position = new Vector3 (col.gameObject.transform.position.x, col.gameObject.transform.position.y, col.gameObject.transform.position.z);
			ball.velocity = Vector3.zero;
		}

	}

	// Use this for initialization
	void Start () {


		if (ball.velocity == Vector3.zero && InMotion == true) {
			//End Turn


		}
			 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
