using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSelfDestruct : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public int Boost;
    public string textRamasse;

    public bool once;
    

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.GetComponent<Description>().compteur > 50 && once == false)
        {
            Activate();
            once = true;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && once == false)
        {
            Activate();
            once = true;
        }
    }

    public void Activate()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        gameObject.GetComponent<DescriptionText>().description = textRamasse;
        gameObject.GetComponent<Description>().compteur = 55;
        gameObject.GetComponent<Description>().addnalyse();

        Gestionnaire.PowerUps[Boost] = Gestionnaire.PowerUps[Boost] + 1;
        if (Boost == 1)
        {
            Gestionnaire.disfunction = false;
        }
        if (Boost == 4)
        {
            Gestionnaire.life = Gestionnaire.life + 1;
        }

        StartCoroutine("selfDestroy");
    }

     

    IEnumerator selfDestroy()
    {
        GameObject.Find("Directiowerfer").GetComponent<AnalysisBeam>().ReturnWait(3f);
        yield return new WaitForSeconds(0.2f);
     //   gameObject.GetComponent<Description>().hidenalyse();
        Destroy(gameObject);
    }
}

