using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatpoint : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public Gestionnaire Template;
    public bool active;

    // Use this for initialization
    void Start() {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        if (active == true)
        {
            ActivatePoint();
        }      
    }
	
public void ActivatePoint()
    {
        character.transform.position = gameObject.transform.position;
        Gestionnaire.SuitActivated = Template.SuitActivated;
        Gestionnaire.disfunction = Template.disfunction;
        Gestionnaire.Checkpoint = gameObject.transform.position;
        for (int i = 0; i < Gestionnaire.PowerUps.Length; i++)
        {
            Gestionnaire.PowerUps[i] = Template.PowerUps[i];
        }
    }
}
