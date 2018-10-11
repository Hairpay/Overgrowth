using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2AI : MonoBehaviour {

    Actions myState;
    public int layer_mask;

    public float distance;
    public GameObject character;
    public Rigidbody2D charBody;

    public Gestionnaire gestionnaire;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        charBody = character.GetComponent<Rigidbody2D>();
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        layer_mask = LayerMask.GetMask("Player", "Environment", "Door");
        myState = Actions.patrol;
    }
	public enum Actions
    {
        patrol,
        follow,
        attac,
        shoot,
    }
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.right, 200f, layer_mask);
        Debug.DrawLine(transform.position, hit.point, new Color(252, 252, 0));
        distance = Vector3.Distance(character.transform.position, transform.position);


        if (hit.collider != null )
        {
            if (hit.collider.tag == "Player" && distance > 10)
            {
                myState = Actions.follow;
            }
            if (hit.collider.tag == "Player" && distance < 10)
            {
                myState = Actions.attac;
            }

        }

        switch (myState)
        {
            case Actions.patrol:
                Patrol();
                    break;
            case Actions.follow:
                Follow();
                break;
            case Actions.attac:
                Attac();
                break;
            case Actions.shoot:
                Shoot();
                break;
        }
	}
    public void Patrol()
    {

    }
    public void Follow()
    {

    }
    public void Attac()
    {

    }
    public void Shoot()
    {

    }
}
