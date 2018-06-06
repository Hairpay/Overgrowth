using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniDeathZone : MonoBehaviour
{

    public GameObject character;
    public Gestionnaire Gestionnaire;

    // Use this for initialization
    void Start()
    {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        Gestionnaire.Checkpoint = character.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            character.transform.position = new Vector3(Gestionnaire.Checkpoint.x, Gestionnaire.Checkpoint.y, 0f);
            if (Gestionnaire.invicible == false)
            {
                Gestionnaire.life = Gestionnaire.life - 1;
            }
        }
    }
}


