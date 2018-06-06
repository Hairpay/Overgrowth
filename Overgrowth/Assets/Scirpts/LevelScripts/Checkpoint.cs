using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public float dist;
    public Color baseColor;
    public Color otherColor;

	// Use this for initialization
	void Start () {
        Character = GameObject.Find("character");
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;        
        otherColor.r = 0;
    }
	
	// Update is called once per frame
	void Update () {

        dist = Vector3.Distance(Character.transform.position, transform.position);

        if(dist < 10f)
        {
            Gestionnaire.Checkpoint = gameObject.transform.position;
            gameObject.GetComponent<SpriteRenderer>().color = otherColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }
}
