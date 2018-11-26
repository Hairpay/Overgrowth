using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitMove : MonoBehaviour {

    private Rigidbody2D body;

    public float firingSpeed = 10f;
    public float maxSpeedBase = 10f;
    public float maxSpeedPlant = 13f;
    public float maxCrouchSpeed = 7f;
  
    public float speed;
    public int isJumping;

    public Gestionnaire gestionnaire;
    private int layerMask;
    public Vector2 sizebox;

    private Vector2 m_LastPos;
    public bool m_HasMoved;
    public float glideFactor;

    // Use this for initialization
    void Start () {

        body = gameObject.GetComponent<Rigidbody2D>();
        speed = maxSpeedBase;
        gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        layerMask = LayerMask.GetMask("Environment","VineBridge","Glass","Door");
        sizebox = gameObject.GetComponent<CapsuleCollider2D>().size;

    }
	
	// Update is called once per frame
	void Update () {

      
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        isJumping = gestionnaire.JumpCD;
        Move(h,v);
        body.gravityScale = 10;
        gestionnaire.Speed = Mathf.Abs(body.velocity.x);       

        if (gestionnaire.Crouch == true)
        {
            speed = maxCrouchSpeed;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.1f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(sizebox.x, sizebox.y * 0.5f);
        }
        else if (gestionnaire.isFiring == true)
        {
            speed = firingSpeed;
        }
        else if (gestionnaire.SuitActivated == true)
        {
            speed = maxSpeedBase;
        }
        else if (gestionnaire.SuitActivated == false)
        {
            speed = maxSpeedPlant;
        }

        if (gestionnaire.Crouch == false)
        {
            gameObject.GetComponent<CapsuleCollider2D>().size = sizebox;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.1f);
        }

        CheckCrouch();
        CheckGround();
    }
    public void CheckGround()
    {
        Vector3 basPos = new Vector3(transform.position.x, transform.position.y - 1, 0);
        Vector3 basPosG = new Vector3(transform.position.x - 0.7f, transform.position.y - 1, 0);
        Vector3 basPosD = new Vector3(transform.position.x + 0.7f, transform.position.y - 1, 0);

        RaycastHit2D rayGround = Physics2D.Raycast(basPos, Vector2.up * -1, 0.8f, layerMask);
        Debug.DrawRay(basPos, Vector2.up * -0.8f, new Color(252, 252, 0));
        RaycastHit2D rayGroundG = Physics2D.Raycast(basPosG, Vector2.up * -1, 0.8f, layerMask);
        Debug.DrawRay(basPosG, Vector2.up * -0.8f, new Color(252, 252, 0));
        RaycastHit2D rayGroundD = Physics2D.Raycast(basPosD, Vector2.up * -1, 0.8f, layerMask);
        Debug.DrawRay(basPosD, Vector2.up * -0.8f, new Color(252, 252, 0));

        if (gestionnaire.Jcd == false)
        {
            if(rayGround.collider != null || rayGroundG.collider != null|| rayGroundD.collider != null)
            {
                Debug.Log("grounded");
                gestionnaire.JumpCD = 0;
                gestionnaire.grounded = true;
            }
            else
            {
                gestionnaire.grounded = false;
            }
         
        }
    }

    public void CheckCrouch()
    {
        Vector3 hautPos = new Vector3(transform.position.x, transform.position.y + 1.6f, 0);
        Vector3 basPos = new Vector3(transform.position.x, transform.position.y - 1, 0);

        RaycastHit2D leftHaut = Physics2D.Raycast(hautPos, Vector2.left, 0.96f, layerMask);
        Debug.DrawRay(hautPos, Vector2.left * 0.96f);       
        RaycastHit2D leftBas = Physics2D.Raycast(basPos, Vector2.left, 0.96f, layerMask);
        Debug.DrawRay(basPos, Vector2.left * 0.96f);       
      
        RaycastHit2D rightHaut = Physics2D.Raycast(hautPos, Vector2.right, 0.96f, layerMask);
        Debug.DrawRay(hautPos, Vector2.right * 0.96f);       
        RaycastHit2D rightBas = Physics2D.Raycast(basPos, Vector2.right, 0.96f, layerMask);
        Debug.DrawRay(basPos, Vector2.right * 0.96f);

        RaycastHit2D centre = Physics2D.Raycast(transform.position, Vector2.up, 2, layerMask);
        Debug.DrawRay(transform.position, Vector2.up * 2);

        //check crouch here
        if (leftBas.collider == null && leftHaut.collider != null || rightBas.collider == null && rightHaut.collider != null)
        {
            if (gestionnaire.grounded == true)
            {
                gestionnaire.Crouch = true;
            }                   
        }
        else
        {
            if (centre.collider == null)
            {
                gestionnaire.Crouch = false;
            }          
        }
        //check glide here
       
        if (leftBas.collider != null && gestionnaire.grounded == false)
        {
            if (gestionnaire.isGlinding == false)
            {
                Glide();
                gestionnaire.GlideGauche = true;
            }
           
        }
        else if ( rightBas.collider != null && gestionnaire.grounded == false)
        {
            if (gestionnaire.isGlinding == false)
            {
                Glide();
                gestionnaire.GlideGauche = false;
            }
        }
        else
        {
            gestionnaire.isGlinding = false;
            glideFactor = 1;
        }
    }

    public void Glide()
    {                   
         if (gestionnaire.PowerUps[6] > 0)
         {
            glideFactor = 0.5f;
            gestionnaire.isGlinding = true;
            gestionnaire.JumpCD = 0;
         }       
    }

    public void Move( float move, float vmove)
    {
        if( isJumping > 0 && gestionnaire.SuitActivated == true || gestionnaire.KnockbackCD == true)
        {
            body.velocity = new Vector2( ( move * speed * 0.06f ) + ( body.velocity.x ), body.velocity.y);
        }
        else
        {
            if (gestionnaire.isGlinding == true && gestionnaire.SuitActivated == false && gestionnaire.PowerUps[6] > 0)
            {
                if (vmove < 0.01f && vmove > -0.01f)
                {
                    body.simulated = false;
                    body.velocity = new Vector2(0f, 0f);
                }
                else
                {
                    body.simulated = true;
                }
                body.gravityScale = 0;
                body.velocity = new Vector2(move * speed, vmove * 10);
                Debug.Log("boi");
            }
            else
            {
                body.velocity = new Vector2(move * speed, body.velocity.y * glideFactor);
            }         
        }
        m_HasMoved = HasMoved();
        m_LastPos = body.position;
    }

    private bool HasMoved()
    {
        float epsilon = 0.0001f;
        if( body.position.x > m_LastPos.x + epsilon || body.position.x < m_LastPos.x - epsilon )
        {
            return true;
        }
        if( body.position.y > m_LastPos.y + epsilon || body.position.y < m_LastPos.y - epsilon )
        {
            return true;
        }
        return false;
    }
}
