using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform FollowWhat;
	public Vector3 MoveVector { get; set; }
	public Transform camTransform;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		var X = FollowWhat.transform.position.x; //get x value of camera
		var Y = FollowWhat.transform.position.y; //get y value of camera
		var Z = FollowWhat.transform.position.z; //get z value of camera



		transform.position = new Vector3 (X, Y + 2, Z + 4); //move the camera to object	



	}
}
