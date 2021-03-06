﻿using UnityEngine;

public enum GrenadeType
{
	Timer,
	Impact
}

public class GrenadeScript : MonoBehaviour {

	public GameObject ExplosionObjectPrefab;
	public GrenadeType GrenadeType;
    public float GrenadeTimeMillis;
    public float Radius;

	// Use this for initialization
	void Start () {
    }

	// Update for timer
	void Update()
	{
		if(GrenadeType.Timer != GrenadeType) return;

        GrenadeTimeMillis -= Time.deltaTime * 1000;

		if(GrenadeTimeMillis <= 0)
		{
			Explode();
		}
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
    {
		if(GrenadeType != GrenadeType.Impact) return;

        if(collision.gameObject.tag != "Player")
		{
			Explode();
		}
    }
	
	private void Explode()
	{
		Instantiate(
		ExplosionObjectPrefab,
		transform.position,
		Quaternion.identity);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius, Vector3.up);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                GameObject enemy = hit.collider.gameObject;
                enemy.GetComponent<Target>().TakeDamage(50);
            }
            else if (hit.collider.gameObject.CompareTag("MetalBox"))
            {
                GameObject enemy = hit.collider.gameObject;
                enemy.GetComponent<Destructible>().TakeDamage(50);
            }
        }

        Destroy(gameObject);
    }
}
