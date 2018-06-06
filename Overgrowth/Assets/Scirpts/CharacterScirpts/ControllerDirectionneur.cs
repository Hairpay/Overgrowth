using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDirectionneur : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;
    public bool mainD;
    public GameObject MainCamera;

    // Use this for initialization
    void Start () {

      
        character = GameObject.Find("character");
        MainCamera = GameObject.Find("CameraUltima");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;      
        Gestionnaire.manetteMode = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            Gestionnaire.manetteMode = false;
        }
        if (Mathf.Abs(Input.GetAxis("JumpM")) > 0.1)
        {
            Gestionnaire.manetteMode = true;
        }

        if (Gestionnaire.manetteMode == true)
        {
            gameObject.GetComponent<DirectioneurMouse>().enabled = false;

            float angH = Input.GetAxis("RightH");
            float angV = Input.GetAxis("RightV");

            if (Mathf.Abs(angV) > 0.01 && Mathf.Abs(angH) > 0.01)
            {
                if (MainCamera.GetComponent<UltimaCameraScirpt>().direction == false && mainD == false)
                {
                    gameObject.transform.localEulerAngles = new Vector3(0, 0, angV > 0 ? Mathf.Acos(angH) / Mathf.PI * 180 : 360 - Mathf.Acos(angH) / Mathf.PI * 180);
                }
                else
                {
                    gameObject.transform.localEulerAngles = new Vector3(0, 0, angV < 0 ? Mathf.Acos(angH) / Mathf.PI * 180 : 360 - Mathf.Acos(angH) / Mathf.PI * 180);
                }
            }
        }
        else
        {
            gameObject.GetComponent<DirectioneurMouse>().enabled = true;
        }     
    }
}
