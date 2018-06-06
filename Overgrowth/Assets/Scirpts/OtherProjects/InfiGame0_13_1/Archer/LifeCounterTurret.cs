using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounterTurret : MonoBehaviour
{

    public int vie = 3;
    public GameObject Redaie;
    public bool Iframe = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vie < 1)
        {
            gameObject.GetComponent<ExplotronVoid>().Explosion();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            if (Iframe == false)
            {
                gameObject.GetComponent<ArcherAITurret>().cone = true;
                vie = vie - 1;
                Iframe = true;
                StartCoroutine("Cooldown");
                Redaie.GetComponent<Light>().enabled = true;
            }

        }
    }
    IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(0.5f);
        Iframe = false;
        Redaie.GetComponent<Light>().enabled = false;
    }
}
