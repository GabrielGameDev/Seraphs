using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varinha : MonoBehaviour
{

    public Transform tiroSpawner;
    public Rigidbody2D tiroPrefab;
    public float fireRate = 1f;
    public float shotForce = 10f;

    Transform player;
    Vector3 offset;

	private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;	
        offset = transform.position - player.position;

        StartCoroutine(Atirar());
    }

	// Update is called once per frame
	void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = player.position + offset;

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
