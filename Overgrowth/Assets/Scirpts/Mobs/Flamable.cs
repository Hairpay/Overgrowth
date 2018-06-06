using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour {

    public float burn;
    public Color burnColor;
    public Color baseColor;
    public Color actualColor;


    // Use this for initialization
    void Awake () {
        
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        burnColor =  baseColor;
        actualColor = baseColor;
        burnColor.g = 0;
    }

    // Update is called once per frame
    void Update() {
                
        if (burn > 1)
        {
            actualColor.g = (Mathf.Lerp(baseColor.g, burnColor.g, burn * 0.02f));
            gameObject.GetComponent<SpriteRenderer>().color = actualColor;
        }

        if (burn > 50)
        {
            Destroy(gameObject);
        }

    }
}
