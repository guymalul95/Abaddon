using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {
    internal int Score;
    internal int Health;
    public Text ScoreText;
    public Text HealthText;
    internal bool IsAlive;

    // Use this for initialization
    void Start () {
        Score = 0;
        Health = 100;
        UpdateGUI();
        IsAlive = true;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            Die();

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

    private void Die()
    {
        IsAlive = false;

        // scream sound
    }
}
