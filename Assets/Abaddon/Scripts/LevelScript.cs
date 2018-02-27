using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    private PlayerState PlayerState;

    // Use this for initialization
    void Start () {
        PlayerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }
	
    public bool ShouldBeActive(int minimumScore)
    {
        return PlayerState.IsAlive && (PlayerState.Score >= minimumScore);
    }
}
