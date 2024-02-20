using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	GameObject StartPanel;
	[SerializeField]
	GameObject GameOverPanel;
	[SerializeField]
	Character character;
	[SerializeField]
	Image ScoreContainer;
	[SerializeField]
	Image healtContainer;
	
	
	
	public void NotDisplayStartPanel()
	{
		StartPanel.transform.DOMoveY(2000,.55f).SetEase(Ease.InOutBack).OnComplete(()=>
		{
			StartPanel.SetActive(false);
			character.StartGameEvent?.Invoke();
		});
	}
	
	public void DisplayGameOver()
	{
		GameOverPanel.transform.DOLocalMoveX(0, .55f).SetEase(Ease.InOutBack);
		ScoreContainer.DOColor(Color.red,.55f);
		healtContainer.DOColor(Color.red,.55f);
		
	}
	
	public void StartGame()
	{
		NotDisplayStartPanel();
		
	}
	
	public void RestartGame()
	{
		SceneManager.LoadScene(sceneBuildIndex:0);
	}
	
}
