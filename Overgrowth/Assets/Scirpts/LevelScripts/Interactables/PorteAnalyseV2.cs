using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PorteAnalyseV2 : MonoBehaviour
{
    public States myState;

    public GameObject character;
    public Gestionnaire gestionnaire;
    public float dist;
    public int baseLayer;

    public bool autoClose;
    public bool onlySuit;
    public int analysisLevel;
    public bool power = true;
    public bool errorBlocked;

    public Description description;

    public Animator animator;
    public bool ready2unlock;

    // Use this for initialization
    void Awake()
    {
        description = gameObject.GetComponent<Description>();
        baseLayer = gameObject.layer;
    }

    void Start()
    {
        character = GameObject.Find("character");
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        animator = gameObject.GetComponent<Animator>();
    }

    public enum States
    {
      noPower,
      openable,
      closed,
      open
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case States.noPower:
                animator.Play("porteNoPower");
                Checkstate();
                break;
            case States.closed:
                animator.Play("PorteDENIED");
                Checkstate();
                break;

            case States.openable:
                animator.Play("PorteOuvrable");
                Checkstate();
                break;

            case States.open:
                animator.Play("Porte_Ouverture");
                Ouverture();
                if (autoClose == true)
                {
                    dist = (character.transform.position.y - transform.position.y);
                    if (Mathf.Abs(dist) < 1)
                    {
                        ready2unlock = true;
                    }
                    if (Mathf.Abs(dist) > 3 && ready2unlock == true)
                    {
                        ready2unlock = false;
                        fermeture();
                    }
                }
                break;
        }     
    }
    public void Ouverture()
    {      
        gameObject.layer = 24;
        if (autoClose == false)
        {
            gameObject.GetComponent<cakeslice.Outline>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            description.enabled = false;
        }
    }
    public void fermeture()
    {
        gameObject.layer = baseLayer;
        Checkstate();
    }

    public void Checkstate()
    {
        if (power == false)
        {
            if (description.compteur > 50)
            {
                description.sayText("Error: This door have no power.");
                description.stopeth();
            }          
            myState = States.noPower;
        }
        else if (analysisLevel > gestionnaire.PowerUps[0])
        {
            if (description.compteur > 50)
            {
                description.sayText("This door require a level " + analysisLevel + " security.");
                description.stopeth();
            }        
            myState = States.closed;
        }
        else if (gestionnaire.SuitActivated == false && onlySuit == true)
        {
            if (description.compteur > 50)
            {
                description.sayText("Error: Unknow biological signature.");
                description.stopeth();
            }
            myState = States.closed;
        }
        else
        {          
            if (description.compteur > 50)
            {
                myState = States.open;
                description.stopeth();
            }
            else
            {
                myState = States.openable;
            }
        }
    }

}
