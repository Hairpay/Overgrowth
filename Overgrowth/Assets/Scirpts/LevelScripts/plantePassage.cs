using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantePassage : MonoBehaviour {
    public GameObject character;
    public Gestionnaire Gestionnaire;
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
    }

    void Start () {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        baseLayer = gameObject.layer;
    }
	
	// Update is called once per frame
	void Update () {

        if ( Gestionnaire. SuitActivated == suitMode)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().color = otherColor;
            gameObject.layer = 0;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
            gameObject.layer = baseLayer;        
        }                      
	}
}
