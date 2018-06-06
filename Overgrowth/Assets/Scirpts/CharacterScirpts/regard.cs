using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regard : MonoBehaviour {

    public GameObject MainCamera;
    public Vector3 baseScale;

	// Use this for initialization
	void Start () {

        MainCamera = GameObject.Find("CameraUltima");
        baseScale = gameObject.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {

        if (MainCamera.GetComponent<UltimaCameraScirpt>().direction == true)
        {
            gameObject.transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }
		
	}
}
