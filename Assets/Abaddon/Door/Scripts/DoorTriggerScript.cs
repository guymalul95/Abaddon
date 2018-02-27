using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(LevelScript))]
public class DoorTriggerScript : MonoBehaviour {
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip DoorOpenAudio;
    public AudioClip DoorCloseAudio;
    private const string PlayerTag = "Player";
    private const string EnemyTag = "Enemy";
    public int MinimumScoreToActive;
    private LevelScript LevelScript;
    public TextMesh ScoreText;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        LevelScript = GetComponent<LevelScript>();
        audioSource.loop = false;
        ScoreText.text = "SCORE  NEEDED TO  OPEN: " + MinimumScoreToActive.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        // if minimum score is not meet then do not open
        if (!LevelScript.ShouldBeActive(MinimumScoreToActive))
            return;

        ScoreText.text = "";

        switch (other.tag)
        {
            case PlayerTag:
            case EnemyTag:
                break;
            default: return;
        }
        animator.ResetTrigger("DoorCloseTrigger");
        animator.SetTrigger("DoorOpenTrigger");

        audioSource.clip = DoorOpenAudio;
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        switch(other.tag)
        {
            case PlayerTag:
            case EnemyTag:
                break;
            default: return;
        }
        animator.ResetTrigger("DoorOpenTrigger");
        animator.SetTrigger("DoorCloseTrigger");

        audioSource.clip = DoorCloseAudio;
        audioSource.PlayDelayed(0.5f);
    }
}
