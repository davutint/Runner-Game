using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinRotate : MonoBehaviour
{
	[SerializeField]
	Vector3 rot;
	float duration=1f;
	[SerializeField]
	ParticleSystem CollectParty;
	void Start()
	{
		duration=Random.Range(.7f,1.1f);
		transform.DORotate(rot, duration, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetRelative().SetEase(Ease.Linear);

	}
	private void OnDestroy()
	{
		DOTween.Kill(transform);
	}
	
	public void CollectPlay()
	{
		if (CollectParty != null)
		{
			CollectParty.transform.SetParent(null);
			CollectParty.Play();
		}

		Destroy(gameObject);
	}


}
