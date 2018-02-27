using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ChasePlayerScript))]
[RequireComponent(typeof(NavMeshAgent))]
public class Target : MonoBehaviour {
    public float Health;
    public int Power;
    public TextMesh FloatTextMesh;
    private Animator animator;
    private PlayerState PlayerState;

    void Start()
    {
        animator = GetComponent<Animator>();
        FloatTextMesh.text = Health.ToString();
        PlayerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
        else
            animator.SetTrigger("Damage");

        FloatTextMesh.text = (Health > 0 ) ? Health.ToString() : "DEAD";
        // Animation
    }

    private void Die()
    {
        GetComponent<ChasePlayerScript>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        PlayerState.KilledEnemy();

        animator.SetTrigger("Die");
        Destroy(gameObject, 5.0f);
    }
}
