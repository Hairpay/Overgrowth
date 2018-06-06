using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

    public GameObject VieuxLevel;
    public GameObject NewLevel;

	// Use this for initialization
	void Start () {
        NewLevel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)

    {  
        if (other.tag == "Player")
        {
            VieuxLevel.SetActive( false);
            NewLevel.SetActive(true);
            
        }

    }
}
