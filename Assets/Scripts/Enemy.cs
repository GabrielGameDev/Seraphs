using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fireRate = 1f;
	public Transform tiroSpawner;
	public Rigidbody2D tiroPrefab;
	public float shotForce = 7f;

	public float speed = 5f;
	public float minHeight = 1;

	float startSpeed;

	Transform player;

	private void Start()
	{
		StartCoroutine(Atirar());
		player = GameObject.FindGameObjectWithTag("Player").transform;
		startSpeed = speed;
	}

	// Update is called once per frame
	void Update()
    {
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		if (transform.position.y < minHeight)
		{
			transform.position = new Vector2(transform.position.x, minHeight);
		}
		else if (transform.position.y < minHeight + 3)
		{
			speed = speed * 0.9f;
		}

		speed = Mathf.Clamp(speed, startSpeed / 2, startSpeed);

		var dir = player.position - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
	}

	IEnumerator Atirar()
	{
		while (true)
		{
			yield return new WaitForSeconds(fireRate);
			Rigidbody2D tiro = Instantiate(tiroPrefab, tiroSpawner.position, tiroSpawner.rotation);
			tiro.AddForce(tiroSpawner.right * shotForce, ForceMode2D.Impulse);
		}

	}
}
