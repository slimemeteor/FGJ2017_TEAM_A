using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed;

    private Rigidbody2D rb2D;
    private Animator animPlayer;

    private Vector3 movement;
	// Use this for initialization
	void Awake () 
	{
        rb2D = GetComponent<Rigidbody2D>();			
	}

    private void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
             
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb2D.AddForce(movement * speed);

        if (Input.GetKeyDown("space")&&rb2D.velocity.y==0)
        {
            rb2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    
}
