using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSpawner
{
    Enemy,
    Player
}

[RequireComponent(typeof(AudioSource))]
public class Bullet2DSpawnScript : MonoBehaviour {
    public GameObject BulletPrefab;
    public float FireDelayMillis;
    private float LastFireMillis;
    public AudioClip BulletFireAudio;
    private AudioSource ShootAudioPlayer;
    public BulletSpawner Spawner;

    // Use this for initialization
    void Start () {
        LastFireMillis = FireDelayMillis;

        ShootAudioPlayer = GetComponent<AudioSource>();
        ShootAudioPlayer.clip = BulletFireAudio;
        ShootAudioPlayer.loop = false;
    }

    // Update is called once per frame
    void Update () {
        LastFireMillis -= Time.deltaTime * 1000;

        if (LastFireMillis > 0) return;

        switch(Spawner)
        {
            case BulletSpawner.Player:
            {
                if(Input.GetButton("Fire1")) 
                    Fire();

                break;
            }
            case BulletSpawner.Enemy:
            {
                Fire();
                break;
            }
        }
    }

    void Fire()
    {
        LastFireMillis = FireDelayMillis;

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            BulletPrefab,
            transform.position,
            transform.rotation);

        bullet.tag = transform.parent.tag;

        ShootAudioPlayer.Play();

        // Destroy the bullet after 5 seconds or later if it collides
        Destroy(bullet, 3.0f);
    }
}
