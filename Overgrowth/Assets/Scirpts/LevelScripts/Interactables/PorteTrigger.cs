using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteTrigger : MonoBehaviour {

    public Color baseColor;
    public Color otherColor;
 
    public bool open;

    // Use this for initialization
    void Awake()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.a = baseColor.a * 0.3f;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Ouverture()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().color = otherColor;
        open = true;
    }

    public void fermeture()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        open = false;
    }
}
