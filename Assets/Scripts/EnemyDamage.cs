using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    public float damage = 1;
    public bool destroyOnImpact = true;
    

    void OnTriggerEnter2D(Collider2D other)
    {
		PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
			playerHealth.TakeDamage(damage);
            if(destroyOnImpact)
            {
				Destroy(gameObject);
			}
		}
	}
}
