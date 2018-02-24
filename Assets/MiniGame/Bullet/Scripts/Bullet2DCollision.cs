using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet2DCollision : MonoBehaviour 
{
    private Vector3 PrevPosition;
    public int Damage;

	// Use this for initialization
	void Start()
    {
		PrevPosition = transform.position;
	}

    void FixedUpdate()
    {
        PrevPosition = transform.position;
        transform.position += transform.up * 0.15f;
        Vector3 vec = transform.position - PrevPosition;
        RaycastHit2D rayhit;

        rayhit = Physics2D.Raycast(PrevPosition,(vec).normalized,vec.magnitude,Physics.DefaultRaycastLayers);

        if(null == rayhit.collider) return;
        
        string tag = rayhit.collider.gameObject.tag;

        switch(tag)
        {
            case "Enemy":
            {
                if(!CompareTag("Enemy"))
                {
                    // We hit the enemy
                    GameObject enemy = rayhit.collider.gameObject;
                    var script = enemy.GetComponent<EnemyScript>();
                    script.Hurt(Damage);
                    
                    break;
                }
                
                return;
            }
            case "Player":
                if(!CompareTag("Player"))
                {
                    // We hit the player
                    GameObject player = rayhit.collider.gameObject;
                    var script = player.GetComponent<PlayerScript>();
                    script.Hurt(Damage);
                    
                    break;
                }
                
                return;
            default: {
                Destroy(gameObject);
                return; 
            }
        }

        // Destroy Bullet
        Destroy(gameObject);
    }
}
