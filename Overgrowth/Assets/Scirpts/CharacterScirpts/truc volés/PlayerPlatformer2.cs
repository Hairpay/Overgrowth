using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformer2 : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public GameObject character;
    public Gestionnaire Gestionnaire;
    public Animator animator;
    
    // Use this for initialization
    void Awake()
    {
       // character = GameObject.Find("character");
        //Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
    }
    void Start()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");

        if (Gestionnaire.SuitActivated == false)
        {
            if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }         
        }   
      
        if (grounded == true && Gestionnaire.Jcd == false)
        {
           // Debug.Log("grounded");
            Gestionnaire.JumpCD = 0;           
        }

        Gestionnaire.grounded = grounded;
        targetVelocity = move * maxSpeed;
    }

   
}