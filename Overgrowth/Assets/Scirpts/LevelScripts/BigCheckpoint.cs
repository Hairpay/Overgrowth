using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCheckpoint : MonoBehaviour
{

    public GameObject Character;
    public Gestionnaire Gestionnaire;
    //public Gestionnaire Savestionnaire;

    public float dist;
    public Color baseColor;
    public Color otherColor;

    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
        //Savestionnaire = Character.GetComponent<PowerUps>().Savestionnaire;
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.b = 0;
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector3.Distance(Character.transform.position, transform.position);

        if (dist < 10f)
        {
            Gestionnaire.bigCheckpoint.x = gameObject.transform.position.x;
            Gestionnaire.bigCheckpoint.y = gameObject.transform.position.y;
            gameObject.GetComponent<SpriteRenderer>().color = otherColor;
            Gestionnaire.life = Gestionnaire.maxLife;
            Gestionnaire.atPoint = true;
          //   Savestionnaire = Gestionnaire;            
        }
        else if (dist < 20f && dist > 11f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
            Gestionnaire.atPoint = false;
        }
    }
}
