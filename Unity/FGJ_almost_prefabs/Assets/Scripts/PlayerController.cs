using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //public float speed;
    public GameObject lovePowerR;
    public GameObject lovePowerL;
    public GameObject heart;
    public Transform attackFromR;
    public Transform attackFromL;
    public AudioClip audioJump;
    public AudioClip audioLazer;
    
    public float moveSpeed = 0.1f;
    public float timeBetweenAttack = 1f;

    private float timer;
    private float moveHorizontal;

    private Rigidbody2D rb2D;
    private Animator anim;
    private AudioSource audio;

    private bool faceRight;
    

    private bool canMove;
    
    //private Vector2 spawnPosition;

    GameObject bar;
    PlayerEnergy playerEnergy;

    void Awake()
    {
        timer = 1f;
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        faceRight = true;
        canMove = true;
        bar = GameObject.FindGameObjectWithTag("bar");
        playerEnergy = bar.GetComponent<PlayerEnergy>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            //Debug.Log(moveHorizontal);
            //Vector2 movement = new Vector2(moveHorizontal, 0.0f);

            if (moveHorizontal >= 1.0)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
                SpriteRenderer[] list = GetComponents<SpriteRenderer>();
                foreach (SpriteRenderer sr in list)
                {
                    sr.flipX = false;
                }
                faceRight = true;
            }
            else if (moveHorizontal <= -1.0)
            {
                transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
                SpriteRenderer[] list = GetComponents<SpriteRenderer>();
                foreach (SpriteRenderer sr in list)
                {
                    sr.flipX = true;
                }
                faceRight = false;
            }

            if (rb2D.velocity.y == 0)
            {
                //rb2D.AddForce(movement * speed);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    rb2D.AddForce(new Vector2(0, 16), ForceMode2D.Impulse);
                    anim.SetBool("isGrounded", false);
                }
            }
            /*
            else if (rb2D.velocity.y != 0)
            {
                ////rb2D.AddForce(movement * (speed-2));
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                }
            }
            */
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //角色攻擊
        if (Input.GetKeyDown(KeyCode.X) && playerEnergy.energyCount != 0 && timer >= timeBetweenAttack)
            StartCoroutine("Attack");

        bool walking = (moveHorizontal != 0.0f);
        //Debug.Log(moveHorizontal+"walking:"+walking);
        anim.SetBool("isWalking", walking);
    }

    IEnumerator Attack()
    {
        timer = 0f;
        canMove = false;
        if (faceRight)
        {
            Instantiate(lovePowerR, attackFromR.position, Quaternion.identity);
            anim.SetTrigger("Attack");
            playerEnergy.EnergyLost();
        }
        else
        {
            Instantiate(lovePowerL, attackFromL.position, Quaternion.identity);
            anim.SetTrigger("Attack");
            playerEnergy.EnergyLost();
        }
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HelpPickup")) 
        {
            other.gameObject.SetActive(false);
            playerEnergy.EnergyRecover();
            Instantiate(heart, other.transform.position, Quaternion.identity);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(0);
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("isGrounded", true);
        }
    }




}