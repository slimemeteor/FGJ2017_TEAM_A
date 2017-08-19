using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject kemuri;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.tag == "Enemy")
        {            
            Instantiate(kemuri, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        
    }

    
}
