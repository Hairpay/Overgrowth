using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOIgo : MonoBehaviour {

    public GameObject w;
    public Vector2 pos;
    public GameObject MainCamera;
    public GameObject explosion;

    public Vector3 baseRotation;

    // Use this for initialization
    void Start () {

        baseRotation = gameObject.transform.localEulerAngles;

        MainCamera = GameObject.Find("CameraUltima");

        if (MainCamera.GetComponent<UltimaCameraScirpt>().direction == true)
        {
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, w.transform.position.y - gameObject.transform.position.y);
            gameObject.transform.localEulerAngles = baseRotation;
        }
        else
        {
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, -(w.transform.position.y - gameObject.transform.position.y));
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -(baseRotation.z + 180));
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
       
 
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Rigidbody2D>().AddForce(pos*100);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag != "Player")
        {
            GameObject EXP = Instantiate(explosion);
            EXP.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }           
    }
}
