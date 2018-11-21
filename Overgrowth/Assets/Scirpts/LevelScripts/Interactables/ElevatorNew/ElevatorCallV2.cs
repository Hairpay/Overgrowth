using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCallV2 : MonoBehaviour {

    States myState;
    public GameObject plateforme;
    public GameObject flecheHaut;
    public Animator animator;

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
        flecheHaut,
        flecheBas,
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
                    Call();
                }
                break;
            case States.fleches:
                animator.Play("Fleches");
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.8f);
                flecheHaut.SetActive(true);

                if (gameObject.GetComponent<Description>().compteur > 50)
                {
                    GoBas();
                }

                break;
            case States.flecheHaut:
                animator.Play("FlecheHaut");
                if (gameObject.GetComponent<Description>().compteur > 50)
                {
                    GoHaut();
                }

                break;
            case States.flecheBas:
                animator.Play("FlecheBas");
                if (gameObject.GetComponent<Description>().compteur > 50)
                {
                    GoBas();
                }

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
        Debug.Log("Elevator Called.");
        plateforme.GetComponent<ElevatorMainV2>().Called();

        plateforme.GetComponent<ElevatorMainV2>().myState = ElevatorMainV2.States.moving;
        plateforme.GetComponent<ElevatorMainV2>().futurePos = gameObject;
    }
    public void GoBas()
    {
        Debug.Log("Elevator goes down.");
    }
    public void GoHaut()
    {
        Debug.Log("Elevator goes up.");
    }
}
