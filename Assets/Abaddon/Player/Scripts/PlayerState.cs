using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class PlayerState : MonoBehaviour {
    internal int Score;
    internal int Health;
    public Text ScoreText;
    public Text HealthText;
    public Text GameMessageText;
    public AudioSource TakeDamageAudioPlayer;
    public AudioSource DieAudioPlayer;
    internal bool IsAlive;

    private void OnEnable()
    {
        int miniGameStatus = PlayerPrefs.GetInt("MiniGameWin");
        
        if (miniGameStatus == 1)
        {
            Score += 1000;

            UpdateGUI();
        }
    }

    // Use this for initialization
    void Start () {
        Score = 0;
        Health = 100;
        UpdateGUI();
        IsAlive = true;

        var audioSources = GetComponents<AudioSource>();

        TakeDamageAudioPlayer = audioSources[1];
        TakeDamageAudioPlayer.loop = false;
        TakeDamageAudioPlayer.playOnAwake = false;

        DieAudioPlayer = audioSources[2];
        DieAudioPlayer.loop = false;
        DieAudioPlayer.playOnAwake = false;
    }

    public void HealthPower()
    {
        Health = 200;
        UpdateGUI();
    }

    public void TakeDamage(int damage)
    {
        // Do not repeat death
        if (Health <= 0) return;

        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            TakeDamageAudioPlayer.Play();
        }

        UpdateGUI();
        // TODO: Make ouch sound
    }

    private void UpdateGUI()
    {
        ScoreText.text = Score.ToString();
        HealthText.text = Health.ToString();
    }

    public void KilledEnemy()
    {
        Score += 100;

        UpdateGUI();
    }

    public void BonusPoints(int score)
    {
        Score += score;

        UpdateGUI();
    }

    private void Die()
    {
        Health = 0;
        IsAlive = false;
        DieAudioPlayer.Play();
        GameMessageText.text = "YOU DIED";

        var fpc = GetComponent<FirstPersonController>();

        if (fpc != null)
            fpc.enabled = false;

        Time.timeScale = 0.5f;
        Invoke("Reset", 2);
    }

    public void Win()
    {
        GameMessageText.text = "YOU WIN";

        var fpc = GetComponent<FirstPersonController>();

        if (fpc != null)
            fpc.enabled = false;

        Time.timeScale = 0.5f;
        Invoke("Reset", 2);
    }

    private void Reset()
    {
        GameMessageText.text = "";
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
