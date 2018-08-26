using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSelfDestruct : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public int Boost;
    

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {          
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Description>().compteur = 55;
            gameObject.GetComponent<Description>().addnalyse();

            Gestionnaire.PowerUps[Boost] = Gestionnaire.PowerUps[Boost] + 1;

            StartCoroutine("selfDestroy");
        }
    }   

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<Description>().hidenalyse();
        Destroy(gameObject);
    }
}

