using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
        Health = 0;
        IsAlive = false;

        // scream sound
        var fpc = GetComponent<FirstPersonController>();

        if (fpc != null)
            fpc.enabled = false;
    }
}
