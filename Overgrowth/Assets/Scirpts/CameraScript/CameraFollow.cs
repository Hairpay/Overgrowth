using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject Character;
    public GameObject Directioneur;

    public float Directiangle;

    public float t;
    public float t2;

    public Vector3 basePos;
    public Vector3 otherPos;
    public Vector3 actualPos;

    public bool leftmode;
    public bool switching;
   
    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        Directioneur = GameObject.Find("Directioneur");
        basePos = gameObject.transform.localPosition;
        otherPos = new Vector3(-basePos.x, basePos.y, basePos.z);
    }

    // Update is called once per frame
    void Update()
    {
        Directiangle = Directioneur.transform.localRotation.z;
        Directiangle = Mathf.Abs(Directiangle);      
        
        if (leftmode == false)
        {
            if (Directiangle < 0.55f)
            {
                t2 = 0.0f;
                switching = true;
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(basePos.x, otherPos.x, t), 0f, 0f);
                t += 2f * Time.deltaTime;
               
                if (t > 1.2f)
                {
                    switching = false;
                    leftmode = true;
                    t = 0.0f;                 
                }
            }

            if (Directiangle > 0.85f && switching == true)
            {
                t = 0.0f;
                actualPos = gameObject.transform.localPosition;
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(actualPos.x, basePos.x, t2), 0f, 0f);
                t2 += 2f * Time.deltaTime;

                if (t2 > 1.2f)
                {
                    switching = false;
                    leftmode = false;
                    t2 = 0.0f;                 
                }
            }
        }     
       if (leftmode == true)
        {
             if (Directiangle > 0.85f)
            {
                t2 = 0.0f;
                switching = true;
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(otherPos.x, basePos.x, t), 0f, 0f);
                t += 2f * Time.deltaTime;
                
                if (t > 1.2f)
                {
                    switching = false;
                    leftmode = false;
                    t = 0.0f;                  
                }
            }
            if (Directiangle < 0.55f && switching == true)
            {
                t = 0.0f;
                actualPos = gameObject.transform.localPosition;
                gameObject.transform.localPosition = new Vector3(Mathf.Lerp(actualPos.x, otherPos.x, t2), 0f, 0f);
                t2 += 2f * Time.deltaTime;

                if (t2 > 1.2f)
                {
                    switching = false;
                    leftmode = true;                
                    t2 = 0.0f;
                }
            }
        }
    }  
}