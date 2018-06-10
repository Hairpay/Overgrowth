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
    public float distanceReset; 
    public float baseGravity;  
    private int layer_mask;

    // Use this for initialization
    void Start () {

        layer_mask = ~LayerMask.GetMask("Player");
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
        if (Gestionnaire.JumpCD > 0)
        {
            /*
            RaycastHit2D hitSol = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up * -1, 2f, layer_mask);
         
            if (hitSol.collider != null && hitSol.collider.tag == "Sol")
            {
                Debug.Log("outch" + hitSol.collider.name);
                Gestionnaire.JumpCD = 0;
                
            }
            */
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (Mathf.Abs(Vitesse.x) > Collision || Mathf.Abs(Vitesse.y) > Collision )
        {
            gameObject.GetComponent<Shockwave>().wave();
            if (coll.gameObject.tag == "waveBreakable" &&
                Gestionnaire.ShockWave == true 
                
                || coll.gameObject.tag == "Mob" &&
                Gestionnaire.ShockWave == true)
            {
                coll.gameObject.GetComponent<waveBreak>().waveBroke();
            }
        }

        if (coll.gameObject.tag == "Wall" && coll.gameObject.tag != "Sol")
        {
            if (Gestionnaire.WallProps == true)
            {             
                Gestionnaire.JumpCD = 0;
            }

            Gestionnaire.isGlinding = true;
            Gestionnaire.GlideGauche = true;
            //   distance = gameObject.transform.position.x - coll.gameObject.transform.position.x;

            RaycastHit2D hitWall = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, 2f, layer_mask);

            if (hitWall.collider != null && hitWall.collider.tag == "Wall")
            {
                Debug.Log("touche " + hitWall.collider.name);
                Gestionnaire.GlideGauche = true;
                distance = -1;
            }
            else
            {
                Gestionnaire.GlideGauche = false;
                distance = 1;
            }

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
