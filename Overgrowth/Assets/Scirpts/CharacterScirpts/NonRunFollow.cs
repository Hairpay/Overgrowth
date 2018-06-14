using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRunFollow : MonoBehaviour
{

    public GameObject Character;
    
    
   

    // Use this for initialization
    void Start()
    {
        
        Character = GameObject.Find("character");
      
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Character.transform.position;

        //float h = Input.GetAxis("Horizontal");
        /*
        if(h < 0)
        {
            FlecheRegard.transform.localScale = new Vector3(-baseScale.x,baseScale.y, baseScale.z);
        }

        if (h > 0)
        {
           FlecheRegard.transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
        }*/
    }    
}