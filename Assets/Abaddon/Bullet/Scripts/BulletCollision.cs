using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletCollision : MonoBehaviour 
{
    public GameObject bulletImpactPrefab_Metal;
    public GameObject bulletImpactPrefab_Wood;
    public GameObject bulletImpactPrefab_Concrete;

    private Vector3 PrevPosition;

	// Use this for initialization
	void Start()
    {
		PrevPosition = transform.position;
	}

    void FixedUpdate()
    {
        PrevPosition = transform.position;
        transform.position += transform.forward.normalized * 8;
        Vector3 vec = transform.position - PrevPosition;
        RaycastHit rayhit;

        if(!Physics.Raycast(PrevPosition,(vec).normalized,out rayhit,vec.magnitude,Physics.DefaultRaycastLayers))
            return;

        GameObject bulletImpactPrefab;
        string tag = rayhit.collider.gameObject.tag.ToLower();

        switch(tag)
        {
            case "metal": {
                bulletImpactPrefab = bulletImpactPrefab_Metal;
                break;
            }
            case "wood": {
                bulletImpactPrefab = bulletImpactPrefab_Wood;
                break;
            }
            case "concrete": {
                bulletImpactPrefab = bulletImpactPrefab_Concrete;
                break;
            }
            case "enemy":
            {
                bulletImpactPrefab = bulletImpactPrefab_Metal;
                break;
            }
            case "player":
                return;
            default: {
                Destroy(gameObject);
                return; 
            }
        }

        GameObject impact = (GameObject) Instantiate(
                    bulletImpactPrefab,
                    rayhit.point,
                    Quaternion.LookRotation(rayhit.normal)
        );

        impact.transform.parent = rayhit.collider.transform;
        // Destroy in 3 sec
        Destroy(impact, 3.0f);

        // Destroy Bullet
        Destroy(gameObject);
    }

}
