using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject enemyPrefab;
	public int round;
	public int enemyDamage = 1;

	public int enemiesAlive;

	public bool gameOver;

	public List<Upgrade> upgrades = new List<Upgrade>();

	public GameObject upgradePanel;
	public TMP_Text[] upgradeTitles;
	public TMP_Text[] upgradeDescriptions;
	public Image[] upgradeImages;
	bool choosingUpgrade;

	[Header("Player Stats")]
	public float playerDamage = 4;
	public Varinha varinha;
	public PlayerMovement player;

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
			yield return StartCoroutine(ChoosingUpgrade());
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
		enemyDamage++;
	}

	IEnumerator ChoosingUpgrade()
	{
		choosingUpgrade = true;
		System.Random random = new System.Random();
		for (int i = 0; i < upgrades.Count; i++)
		{
			int j = random.Next(i, upgrades.Count);
			Upgrade temp = upgrades[i];
			upgrades[i] = upgrades[j];
			upgrades[j] = temp;
		}
		for (int i = 0; i < 3; i++)
		{
			upgradeTitles[i].text = upgrades[i].title;
			upgradeDescriptions[i].text = upgrades[i].description;
			upgradeImages[i].sprite = upgrades[i].image;
		}
		upgradePanel.SetActive(true);
		while (choosingUpgrade)
		{
			yield return null;
		}
		
		upgradePanel.SetActive(false);
	}

	void SpawnEnemies()
	{
		for (int i = 0; i < round + 3; i++)
			{
				GameObject newEnemy = Instantiate(enemyPrefab, new Vector2(Random.Range(-17f, 17f), 11), Quaternion.identity);
			newEnemy.GetComponent<EnemyHealth>().maxHealth = round * 4 + 1;	
			enemiesAlive++;
			}
		
	}

	public void SetUpgrade(Upgrade upgrade)
	{
		if(upgrade.type == UpgradeType.Catalyst)
		{
			playerDamage += 2;
		}
		else if(upgrade.type == UpgradeType.Resonance)
		{
			varinha.fireRate *= 0.90f;
		}
		else if(upgrade.type == UpgradeType.Swift)
		{
			player.speed *= 1.2f;
		}
		else if(upgrade.type == UpgradeType.Thunderbolt)
		{
			//criar trobão
		}
		choosingUpgrade = false;
	}
}
