using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float maxHealth = 12;
	public ParticleSystem damageEffect;
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
		GameManager.instance.enemiesAlive--;
	}
}
