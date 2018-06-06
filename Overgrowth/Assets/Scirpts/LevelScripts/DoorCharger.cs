using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCharger : MonoBehaviour {

    public GameObject Character;
    public GameObject doorList;
    public Gestionnaire Gestionnaire;

    public int doorNumber;
    public GameObject goDoor;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        doorList = GameObject.Find("DoorList");
        Gestionnaire = gameObject.GetComponentInChildren<PowerUps>().Gestionnaire;   
        goDoor = doorList.GetComponent<DoorInventory>().doors[doorNumber];
        gameObject.transform.position = goDoor.transform.position;
        Character.transform.localPosition = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
