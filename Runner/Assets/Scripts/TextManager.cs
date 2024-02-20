using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
	public static TextManager instance;
	public TextMeshProUGUI HighScore;
	public TextMeshProUGUI CurrentScore;
	public TextMeshProUGUI CoinCount;
	public TextMeshProUGUI HealthText;
	
	
	private int coin;
	private void Awake()
	{
		if (instance!=null)
		{
			Destroy(this);
		}
		else
		instance=this;
	}
	private void Start()
	{
		HighScoreUpdate();
	}
	
	public void HighScoreUpdate()
	{
		HighScore.SetText(PlayerPrefs.GetFloat("HighScore").ToString());
		
	}
	public void HealthUpdate(int health)
	{
		
		HealthText.SetText(health.ToString());
	}
	
	public void AddScore(int Score)
	{
		
		CurrentScore.SetText(Score.ToString());
		CheckHighScore(Score);
	}
	
	public void CheckHighScore(int Score)
	{
		if (Score>PlayerPrefs.GetFloat("HighScore"))
		{
			//yeni skore score
			PlayerPrefs.SetFloat("HighScore",Score);
			//renk değiş
			HighScore.SetText(Score.ToString());
			CurrentScore.color=Color.red;
		}
	}
	
	public void AddCoin()
	{
		coin+=1;
		CoinCount.SetText(coin.ToString());
	}
	
	
}
