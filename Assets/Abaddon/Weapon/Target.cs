using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ChasePlayerScript))]
[RequireComponent(typeof(NavMeshAgent))]
public class Target : MonoBehaviour {
    public float health = 100f;

    private Animator targetAnimator;

    void Start()
    {
        targetAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
            Die();
        else
            targetAnimator.SetTrigger("Damage");


        // Animation
    }

    private void Die()
    {
        GetComponent<ChasePlayerScript>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>().KilledEnemy();

        targetAnimator.SetTrigger("Die");
        Destroy(gameObject, 10.0f);
    }
}
