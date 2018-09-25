using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchmode : MonoBehaviour {

    public bool cooldown;

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public int layer_mask;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        layer_mask = ~LayerMask.GetMask("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire6") && Gestionnaire.isFiring == false && Gestionnaire.PowerUps[1] > 0)
        {
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, layer_mask);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 0.5f, Color.red, 2f);
            if (upHit.collider != null)
            {
                Debug.Log("Cant switch, hit: " + upHit.collider.name);
            }
            else
            {
                Gestionnaire.SuitActivated = !Gestionnaire.SuitActivated;
            }         
        }

       

    }
}
