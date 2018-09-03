using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCinematic : MonoBehaviour {

    public ParticleSystem[] sparks;
    public int triggerCinem;

    public bool done;
    public float tim2w8t = 1f;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < sparks.Length; i++)
        {
            sparks[i].Pause();
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.GetComponent<Ascenseur>().position == triggerCinem && gameObject.GetComponent<Ascenseur>().isMoving == false && done ==false)
        {
            done = true;
            gameObject.GetComponent<Ascenseur>().enabled = false;
            StartCoroutine("fallDown");
        }
		
	}
    IEnumerator fallDown()
    {
        yield return new WaitForSeconds(tim2w8t);
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().mass = 100;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
        for (int i = 0; i < sparks.Length; i++)
        {
            sparks[i].Play();
        }
    }
}
