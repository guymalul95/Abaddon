using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour {
    public GameObject bulletPrefab;
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
            transform.position,
            transform.rotation);

        muzzleFlashParticles.Play();

        // Destroy the bullet after 5 seconds or later if it collides
        Destroy(bullet, 5.0f);
    }
}
