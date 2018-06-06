using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorder : MonoBehaviour {

    public GameObject leftBorder;
    public GameObject rightBorder;
    public GameObject upBorder;
    public GameObject botBorder;

    public float leftDistance;
    public float rightDistance;
    public float upDistance;
    public float botDistance;

    // Use this for initialization
    void Start () {

        leftBorder = GameObject.Find("LeftBorder");
        rightBorder = GameObject.Find("RightBorder");
        upBorder = GameObject.Find("UpBorder");
        botBorder = GameObject.Find("BotBorder");
    }
	
	// Update is called once per frame
	void Update () {

        leftDistance = Vector3.Distance(leftBorder.transform.position, transform.position);
        rightDistance = Vector3.Distance(rightBorder.transform.position, transform.position);
        upDistance = Vector3.Distance(upBorder.transform.position, transform.position);
        botDistance = Vector3.Distance(botBorder.transform.position, transform.position);

    }
}
