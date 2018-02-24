using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpaceshipExploded : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public AudioClip ExplosionAudio;

    // Use this for initialization
    void Start () {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = ExplosionAudio;
        audioSource.loop = false;
        audioSource.Play();

        var explosion = (GameObject)Instantiate(ExplosionPrefab,
            transform.position,
            Quaternion.identity);

        Destroy(explosion, 3.0f);
        Destroy(gameObject, 5.0f);
    }
}
