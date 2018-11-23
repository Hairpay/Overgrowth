using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCallV2 : MonoBehaviour {

    public States myState;
    public GameObject plateforme;
    public GameObject flecheHaut;
    public Animator animator;

    public GameObject posPlateforme;
    public int position;

	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
        myState = States.off;
	}
    public enum States
    {
        call,
        fleches,
        off
    }
    // Update is called once per frame
    void Update ()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 2);
        flecheHaut.SetActive(false);

        switch (myState)
        {
            case States.off:
                animator.Play("Off");
                if (plateforme.GetComponent<ElevatorMainV2>().power == true)
                {
                    myState = States.call;
                }
                else if(gameObject.GetComponent<Description>().compteur > 50)
                {
                    ErrorMessage();               
                }
                break;

            case States.call:
                animator.Play("Call");
                if (plateforme.GetComponent<ElevatorMainV2>().power == false)
                {
                    myState = States.off;
                }
                //else if (plateforme.GetComponent<ElevatorMainV2>().myState ==)
                else if (gameObject.GetComponent<Description>().compteur > 50)
                {
                    //analysisText.text = "Elevator Called.";
                    gameObject.GetComponent<Description>().stopeth();
                    Call();
                }
                break;

            case States.fleches:

                if (position == 0)
                {
                    animator.Play("FlecheBas");
                    if (gameObject.GetComponent<Description>().compteur > 50)
                    {
                        gameObject.GetComponent<Description>().stopeth();
                        GoBas();
                    }
                }
                else if (position == plateforme.GetComponent<ElevatorMainV2>().Arrets.Length - 1)
                {
                    animator.Play("FlecheHaut");
                    if (gameObject.GetComponent<Description>().compteur > 50)
                    {
                        gameObject.GetComponent<Description>().stopeth();
                        GoHaut();
                    }
                }
                else
                {
                    animator.Play("Fleches");
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.8f);
                    flecheHaut.SetActive(true);

                    if (gameObject.GetComponent<Description>().compteur > 50)
                    {
                        gameObject.GetComponent<Description>().stopeth();
                        GoBas();
                    }
                }

                myState = States.call;        
                break;       
        }
    }
    public void ErrorMessage()
    {
        Debug.Log("errorMessage");
        plateforme.GetComponent<ElevatorMainV2>().Error();
    }
    public void Call()
    {
        if (plateforme.GetComponent<ElevatorMainV2>().position == position)
        {
            Debug.Log("Elevator Called but it's here.");
            plateforme.GetComponent<ElevatorMainV2>().Message("Elevator is already here.");
        }
        else
        {
            Debug.Log("Elevator Called.");
            plateforme.GetComponent<ElevatorMainV2>().Message("Elevator called.");
            plateforme.GetComponent<ElevatorMainV2>().myState = ElevatorMainV2.States.moving;
            plateforme.GetComponent<ElevatorMainV2>().futurePos = new Vector2(posPlateforme.transform.position.x, posPlateforme.transform.position.y);
            plateforme.GetComponent<ElevatorMainV2>().position = position;
        }
        
    }
    public void GoBas()
    {
        Debug.Log("Elevator goes down.");
        plateforme.GetComponent<ElevatorMainV2>().Message("Elevator goes down.");
        plateforme.GetComponent<ElevatorMainV2>().myState = ElevatorMainV2.States.moving;
        plateforme.GetComponent<ElevatorMainV2>().position = plateforme.GetComponent<ElevatorMainV2>().position + 1;
        plateforme.GetComponent<ElevatorMainV2>().futurePos = plateforme.GetComponent<ElevatorMainV2>().Arrets[plateforme.GetComponent<ElevatorMainV2>().position].GetComponent<ElevatorCallV2>().posPlateforme.transform.position;
    }
    public void GoHaut()
    {
        Debug.Log("Elevator goes up.");
        plateforme.GetComponent<ElevatorMainV2>().Message("Elevator goes up.");
        plateforme.GetComponent<ElevatorMainV2>().myState = ElevatorMainV2.States.moving;
        plateforme.GetComponent<ElevatorMainV2>().position = plateforme.GetComponent<ElevatorMainV2>().position - 1;
        plateforme.GetComponent<ElevatorMainV2>().futurePos = plateforme.GetComponent<ElevatorMainV2>().Arrets[plateforme.GetComponent<ElevatorMainV2>().position].GetComponent<ElevatorCallV2>().posPlateforme.transform.position;
    }
}
