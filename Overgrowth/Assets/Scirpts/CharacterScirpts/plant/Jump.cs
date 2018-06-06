using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public Gestionnaire Gestionnaire;
    private Rigidbody2D body;
    private Vector2 finalJump;

    public float jumpPower;
   // public int Cooldown;
    public float touchSol;

    public bool isGliding;
    public float distance;
    public float baseGravity;
    public float decale;
    public bool isPlaning;

    public bool jumpPressed;

    // Use this for initialization
    void Start () {

        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        body = gameObject.GetComponent<Rigidbody2D>();
        baseGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpPressed = false;
        }

        if (jumpPressed == true || Input.GetAxis("JumpM") < -0.3)
        {
            _Jump();
        }

        if (jumpPressed == false && Gestionnaire.manetteMode == false || Mathf.Abs( Input.GetAxis("JumpM")) < 0.1 && Gestionnaire.manetteMode == true)
        {            
            _JumpUp();
        }

        if (isGliding == true)
        {
            isPlaning = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity * 0.1f;
        }
        else if (isPlaning == false)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity;
        }
    }
        public void _Jump()
    {
        if (Gestionnaire.Planeur == true && isGliding == false && body.velocity.y < 0)
        {
            isPlaning = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity * 0.1f;
        }

        if (Gestionnaire.JumpCD == 0)
            {

                if (isGliding == true && distance < 0)
                {
                    gameObject.GetComponent<SuitMove>().enabled = false;
                    decale = -200f;
                    StartCoroutine("ReturnVariables");
                }

                else if (isGliding == true && distance > 0)
                {
                    gameObject.GetComponent<SuitMove>().enabled = false;
                    decale = 200f;
                    StartCoroutine("ReturnVariables");
                }
                else
                {
                    decale = 0f;
                }

                finalJump = new Vector2(decale, jumpPower);
                body.velocity = new Vector2(body.velocity.x, 0f);
                body.AddForce(finalJump);
                Gestionnaire.JumpCD = Gestionnaire.JumpCD + 1;
        }

              
    }

    public void _JumpUp()
    {
        if (isGliding == false && Gestionnaire.JumpCD > 0)
        {
            isPlaning = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity;
        }
    }



        
    void OnCollisionEnter2D(Collision2D coll)
    {      
        touchSol = gameObject.transform.position.y - coll.gameObject.transform.position.y;

        if (touchSol > 2f && coll.gameObject.tag != "Wall")
        {
            Gestionnaire.JumpCD = 0;                       
        }

        if (coll.gameObject.tag == "Wall" && coll.gameObject.tag != "Sol" || coll.gameObject.tag == "flamable" && coll.gameObject.tag != "Sol")
        {
            if (Gestionnaire.WallProps == true)
            {
                Gestionnaire.JumpCD = 0;
            }

            isGliding = true;
            distance = gameObject.transform.position.x - coll.gameObject.transform.position.x;

        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        // gameObject.GetComponent<SuitMove>().enabled = false;

        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "flamable")
        {
            isGliding = false;
        }
    }

    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SuitMove>().enabled = true;
    }
}
