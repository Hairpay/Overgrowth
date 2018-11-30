using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBeam : MonoBehaviour {

    public bool cooldown;
    public GameObject projectile;

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public ParticleSystem firedP;
    public int reloading;
 
    public float firespeed;

    // Use this for initialization
    void Start()
    {

        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire6") && Gestionnaire.isFiring == false && Gestionnaire.PowerUps[1] > 0)
        {        
            if (cooldown == true && reloading == 0)
            {
                cooldown = false;
            }        
        }

        if (Gestionnaire.isFiring == true 
            && Gestionnaire.PowerUps[3] > 0 
            && Gestionnaire.Locked == false 
            && Gestionnaire.disfunction == false 
            && Gestionnaire.Crouch == false)
        {
            if (cooldown == false)
            {
                if(Gestionnaire.SuitActivated == true)
                {
                    firespeed = 0.2f;
                    cooldown = true;
                    StartCoroutine("firePrep");
                }
                else if (Gestionnaire.PowerUps[3] > 1)
                {
                    firespeed = 0.1f;
                    cooldown = true;
                    StartCoroutine("firePrep");
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

      
    }

    void Fire()
    {
       // Gestionnaire.SuitActivated = false;
        GameObject DOI = Instantiate(projectile);
   
        DOI.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);                
        DOI.transform.localRotation = gameObject.transform.localRotation;      
        DOI.GetComponent<DOIgo>().dammage = 1;
        cooldown = false;
        
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
