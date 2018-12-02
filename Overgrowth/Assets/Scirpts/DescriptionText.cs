using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionText : MonoBehaviour
{

    public string description;

    // Use this for initialization
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Description>().compteur > 50)
        {
            gameObject.GetComponent<Description>().sayText(description);
        }       
    }
}
