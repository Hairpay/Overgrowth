using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_AutoAim : MonoBehaviour {

    public GameObject character;
    public Rigidbody2D charBody;

    public Gestionnaire gestionnaire;
    public Vector2 touche;
    public Vector2 impact;  
    public bool cooldown;

    public int layer_mask;

    public bool turnmode;
    public bool aimMode;

    public int compteur;


    public LineRenderer line;

    // Use this for initialization
    void Start () {
        character = GameObject.Find("character");
        charBody = character.GetComponent<Rigidbody2D>();
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        layer_mask = LayerMask.GetMask("Player", "Environment","Door");

        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        

    }
	
	// Update is called once per frame
	void Update ()
    {
       if (aimMode == false)
        {
            Autorotate();
            ShootLaser();
        }
       else 
        {
            if (gestionnaire.SuitActivated == true)
            {
                HideLaser();              
            }
            else
            {
                if (gameObject.GetComponent<JumpingMob>() != null)
                {
                    gameObject.GetComponent<JumpingMob>().enabled = false;
                    gameObject.GetComponent<LineRenderer>().enabled = true;
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                }

                if (Vector3.Distance(character.transform.position, transform.position) < 50)
                {
                    compteur++;

                    if (compteur < 120)
                    {
                        AimLaser();
                        LookAtPlayer();
                    }
                    else if (compteur > 180 && compteur < 210)
                    {
                        ShootLaser();
                    }
                    else if (compteur > 240)
                    {
                        compteur = 0;
                    }
                }
                else if (Vector3.Distance(character.transform.position, transform.position) > 50)
                {
                    HideLaser();
                }                   
            }                     
        }      
    }
    public void HideLaser()
    {
        if (gameObject.GetComponent<JumpingMob>() != null)
        {
            gameObject.GetComponent<JumpingMob>().enabled = true;
        }      
        gameObject.GetComponent<LineRenderer>().enabled = false;
        compteur = 0;
    }
    public void AimLaser()
    {
        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, character.transform.position - gameObject.transform.position, 200f, layer_mask);
        Debug.DrawLine(transform.position, target.point, new Color(252, 252, 0));

        line.enabled = true;
        line.SetPosition(0, line.gameObject.transform.position);
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.SetPosition(1, target.point);
    }

    public void LookAtPlayer()
    {
        Vector3 difference = character.transform.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

    public void ShootLaser()
    {
        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, transform.right, 200f, layer_mask);
        Debug.DrawLine(transform.position, target.point, new Color(252, 252, 0));

        if (target.collider != null && target.collider.tag == "Player")
        {
            Aie();
        }

        line.enabled = true;
        line.SetPosition(0, line.gameObject.transform.position);
        line.startWidth = 1f;
        line.endWidth = 1f;
        line.SetPosition(1, target.point);

    }


    public void Autorotate()
    {
        if (turnmode == true)
        {
            transform.Rotate(new Vector3(0, 0, 0.5f));
        }
    }

    public void Aie()
    {
        cooldown = true;
        StartCoroutine("ReturnVariables");

        if (gestionnaire.invicible == false)
        {
            gestionnaire.life = gestionnaire.life - 1;
            Debug.Log("aie");
            gestionnaire.KnockbackCD = true;
            StartCoroutine("resetKCD");
            if (impact.x > 0.5f)
            {
                charBody.AddForce(new Vector2(-5000f, 4000f));
            }
            else if (impact.x < -0.5f)
            {
                charBody.AddForce(new Vector2(5000f, 4000f));
            }
            else
            {
                charBody.AddForce(new Vector2(0f, 4000f));
            }
        }
    }
    IEnumerator resetKCD()
    {
        yield return new WaitForSeconds(0.5f);
        gestionnaire.KnockbackCD = false;
    }
    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
