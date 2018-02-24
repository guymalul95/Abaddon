using UnityEngine;

public enum GrenadeType
{
	Timer,
	Impact
}

public class GrenadeScript : MonoBehaviour {

	public GameObject ExplosionObjectPrefab;
	public GrenadeType GrenadeType;
    public float GrenadeTimeMillis;

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

        // TODO: Sphere Raycast for enemy layer / destroyable layer

		Destroy(gameObject);
    }
}
