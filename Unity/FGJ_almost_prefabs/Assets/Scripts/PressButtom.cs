using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressButtom : MonoBehaviour {

    public Sprite sprite01;
    public Sprite sprite02;
    public Image lightOff;
    public Color blackColour = new Color(0f, 0f, 0f, 1f);
    public Color normalColour = new Color(0f, 0f, 0f, 0f);
    private SpriteRenderer spriteRender;
   
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRender.sprite = sprite02;
            lightOff.color = blackColour;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRender.sprite = sprite01;
            lightOff.color = normalColour;
        }
    }
}
