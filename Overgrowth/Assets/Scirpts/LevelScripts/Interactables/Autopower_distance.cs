using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autopower_distance : MonoBehaviour
{

    public GameObject character;

    public float activeDist;
    public float dist;

    public bool depower;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(character.transform.position, transform.position);

        if (Mathf.Abs(dist) < activeDist)
        {
            gameObject.GetComponent<PowerSource>().power = true;
        }
        else if (depower == true && Mathf.Abs(dist) > activeDist * 2)
        {
            gameObject.GetComponent<PowerSource>().power = false;
        }
    }
}
