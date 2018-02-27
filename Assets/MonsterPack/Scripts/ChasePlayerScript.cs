using UnityEngine;
using UnityEngine.AI;

enum ChaseState
{
    Chase,
    Attack
}

public class ChasePlayerScript : MonoBehaviour
{
    GameObject player;
    Target enemyStats;
    NavMeshAgent nav;
    Animator animator;
    ChaseState state;
    private float AttackRange;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyStats = GetComponent<Target>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        state = ChaseState.Chase;
        AttackRange = nav.stoppingDistance;
    }


    void Update ()
    {
        // If the enemy and the player have health left...
        if (enemyStats.Health == 0)
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
            return;
        }

        // Enemy is alive
        if(state == ChaseState.Chase)
            nav.SetDestination(player.transform.position);
    }

    float GetDistanceToPlayer()
    {
        Vector3 enemyGroundPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerGroundPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        return Vector3.Distance(enemyGroundPos, playerGroundPos);
    }

    void FixedUpdate()
    {
        if (GetDistanceToPlayer() < AttackRange && state == ChaseState.Chase) {
            state = ChaseState.Attack;
            animator.SetTrigger("Attack");
            
        }
    }

    public void PostAttack()
    {
        state = ChaseState.Chase;

        if (GetDistanceToPlayer() < AttackRange + 1.0f)
            player.GetComponent<PlayerState>().TakeDamage(enemyStats.Power);
    }
}