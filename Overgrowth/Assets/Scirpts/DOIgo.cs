using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOIgo : MonoBehaviour {

    public GameObject w;
    public Vector2 pos;
    public GameObject MainCamera;
    public GameObject explosion;

    // Use this for initialization
    void Start () {

        MainCamera = GameObject.Find("CameraUltima");

        if (MainCamera.GetComponent<UltimaCameraScirpt>().direction == true)
        {
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, w.transform.position.y - gameObject.transform.position.y);
        }
        else
        {
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, -(w.transform.position.y - gameObject.transform.position.y));
        }
       
 
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Rigidbody2D>().AddForce(pos*50);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject EXP = Instantiate(explosion);
        EXP.transform.position = gameObject.transform.position;
        Destroy(gameObject);        
    }
}
