using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 fireRate = new Vector2(1f,2f);
	public Transform tiroSpawner;
	public Rigidbody2D tiroPrefab;
	public float shotForce = 7f;

	public float speed = 5f;
	public Vector2 minHeight = new Vector2(1f, 4f);

	float startSpeed;

	Transform player;

	public float distanceToPlayer;
	float newMinHeight;

	private void Start()
	{
		StartCoroutine(Atirar());
		player = GameObject.FindGameObjectWithTag("Player").transform;
		startSpeed = speed;
		distanceToPlayer = Random.Range(8f, 16f);
		newMinHeight = Random.Range(minHeight.x, minHeight.y);
	}

	// Update is called once per frame
	void Update()
    {
		float distance = Vector2.Distance(transform.position, player.position);
		if (distance > distanceToPlayer)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		}
		
		if (transform.position.y < newMinHeight)
		{
			transform.position = new Vector2(transform.position.x, newMinHeight);
		}
		if (transform.position.y < newMinHeight + 2)
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
			yield return new WaitForSeconds(Random.Range(fireRate.x, fireRate.y));
			Rigidbody2D tiro = Instantiate(tiroPrefab, tiroSpawner.position, tiroSpawner.rotation);
			tiro.AddForce(tiroSpawner.right * shotForce, ForceMode2D.Impulse);
		}

	}
}
