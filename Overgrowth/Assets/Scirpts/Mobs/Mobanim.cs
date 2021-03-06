﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobanim : MonoBehaviour {

    public Animator animob;
    public Rigidbody2D mobody;

    public Vector3 baseScale;
    public GameObject mobroll;

    public bool dieonce;

	// Use this for initialization
	void Start ()
    {
        animob = gameObject.GetComponent<Animator>();
        baseScale = gameObject.transform.localScale;
        mobody = mobroll.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = mobroll.transform.position;

        if(mobroll.GetComponent<MecheMob>().isDed == true && dieonce == false)
        {
            animob.Play("ded");
            dieonce = true;
        }

        else
        {
            if (mobody.velocity.x < -1)
            {
                gameObject.transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
            }
            else if (mobody.velocity.x > 1)
            {
                gameObject.transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
            }

            if (Mathf.Abs(mobody.velocity.x) > 0.05f)
            {
                animob.SetBool("isWalking", true);
            }
            else
            {
                animob.SetBool("isWalking", false);
            }
        }

       
    }
}
