using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlecheHaut_C : MonoBehaviour
{


    public GameObject elevatorCall;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Description>().compteur > 50)
        {
            //analysisText.text = "Elevator Called.";
            Debug.Log("boihaut");
            elevatorCall.GetComponent<ElevatorCallV2_C>().GoHaut();
            gameObject.GetComponent<Description>().stopeth();
        }
    }
}
