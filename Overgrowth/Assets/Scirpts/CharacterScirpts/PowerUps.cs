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

        basePos = Gestionnaire.Checkpoint;
        suitColor = Torse.GetComponent<SpriteMeshInstance>().color;
        plantColor = suitColor;        
        plantColor.b = 0;

        Gestionnaire.KnockbackCD = false;
        Gestionnaire.JumpCD = 0;
        Gestionnaire.SuitActivated = true;
        Gestionnaire.Locked = false;
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
   
        
    
    void OnApplicationQuit()
    {
              
        
        //  Gestionnaire.VineBridge = false;

        Gestionnaire.life = Gestionnaire.maxLife;

        Gestionnaire.SuitActivated = true;
        Gestionnaire.canSwitch = false;
        Gestionnaire.KnockbackCD = false;

        Gestionnaire.Checkpoint = basePos;
        Gestionnaire.analysisLevel = 0;
    }
}
