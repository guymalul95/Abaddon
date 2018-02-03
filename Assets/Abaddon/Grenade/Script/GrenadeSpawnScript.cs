using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeSpawnScript : MonoBehaviour {
    public GameObject GrenadePrefab;
    public float FireDelayMillis = 5000;
    public float ThrowForce;
    public string ActionButton;
    private float LastFireMillis;

    // Use this for initialization
    void Start () {
        LastFireMillis = 0;
    }

    // Update is called once per frame
    void Update () {

        LastFireMillis -= Time.deltaTime * 1000;

        if (Input.GetButton(ActionButton) && LastFireMillis <= 0)
        {
            LastFireMillis = FireDelayMillis;
            Fire();
        }
    }
    

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject grenade = (GameObject)Instantiate(
            GrenadePrefab,
            transform.position,
            transform.rotation);

        Rigidbody rigitbody = grenade.GetComponent<Rigidbody>();
        Vector3 throwVector = transform.forward.normalized;

        rigitbody.AddForce(ThrowForce * throwVector,ForceMode.Impulse);
        Destroy(grenade, 10.0f);
    }
}
