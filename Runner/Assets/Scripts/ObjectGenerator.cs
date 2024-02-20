using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
   	public GameObject[] groundPrefabs;
	public GameObject[] CoinPrefabs;
	public GameObject[] KaymalıPrefabs;
	public Transform playerTransform; // Karakterin transformu
	public int ObjPoolSize = 10; // Havuz boyutu
	public int KaymalıPoolSize = 10;
	public int CoinPoolSize = 100;
	public float spawnOffset = 20f; // Yeni zeminlerin karakterin önüne ekleneceği mesafe
	public float spawnSpace=10f;

	private List<GameObject> groundPool = new List<GameObject>();
	private List<GameObject> CoinPool = new List<GameObject>(); 
	private List<GameObject> KaymalıPool = new List<GameObject>(); 

	private float lastSpawnZ; // Son oluşturulan zeminin Z pozisyonu

	void Start()
	{
		// Zeminleri havuza ekle
		for (int i = 0; i < ObjPoolSize; i++)
		{
			GameObject groundObject = InstantiateObj();
			groundObject.SetActive(false);
			groundPool.Add(groundObject);
		}
		for (int i = 0; i < CoinPoolSize; i++)
		{
			GameObject groundObject = InstantiateCoin();
			groundObject.SetActive(false);
			KaymalıPool.Add(groundObject);
		}
		for (int i = 0; i < KaymalıPoolSize; i++)
		{
			GameObject groundObject = InstantiateKaymalı();
			groundObject.SetActive(false);
			CoinPool.Add(groundObject);
		}

		// Başlangıçta ilk zeminleri oluştur
		lastSpawnZ = 10f;
		SpawnGround();
		/*for (int i = 0; i < groundPool.Count; i++)
		{
			SpawnObject();
		}
		for (int i = 0; i < CoinPool.Count; i++)
		{
			SpawnCoin();
		}
		for (int i = 0; i < KaymalıPool.Count; i++)
		{
			SpawnKaymalı();
		}*/
	}

	void Update()
	{
		// Karakterin ilerlemesine bağlı olarak yeni zeminler oluştur
		if (playerTransform.position.z + spawnOffset > lastSpawnZ)
		{
			
			SpawnGround();
		}
	}

	private void SpawnGround()
	{
		int rnd=Random.Range(0,100);
		if(playerTransform.GetComponent<CharacterMovement>().speed>0)
		{
			if (rnd>=0&&rnd<=70)
			{
				SpawnCoin();
			}
			else if(rnd>=71&&rnd<=89)
			{
				SpawnObject();
			}
			else if(rnd>=90&&rnd<=100)
			{
				SpawnKaymalı();
			}
			
		}
		
		
	}

	GameObject InstantiateObj()
	{
		int rnd = Random.Range(0, groundPrefabs.Length);
		// Rastgele bir zemin prefabı seç ve oluştur
		return Instantiate(groundPrefabs[rnd], Vector3.zero,groundPrefabs[rnd].transform.rotation);
	}
	GameObject InstantiateKaymalı()
	{
		int rnd = Random.Range(0, KaymalıPrefabs.Length);//eğer bulabilirsen veya zaman kalırsa çeşitlendir prefabları
		// Rastgele bir zemin prefabı seç ve oluştur
		return Instantiate(KaymalıPrefabs[rnd], Vector3.zero,KaymalıPrefabs[rnd].transform.rotation);
	}
	
	GameObject InstantiateCoin()
	{
		int rnd = Random.Range(0, CoinPrefabs.Length);
		// Rastgele bir zemin prefabı seç ve oluştur
		return Instantiate(CoinPrefabs[rnd], Vector3.zero,CoinPrefabs[rnd].transform.rotation);
	}

	private float RandomXPoz()
	{
		float left=2;
		float middle=6;
		float right=10;
		float rnd=Random.Range(0,30);
		if (rnd>=0&&rnd<=10)
		{
			return left;
		}
		else if (rnd>11&&rnd<=20)
		{
			return middle;
		}
		else if (rnd>=21&&rnd<=30)
		{
			return right;
		}
		return 400;
	}
	
	private float KaymalıXPoz()
	{
		float left=2.5f;
		float middle=6.5f;
		float right=10.5f;
		float rnd=Random.Range(0,30);
		if (rnd>=0&&rnd<=10)
		{
			return left;
		}
		else if (rnd>11&&rnd<=20)
		{
			return middle;
		}
		else if (rnd>=21&&rnd<=30)
		{
			return right;
		}
		return 400;
	}

	void SpawnObject()
	{
		
		
		// Havuzdaki bir zemini al
		GameObject groundObject = GetPooledGround();
		// Pozisyonunu ve aktifleştir
		groundObject.transform.position = new Vector3(x: RandomXPoz(), 0f, lastSpawnZ);
		groundObject.SetActive(true);
		// Son oluşturulan zeminin Z pozisyonunu güncelle
		lastSpawnZ += spawnSpace;
	}
	void SpawnKaymalı()
	{
		GameObject groundObject = GetPooledKaymalı();
		groundObject.transform.position = new Vector3(KaymalıXPoz(), 0f, lastSpawnZ);
		groundObject.SetActive(true);
		lastSpawnZ += 25f;
	}
	void SpawnCoin()
	{
		
		GameObject groundObject = GetPooledCoin();
		groundObject.transform.position = new Vector3(RandomXPoz(), 0f, lastSpawnZ);
		groundObject.SetActive(true);
		lastSpawnZ += 1f;
	}
	
	
	
	
	GameObject GetPooledCoin()
	{
		// Havuzdaki bir zemini döndür
		foreach (GameObject ground in CoinPool)
		{
			if (!ground.activeInHierarchy)
			{
				return ground;
			}
		}
		// Havuzda kullanılabilir zemin yoksa, yeni bir tane oluştur
		GameObject newGround = InstantiateCoin();
		CoinPool.Add(newGround);
		return newGround;
	}

	
	GameObject GetPooledKaymalı()
	{
		// Havuzdaki bir zemini döndür
		foreach (GameObject ground in groundPool)
		{
			if (!ground.activeInHierarchy)
			{
				return ground;
			}
		}
		// Havuzda kullanılabilir zemin yoksa, yeni bir tane oluştur
		GameObject newGround = InstantiateObj();
		groundPool.Add(newGround);
		return newGround;
	}
	
	GameObject GetPooledGround()
	{
		// Havuzdaki bir zemini döndür
		foreach (GameObject ground in groundPool)
		{
			if (!ground.activeInHierarchy)
			{
				return ground;
			}
		}
		// Havuzda kullanılabilir zemin yoksa, yeni bir tane oluştur
		GameObject newGround = InstantiateObj();
		groundPool.Add(newGround);
		return newGround;
	}
}
