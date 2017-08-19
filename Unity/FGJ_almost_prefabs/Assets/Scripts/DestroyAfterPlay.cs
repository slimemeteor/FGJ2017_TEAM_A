using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {

	public void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}
