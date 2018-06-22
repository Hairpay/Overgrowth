using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerJump : MonoBehaviour {

    public Rigidbody2D body;    
    public Vector3 p;
    public Vector2 Jump;
    public Camera Mcamera;

    public float MinMultiplier = 100;
    public float MaxMultiplier = 700;
    public float Multiplier;
    public float factor;

    public bool GravityAnchor = false;
    //public int Cooldown = 0;
    public bool zCD;

    public Light BoosterLight;
    public Light AnchorLight;

    public GameObject Directioneur;

    public Gestionnaire Gestionnaire;

    public bool axisPressed;



    // Use this for initialization
    void Start () {

        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        body = gameObject.GetComponent<Rigidbody2D>();

        Multiplier = MinMultiplier;
        BoosterLight.range = 1;
        AnchorLight.enabled = false;

        Directioneur = GameObject.Find("Fleche");



    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire6"))
        {
            Multiplier = MinMultiplier;
            //body.AddForce(new Vector2(0f, 1000f));         
            //zCD = true;
            //StartCoroutine("ReturnVariables");
        }
        
        GravityAnchor = Gestionnaire.GravityAnchor;

        if (Gestionnaire.CharJump == true && Multiplier < MaxMultiplier) 
        {
            if (Gestionnaire.JumpCD < 1 || Gestionnaire.JumpCD < 2 && Gestionnaire.GravityAnchor == true)
            {
                if (Gestionnaire.grounded == true)
                {
                    body.velocity = new Vector2(0f, body.velocity.y);
                }
                Multiplier = Multiplier + 45;
                BoosterLight.range = (Multiplier * 0.007f);
            }
       
        }

        p = Directioneur.transform.position;

        if(Gestionnaire.SuitActivated == true)
        {
            if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpM") < -0.9)
            {
                _PJumpDown();
                axisPressed = true;
            }
            if (Input.GetButtonUp("Jump") || Input.GetAxis("JumpM") > -0.1 && axisPressed == true && Gestionnaire.manetteMode == true || Multiplier > MaxMultiplier -1)
            {              
                _PJumpUp();
                axisPressed = false;
                Gestionnaire.CharJump = false;
            }
        }      
    }

    public void _PJumpDown()
    {
        Gestionnaire.CharJump = true;
    //    gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedCharge;
                
            if (GravityAnchor == true && Gestionnaire.JumpCD == 1)
            {
                body.velocity = new Vector2(0f, 0f);
                body.simulated = false;
                AnchorLight.enabled = true;
            }
        
    }
    public void _Jump()
    {
        Jump = new Vector2((p.x - transform.position.x)*350* factor, (p.y - transform.position.y)*350*factor);
    }

    public void _PJumpUp()
    {
        if (Gestionnaire.JumpCD < 1 || Gestionnaire.JumpCD < 2 && GravityAnchor == true)
        {
            body.simulated = true;
            body.velocity = new Vector2(0f, 0f);
            Jump = new Vector2((p.x - transform.position.x) * factor * Multiplier, (p.y - transform.position.y) * factor * Multiplier);

            /*
            if (Gestionnaire.isGlinding == true && gameObject.GetComponent<ResetJumpCD>().distance < 0 && Jump.x > 0)
            {
                Jump.x = -Jump.x * 0.2f;
            }

            if (Gestionnaire.isGlinding == true && gameObject.GetComponent<ResetJumpCD>().distance > 0 && Jump.x < 0)
            {
                Jump.x = -Jump.x * 0.2f;
            }

    */
            Gestionnaire.JumpCD = Gestionnaire.JumpCD + 1;
            Gestionnaire.Jcd = true;
            StartCoroutine("ResetJcd");
            Multiplier = MinMultiplier;
         
            BoosterLight.range = 1;
            AnchorLight.enabled = false;

            //gameObject.GetComponent<SuitMove>().enabled = false;
           // gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedBase;
           

            if (Gestionnaire.grounded == true && Jump.y < 2000f)
            {
                Jump.y = 2000;
            }

            body.AddForce(Jump);
            // StartCoroutine("ReturnVariables");
        }       
    }


    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(0.5f);
        body.velocity = new Vector2(0f, 0f);
       
    }

    IEnumerator ResetJcd()
    {
        yield return new WaitForSeconds(0.2f);
        Gestionnaire.Jcd = false;
    }
}
