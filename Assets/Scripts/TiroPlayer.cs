using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    public float damage = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
		EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
			enemy.TakeDamage(damage, transform.position);
			if(enemy.destroyPlayerBullet)
			{
				Destroy(gameObject);
			}
			
		}
		
	}
}
