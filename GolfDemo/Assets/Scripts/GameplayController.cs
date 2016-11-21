using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GameplayController : MonoBehaviour {


	public Rigidbody Ball_Body;
	public Transform Ball_Position;
	public Animation PlayerAnimation;
	private Button btn_ChangeView;
	private Button btn_Shoot;
	private CanvasGroup btnCanvas_ChangeView;
	private CanvasGroup btnCanvas_Shoot;
	private CanvasGroup sliderCanvas;


	public bool HasSwung = false;
	private bool BallHit = false;

	private float ShotPower = 0.0f;

	public CameraView cameraView = CameraView.PlayerView;

	public CameraFollow cameraFollow;

	//Vectrosity projection line
	private Material lineMaterial;
	private int maxPoints = 500;
	private Rigidbody GhostBall;

	private VectorLine pathLine;
	private int pathIndex = 0;
	//private Vector3[] pathPoints;
	private List<Vector3> PathPoints;



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
		if (GameObject.Find ("btn_View") != null) {

			btn_ChangeView = GameObject.Find ("btn_View").GetComponent<Button>();
			btnCanvas_ChangeView = GameObject.Find ("btn_View").GetComponent<CanvasGroup> ();
		}
		if (GameObject.Find ("btn_Shoot") != null) {
			btn_Shoot = GameObject.Find ("btn_Shoot").GetComponent<Button> ();
			btnCanvas_Shoot = GameObject.Find ("btn_Shoot").GetComponent<CanvasGroup> ();
		}
		if (GameObject.Find ("SliderPower") != null)
			sliderCanvas = GameObject.Find ("SliderPower").GetComponent<CanvasGroup> ();


//		PathPoints = new List<Vector3>();
//		pathLine = new VectorLine (
//			"Path",
//			PathPoints,
//			lineMaterial, 
//			12.0); //Or Dotted
	}

	private void SamplePoints() {

		//Reset Variables



//		bool running = true;
//		while (running) {
//
//			PathPoints[pathIndex] =  GhostBall.position;
//
//			if (++pathIndex == maxPoints) {
//				running = false;
//			}
//
//			StartCoroutine(TimeDelay(0.05f));
//
//			pathLine.Draw3D ();
////			pathLine.maxDrawIndex = pathIndex-1;
////			VectorLine.SetLine3D (Color.black, pathLine);
////			Vector.SetTextureScale (pathLine, 1.0);
//
//		}

	}

	IEnumerator TimeDelay(float time) {
		yield return new WaitForSeconds(time);
	}

	public void AdjustPower(float Power) {

		ShotPower = Power;
	}


	public void ShootBall() {
		
		if (HasSwung == false) {
			//Play Animation

			//Hide UI elements
			btnCanvas_ChangeView.alpha = 0;
			btnCanvas_Shoot.alpha = 0;
			sliderCanvas.alpha = 0;

			//Disable UI elements
			btnCanvas_ChangeView.interactable = false;
			btnCanvas_Shoot.interactable = false;
			sliderCanvas.interactable = false;


			HasSwung = true;
			PlayerAnimation.CrossFade("golfer_putt");
		}

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

		}
		else if (PlayerAnimation["golfer_putt"].time > 1.29f && PlayerAnimation["golfer_putt"].time < 1.34f && BallHit == false) {
			//do something interesting

			BallHit = true;

			//Apply Force
			Quaternion rotation = Quaternion.Euler (0, cameraFollow.currentCameraX, 0);
			Vector3 dir = new Vector3 (0.0f, 1.0f, 1.0f); //Forward and Up
			Vector3 ForceVector = rotation * dir;

			ForceVector.Normalize(); //normalize

			Ball_Body.AddForce (ForceVector * ShotPower, ForceMode.Impulse);
		}

	}
}
