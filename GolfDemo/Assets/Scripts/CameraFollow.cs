using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform lookAt;
	public Transform camTransform;

	private const float X_ANGLE_MIN = 4.0f;
	private const float X_ANGLE_MAX = 25.0f;
	private const float Y_ANGLE_MIN = 4.0f;
	private const float Y_ANGLE_MAX = 25.0f;

	private float distance = 3.35f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensitivityX = 2f;
	private float sensitivityY = 0.5f;

	public Transform Golfer;
	private float OriginalX = 0.0f;

	// Use this for initialization
	void Start () {
	
		camTransform = transform;

		//lookAt.LookAt ();
		currentX = 180.0f;
		currentY = 0.0f;
	}

	private void LateUpdate() {

		//Vector3 lookAtNew = new Vector3 (lookAt.position.x, lookAt.position.y + 1.1f, lookAt.position.z); //Look slightly above ball
		Vector3 lookAtNew = lookAt.position + new Vector3 (0,1.1f, 0); //Look slightly above ball

		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (0, 0, 0);
		camTransform.position = lookAtNew + rotation * dir; //lookAt.position
		camTransform.LookAt(lookAtNew); //lookAt.position

		if (OriginalX == 0.0f) {

			OriginalX = camTransform.position.x;
			//Debug.Log (OriginalX);
		}

		//Player
		Vector3 dir1 = new Vector3 (0.0f, 0.0f, -0.5f);
		Quaternion rotation1 = Quaternion.Euler (0, currentX, 0);
		Golfer.position = lookAt.position - new Vector3 (0, 0.075f, 0) + rotation1 * dir1;
		//Golfer.LookAt (lookAt.position);


//		Vector3 reletivePos = lookAt.position - Golfer.position;
//		Quaternion rotation2 = Quaternion.LookRotation(reletivePos);
//		Quaternion current = Golfer.localRotation;
//		rotation2.x = 0;
//		Golfer.localRotation = Quaternion.Slerp(current, rotation2, Time.deltaTime);



//		Vector3 dir1 = new Vector3 (0.0f, 0, -0.5f);
//		Quaternion rotation1 = Quaternion.Euler (0, currentX , 0);
//		Golfer.position = lookAt.position - new Vector3 (0, 0.075f, 0) + rotation1 * dir1;
//		Golfer.LookAt (lookAt.position);

	}

	// Update is called once per frame
	void Update () {
		
		//PC Mouse Movement
		currentX += Input.GetAxis ("Mouse X") * sensitivityX;
		currentY += Input.GetAxis ("Mouse Y") * sensitivityY;
		//currentX = Mathf.Clamp (currentX, X_ANGLE_MIN, X_ANGLE_MAX);
		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

		if (Input.touchCount > 0) { //Mobile Touch

			currentX += Input.touches [0].deltaPosition.x / 50.0f * sensitivityX;
			currentY += Input.touches [0].deltaPosition.y / 50.0f * sensitivityY;
			currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
		} 
		else if (Input.touchCount == 0) { //No Touch

//			//Go Back to Defult Position
//			if (OriginalX != 0.0f) {
//
//				if (OriginalX < camTransform.position.x && camTransform.position.x - OriginalX >= 0.03f) {
//
//					currentX -= 1f;
//				}
//				else if (OriginalX > camTransform.position.x && OriginalX - camTransform.position.x >= 0.03f) { 
//
//					currentX += 1f;
//				}
//			}
		}
			
	}
}
