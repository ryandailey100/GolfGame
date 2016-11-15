using UnityEngine;
using System.Collections;

public class GolfPlayer : MonoBehaviour {

	public int Speed = 0;

	public void DoFullSwing() {

		Speed++;
		Debug.Log ("Swing");

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
