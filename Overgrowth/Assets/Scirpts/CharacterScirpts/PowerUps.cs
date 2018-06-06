using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class PowerUps : MonoBehaviour {

    public Gestionnaire Gestionnaire;
    public GameObject flecherwerfer;

    public Color suitColor;
    public Color plantColor;
    public GameObject Torse;

    public Vector3 basePos;


    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {

        basePos = Gestionnaire.bigCheckpoint;
        flecherwerfer = GameObject.Find("Flecherwerfer");
       // Torse = GameObject.Find("Torse");
        suitColor = Torse.GetComponent<SpriteMeshInstance>().color;
        plantColor = suitColor;        
        plantColor.b = 0;            
    }
    void Update()
    {
        if (Gestionnaire.SuitActivated == true)
        {
            suitMode();
            Torse.GetComponent<SpriteMeshInstance>().color = suitColor;
        }
        else
        {
            planteMode();
            Torse.GetComponent<SpriteMeshInstance>().color = plantColor;
        }
/*
        if (Input.GetButtonDown("Fire5") && Gestionnaire.atPoint == true && Gestionnaire.canSwitch == true)
        {
            Gestionnaire.SuitActivated = !Gestionnaire.SuitActivated;
        }
        */
     }

    public void planteMode()
    {
        gameObject.GetComponent<PowerJump>().enabled = false;
        gameObject.GetComponent<Shockwave>().enabled = false;
        gameObject.GetComponent<ResetJumpCD>().enabled = false;
      //  flecherwerfer.GetComponent<Laser>().enabled = false;

        gameObject.GetComponent<Jump>().enabled = true;
      //  gameObject.GetComponent<VineBridge>().enabled = true;

        gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedPlant;

    }

    public void suitMode()
    {
        gameObject.GetComponent<PowerJump>().enabled = true;
        gameObject.GetComponent<Shockwave>().enabled = true;
        gameObject.GetComponent<ResetJumpCD>().enabled = true;
      //  flecherwerfer.GetComponent<Laser>().enabled = true;

        gameObject.GetComponent<Jump>().enabled = false;
       // gameObject.GetComponent<VineBridge>().enabled = false;

        gameObject.GetComponent<SuitMove>().Speed = gameObject.GetComponent<SuitMove>().MaxSpeedBase;

    }

    // Update is called once per frame	
    void OnCollisionEnter2D(Collision2D coll)
    {
        // suit & plant power ups
        if (coll.gameObject.tag == "WallProps")
        {
            Gestionnaire.WallProps = true;
        }

        if (coll.gameObject.tag == "canSwitch")
        {
            Gestionnaire.canSwitch = true;
        }

        if ( Gestionnaire.SuitActivated == true)
        {
            // suit power ups
            if (coll.gameObject.tag == "GravityAnchor")
            {
                Gestionnaire.GravityAnchor = true;
            }

            if (coll.gameObject.tag == "Flamenwerfer")
            {
                Gestionnaire.Flamenwerfer = true;
            }

            if (coll.gameObject.tag == "ShockWave")
            {
                Gestionnaire.ShockWave = true;
            }
        }
        else if(Gestionnaire.SuitActivated == false)
        {         
            /*
            if (coll.gameObject.tag == "VineBridge")
            {
                Gestionnaire.VineBridge = true;
            }
            */
            if (coll.gameObject.tag == "Planeur")
            {
                Gestionnaire.Planeur = true;
            }
        }     
        
    }
    void OnApplicationQuit()
    {
        
        Gestionnaire.GravityAnchor = false;
        Gestionnaire.WallProps = false;
        Gestionnaire.Flamenwerfer = false;
        Gestionnaire.ShockWave = false;
      
        Gestionnaire.Planeur = false;
      //  Gestionnaire.VineBridge = false;

        Gestionnaire.life = Gestionnaire.maxLife;

        Gestionnaire.SuitActivated = true;
        Gestionnaire.canSwitch = false;
        Gestionnaire.resetPU = true;

        Gestionnaire.bigCheckpoint = basePos;
    }
}
