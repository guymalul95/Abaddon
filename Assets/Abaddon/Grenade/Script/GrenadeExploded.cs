using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GrenadeExploded : MonoBehaviour {
    public GameObject ExplosionPrefab;

    // Use this for initialization
    void Start () {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        GameObject explosion = Instantiate(ExplosionPrefab,
            transform.position,
            Quaternion.identity);

        Destroy(explosion, 3.0f);
        Destroy(gameObject, 5.0f);
    }
}
