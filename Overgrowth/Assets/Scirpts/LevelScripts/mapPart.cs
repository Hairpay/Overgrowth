using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPart : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;
    public SpriteRenderer rend;

    public int numero;
    public bool explored;
    public Color baseColor;
    public Color otherColor;

    public bool fond;


    // Use this for initialization
    void Start () {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        rend = gameObject.GetComponent<SpriteRenderer>();
        rend.enabled = false;

        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.b = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if(Gestionnaire.currentSalle == numero)
        {
            explored = true;
        }

        if (Input.GetButtonDown("map") && explored == true)
        {
            rend.enabled = true;
            if (Gestionnaire.currentSalle == numero && fond == false)
            {
                gameObject.GetComponent<SpriteRenderer>().color = otherColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = baseColor;
            }
        }

        if (Input.GetButtonUp("map") && explored == true)
        {
            rend.enabled = false;
        }

    }
}
