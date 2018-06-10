using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class PowerUps : MonoBehaviour {

    public Gestionnaire Gestionnaire;
    //public GameObject flecherwerfer;

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
        suitColor = Torse.GetComponent<SpriteMeshInstance>().color;
        plantColor = suitColor;        
        plantColor.b = 0;            
    }

    
    void Update()
    {
        if (Gestionnaire.SuitActivated == true)
        {
           
            Torse.GetComponent<SpriteMeshInstance>().color = suitColor;
        }
        else
        {
          
            Torse.GetComponent<SpriteMeshInstance>().color = plantColor;
        }
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
        Gestionnaire.KnockbackCD = false;
        Gestionnaire.isGlinding = false;

        Gestionnaire.bigCheckpoint = basePos;
    }
}
