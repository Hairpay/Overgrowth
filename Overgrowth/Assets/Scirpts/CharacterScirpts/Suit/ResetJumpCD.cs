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
        if (touchSol > 2f && coll.gameObject.tag != "Wall")
        {
            Gestionnaire.JumpCD = 0;           
        }
        */
        RaycastHit2D hitSol = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up *-1, 3f);
        if (hitSol.collider != null && hitSol.collider.transform.tag == "Sol")
        {
            Gestionnaire.JumpCD = 0;
        }

        if (coll.gameObject.tag == "Wall" && coll.gameObject.tag != "Sol")
        {
            if (Gestionnaire.WallProps == true)
            {             
                Gestionnaire.JumpCD = 0;
            }

            Gestionnaire.isGlinding = true;         
            distance = gameObject.transform.position.x - coll.gameObject.transform.position.x;
            Gestionnaire.directGlide = distance;       
            body.velocity = new Vector2(0f, 0f);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            Gestionnaire.isGlinding = false;
        }     
    }
 }
