using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;

    bool isGrounded;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {      
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }

	private void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
	}
}
