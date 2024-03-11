using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varinha : MonoBehaviour
{

    Transform player;
    Vector3 offset;

	private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;	
        offset = transform.position - player.position;
    }

	// Update is called once per frame
	void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = player.position + offset;

    }
}
