using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour {
    public GameObject ObstancleToRemove;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetString("MainGameScene", SceneManager.GetActiveScene().name);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if(Input.GetButtonDown("Action"))
        {
            // Switch to mini game
            Scene mainScene = SceneManager.GetActiveScene();

            Destroy(gameObject);

            if (ObstancleToRemove != null)
                Destroy(ObstancleToRemove);

            foreach (GameObject obj in mainScene.GetRootGameObjects())
                obj.SetActive(false);

            // Load minigame
            SceneManager.LoadScene("MiniGame", LoadSceneMode.Additive);
        }
    }
}
