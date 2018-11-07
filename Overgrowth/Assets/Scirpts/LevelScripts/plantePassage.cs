using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantePassage : MonoBehaviour {
    public GameObject character;
    public Gestionnaire gestionnaire;
    public bool suitMode;

    public Color baseColor;
    public Color otherColor;
    public int baseLayer;

    // Use this for initialization
    void Awake ()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.a = baseColor.a * 0.3f;
        baseLayer = gameObject.layer;
    }

    void Start () {

        character = GameObject.Find("character");
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

    }
	
	// Update is called once per frame
	void Update () {

        if ( gestionnaire. SuitActivated == suitMode)
        {
            //    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.layer = 24;
            gameObject.GetComponent<SpriteRenderer>().color = otherColor;           
        }
        else
        {
            // gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.layer = baseLayer;
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;                  
        }                      
	}
}
