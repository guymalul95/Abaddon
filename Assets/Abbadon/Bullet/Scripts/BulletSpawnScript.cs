using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum FireRateType
{
    Auto,
    Semi
}

public class BulletSpawnScript : MonoBehaviour {
    public GameObject BulletPrefab;
    public ParticleSystem MuzzleFlashParticles;
    public float FireDelayMillis = 125;
    public FireRateType FireRateType;
    public float MinimumBulletSpread = 0.8f;
    public float MaximumBulletSpread = 1.8f;
    private float CurrentBulletSpread;
    private const float BulletSpreadRatio = 0.1f;
    private float LastFireMillis;
    private bool IsTriggerHeld;

    // Use this for initialization
    void Start () {
        MuzzleFlashParticles.Stop();
        LastFireMillis = 0;
        CurrentBulletSpread = MinimumBulletSpread;
        IsTriggerHeld = false;
        UnityEngine.Random.InitState((int)System.Environment.TickCount);

        // Assert
        Debug.Assert(MinimumBulletSpread <= MaximumBulletSpread);
    }

    private void FixedUpdate()
    {
        if(IsTriggerHeld && CurrentBulletSpread < MaximumBulletSpread)
            CurrentBulletSpread+=BulletSpreadRatio;
        else if(!IsTriggerHeld && CurrentBulletSpread > MinimumBulletSpread)
            CurrentBulletSpread-=BulletSpreadRatio * 3;
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
                    IsTriggerHeld = true;
                    LastFireMillis = FireDelayMillis;
                }
                else if(Input.GetButtonUp("Fire1"))
                {
                    IsTriggerHeld = false;
                }

                break;
            }
            case FireRateType.Semi: 
            {
                if((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && !IsTriggerHeld)
                {
                    IsTriggerHeld = true;
                    Fire();
                }
                else if(Input.GetButtonUp("Fire1"))
                {
                    IsTriggerHeld = false;
                }

                break;
            }
            default: break;
        }
    }

    void Fire()
    {
        float xSpread = UnityEngine.Random.Range(-1f, 1f) + 0.01f;
        float ySpread = UnityEngine.Random.Range(-1f, 1f) + 0.01f;
        float zSpread = UnityEngine.Random.Range(-1f, 1f) + 0.01f;

        //normalize the spread vector to keep it conical
        Vector3 spread = new Vector3(xSpread, ySpread, zSpread).normalized * CurrentBulletSpread;

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            BulletPrefab,
            transform.position,
            Quaternion.Euler(spread) * transform.rotation);

        MuzzleFlashParticles.Play();

        // Destroy the bullet after 5 seconds or later if it collides
        Destroy(bullet, 5.0f);
    }
}
