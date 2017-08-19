using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    public float speed;
    public GameObject spawnEnemy;
    Vector2 movement = new Vector2(-1, 0);

    private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.insideUnitCircle * 400;
        Debug.Log("Push!"+randomVector);
        rb2D.AddForce(randomVector);
        rb2D.AddForce(new Vector2(0, 30));
        //rb2D.AddForce(new Vector2(0, 50));
    }
	
	
	void Update () {

        //rb2D.AddForce(movement * speed * Time.deltaTime);
        transform.Translate(movement * speed * Time.deltaTime);
		
	}

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HelpPickup"))
        {
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 1);
            Instantiate(spawnEnemy, spawnPosition, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
    
}
