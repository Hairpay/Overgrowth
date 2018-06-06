using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceChange : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public float dist;
    public float neeDistance;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;             
    }
	
	// Update is called once per frame
	void Update () {

        dist = Vector3.Distance(Character.transform.position, transform.position);

        if (dist < neeDistance)
        {
            Gestionnaire.SuitActivated = false;                    
        }
    }
}
