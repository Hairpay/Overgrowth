using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour {

    public GameObject character;
    public Gestionnaire gestionnaire;
    public Gestionnaire gestionnaireCP;


    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.GetComponent<Description>().compteur > 50)
        {
            saveProgress();
        }
    }

    public void saveProgress()
    {
        gameObject.GetComponent<Description>().sayText("Progress has been saved.");
        gestionnaire.life = gestionnaire.PowerUps[4];

        for (int i = 0; i < gestionnaire.PowerUps.Length; i++)
        {
            gestionnaireCP.PowerUps[i] = gestionnaire.PowerUps[i];
        }

        gestionnaire.Checkpoint = gameObject.transform.position;
    }
}
