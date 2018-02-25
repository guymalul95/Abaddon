using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    int Score;
    int Health;

	// Use this for initialization
	void Start () {
        Score = 0;
        Health = 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            Die();

        // TODO: Make ouch sound
    }

    public void KilledEnemy()
    {
        Score += 100;

        // Update UI
    }

    private void Die()
    {

    }
}
