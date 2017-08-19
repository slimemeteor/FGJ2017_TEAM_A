using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //private Vector2 currentPosition;

    public Transform target;
    public float smoothing = 5.0f;

    private Vector2 offset;

    void Start () {
        //currentPosition = transform.position;      
        
        //calculating the offset from target to origin camera position.
        offset = transform.position - target.position;  
    }

    private void FixedUpdate()

    {
        //store a vector3 value that represent the position where camera should going now.
        Vector2 targetCamPos = new Vector2(target.position.x + offset.x, target.position.y + offset.y);
        //target.position + offset;

        //.Lerp(from, to, t: float)
        //using Lerp() linear interpolation to make smoothing effect. In here, we move smoothing.
        transform.position = Vector2.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
