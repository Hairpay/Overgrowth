using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous : MonoBehaviour {

    public GameObject Character;
    public Rigidbody2D charBody;

    public Gestionnaire Gestionnaire;
    public Vector2 touche;
    public Vector2 impact;

    public bool cooldown;

    // Use this for initialization
    void Start () {
        Character = GameObject.Find("character");
        charBody = Character.GetComponent<Rigidbody2D>();
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
    public void aie()
    {
            
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && cooldown == false && gameObject.GetComponent<MecheMob>().isDed == false)
        {
            cooldown = true;
            StartCoroutine("ReturnVariables");
         
            if (Gestionnaire.invicible == false)
            {
                Gestionnaire.life = Gestionnaire.life - 1;
                Debug.Log("aie");

                Gestionnaire.KnockbackCD = true;
                StartCoroutine("resetKCD");

                impact = gameObject.transform.position - coll.gameObject.transform.position;
                impact.Normalize();

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
    }
    IEnumerator resetKCD()
    {
        yield return new WaitForSeconds(0.5f);
        Gestionnaire.KnockbackCD = false;       
    }

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
