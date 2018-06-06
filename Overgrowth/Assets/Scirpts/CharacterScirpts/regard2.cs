using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regard2 : MonoBehaviour
{

    public GameObject MainCamera;
    public Vector3 baseScale;
    public Vector3 baseRotation;

    // Use this for initialization
    void Start()
    {

        MainCamera = GameObject.Find("CameraUltima");
        baseScale = gameObject.transform.localScale;
        baseRotation = gameObject.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (MainCamera.GetComponent<UltimaCameraScirpt>().direction == true)
        {
          //  gameObject.transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
            gameObject.transform.localEulerAngles = baseRotation;
        }
        else
        {
           // gameObject.transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -(baseRotation.z + 180));
        }

    }
}
