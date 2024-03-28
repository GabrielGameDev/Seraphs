using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float maxHealth = 12;
	public ParticleSystem damageEffect;
	public bool destroyPlayerBullet = true;
	public Rigidbody2D floatNumber;
	float currentHealth;
	SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage, Vector2 position)
	{
		currentHealth -= damage;
		Color color = spriteRenderer.color;
		color.a = currentHealth / maxHealth;
		spriteRenderer.color = color;
		damageEffect.transform.position = position;
		damageEffect.Play();

		if(floatNumber != null)
		{
			Rigidbody2D floatNumberInstance = Instantiate(floatNumber, transform.position, Quaternion.identity);
			floatNumberInstance.GetComponentInChildren<TMP_Text>().text = damage.ToString();
			floatNumberInstance.AddForce(new Vector2(Random.Range(-5f, 5f), 10), ForceMode2D.Impulse);
			Destroy(floatNumberInstance.gameObject, 0.5f);
		}
		

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{		
		damageEffect.transform.parent = null;
		damageEffect.transform.localScale = new Vector3(1, 1, 1);
		Destroy(damageEffect.gameObject, damageEffect.main.duration);
		Destroy(gameObject);
		
		if(destroyPlayerBullet)
		{
			GameManager.instance.enemiesAlive--;
		}
		
	}
}
