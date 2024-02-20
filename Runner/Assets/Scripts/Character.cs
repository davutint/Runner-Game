using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
	[SerializeField]
	private CharacterMovement characterMovement;
	[SerializeField]
	private InputCoolDown ınputCoolDown;
	
	float StartPozZ;
	
	[Header("EVENTS")]
	[SerializeField]
	public UnityEvent GetHit;
	public UnityEvent FallEvent;
	public UnityEvent FallBackEvent;
	public UnityEvent HitWallEvent;
	public UnityEvent DeadEvent;
	public UnityEvent StartGameEvent;
	private int score;
	private float startSpeed=9;
	private float speedUp=1.0f;
	
	
	private void Start()
	{
		StartPozZ=transform.position.z;
		
	}
	private void LateUpdate()
	{
		characterMovement.CharacterMoveToward();
		CharacterInput();
		
	}
	private void Update()
	{
		GetScore();
		
	}
	
	private void GetScore()
	{
		if (characterMovement.speed>0)
		{	
			
		 // Karakterin ilerlediği mesafeyi hesapla ve skor olarak güncelle
			float distanceTraveled = transform.position.z - StartPozZ;
	   	 	score = Mathf.FloorToInt(distanceTraveled); // Mesafeyi tam sayı olarak al
			GettingSpeed();
			TextManager.instance.AddScore(score);
		}
		
	}
	
	private void GettingSpeed()
	{
		
		if (score % 100 == 0 && score > 0)
		{
			speedUp+=0.002f;
			characterMovement.speed=(speedUp*startSpeed);
			Debug.Log("Speed: "+characterMovement.speed);
		}
		
	}
	
	
	
	private void CharacterInput()
	{
		
		if(characterMovement.speed<=0)return;
		
		if (ınputCoolDown.IsCoolingDown)return;
		
		if (Input.GetKeyDown(KeyCode.A))
		{
			
			characterMovement.LeftMove();
			//ınputCoolDown.StartCoolDown();
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			
			characterMovement.RightMove();
			//ınputCoolDown.StartCoolDown();
		}
		else if(Input.GetKeyDown(KeyCode.Space))
		{
			if (characterMovement.IsGround())
			{
				
				characterMovement.Jump();
				//ınputCoolDown.StartCoolDown();
			}
			
			
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			if (characterMovement.IsGround())
			{
				
				characterMovement.Slide();
				//ınputCoolDown.StartCoolDown();
			}
		}
	
	}
	
}
