using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {

    void Start()
    {
        Scene minigameScene = SceneManager.GetSceneByName("MiniGame");
        SceneManager.SetActiveScene(minigameScene);
    }

    // Called from animator after text is displayed
    public void EndGame()
	{
		// Switch scene
		Debug.Log("MINIGAME ENDED");

		string gameScene = PlayerPrefs.GetString("MainGameScene");
        Scene mainScene = SceneManager.GetSceneByName(gameScene);
        Scene minigameScene = SceneManager.GetSceneByName("MiniGame");

        foreach (GameObject obj in minigameScene.GetRootGameObjects())
        {
            obj.SetActive(false);
            if (null != obj)
                Destroy(obj);
        }

        SceneManager.SetActiveScene(mainScene);

        foreach (GameObject obj in mainScene.GetRootGameObjects())
        {
            if (null != obj)
                obj.SetActive(true);
        }
    }
}
