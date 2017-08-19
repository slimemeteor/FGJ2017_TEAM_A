using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed;
    
    public Transform attackFrom;
    public GameObject lazer;

    private Rigidbody2D rb2D;
    private Animator animPlayer;
    private Vector2 spawnPosition;

    //int lazerSize = 0;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);

            if (rb2D.velocity.y == 0)
            {
                rb2D.AddForce(movement * speed);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rb2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                }
            }
            else if (rb2D.velocity.y != 0)
            {
                rb2D.AddForce(movement * speed);
            }
        
    }

    private void Update()
    {
        spawnPosition = attackFrom.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(lazer, spawnPosition, Quaternion.identity);

        }
    }




}
