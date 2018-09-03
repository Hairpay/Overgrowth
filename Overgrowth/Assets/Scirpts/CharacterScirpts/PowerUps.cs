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
    
    void OnApplicationQuit()
    {
              
        
        //  Gestionnaire.VineBridge = false;

        Gestionnaire.life = Gestionnaire.PowerUps[4];

        Gestionnaire.SuitActivated = true;      
        Gestionnaire.KnockbackCD = false;
        Gestionnaire.disfunction = false;
        Gestionnaire.Locked = false;
        Gestionnaire.Checkpoint = basePos;

        for (int i = 0; i < Gestionnaire.PowerUps.Length; i++)
        {
            Gestionnaire.PowerUps[i] = 0;            
        }
       
    }
}
