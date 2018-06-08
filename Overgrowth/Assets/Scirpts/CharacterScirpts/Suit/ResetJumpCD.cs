using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJumpCD : MonoBehaviour {

    private Rigidbody2D body;
    public Vector2 Vitesse;
    public int Collision = 40;
    public Gestionnaire Gestionnaire;

   // public bool isGliding;
    public float distance;

    public float baseGravity;
    public float touchSol;
    
    // Use this for initialization
    void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        baseGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }
	
	// Update is called once per frame
	void Update () {

        Vitesse = body.velocity;

        if(Gestionnaire.isGlinding == true)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity * 0.1f;          
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameObject.GetComponent<SuitMove>().enabled = true;
        //  Debug.Log(coll.gameObject);

        // RaycastHit2D wallHit = Physics2D.Raycast(gameObject.transform.position, coll.gameObject.transform.position);
        touchSol = gameObject.transform.position.y - coll.gameObject.transform.position.y;

        if (Mathf.Abs(Vitesse.x) > Collision || Mathf.Abs(Vitesse.y) > Collision )
        {
            gameObject.GetComponent<Shockwave>().wave();
            if (coll.gameObject.tag == "waveBreakable" && Gestionnaire.ShockWave == true || coll.gameObject.tag == "Mob" && Gestionnaire.ShockWave == true)
            {
                coll.gameObject.GetComponent<waveBreak>().waveBroke();
            }
        }

        /*
        else if (coll.gameObject.tag == "Mob")
        {
            coll.gameObject.GetComponent<Dangerous>().aie();
        }
        */

        //   Debug.Log("touché");
        if (touchSol > 2f && coll.gameObject.tag != "Wall")
        {
           // Debug.Log("touché et sol");
          //  gameObject.GetComponent<PowerJump>().Cooldown = 0;
            Gestionnaire.JumpCD = 0;
            gameObject.GetComponent<SuitMove>().enabled = true;
        }

        if (coll.gameObject.tag == "Wall" && coll.gameObject.tag != "Sol")
        {
            if (Gestionnaire.WallProps == true)
            {
               // gameObject.GetComponent<PowerJump>().Cooldown = 0;
                Gestionnaire.JumpCD = 0;
            }

            Gestionnaire.isGlinding = true;         
            distance = gameObject.transform.position.x - coll.gameObject.transform.position.x;
            Gestionnaire.directGlide = distance;

            /*
            if (Mathf.Abs(Vitesse.x) > 15 || Mathf.Abs(Vitesse.y) > 15)
            {
              //  Debug.Log("touché et Mur");
                body.velocity = new Vector2(0f, 0f);               
            }
            */
            body.velocity = new Vector2(0f, 0f);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
       // gameObject.GetComponent<SuitMove>().enabled = false;

        if (coll.gameObject.tag == "Wall")
        {
            Gestionnaire.isGlinding = false;
        }     
    }
 }
