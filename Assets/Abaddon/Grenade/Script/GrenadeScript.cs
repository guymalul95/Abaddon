using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrenadeType
{
	Timer,
	Impact
}
public class GrenadeScript : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public GrenadeType GrenadeType;
	public int GrenadeTimeMS;

	// Use this for initialization
	void Start () {
		
	}

	// Update for timer
	void Update()
	{
		
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
    {
		if(GrenadeType != GrenadeType.Impact) return;

        if(collision.gameObject.tag != "Player")
		{

		}
    }
}
