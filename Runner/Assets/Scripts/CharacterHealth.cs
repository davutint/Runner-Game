using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField]
	public int health;
	[SerializeField]
	Character character;
	public void Hit()
	{
		health--;
		Debug.Log("hasar aldı");
		TextManager.instance.HealthUpdate(health);
		if (health<=0)
		{
			character.DeadEvent?.Invoke();
			
			
		}
		
	}
	
}
