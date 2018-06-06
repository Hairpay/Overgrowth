using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public GameObject flame;
    public ParticleSystem flameParticle;
    public Gestionnaire Gestionnaire;

    public bool firing;   
    public float maxDistance = 15f;
    private int layer_mask;

    // Use this for initialization
    void Start()
    {
        flame = GameObject.Find("Flames");
        flameParticle = flame.GetComponent<ParticleSystem>();
        flameParticle.Stop();
        layer_mask = ~LayerMask.GetMask("Player");
    }
    // Update is called once per frame
    void Update () {

        if (firing == true && Gestionnaire.Flamenwerfer == true)
        {            
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right * 1000, 15f, layer_mask);
            Debug.DrawLine(gameObject.transform.position, gameObject.transform.right * 1000, Color.red);
            //Debug.Log(hit.collider.gameObject);

            if (hit.collider != null && hit.collider.tag == "flamable" || hit.collider != null && hit.collider.tag == "Mob")
            {
                Debug.Log("burn !");              
                hit.collider.gameObject.GetComponent<Flamable>().burn = hit.collider.gameObject.GetComponent<Flamable>().burn + 1;
            }
        }


        if(Gestionnaire.Flamenwerfer == true)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                flameParticle.Play();             
                firing = true;

            }

            if (Input.GetButtonUp("Fire3"))
            {
                flameParticle.Stop();
                    firing = false;
            }
        }   
    }
}
