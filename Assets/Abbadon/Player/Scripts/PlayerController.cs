using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public ParticleSystem muzzleFlashParticles;

    // Use this for initialization
    void Start () {
        muzzleFlashParticles.Stop();
    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update () {

        // Get Input (Fire weapon, etc...)
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }   

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward.normalized * 10;

        muzzleFlashParticles.Play();

        // Destroy the bullet after 5 seconds or later if it collides
        Destroy(bullet, 5.0f);
    }
}
