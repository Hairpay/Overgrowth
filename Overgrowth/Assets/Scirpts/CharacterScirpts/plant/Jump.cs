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
        if (Gestionnaire.SuitActivated == false)
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

            if (Gestionnaire.isGlinding == true)
            {
                isPlaning = false;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity * 0.1f;
            }
            else if (isPlaning == false)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = baseGravity;
            }
        }
        
    }
        public void _Jump()
    {      
        if (Gestionnaire.JumpCD == 0)
            {
            /*
                if (Gestionnaire.isGlinding == true && Gestionnaire.GlideGauche == false)
                {
                    gameObject.GetComponent<SuitMove>().enabled = false;
                    decale = -1000f;
                    StartCoroutine("ReturnVariables");
                }

                else if (Gestionnaire.isGlinding == true && Gestionnaire.GlideGauche == true)
                {
                    gameObject.GetComponent<SuitMove>().enabled = false;
                    decale = 1000f;
                    StartCoroutine("ReturnVariables");
                }
                else
                {
                    decale = 0f;
                }
                */
                finalJump = new Vector2(decale, jumpPower);
                body.velocity = new Vector2(body.velocity.x, 0f);
                body.AddForce(finalJump);
                Gestionnaire.JumpCD = Gestionnaire.JumpCD + 1;
        }            
    }

    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SuitMove>().enabled = true;
    }
}
