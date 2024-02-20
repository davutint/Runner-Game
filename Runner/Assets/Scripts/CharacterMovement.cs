using System.Collections;
using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Media;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
	[SerializeField]
	public float speed,jumpForce=5;
	[SerializeField]
	float radius;
	[SerializeField]
	Animator animator;
	
	[SerializeField]
	Material CharacterMat;
	
	private CapsuleCollider capsuleCollider;
	[SerializeField]
	private Rigidbody rig;
	[SerializeField]
	private CharacterHealth characterHealth;
	[SerializeField]
	private UIManager UIManager;
	[SerializeField]
	private Character character;
	
	
	
	bool isGround;
	
	private float OmuzAnimWait=4f;
	private void Awake()
	{
		capsuleCollider=GetComponent<CapsuleCollider>();
		animator.SetBool("Alive",true);
		characterHealth=GetComponent<CharacterHealth>();
		CharacterMat.DOColor(Color.cyan,.1f);
		
	}
	public void CharacterMoveToward()
	{
		transform.Translate(Vector3.forward*speed*Time.deltaTime);
		
	}
	
#region  EVENTCALLBACKS
	public void FallBackAnim()
	{
		speed=0;
		animator.SetTrigger("FallBack");
		CharacterMat.DOColor(Color.red,1f);
		character.DeadEvent?.Invoke();
		
		
	}
	public void FallEventAnim()
	{
		
		
		StartCoroutine(Wait());
		animator.SetTrigger("Fall");
		capsuleCollider.enabled=false;
		rig.useGravity=false;
		CharacterMat.DOFade(0,.5f);
		StartCoroutine(AnimEnded(4.2f));//anim bitişinde hız ver
		
	}
	
	public void HitSideWallEventAnim()
	{
		//speed 0 yao
		//animasyon oynat
		
		//StartCoroutine(Wait());
		speed=0;
		CharacterSetXPozNormalize();
		animator.SetTrigger("HitSideWall");
		
		CharacterMat.DOFade(0,.5f);
		
		StartCoroutine(AnimEnded(3));
		//StartCoroutine(WaitForBackToGame());
	}
	
	public void Dead()
	{
		animator.SetBool("Alive",false);
		CharacterMat.DOColor(Color.red,1f);
		UIManager.DisplayGameOver();
		TextManager.instance.HealthUpdate(0);
	}
#endregion
	
	
#region MOVEMENTS
	public void RightMove()
	{
		float xpoz=transform.position.x;
		
		if (xpoz>=1&&xpoz<=5)
		{
			if(xpoz!>=7&&xpoz!<=9) return;
			
			transform.DOLocalMoveX(8,0.5f);
			//RightRotateAnim();
		}
		else if(xpoz>5&&xpoz<9)
		{
			
			if(xpoz!>=11&&xpoz!<=13) return;
			
			
			transform.DOLocalMoveX(12,0.5f);
			
			//RightRotateAnim();
		}
		
		
	}
	public void LeftMove()
	{
		float xpoz=transform.position.x;
		
		if (xpoz>9&&xpoz<15)
		{
			if(xpoz!>=7&&xpoz!<=9) return;
			
			
			transform.DOLocalMoveX(8,0.5f);
	
			//LeftRotateAnim();
			
		}
		else if(xpoz>5&&xpoz<9)
		{
			if(xpoz!>=3&&xpoz!<=5) return;
			
			
			transform.DOLocalMoveX(4,0.5f);
			//LeftRotateAnim();
		}
	}
#endregion	

	
#region ROTATIONS
	private void LeftRotateAnim()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("RUN"))
		{
			animator.SetTrigger("SolRotate");
		}
	}
	private void RightRotateAnim()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("RUN"))
		{
			animator.SetTrigger("SagRotate");
		}
	}
#endregion
	
	
#region Status
	public void Jump()
	{
		
		rig.velocity = Vector2.up * jumpForce;
		rig.useGravity=true;
		animator.SetTrigger("Jump");
		
		
	}


	
	public void Slide()
	{
		animator.SetTrigger("Slide");
		//capsuleCollider.isTrigger=true;
		capsuleCollider.height=0.4f;
		capsuleCollider.center=new Vector3(0,0.26f,0);
		rig.useGravity=false;
		StartCoroutine(SlideEventEnd());
		
	}
#endregion
	
	private void OnCollisionStay(Collision other)
	{
		
		Debug.Log("yerde");
		isGround=true;
		IsGround();
		
	}
	private void OnCollisionExit(Collision other)
	{
		Debug.Log(message: "havada");
		isGround=false;
		IsGround();
	}
	
	public void StartGameEvent()
	{
		speed=9;
	}
	
	
	
	public bool IsGround()
	{
		return isGround;
	}
	#region COROUTINES
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(.8f);
		speed=0f;
	}
	IEnumerator AnimEnded(float WaitTime)
	{	
		yield return new WaitForSeconds(WaitTime);
		if (characterHealth.health>0)
		{
			CharacterMat.DOFade(1,.5f);
			capsuleCollider.enabled=true;
			rig.useGravity=true;
			speed=9;
		}
		
	}
	
	private void CharacterSetXPozNormalize()
	{
		float xpoz=transform.position.x;
		
		if (xpoz>=6&&xpoz<=10)
		{
			transform.position=new Vector3(8,transform.position.y,transform.position.z);
		}
		else if(xpoz<12&&xpoz>10)
		{
			transform.position=new Vector3(12,transform.position.y,transform.position.z);
			
		}
		else if(xpoz>4&&xpoz<6)
		{
			transform.position=new Vector3(4,transform.position.y,transform.position.z);
			
		}
		
	}
	
	IEnumerator SlideEventEnd()
	{
		yield return new WaitForSeconds(.8f);
		//capsuleCollider.isTrigger=false;
		rig.useGravity=true;

		capsuleCollider.height=1.6f;
		capsuleCollider.center=new Vector3(0,0.72f,0);
	}
	
	
#endregion

	void OnTriggerEnter(Collider other)
	{
		//domove collider çarpışmasını bypass geçiyor,bu çözebilir
		
		if (other.CompareTag("Deneme"))
		{
			DOTween.Kill(transform);
			character.HitWallEvent?.Invoke();
			//LeftorRight = false;
			character.GetHit?.Invoke();
			
			//StopAllCoroutines();
			//StartCoroutine(WaitBeforeMoveAgain());
		}
	}
	
	 void OnDrawGizmosSelected()
	{
		// Gizmoz çiziminde kullanılacak renk
		Gizmos.color = Color.yellow;

		// Küre çizgisi çizimi
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	private void OnDrawGizmos()
	{
		OnDrawGizmosSelected();
		
	}
	
}
