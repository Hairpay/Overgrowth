using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCallV2 : MonoBehaviour {

    public States myState;
    public GameObject plateforme;
    private ElevatorMainV2 ElevatorMain;
    public GameObject flecheHaut;
    public Animator animator;

    public GameObject posPlateforme;
    public int position;

	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
        myState = States.off;
        ElevatorMain = plateforme.GetComponent<ElevatorMainV2>();
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
        flecheHaut.GetComponent<BoxCollider2D>().enabled = false; 

        switch (myState)
        {
            case States.off:
                animator.Play("Off");
                if (ElevatorMain.power == true)
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
                if (ElevatorMain.power == false)
                {
                    myState = States.off;
                }
                //else if (ElevatorMain.myState ==)
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
                else if (position == ElevatorMain.Arrets.Length - 1)
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
                    flecheHaut.GetComponent<BoxCollider2D>().enabled = true;

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
        ElevatorMain.Error();
    }
    public void Call()
    {
        if (ElevatorMain.position == position)
        {
            Debug.Log("Elevator Called but it's here.");
            ElevatorMain.Message("Elevator is already here.");
        }
        else
        {
            Debug.Log("Elevator Called.");
            ElevatorMain.Message("Elevator called.");
            ElevatorMain.myState = ElevatorMainV2.States.moving;
            ElevatorMain.futurePos = new Vector2(posPlateforme.transform.position.x, posPlateforme.transform.position.y);
            ElevatorMain.factor = 30;
            ElevatorMain.position = position;
        }
        
    }
    public void GoBas()
    {
        Debug.Log("Elevator goes down.");
        ElevatorMain.Message("Elevator goes down.");
        ElevatorMain.myState = ElevatorMainV2.States.moving;
        ElevatorMain.position = ElevatorMain.position + 1;
        ElevatorMain.futurePos = ElevatorMain.Arrets[ElevatorMain.position].GetComponent<ElevatorCallV2>().posPlateforme.transform.position;
        ElevatorMain.factor = 15;
    }
    public void GoHaut()
    {
        Debug.Log("Elevator goes up.");
        ElevatorMain.Message("Elevator goes up.");
        ElevatorMain.myState = ElevatorMainV2.States.moving;
        ElevatorMain.position = ElevatorMain.position - 1;
        ElevatorMain.futurePos = ElevatorMain.Arrets[ElevatorMain.position].GetComponent<ElevatorCallV2>().posPlateforme.transform.position;
        ElevatorMain.factor = 15;
    }
}
