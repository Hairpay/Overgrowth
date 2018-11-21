using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallElevator : MonoBehaviour {

    public GameObject Elevator;
    public int PositionArret;

    public GameObject charaDual;
    public Text analysisText;
    public Image analysisPanel;

    // Use this for initialization
    void Start ()
    {
        charaDual = GameObject.Find("character");
        analysisPanel = charaDual.GetComponent<UIGereur>().analysisPanel;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.GetComponent<Description>().compteur > 50)
        {
            analysisText.text = "Elevator Called.";
            Call();
        }

    }

    public void Call()
    {
        if (Elevator.GetComponent<Ascenseur>().power == true)
        {
            Elevator.GetComponent<Ascenseur>().Called();

            if (Elevator.GetComponent<Ascenseur>().position != PositionArret && Elevator.GetComponent<Ascenseur>().isMoving == false)
            {
                Elevator.GetComponent<Ascenseur>().position = PositionArret;
                Elevator.GetComponent<Ascenseur>().isMoving = true;
                Elevator.GetComponent<Ascenseur>().noHitbox = true;
              
            }
        }
        else
        {
            Elevator.GetComponent<Ascenseur>().Error();
        }
                      
    }
}
