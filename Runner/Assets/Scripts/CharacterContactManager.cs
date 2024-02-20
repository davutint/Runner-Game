using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterContactManager : MonoBehaviour
{
	[SerializeField]
	Character character;
	[SerializeField]
	ParticleSystem HitTheWallParticle;
	
	[SerializeField]
	ParticleSystem FallParticle;
	
	[SerializeField]
	ParticleSystem ContactParticle;
   
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("DüzEngel"))
		{
			//3 can burada eksilsin
			
			character.FallEvent?.Invoke();
			ContactParticlePlay();
			FallParticlePlay();
			character.GetHit?.Invoke();
			
		}
		if (other.CompareTag("Engel"))
		{
			//character.GetHit?.Invoke(); burada direkt ölsün karakter
			character.FallBackEvent?.Invoke();
			ParticlePlay();
		}
		
		if (other.CompareTag("Coin"))
		{
			TextManager.instance.AddCoin();
			other.gameObject.GetComponent<CoinRotate>().CollectPlay();
			
		}
		
	}
	
	private void ParticlePlay()
	{
		HitTheWallParticle.Play();
	}
	private void FallParticlePlay()
	{
		FallParticle.Play();
	}
	private void ContactParticlePlay()
	{
		ContactParticle.Play();
	}
	

	
}
