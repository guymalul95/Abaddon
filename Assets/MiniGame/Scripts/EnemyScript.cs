using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyScript : MonoBehaviour {
	public float Health;
	private Animator HurtIndicatorAnim;
	public GameObject ExplosionPrefab;
	private GameObject TargetPlayer;
	private float MinimumDistance;
	private bool IsSleeping;

	// Use this for initialization
	void Start() {
		IsSleeping = false;
		HurtIndicatorAnim = GetComponent<Animator>();
	}

	public void SetTarget(GameObject target, float minDistance) 
	{
		TargetPlayer = target;
		MinimumDistance = minDistance;
	}

	void FixedUpdate()
	{
		if(IsSleeping) return;

		transform.position -= transform.up * 0.05f;

		Vector3 playerLine = new Vector3(transform.position.x,TargetPlayer.transform.position.y,transform.position.z);
		float distance = Vector3.Distance(transform.position,playerLine);

		if(distance <= MinimumDistance)
			IsSleeping = true;
	}

	public void Hurt(float damage)
	{
		Health-=damage;

		HurtIndicatorAnim.SetTrigger("Hurt");

		if(Health <= 0)
			Explode();
	}

	private void Explode()
	{
		var playerScript = TargetPlayer.GetComponent<PlayerScript>();
		playerScript.KilledEnemy();

		Instantiate(
		ExplosionPrefab,
		transform.position,
		Quaternion.identity);

		Destroy(gameObject);
    }
}
