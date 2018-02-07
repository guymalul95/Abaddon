using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpaceshipMove : MonoBehaviour {

	Animator spaceshipAnim;
	public float Speed;
	int lastDirection;
	// Use this for initialization
	void Start () {
		spaceshipAnim = GetComponent<Animator>();
		lastDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {
		int inputX = NormalizeInput(Input.GetAxis("HorizontalPress"));
		//int inputY = NormalizeInput(Input.GetAxis("VerticalPress"));

		if(inputX != lastDirection) {
			spaceshipAnim.SetInteger("Direction",inputX);
			lastDirection = inputX;
		}

		if(0 != inputX) {
			Vector3 movement = new Vector3(Time.deltaTime * (float)inputX * Speed,0,0);
			transform.Translate(movement);
		}
	}

	int NormalizeInput(float axis)
	{
		if(axis > 0) return 1;
		if(axis < 0) return -1;
		return 0;
	}
}
