using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString("MainGameScene", SceneManager.GetActiveScene().name);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if(Input.GetButtonDown("Action"))
        {
            // Switch to game
            SceneManager.LoadScene("MiniGame",LoadSceneMode.Single);
        }
    }
}
