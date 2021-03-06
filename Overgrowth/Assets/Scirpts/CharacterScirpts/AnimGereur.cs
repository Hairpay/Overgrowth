﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGereur : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public Animator charnim;

    public bool isJumping;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        charnim = gameObject.GetComponent<Animator>();
      
    }
	
	// Update is called once per frame
	void Update () {

        if (Gestionnaire.Locked == false)
        {
            if (Gestionnaire.Crouch == true)
            {
                charnim.SetBool("isCrouching", true);              

                if (Gestionnaire.Speed > 0.05f && Gestionnaire.JumpCD < 1)
                {
                    charnim.SetBool("isWalking", true);
                    charnim.Play("CrouchWalk");
                }
                else
                {
                    charnim.SetBool("isWalking", false);
                    charnim.Play("CrouchIdle");
                }
            }
            else
            {
                charnim.SetBool("isCrouching", false);

                if (Input.GetButtonDown("Fire5"))
                {
                    charnim.Play("CacAtac");
                }
                else
                {
                    if (Gestionnaire.isGlinding == true)
                    {
                        charnim.SetBool("isSliding", true);
                        charnim.Play("Sliding");
                    }
                    else
                    {
                        charnim.SetBool("isSliding", false);
                        charnim.SetFloat("Velocity", Gestionnaire.Speed * 0.4f);

                        if (Gestionnaire.CharJump == true)
                        {
                            charnim.Play("CharJump");
                        }
                        else
                        {
                            if (Gestionnaire.Speed > 0.05f && Gestionnaire.JumpCD < 1)
                            {
                                charnim.SetBool("isWalking", true);
                            }
                            else
                            {
                                charnim.SetBool("isWalking", false);
                            }

                            if (Gestionnaire.KnockbackCD == true)
                            {
                                if (isJumping == true)
                                {
                                    charnim.Play("JumpingKnock");
                                }
                                else
                                {
                                    charnim.Play("Knockback");
                                }

                            }

                            else if (Gestionnaire.JumpCD > 0)
                            {
                                charnim.Play("Jumping");
                                isJumping = true;
                                charnim.SetBool("isJumping", true);
                            }
                            else if (Gestionnaire.JumpCD < 1)
                            {
                                isJumping = false;
                                charnim.SetBool("isJumping", false);
                            }
                        }
                    }
                }
            }

            
        }
        else
        {
            charnim.Play("Idle");
        }
    }

       
}

       
