using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DoorTriggerScript : MonoBehaviour {
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip DoorOpenAudio;
    public AudioClip DoorCloseAudio;
    private const string PlayerTag = "Player";

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (PlayerTag != other.tag) return;
        animator.ResetTrigger("DoorCloseTrigger");
        animator.SetTrigger("DoorOpenTrigger");

        audioSource.clip = DoorOpenAudio;
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (PlayerTag != other.tag) return;
        animator.ResetTrigger("DoorOpenTrigger");
        animator.SetTrigger("DoorCloseTrigger");

        audioSource.clip = DoorCloseAudio;
        audioSource.PlayDelayed(0.5f);
    }
}
