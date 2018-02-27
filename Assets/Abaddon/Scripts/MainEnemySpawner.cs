using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LevelScript))]
public class MainEnemySpawner : MonoBehaviour {

    public GameObject[] Enemies;
    public int MaxEnemiesAlive;
    public float EnemySpawnDelaySec;
    private float LastSpawnTime;
    public int MaxTotalSpawn;
    private int TotalSpawned;
    private int CurrentAliveEnemies;
    private List<GameObject> Spawned;
    private LevelScript LevelScript;
    public int MinimumScoreToActive;
    private bool IsPlayerInArea;

    // Use this for initialization
    void Start () {
        LevelScript = GetComponent<LevelScript>();
        CurrentAliveEnemies = 0;
        IsPlayerInArea = false;
        Spawned = new List<GameObject>(MaxEnemiesAlive);
        LastSpawnTime = 0;

        Random.InitState(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        IsPlayerInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        IsPlayerInArea = false;
    }

    void LateUpdate()
    {
        RemoveDeadEnemies();

        if(CurrentAliveEnemies < MaxEnemiesAlive)
            LastSpawnTime -= Time.deltaTime;

        if (TotalSpawned >= MaxTotalSpawn ||
            CurrentAliveEnemies >= MaxEnemiesAlive ||
            !LevelScript.ShouldBeActive(MinimumScoreToActive) ||
            IsPlayerInArea ||
            LastSpawnTime > 0)
            return;

        // Spawn enemy
        
        GameObject enemy = Instantiate(Enemies[(int)(Random.value * 100) % Enemies.Length]);
        Vector3 pos = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
        enemy.transform.SetPositionAndRotation(pos, Quaternion.identity);

        Spawned.Add(enemy);
        CurrentAliveEnemies++;
        TotalSpawned++;
        LastSpawnTime = EnemySpawnDelaySec;
    }

    private void RemoveDeadEnemies()
    {
        if (CurrentAliveEnemies == 0)
            return;

        // Remove dead enemies
        List<int> toDestroyIndexes = new List<int>(2);
        for (int i = 0; i < CurrentAliveEnemies; ++i)
        {
            if (!Spawned[i].activeSelf)
                toDestroyIndexes.Add(i);
        }
        
        foreach (int objIndx in toDestroyIndexes)
        {
            Destroy(Spawned[objIndx]);
            Spawned.RemoveAt(objIndx);
            CurrentAliveEnemies--;
        }

        toDestroyIndexes.Clear();
    }
}
