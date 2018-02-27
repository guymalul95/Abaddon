using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ChasePlayerScript))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
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
        // prevent second time
        if (Health <= 0) return;
        
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            FloatTextMesh.text = "DEAD";
            animator.SetTrigger("Die");
            PlayerState.KilledEnemy();
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");
            FloatTextMesh.text = Health.ToString();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
