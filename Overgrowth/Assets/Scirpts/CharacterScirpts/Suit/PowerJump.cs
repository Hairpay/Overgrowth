using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerJump : MonoBehaviour {

    private Rigidbody2D body;
    private Vector3 mousePos;
    public Vector3 p;
    public Vector2 Jump;
    public Camera Mcamera;

    public float MinMultiplier;
    public float MaxMultiplier;
    public float Multiplier;

    public bool increaser;

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

        body = gameObject.GetComponent<Rigidbody2D>();

        Multiplier = MinMultiplier;
        BoosterLight.range = 1;
        AnchorLight.enabled = false;

        Directioneur = GameObject.Find("Fleche");



    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire6") && zCD == false)
        {
            body.AddForce(new Vector2(0f, 1000f));
            zCD = true;
            StartCoroutine("ReturnVariables");
        }

        GravityAnchor = Gestionnaire.GravityAnchor;

        if (increaser == true && Multiplier < MaxMultiplier) 
        {
            Multiplier = Multiplier + 15;
            BoosterLight.range = (Multiplier*0.007f);
        }

        p = Directioneur.transform.position;

        if (Input.GetButtonDown("Jump") || Input.GetAxis("JumpM") < -0.9) 
        {
            _PJumpDown();
            axisPressed = true;
        }
        if (Input.GetButtonUp("Jump") || Input.GetAxis("JumpM") > -0.1 && axisPressed == true && Gestionnaire.manetteMode == true)
        {
            _PJumpUp();
            axisPressed = false;
        }


    }
    public void _PJumpDown()
    {
        increaser = true;
        gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedCharge;
                
            if (GravityAnchor == true && Gestionnaire.JumpCD == 1)
            {
                body.velocity = new Vector2(0f, 0f);
                body.simulated = false;
                AnchorLight.enabled = true;
            }
        
    }
    public void _Jump()
    {
        Jump = new Vector2((p.x - transform.position.x)*1500, (p.y - transform.position.y)*1500);
    }

    public void _PJumpUp()
    {
        if (Gestionnaire.JumpCD < 1 || Gestionnaire.JumpCD < 2 && GravityAnchor == true)
        {
            body.simulated = true;
            body.velocity = new Vector2(0f, 0f);
            Jump = new Vector2((p.x - transform.position.x) * 4 * Multiplier, (p.y - transform.position.y) * 4 * Multiplier);

            if (Gestionnaire.isGlinding == true && gameObject.GetComponent<ResetJumpCD>().distance < 0 && Jump.x > 0)
            {
                Jump.x = -Jump.x * 0.2f;
            }

            if (Gestionnaire.isGlinding == true && gameObject.GetComponent<ResetJumpCD>().distance > 0 && Jump.x < 0)
            {
                Jump.x = -Jump.x * 0.2f;
            }


            Gestionnaire.JumpCD = Gestionnaire.JumpCD + 1;
            Multiplier = MinMultiplier;
            increaser = false;
            BoosterLight.range = 1;
            AnchorLight.enabled = false;

            //gameObject.GetComponent<SuitMove>().enabled = false;
            gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedBase;
            body.AddForce(Jump);
            }       
    }


    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(2f);
        zCD = false;
    }
}
