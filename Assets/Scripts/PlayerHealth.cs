using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 12;
    public float invincibleTime = 1;

	public Image healthBar;
	public TMP_Text healthText;

	float currentHealth;
    SpriteRenderer spriteRenderer;
    bool canTakeDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
        {
			return;
		}
        GameManager.instance.PlaySound(GameManager.instance.playerDamageSound);
		currentHealth -= damage;
        canTakeDamage = false;
        healthBar.rectTransform.localScale = new Vector2(currentHealth / maxHealth, 1);
        healthText.text = currentHealth + " / " + maxHealth;
        StartCoroutine(Flash());
		if (currentHealth <= 0)
        {
			Die();
		}
	}

    IEnumerator Flash()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(invincibleTime);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        canTakeDamage = true;
    }

    void Die()
    {
		GameManager.instance.GameOver();
	}
}
