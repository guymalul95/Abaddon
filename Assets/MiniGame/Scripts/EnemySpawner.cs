using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] EnemiesPrefab;
	public GameObject TargetPlayer;
	public float SpawnDelayMillis;
	private float LastSpawnTime;
	public float SpawnRadius;
	private int NumEnemies;

	// Use this for initialization
	void Start () {
		LastSpawnTime = 1000;
		NumEnemies = EnemiesPrefab.Length;
        UnityEngine.Random.InitState((int)System.Environment.TickCount);
		Debug.Assert(SpawnRadius > 0);
	}
	
	// Update is called once per frame
	void Update () {
		LastSpawnTime -= Time.deltaTime * 1000;

		if(LastSpawnTime > 0) return;

		LastSpawnTime = SpawnDelayMillis;

		/* Spawn */
		int enemyId = ((int)(Random.value * 100)) % NumEnemies;
		Vector3 position = new Vector3(transform.position.x +
		Random.value * SpawnRadius * ((Random.value > 0.5f) ? 1 : -1),
		transform.position.y,transform.position.z);

		var enemy = (GameObject)Instantiate(
			EnemiesPrefab[enemyId],
			position,
			transform.rotation);

		var behavior = enemy.GetComponent<EnemyScript>();
		behavior.SetTarget(TargetPlayer,3f + ((int)(Random.value * 100)) % 3);
	}
}
