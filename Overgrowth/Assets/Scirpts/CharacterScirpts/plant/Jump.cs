﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public Gestionnaire Gestionnaire;
    private Rigidbody2D body;
    private Vector2 finalJump;

    public float jumpPower;
   // public int Cooldown;
    public float touchSol;

    public float distance;
    public float baseGravity;
    public float decale;
    public bool isPlaning;

    public bool jumpPressed;
    public bool jcd;

    // Use this for initialization
    void Start () {

        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        body = gameObject.GetComponent<Rigidbody2D>();
        baseGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gestionnaire.SuitActivated == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpPressed = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                jumpPressed = false;
            }

            if (jumpPressed == true || Input.GetAxis("JumpM") < -0.3)
            {
                if (Gestionnaire.Locked == false && Gestionnaire.Crouch == false)
                {
                    _Jump();
                }            
            }
            if (Input.GetButtonDown("Fire6") && Gestionnaire.isFiring == false && Gestionnaire.PowerUps[1] > 0)
            {
                jumpPressed = false;
            }
            /*
            if (Gestionnaire.isGlinding == true)
            {
                isPlaning = false;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity * 0.1f;
            }
            else if (isPlaning == false)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity;
            }
            */
        }
        
    }
        public void _Jump()
    {      
        if (jcd == false)
        {
            body.simulated = true;
            jcd = true;
            StartCoroutine("ReturnCD");

            if (Gestionnaire.JumpCD == 0 && Gestionnaire.SuitActivated == false )
            {
                if (Gestionnaire.PowerUps[6] > 0)
                {
                    if (Gestionnaire.isGlinding == true && Gestionnaire.GlideGauche == false)
                    {
                        Gestionnaire.KnockbackCD = true;
                        decale = -2000f;
                        StartCoroutine("ReturnVariables");
                    }

                    else if (Gestionnaire.isGlinding == true && Gestionnaire.GlideGauche == true)
                    {
                        Gestionnaire.KnockbackCD = true;
                        decale = 2000f;
                        StartCoroutine("ReturnVariables");
                    }
                    else
                    {
                        decale = 0f;
                    }
                }
                else
                {
                    decale = 0f;
                }


                finalJump = new Vector2(decale, jumpPower);
                body.velocity = new Vector2(body.velocity.x, 0f);
                body.AddForce(finalJump);
                Gestionnaire.JumpCD = Gestionnaire.JumpCD + 1;
            }
        }
       
    }

    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(0.3f);
        Gestionnaire.KnockbackCD = false;
    }
    IEnumerator ReturnCD()
    {
        yield return new WaitForSeconds(0.1f);
        jcd = false;
    }
}
