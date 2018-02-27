using UnityEngine;

[RequireComponent(typeof(LevelScript))]
public class MainEnemySpawner : MonoBehaviour {

    public GameObject[] Enemies;
    public int MaxEnemiesAlive;
    private int CurrentAliveEnemies;
    private GameObject[] Spawned;
    private LevelScript LevelScript;
    public int MinimumScoreToActive;
    private bool IsPlayerInArea;

    // Use this for initialization
    void Start () {
        LevelScript = GetComponent<LevelScript>();
        MaxEnemiesAlive = 0;
        CurrentAliveEnemies = 0;
        IsPlayerInArea = false;
        Spawned = new GameObject[MaxEnemiesAlive];
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

    void LateUpdate () {
        if (CurrentAliveEnemies >= MaxEnemiesAlive ||
            !LevelScript.ShouldBeActive(MinimumScoreToActive) ||
            IsPlayerInArea)
           return;

        // Spawn enemy
	}
}
