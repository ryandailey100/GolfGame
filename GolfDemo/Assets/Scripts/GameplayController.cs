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

	private Vector3 P1_BallPosition = Vector3.zero;
	private Vector3 P2_BallPosition = Vector3.zero;

	private int P1_Strokes = 0;
	private int P2_Strokes = 0;

	private Turn WhosTurn;

	private TeeGround TeeMarker;

	private CourseMap Map; // <--Selected Map


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

	private enum Turn
	{
		Player1,
		Player2
	}

	public enum TeeGround
	{
		Blue,  //Champions Tee
		White, //Men's Tee
		Red,   //Women's Tee
		Green  //Senior's Tee

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

		TeeMarker = TeeGround.White;
		SetMap (6);

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

			//Hide UI elements
			btnCanvas_Shoot.alpha = 1;
			sliderCanvas.alpha = 1;

			//Disable UI elements
			btnCanvas_Shoot.interactable = true;
			sliderCanvas.interactable = true;

			btn_ChangeView.GetComponentInChildren<Text>().text = "Peak";
		}
		else {      //Switch to Peak View
			
			cameraView = CameraView.PeakView; 

			//Save rotation position
			cameraFollow.currentPlayerX = cameraFollow.currentCameraX;

			//Set OriginalCamX
			cameraFollow.OriginalCamX = cameraFollow.camTransform.position.x;

			//Hide UI elements
			btnCanvas_Shoot.alpha = 0;
			sliderCanvas.alpha = 0;

			//Disable UI elements
			btnCanvas_Shoot.interactable = false;
			sliderCanvas.interactable = false;

			btn_ChangeView.GetComponentInChildren<Text>().text = "Move";

		}
	}

	private void SetMap(int Hole) {

		//Set new map
		Map = new CourseMap();
		//Set Hole
		Map.Hole = Hole; //gameObject.Find("NameOfTheGameObjectTarget").GetComponent<NameOfTheScrit>().NameOfTheProperty = ...;
		//Set Flag
		if (GameObject.Find ("flag hole " + Hole) != null)
			Map.Flag = GameObject.Find ("flag hole " + Hole).GetComponent<GameObject>();
		
		//Set Tee's       <-------------------Check for TeeMarker Color!!!!!
		//Left Blue Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_blue_L") != null)
			Map.Tee_Blue_Left = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_blue_L").GetComponent<Transform>();
		//Right Blue Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_blue_R") != null)
			Map.Tee_Blue_Right = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_blue_R").GetComponent<Transform>();
		//Left White Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_white_L") != null)
			Map.Tee_White_Left = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_white_L").GetComponent<Transform>();
		//Right White Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_white_R") != null)
			Map.Tee_White_Right = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_white_R").GetComponent<Transform>();
		//Left Red Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_red_L") != null)
			Map.Tee_Red_Left = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_red_L").GetComponent<Transform>();
		//Right Red Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_red_R") != null)
			Map.Tee_Red_Right = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_red_R").GetComponent<Transform>();
		//Left Green Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_green_L") != null)
			Map.Tee_Green_Left = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_green_L").GetComponent<Transform>();
		//Right Green Tee Marker
		if (GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_green_R") != null)
			Map.Tee_Green_Right = GameObject.Find ("tee_markers hole " + Hole + "/" + "tee_marker_green_R").GetComponent<Transform>();

		SetPlayerPosition ();
	

	}

	private void SetPlayerPosition() {

		if (WhosTurn == Turn.Player1 && P1_BallPosition != Vector3.zero)
			Ball_Position.position = P1_BallPosition;
		else if (WhosTurn == Turn.Player1 && P1_BallPosition != Vector3.zero)
			Ball_Position.position = P1_BallPosition;
		else {
			//Defult Position
			if (TeeMarker == TeeGround.Blue) {
				Ball_Position.position = Vector3.Lerp (Map.Tee_Blue_Left.position, Map.Tee_Blue_Right.position, 0.5f) + new Vector3(0,1,0);
			}
			else if (TeeMarker == TeeGround.White) {
				Ball_Position.position = Vector3.Lerp (Map.Tee_White_Left.position, Map.Tee_White_Right.position, 0.5f) + new Vector3(0,1,0);
			}
			else if (TeeMarker == TeeGround.Red) {
				Ball_Position.position = Vector3.Lerp (Map.Tee_Red_Left.position, Map.Tee_Red_Right.position, 0.5f) + new Vector3(0,1,0);
			}
			else if (TeeMarker == TeeGround.Green) {
				Ball_Position.position = Vector3.Lerp (Map.Tee_Green_Left.position, Map.Tee_Green_Right.position, 0.5f) + new Vector3(0,1,0);
			}

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
			Vector3 ForceVector = rotation * dir; //1, 10, -19

			ForceVector.Normalize(); //normalize

			Ball_Body.AddForce (ForceVector * ShotPower, ForceMode.Impulse);
		}

	}
}
