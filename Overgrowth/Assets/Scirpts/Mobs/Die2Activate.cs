using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die2Activate : MonoBehaviour
{

    public bool once;
    public GameObject[] toPower;

    public GameObject[] toHide;
    public GameObject[] toShow;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < toShow.Length; i++)
        {
            toShow[i].gameObject.SetActive(false);
        }

    }

    public void activateDed()
    {
        if (once == false)
        {
            once = true;
            for (int i = 0; i < toPower.Length; i++)
            {
                if (toPower[i].gameObject.GetComponent<PowerSource>() != null)
                {
                    toPower[i].gameObject.GetComponent<PowerSource>().power = true;
                }
                else if (toPower[i].gameObject.GetComponentInChildren<PowerSource>() != null)
                {
                    toPower[i].gameObject.GetComponentInChildren<PowerSource>().power = true;
                }
                else
                {
                    Debug.Log(toPower[i].gameObject + " has no power source");
                }
            }
            for (int i = 0; i < toShow.Length; i++)
            {
                toShow[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < toHide.Length; i++)
            {
                toHide[i].gameObject.SetActive(false);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

       

    }
}
