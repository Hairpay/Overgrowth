using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCallV2_C : MonoBehaviour
{

    public States myState;
    public GameObject plateforme;
    private ElevatorMainV2_1 ElevatorMain;
    public GameObject flecheHaut;
    public GameObject flecheBas;

    public GameObject posPlateforme;
    public int position;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myState = States.off;
        ElevatorMain = plateforme.GetComponent<ElevatorMainV2_1>();
    }
    public enum States
    {
        call,
        fleches,
        off
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 2);
        flecheBas.GetComponent<BoxCollider2D>().enabled = false;
        flecheHaut.GetComponent<BoxCollider2D>().enabled = false;
        flecheBas.GetComponent<Renderer>().enabled = false;
        flecheHaut.GetComponent<Renderer>().enabled = false;
        flecheBas.GetComponent<cakeslice.Outline>().enabled = false;
        flecheHaut.GetComponent<cakeslice.Outline>().enabled = false;

        switch (myState)
        {
            case States.off:
                animator.Play("Wait");
                if (ElevatorMain.power == true)
                {
                    myState = States.call;
                }
                else if (gameObject.GetComponent<Description>().compteur > 50)
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
                else if (gameObject.GetComponent<Description>().compteur > 50)
                {                  
                    gameObject.GetComponent<Description>().stopeth();
                    Call();                   
                }
                break;

            case States.fleches:

                animator.Play("Wait");

                if (position == 0)
                {
                    flecheBas.GetComponent<BoxCollider2D>().enabled = true;
                    flecheBas.GetComponent<Renderer>().enabled = true;
                    flecheBas.GetComponent<cakeslice.Outline>().enabled = true;
                }
                else if (position == ElevatorMain.Arrets.Length - 1)
                {
                    flecheHaut.GetComponent<BoxCollider2D>().enabled = true;
                    flecheHaut.GetComponent<Renderer>().enabled = true;
                    flecheHaut.GetComponent<cakeslice.Outline>().enabled = true;
                }
                else
                {
                    flecheBas.GetComponent<BoxCollider2D>().enabled = true;
                    flecheHaut.GetComponent<BoxCollider2D>().enabled = true;
                    flecheBas.GetComponent<Renderer>().enabled = true;
                    flecheHaut.GetComponent<Renderer>().enabled = true;
                    flecheBas.GetComponent<cakeslice.Outline>().enabled = true;
                    flecheHaut.GetComponent<cakeslice.Outline>().enabled = true;
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
            ElevatorMain.myState = ElevatorMainV2_1.States.moving;
            ElevatorMain.futurePos = new Vector2(posPlateforme.transform.position.x, posPlateforme.transform.position.y);
            ElevatorMain.factor = 100;
            ElevatorMain.position = position;
        }

    }
    public void GoBas()
    {
        Debug.Log("Elevator goes down.");
        ElevatorMain.Message("Elevator goes down.");
        ElevatorMain.myState = ElevatorMainV2_1.States.moving;
        ElevatorMain.position = ElevatorMain.position + 1;
        ElevatorMain.futurePos = ElevatorMain.Arrets[ElevatorMain.position].GetComponent<ElevatorCallV2_C>().posPlateforme.transform.position;
        ElevatorMain.factor = 30;
    }
    public void GoHaut()
    {
        Debug.Log("Elevator goes up.");
        ElevatorMain.Message("Elevator goes up.");
        ElevatorMain.myState = ElevatorMainV2_1.States.moving;
        ElevatorMain.position = ElevatorMain.position - 1;
        ElevatorMain.futurePos = ElevatorMain.Arrets[ElevatorMain.position].GetComponent<ElevatorCallV2_C>().posPlateforme.transform.position;
        ElevatorMain.factor = 30;
    }
}
