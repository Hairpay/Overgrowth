using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchPanel : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;
    public GameObject Destination;
    public GameObject Camera;

    public float dist; 
   

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Camera = GameObject.Find("CameraUltima");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;         
    }
	
	// Update is called once per frame
	void Update () {

        dist = Vector3.Distance(Character.transform.position, transform.position);

        if (dist < 5f)
        {
            Character.transform.position = Destination.transform.position;
            Gestionnaire.currentSalle = Destination.GetComponent<Autofufu>().noSalle;
            Camera.transform.position = new Vector3(Destination.transform.position .x - 3, Destination.transform.position.y + 3, 0);
        }            
    }
}
