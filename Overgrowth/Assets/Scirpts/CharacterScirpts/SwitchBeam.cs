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

    public float firespeed;

    // Use this for initialization
    void Start () {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        body = character.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire6") && Gestionnaire.isFiring == false && Gestionnaire.PowerUps[1] > 0)
        {
            Gestionnaire.SuitActivated = !Gestionnaire.SuitActivated;
            
            if (cooldown == true && reloading == 0)
            {
                cooldown = false;
            }        
        }

        if (Gestionnaire.isFiring == true && Gestionnaire.PowerUps[3] > 0)
        {
            if (cooldown == false)
            {
                if(Gestionnaire.SuitActivated == true)
                {
                    firespeed = 0.2f;
                }
                else
                {
                    firespeed = 0.1f;
                }

                cooldown = true;
                StartCoroutine("firePrep");

                if (Gestionnaire.SuitActivated == false)
                {
                    reloading = 1;
                }
            }            
        }

        if (Input.GetButtonDown("Fire3"))
        {
            Gestionnaire.isFiring = true;
        }

        if (reloading > 0)
        {
            Reload();           
        }      

        if (Input.GetButtonUp("Fire3"))
        {
            //   body.simulated = true;
            Gestionnaire.isFiring = false;
           // reloading = 0;
        }

      
    }

    void Reload()
    {
        reloading++;
        if (reloading > 50)
        {     
            reloading = 0;
            cooldown = false;
            Gestionnaire.isFiring = false;
        }

        reloadLight.range = reloading * 0.1f;
    }

        void Fire()
    {
       // Gestionnaire.SuitActivated = false;
        GameObject DOI = Instantiate(projectile);
   
        DOI.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);                
        DOI.transform.localRotation = gameObject.transform.localRotation;

        if (Gestionnaire.SuitActivated == false)
        {
            body.velocity = new Vector2(0f, 0f);
            character.GetComponent<PowerJump>()._Jump();
            knockback = -character.GetComponent<PowerJump>().Jump;
            body.AddForce(knockback);
            Gestionnaire.KnockbackCD = true;
            DOI.GetComponent<DOIgo>().dammage = 3;
            DOI.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
        }
        else
        {
            DOI.GetComponent<DOIgo>().dammage = 1;
            cooldown = false;
        }

        StopAllCoroutines();
        StartCoroutine("resetKCD");
        firedP.Play();
    }
    IEnumerator resetKCD()
    {
        yield return new WaitForSeconds(0.5f);
        Gestionnaire.KnockbackCD = false;
        firedP.Stop();
       // cooldown = false;
    }
    IEnumerator firePrep()
    {
        yield return new WaitForSeconds(firespeed);
        Fire();
        
    }
}
