using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject enemyPrefab;
	public int round;

	public int enemiesAlive;

	public bool gameOver;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}


	private void Start()
	{
		StartCoroutine(GameLoop());
	}

	IEnumerator GameLoop()
	{
		while (!gameOver)
		{
			yield return StartCoroutine(Round());
		}
		
	}

	IEnumerator Round()
	{		
		SpawnEnemies();
		while (enemiesAlive > 0)
		{
			yield return null;
		}
		round++;
	}

	void SpawnEnemies()
	{
		for (int i = 0; i < round + 3; i++)
			{
				Instantiate(enemyPrefab, new Vector2(Random.Range(-17f, 17f), 11), Quaternion.identity);
				enemiesAlive++;
			}
		
	}
}
