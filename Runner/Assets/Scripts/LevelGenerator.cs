using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public GameObject[] groundPrefabs; // Zemin objeleri, bunları çeşitlendir
	public Transform playerTransform; 
	public int GroundCount; // Başlangıçtaki zemin sayısı,bunu instantiate yapmayacak şekilde optimize et uygun sayıyı bul.//şu anda 6 uygun
	public float groundLength; // Zemin objelerinin uzunluğunu 5 ile çarpmak gerekiyor, zemin prefabı farklı boyutlandırılmış
	public float despawnThreshold = 20f; // Karakterin arkasındaki zeminleri kaldırma mesafesi. Burası düzgün hesaplanmıyor, starterpack prefabları farklı bir boyutlandırma yapmış,deneme yanılma ile yap
	public float spawnOffset = 5f; // Yeni zeminlerin karakterin önüne ekleneceği mesafe,burası da deneme yanılma ile hallolmalı

	private List<GameObject> groundPool = new List<GameObject>(); // Kullanılabilir zemin objeleri
	private List<GameObject> activeGrounds = new List<GameObject>(); // Aktif zemin objeleri

	void Start()
	{
		// Zemin objelerini başlangıçta oluştur ve havuzda sakla
		for (int i = 0; i < GroundCount; i++)
		{
			GameObject groundObject = GenerateRandomGround();
			groundObject.SetActive(false);
			groundPool.Add(groundObject);
		}

		// Başlangıçta zemin objelerini yerleştir
		float initialSpawnZ = 0f;
		for (int i = 0; i < GroundCount; i++)
		{
			GameObject groundObject = GetPooledGround();
			groundObject.transform.position = new Vector3(0f, 0f, initialSpawnZ);
			groundObject.SetActive(true);
			activeGrounds.Add(groundObject);
			initialSpawnZ += groundLength;
		}
	}

	void Update()
	{
		// Karakterin arkasındaki zeminleri kaldır
		if (playerTransform.position.z - despawnThreshold > activeGrounds[0].transform.position.z)
		{
			GameObject groundToDespawn = activeGrounds[0];
			activeGrounds.RemoveAt(0);
			groundToDespawn.SetActive(false);
			groundPool.Add(groundToDespawn);
		}

		// Yeni zeminler oluştur
		if (playerTransform.position.z + spawnOffset > activeGrounds[activeGrounds.Count - 1].transform.position.z)
		{
			GameObject newGround = GetPooledGround();
			newGround.transform.position = new Vector3(0f, 0f, activeGrounds[activeGrounds.Count - 1].transform.position.z + groundLength);
			newGround.SetActive(true);
			activeGrounds.Add(newGround);
		}
	}

	GameObject GenerateRandomGround()
	{
		// Rastgele bir zemin prefabı seç ve oluştur
		return Instantiate(groundPrefabs[Random.Range(0, groundPrefabs.Length)], Vector3.zero, Quaternion.identity);
		
	}

	GameObject GetPooledGround()
	{
		// Kullanılabilir bir zemin objesi al veya oluştur
		if (groundPool.Count > 0)
		{
			GameObject pooledGround = groundPool[0];
			groundPool.RemoveAt(0);
			return pooledGround;
		}
		else
		{
			Debug.Log("Instantiate Etti");//zemin sayısnı 6 yap
			return GenerateRandomGround();
		}
	}
}
