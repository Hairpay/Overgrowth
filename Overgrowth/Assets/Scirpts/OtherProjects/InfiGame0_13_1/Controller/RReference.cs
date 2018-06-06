using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RReference : MonoBehaviour {

    public GameObject Mangora;
    
	// Use this for initialization
	void Start () {
        Mangora = GameObject.Find("Mangora");
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(Mangora.transform.position.x, Mangora.transform.position.y, Mangora.transform.position.z -1);
	}
}
