using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBeam : MonoBehaviour {

    public bool cooldown;
    public GameObject projectile;

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public Rigidbody2D body;
    public Vector2 knockback;
    public ParticleSystem firedP;
    public int reloading;
    public Light reloadLight;

    // Use this for initialization
    void Start () {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        body = character.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire3"))
        {           
            if(cooldown == false)
            {
                cooldown = true;             
                StartCoroutine("firePrep");
               
            }
            else
            {
                reloading = 1;
                /*
                if(Gestionnaire.JumpCD == 0)
                {
                    body.velocity = new Vector2(0f, 0f);                 
                    reloading = 1;
                    body.simulated = false;
                } 
                */
            }
        }

        if (Input.GetButtonUp("Fire3"))
        {       
         //   body.simulated = true;
            reloading = 0;
        }

        if(reloading > 0)
        {
            Reload();
            Gestionnaire.isReloading = true;
        }
        else
        {
            Gestionnaire.isReloading = false;
        }
    }

    void Reload()
    {
        reloading++;
        if (reloading > 50)
        {
            Gestionnaire.SuitActivated = true;
            reloading = 0;
            cooldown = false;
          //  body.simulated = true;
        }

        reloadLight.range = reloading * 0.1f;
    }


    void Fire()
    {
        Gestionnaire.SuitActivated = false;
        GameObject DOI = Instantiate(projectile);
   
        DOI.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);                
        DOI.transform.localRotation = gameObject.transform.localRotation;

        body.velocity = new Vector2(0f, 0f);
        character.GetComponent<PowerJump>()._Jump();
        knockback = - character.GetComponent<PowerJump>().Jump;
        body.AddForce(knockback);

        Gestionnaire.KnockbackCD = true;
        StartCoroutine("resetKCD");
        firedP.Play();
    }
    IEnumerator resetKCD()
    {
        yield return new WaitForSeconds(0.5f);
        Gestionnaire.KnockbackCD = false;
        firedP.Stop();
    }
    IEnumerator firePrep()
    {
        yield return new WaitForSeconds(0.2f);
        Fire();
    }
}
