using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevator : MonoBehaviour {

    public GameObject Elevator;
    public int PositionArret;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Call()
    {
        if (Elevator.GetComponent<Ascenseur>().position != PositionArret && Elevator.GetComponent<Ascenseur>().isMoving == false)
        {
            Elevator.GetComponent<Ascenseur>().position = PositionArret;
            Elevator.GetComponent<Ascenseur>().isMoving = true;
        }                 
    }
}
