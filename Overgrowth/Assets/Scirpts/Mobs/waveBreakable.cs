using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBreakable : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public bool waveable;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
	
	// Update is called once per frame
	void Update () {

        if(Character.GetComponent<ResetJumpCD>().Vitesse.x > Character.GetComponent<ResetJumpCD>().Collision 
            || Character.GetComponent<ResetJumpCD>().Vitesse.y > Character.GetComponent<ResetJumpCD>().Collision)
        {
            waveable = true;
        }
        else
        {
            waveable = false;
        }        
	}

    void OnCollisionEnter2D(Collision2D coll)
    {       
        if (coll.gameObject.tag == "Player" && waveable == true && Gestionnaire.ShockWave == true)
        {
            Destroy(gameObject);
        }
    }
 }
