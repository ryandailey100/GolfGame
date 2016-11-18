using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Rigidbody Ball;
	public Animation PlayerAnimation;

	private bool HasSwung = false;



	// Use this for initialization
	void Start () {
	
		if (GameObject.Find ("golfer") != null)
			PlayerAnimation = GameObject.Find ("golfer").GetComponent<Animation>();
		if (GameObject.Find ("ball") != null) {
			Ball = GameObject.Find ("ball").GetComponent<Rigidbody>();
		}



	}

	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {

			//Play Animation
			PlayerAnimation.CrossFade("golfer_putt");
		}

		//if animation is between frames x and y
		if (PlayerAnimation["golfer_fullswing"].time > 1.45f && PlayerAnimation["golfer_fullswing"].time < 1.5f && HasSwung == false) {
			//do something interesting

			HasSwung = true;

			//Apply Force
			Vector3 ForceVector = new Vector3 (1,50,-19);

			ForceVector.Normalize(); //normalize
		
			Ball.AddForce (ForceVector * 12, ForceMode.Impulse);
		}
		else if (PlayerAnimation["golfer_putt"].time > 1.29f && PlayerAnimation["golfer_putt"].time < 1.34f && HasSwung == false) {
			//do something interesting

			HasSwung = true;

			//Apply Force
			Vector3 ForceVector = new Vector3 (1,7,-19);

			ForceVector.Normalize(); //normalize

			//Ball.AddForce (ForceVector * 10, ForceMode.Impulse);

		}

	}
}
