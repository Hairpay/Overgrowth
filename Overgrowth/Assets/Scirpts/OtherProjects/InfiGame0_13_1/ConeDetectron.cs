using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDetectron : MonoBehaviour {
    public GameObject Archer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<MeshCollider>().enabled = false;
            Archer.GetComponent<ArcherAI>().cone = true;
        }
    }
 }
