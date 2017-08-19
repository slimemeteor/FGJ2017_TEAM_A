using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 10.0f;
    public float moveHorizontal = 1.0f;
    public GameObject clone;
    public AudioClip audioGetHit;
    

    double mytime;
    int LorR;

    private Rigidbody2D rb2D;
    private Animator animPlayer;
    private AudioSource audio;

    // Use this for initialization
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        mytime -= Time.deltaTime;
        if (rb2D.velocity.y == 0)
        {
            if (mytime < 0)
            {
                LorR = Random.Range(1, 3);
                //Debug.Log(LorR);
                mytime = 0.5;
            }
        }

        Vector2 pos = transform.position; //獲得遊戲物件的位置
        /*
        switch (LorR)//左右移動
        {            
            case 1:
                transform.Translate(speed * Time.deltaTime, 0, 0);//Left                
                break;
            case 2:
                transform.Translate(-speed * Time.deltaTime, 0, 0);//Left                
                break;
        }
        */
        if (LorR == 1){
            transform.Translate(speed * Time.deltaTime, 0, 0);//Left            
        }else if (LorR == 2){
            transform.Translate(-speed * Time.deltaTime, 0, 0);//Left            
        }
        if (rb2D.velocity.y == 0)
            rb2D.AddForce(new Vector2(0, Random.Range(4,24)), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HelpPickup"))
        {
            Instantiate(clone, transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
    }
}
