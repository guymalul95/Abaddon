using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorTriggerScript : MonoBehaviour {
    private Animator animator;
    private const string PlayerTag = "Player";

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (PlayerTag != other.tag) return;
        animator.ResetTrigger("DoorCloseTrigger");
        animator.SetTrigger("DoorOpenTrigger");
    }

    void OnTriggerExit(Collider other)
    {
        if (PlayerTag != other.tag) return;
        animator.ResetTrigger("DoorOpenTrigger");
        animator.SetTrigger("DoorCloseTrigger");
    }
}
