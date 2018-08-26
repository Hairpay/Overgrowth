using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascenseur : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public bool active;
    public int layer_mask;

    public GameObject[] Arrets;
    public int position;

    public bool isMoving;
    public float t;

    public bool goUp;
    public bool goDown;

    public Vector3 oldPos;
    public float v;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        layer_mask = LayerMask.GetMask("Player");

        oldPos = gameObject.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        v = Input.GetAxis("Vertical");

        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, 3f, layer_mask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up)* 3f);
        if(upHit.collider != null)
        {
            Debug.Log(upHit.collider.name);
            active = true;
        }
        else
        {
            active = false;
        }

        if (isMoving == false && active == true && v > 0.1f)
        {
            if (position > 0)
            {
                isMoving = true;
                position = position - 1;
                character.GetComponent<Rigidbody2D>().simulated = false;
                character.transform.parent = gameObject.transform;
                Gestionnaire.Locked = true;
            }
            goUp = false;
        }

        if (isMoving == false && active == true && v < -0.1f)
        {
            if(position < Arrets.Length)
            {
                isMoving = true;
                position = position + 1;
                character.GetComponent<Rigidbody2D>().simulated = false;
                character.transform.parent = gameObject.transform;
                Gestionnaire.Locked = true;
            }        
            goDown = false;
        }

        if(isMoving == true)
        {
            gameObject.transform.position = new Vector3( gameObject.transform.position.x, (Mathf.Lerp(oldPos.y, Arrets[position].transform.position.y, t)), gameObject.transform.position.z);          
            if (t < 1)
            {
                t += 0.4f * Time.deltaTime;
            }
            else
            {
                isMoving = false;
                t = 0;
                oldPos = gameObject.transform.position;
                character.GetComponent<Rigidbody2D>().simulated = true;
                character.transform.parent = null;
                Gestionnaire.Locked = false;
            }
        }
    }
}
