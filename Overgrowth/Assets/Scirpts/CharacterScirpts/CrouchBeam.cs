using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchBeam : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public Vector3 basePos;
    public Vector3 crouchPos;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        basePos = gameObject.transform.localPosition;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Gestionnaire.Crouch == false)
        {
            gameObject.transform.localPosition = basePos;
        }
        else
        {
            gameObject.transform.localPosition = crouchPos;
        }
	}
}
