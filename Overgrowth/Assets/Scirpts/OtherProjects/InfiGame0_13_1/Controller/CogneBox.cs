using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogneBox : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start ()
     {
        player = GameObject.Find("Mangora");
	 }	
    void OnTriggerEnter(Collider other)
     {
        if (other.tag == "Obstacle")
        {
           // print("oui");
            player.GetComponent<ControllerV0>().cogneHaut = true;
        }
     }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            player.GetComponent<ControllerV0>().cogneHaut = false;
        }
    }
}
