using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float health = 20f;
    public int points = 50;
    public GameObject destroyedPrefab;
    private PlayerState PlayerState;

    public void Start()
    {
        PlayerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        GameObject destroyed = Instantiate(destroyedPrefab, transform.position, transform.rotation, transform.parent);
        PlayerState.BonusPoints(points);
    }
}