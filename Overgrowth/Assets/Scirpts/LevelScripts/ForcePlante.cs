using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlante : MonoBehaviour
{

    public GameObject Character;
    public Gestionnaire Gestionnaire;
    public string textRamasse;

    // Use this for initialization
    void Start()
    {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            gameObject.GetComponent<Description>().sayText(textRamasse);

           // Gestionnaire.disfunction = true;
            Gestionnaire.SuitActivated = false;

            GameObject.Find("Directiowerfer").GetComponent<AnalysisBeam>().ReturnWait(3f);
            Destroy(gameObject);
        }
    }
}

