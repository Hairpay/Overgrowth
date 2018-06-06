using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBox : MonoBehaviour {


    public GameObject Character;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Character.transform.position;
    }

  
}

