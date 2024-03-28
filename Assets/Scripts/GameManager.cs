using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

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
	public UpgradeCard[] upgradeCards;
	bool choosingUpgrade;

	public GameObject gameOverText;

	[Header("Player Stats")]
	public float playerDamage = 4;
	public Varinha varinha;
	public PlayerMovement player;
	public GameObject thunderboltPrefab;
	public bool hasThunderbolt;
	public float thunderboltFireRate = 8f;
	public float thunderboltDamage = 10;
	float nextThunderboltFire;

	[Header("Sounds")]
	AudioSource audioSource;
	public AudioClip thunderSound;
	public AudioClip[] enemyDamageSounds;
	public AudioClip playerDamageSound;
	public AudioClip playerShot;

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
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(GameLoop());
	}

	void Update()
	{
		if (hasThunderbolt && Time.time > nextThunderboltFire)
		{
			nextThunderboltFire = Time.time + thunderboltFireRate;
			GameObject newThunder = Instantiate(thunderboltPrefab, new Vector2(Random.Range(-17f, 17f), 13), Quaternion.identity);
			newThunder.GetComponent<TiroPlayer>().damage = thunderboltDamage;
			PlaySound(thunderSound);
			Destroy(newThunder, 0.67f);
		}
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
		Time.timeScale = 0;
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
			upgradeCards[i].SetUpgrade(upgrades[i]);
			
		}
		upgradePanel.SetActive(true);
		while (choosingUpgrade)
		{
			yield return null;
		}
		
		upgradePanel.SetActive(false);
		Time.timeScale = 1;
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
			hasThunderbolt = true;
			thunderboltFireRate *= 0.9f;
			thunderboltDamage += 1;
		}
		choosingUpgrade = false;
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.SetActive(true);
		Invoke("Restart", 2);
	}

	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void PlaySound(AudioClip clip)
	{
		audioSource.PlayOneShot(clip);
	}
}
