using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour {
	private int Score;
	private float Health;
	public int ScoreToWin;
	private bool GodMode;
	public Text ScoreText;
	public Text HealthText;
	public Animator EndGameTextAnim;
	public Image EndGameImage;
	public Sprite WinSprite;
	public Sprite GameOverSprite;

	// Use this for initialization
	void Start () {
		GodMode = false;
		Health = 100;
		Score = 0;
		SetScore();
		SetHealth();
    }

	private void SetScore() 
	{
		ScoreText.text = "Score:  " + Score + " / " + ScoreToWin;
	}

	private void SetHealth()
	{
		HealthText.text = "Health: " + Health;
	}

	public void Hurt(float damage)
	{
		if(GodMode) return;

		Health-=damage;

		SetHealth();

		if(Health <= 0)
		{
			// We Lost
			EndGameImage.sprite = GameOverSprite;
			EndGameTextAnim.SetTrigger("Lose");
			PlayerPrefs.SetInt("MiniGameWin",0);

		}
	}

	public void KilledEnemy()
	{
		if(GodMode) return;
		
		Score+=100;
		SetScore();

		if(Score >= ScoreToWin)
		{
			GodMode = true;

			// We Won
			EndGameImage.sprite = WinSprite;
			EndGameTextAnim.SetTrigger("Win");
			PlayerPrefs.SetInt("MiniGameWin",1);
		}
	}
}
