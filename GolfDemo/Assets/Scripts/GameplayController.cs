using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayController : MonoBehaviour {


	public Rigidbody Ball_Body;
	public Transform Ball_Position;
	public Animation PlayerAnimation;
	public Button btn_ChangeView;

	public bool HasSwung = false;
	private bool BallHit = false;

	public CameraView cameraView = CameraView.PlayerView;

	public CameraFollow cameraFollow;


	public enum CameraView 
	{
		PeakView, 
		PlayerView
	}

	// Use this for initialization
	void Start () {
	
		if (GameObject.Find ("golfer") != null)
			PlayerAnimation = GameObject.Find ("golfer").GetComponent<Animation>();
		if (GameObject.Find ("ball") != null) {
			Ball_Body = GameObject.Find ("ball").GetComponent<Rigidbody>();
			Ball_Position = GameObject.Find ("ball").GetComponent<Transform>();
		}
		if (GameObject.Find ("ButtonView") != null)
			btn_ChangeView = GameObject.Find ("ButtonView").GetComponent<Button>();

	}

	//View Change Button
	public void ChangeView() {

		if (cameraView == CameraView.PeakView) {  //Switch to Player View

			cameraView = CameraView.PlayerView;

			//Use the Saved rotation position
			cameraFollow.currentCameraX = cameraFollow.currentPlayerX;

			btn_ChangeView.GetComponentInChildren<Text>().text = "Peak";
		}
		else {      //Switch to Peak View
			
			cameraView = CameraView.PeakView; 

			//Save rotation position
			cameraFollow.currentPlayerX = cameraFollow.currentCameraX;

			//Set OriginalCamX
			cameraFollow.OriginalCamX = cameraFollow.camTransform.position.x;

			btn_ChangeView.GetComponentInChildren<Text>().text = "Move";

		}
	}

	//Set up next shot
	public void NextShot() {
		//Prepare next shot



		HasSwung = false;
		BallHit = false;
	}

	// Update is called once per frame
	void Update () {

//		if (Input.anyKeyDown) {
//			
//			if (HasSwung == false) {
//				//Play Animation
//				PlayerAnimation.CrossFade("golfer_putt");
//				HasSwung = true;
//			}
//		}

		//if animation is between frames x and y
		if (PlayerAnimation["golfer_fullswing"].time > 1.45f && PlayerAnimation["golfer_fullswing"].time < 1.5f && BallHit == false) {
			//do something interesting

			BallHit = true;

			//Apply Force
			Vector3 ForceVector = new Vector3 (1,50,-19);

			ForceVector.Normalize(); //normalize
		
			Ball_Body.AddForce (ForceVector * 12, ForceMode.Impulse);
		}
		else if (PlayerAnimation["golfer_putt"].time > 1.29f && PlayerAnimation["golfer_putt"].time < 1.34f && BallHit == false) {
			//do something interesting

			BallHit = true;

			//Apply Force
			Vector3 ForceVector = new Vector3 (1,7,-19);

			ForceVector.Normalize(); //normalize

			Ball_Body.AddForce (ForceVector * 10, ForceMode.Impulse);

		}

	}
}
