using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
    public float WalkSpeed;
    private Vector3 WalkVector;

	// Use this for initialization
	void Start () {
        WalkVector = new Vector3(0, 0, WalkSpeed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(WalkVector * Time.deltaTime);
    }
}
