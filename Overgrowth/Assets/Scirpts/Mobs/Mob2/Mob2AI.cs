using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2AI : MonoBehaviour {

    Actions myState;
    public int layer_mask;

    public float distance;
    public float distanceHit;
    public GameObject character;
    public Rigidbody2D charBody;

    public Gestionnaire gestionnaire;
    public Rigidbody2D body;

    public float speed;
    public float side = -1;
    public Vector3 toPlayer;
    public int compteurLoss;
    public bool laserCD;

    public GameObject hitbox;
    public float lastLife;


    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        charBody = character.GetComponent<Rigidbody2D>();
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        body = gameObject.GetComponent<Rigidbody2D>();

        layer_mask = LayerMask.GetMask("Player", "Environment", "Door");
        myState = Actions.patrol;
        lastLife = hitbox.GetComponent<MecheMob>().life;
    }
	public enum Actions
    {
        patrol,
        follow,
        attac,
        shoot
    }
	// Update is called once per frame
	void Update ()
    {      
        distance = Vector3.Distance(character.transform.position, transform.position);      

        switch (myState)
        {
            case Actions.patrol:
                CheckActions();
                Debug.Log("Patrol ongoing !");
                Patrol();
                break;
            case Actions.follow:
                Debug.Log("follow ongoing !");
                Follow();
                break;
            case Actions.attac:
                Attac();
                break;
            case Actions.shoot:
                Debug.Log("shoot ongoing !");
                Shoot();
                break;
        }

        lastLife = hitbox.GetComponent<MecheMob>().life;
    }
    public void CheckActions()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.right * side, 200f, layer_mask);
        Debug.DrawLine(transform.position, hit.point, new Color(252, 252, 0));       
        distanceHit = Vector3.Distance(hit.point, transform.position);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player" && distanceHit < 50)
            {
                myState = Actions.follow;
            }         
            else if (hit.collider.tag != "Player" && distanceHit < 5)
            {
                TurnBacc();
            }
            else
            {
                myState = Actions.patrol;
            }
        }

        if (lastLife != hitbox.GetComponent<MecheMob>().life)
        {
            myState = Actions.follow;
        }
    }

    public void Patrol()
    {   
        body.velocity = new Vector2(speed * side, 0f);       
    }
    public void Follow()
    {
       
        toPlayer = character.transform.position - gameObject.transform.position;
        toPlayer = toPlayer.normalized;

        if (toPlayer.x < -0.1 && side > 0 || toPlayer.x > 0.1 && side < 0)
        {
            TurnBacc();
        }     

        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, character.transform.position - gameObject.transform.position, 200f, layer_mask);
        Debug.DrawLine(transform.position, target.point, new Color(252, 0, 252));

        if (target.collider != null && target.collider.tag == "Player")
        {
            if (distance > 10)
            {
                if (laserCD == false)
                {
                    myState = Actions.shoot;
                    laserCD = true;
                }
                else
                {
                    body.velocity = new Vector2(speed * side, 0f);
                    compteurLoss = 0;
                }                       
            }
            else
            {
                myState = Actions.attac;
                body.velocity = new Vector2(0f, 0f);
            }          
        }
        else
        {
            body.velocity = new Vector2(0f, 0f);
            compteurLoss = compteurLoss + 1;
            if (compteurLoss > 150)
            {
                compteurLoss = 0;
                myState = Actions.patrol;
            }
        }
    }
    public void Attac()
    {
        Debug.Log("attac ongoing !");
        myState = Actions.follow;
    }
    public void Shoot()
    {
        Debug.Log("shoot ongoing !");
        myState = Actions.follow;
        StartCoroutine("ResetLaser");
    }
    public void TurnBacc()
    {
        Debug.Log("turnbacc ongoing !");
        body.velocity = new Vector2(0f, 0f);
        side = -side;
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y,1f); 
    }

    IEnumerator ResetLaser()
    {
        yield return new WaitForSeconds(5f);
        laserCD = false;
    }
}
