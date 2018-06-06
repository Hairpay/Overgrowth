using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject Particle;

    public bool une = true;

    // Use this for initialization
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnPoint.transform.position = gameObject.transform.position;
            spawnPoint.transform.rotation = gameObject.transform.rotation;
            if (une == true)
            {
                une = false;
                GameObject Effect = Instantiate(Particle);
                Effect.transform.position = gameObject.transform.position;
            }
        }
    }
}