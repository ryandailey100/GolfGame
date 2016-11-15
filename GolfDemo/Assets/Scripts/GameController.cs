using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Rigidbody Ball;
	public Animation PlayerAnimation;




	// Use this for initialization
	void Start () {
	
		if (GameObject.Find ("golfer") != null)
			PlayerAnimation = GameObject.Find ("golfer").GetComponent<Animation>();
		if (GameObject.Find ("ball") != null)
			Ball = GameObject.Find ("ball").GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {

			//Play Animation
			PlayerAnimation.CrossFade("golfer_fullswing");

			//Apply Force
			Vector3 ForceVector = new Vector3 (1,50,-19);

			ForceVector.Normalize(); //normalize

			Ball.AddForce (ForceVector * 12, ForceMode.Impulse);
		}

//		int count = Input.touchCount;
//
//		if (count > 0)
//		{
//			for (int i=0; i<count; i++)
//			{
//				Touch touch = Input.GetTouch(i);
//
//				if (touch.phase == TouchPhase.Began)
//				{
//					//ANIMATION WHEN PLAYER STARTED TO TOUCH THE SCREEN
//					Debug.Log ("Touch");
//				}
//				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
//				{
//					//ANIMATION WHEN PLAYER ENDED THE TOUCH
//					Debug.Log ("End Touch");
//				}
//			}
//		}

	}
}
