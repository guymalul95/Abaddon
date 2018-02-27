using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float health = 20f;
    public GameObject destroyed;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Instantiate(destroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}