using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum FireRateType
{
    Auto,
    Semi,
    Single
}

public class BulletSpawnScript : MonoBehaviour {
    public GameObject BulletPrefab;
    public Animation WeaponFireAnimation;
    public ParticleSystem MuzzleFlashParticles;
    public float FireDelayMillis = 100;
    public FireRateType FireRateType;
    private float LastFireMillis;
    private bool IsTriggerHeld;

    // Use this for initialization
    void Start () {
        MuzzleFlashParticles.Stop();
        LastFireMillis = 0;
        IsTriggerHeld = false;
    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update () {
        LastFireMillis -= Time.deltaTime * 1000;

        switch(FireRateType)
        {
        case FireRateType.Auto:{
                if (Input.GetButton("Fire1") && LastFireMillis <= 0)
                {
                    Fire();
                    WeaponFireAnimation.Play();
                    LastFireMillis = FireDelayMillis;
                }
                else if (LastFireMillis <= 0 && WeaponFireAnimation.isPlaying)
                {
                    WeaponFireAnimation.Stop();
                }
                break;
            }
            case FireRateType.Semi: 
            {
                if((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && !IsTriggerHeld)
                {
                    IsTriggerHeld = true;
                    Fire();
                    WeaponFireAnimation.Play();
                }
                else if(Input.GetButtonUp("Fire1"))
                {
                    IsTriggerHeld = false;
                    WeaponFireAnimation.Stop();
                }

                break;
            }
            default: break;
        }
    }   

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            BulletPrefab,
            transform.position,
            transform.rotation);

        MuzzleFlashParticles.Play();

        // Destroy the bullet after 5 seconds or later if it collides
        Destroy(bullet, 5.0f);
    }
}
