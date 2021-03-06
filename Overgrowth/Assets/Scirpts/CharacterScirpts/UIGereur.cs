﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGereur : MonoBehaviour {

  //  public GameObject UI;
    public GameObject Character;
    public Gestionnaire Gestionnaire;
    public Gestionnaire gestionnaireCP;

    public Text lifes;
    public int lastLife;
    public float iFrames = 1f;

    public Text analysis;
    public GameObject analysisPanel;

    // Use this for initialization
    void Awake()
    {
        //UI.SetActive(true);
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
        Gestionnaire.Checkpoint = gameObject.transform.position ;

        for (int i = 0; i < Gestionnaire.PowerUps.Length; i++)
        {
            gestionnaireCP.PowerUps[i] = Gestionnaire.PowerUps[i];
        }
    }

    void Start ()
    {
        if (Gestionnaire.life < 1)
        {
            Gestionnaire.life = Gestionnaire.PowerUps[4];
        }

        lastLife = Gestionnaire.life;
        Gestionnaire.invicible = false;
    }

    // Update is called once per frame
    void Update() {       

            if (lastLife != Gestionnaire.life && Gestionnaire.invicible == false)
        {
            lastLife = Gestionnaire.life;
            Gestionnaire.invicible = true;
            StartCoroutine("ReturnVariables");
    
        }

        lifes.text = "Energy: " + Gestionnaire.life.ToString();

        if (Gestionnaire.life < 1)
        {
            Ded();         
        }
		
	}
    public void Ded()
    {
        Gestionnaire.life = Gestionnaire.PowerUps[4];
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.transform.position = Gestionnaire.Checkpoint;
    }

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(iFrames);
        Gestionnaire.invicible = false;     
    }
}
