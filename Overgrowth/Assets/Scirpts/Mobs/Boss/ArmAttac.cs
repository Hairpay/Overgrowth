using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttac : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public float t;

    // Use this for initialization
    void Start () {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(
              (Mathf.Lerp(gameObject.transform.position.x, character.transform.position.x, t)),
              (Mathf.Lerp(gameObject.transform.position.y, character.transform.position.y, t)),
              gameObject.transform.position.z);

        if (t < 1)
        {
            t += 0.4f * Time.deltaTime;
        }
        else
        {          
           // t = 0;          
        }
    }
}
