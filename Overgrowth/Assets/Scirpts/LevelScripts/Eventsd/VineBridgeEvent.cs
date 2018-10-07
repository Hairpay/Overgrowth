using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBridgeEvent : MonoBehaviour
{

    public GameObject Character;
    public Gestionnaire Gestionnaire;
    public string textRamasse;

    public bool once;
    public GameObject[] toHide;
    public GameObject[] toShow;

    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        for (int i = 0; i < toShow.Length; i++)
        {
            toShow[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && Gestionnaire.SuitActivated == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
         
            Gestionnaire.PowerUps[5] = 1;
            Gestionnaire.SuitActivated = false;
            Gestionnaire.life = Gestionnaire.PowerUps[4];

            gameObject.GetComponent<DescriptionText>().description = textRamasse;
            gameObject.GetComponent<Description>().compteur = 55;
            gameObject.GetComponent<Description>().addnalyse();


            for (int i = 0; i < toShow.Length; i++)
            {
                toShow[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < toHide.Length; i++)
            {
                toHide[i].gameObject.SetActive(false);
            }

            StartCoroutine("selfDestroy");
        }
    }

    IEnumerator selfDestroy()
    {
        GameObject.Find("Directiowerfer").GetComponent<laserBeam>().ReturnWait(3f);
        yield return new WaitForSeconds(0.2f);
      //  gameObject.GetComponent<Description>().hidenalyse();
        Destroy(gameObject);
    }
}

