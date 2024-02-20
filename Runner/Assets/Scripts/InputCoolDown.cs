using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCoolDown : MonoBehaviour
{	[SerializeField]
	private float cooldownTime;
	private float _nextInputTime;
	
	public bool IsCoolingDown =>Time.time<_nextInputTime;
	public void StartCoolDown()=>_nextInputTime=Time.time+cooldownTime;
}
