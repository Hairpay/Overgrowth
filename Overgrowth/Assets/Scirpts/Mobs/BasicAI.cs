using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

    public GameObject Character;
    public float dist;
    public Rigidbody2D moBody;
    private Vector2 Jump;

    public bool cooldown;
    private int layer_mask;
    public Gestionnaire Gestionnaire;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        moBody = gameObject.GetComponent<Rigidbody2D>();
        layer_mask = ~LayerMask.GetMask("Mobs");
    }
	
	// Update is called once per frame
	void Update () {

        dist = Vector3.Distance(Character.transform.position, transform.position);

        if (dist < 30f)
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Character.transform.position - gameObject.transform.position, dist, layer_mask);
            Debug.DrawLine(gameObject.transform.position, hit.point);

           // Debug.Log(hit.collider.gameObject);

            if (hit.collider != null && hit.collider.tag == "Player" && cooldown == false)
            {
                Debug.Log("je peux vous voir");
                Jump = Character.transform.position - gameObject.transform.position;
                Jump = Jump.normalized;       
                if (Gestionnaire.SuitActivated == true)
                {
                    moBody.AddForce(Jump * 20000);
                }
                else
                {
                    moBody.AddForce(Jump * -20000);
                }                                        
                cooldown = true;
                StartCoroutine("ReturnVariables");
            }
        }
        if (Input.GetButtonDown("Fire5") && dist < 5f && gameObject.GetComponent<MecheMob>().isDed == true)
        {            
            StartCoroutine("gonadie");         
        }
    }

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(2f);
        cooldown = false;
    }

    IEnumerator gonadie()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MecheMob>().ded();
        if (Gestionnaire.life < Gestionnaire.PowerUps[4])
        {
            Gestionnaire.life = Gestionnaire.life + 1;
        }
      
    }
}
