using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {

	// Called from animator after text is displayed
	public void EndGame()
	{
		// Switch scene
		Debug.Log("SCENE ENDED");

		string gameScene = PlayerPrefs.GetString("MainGameScene");

		SceneManager.LoadScene(gameScene);
	}
}
