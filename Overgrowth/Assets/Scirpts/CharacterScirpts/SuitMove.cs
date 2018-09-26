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

    // Use this for initialization
    void Start () {

        body = gameObject.GetComponent<Rigidbody2D>();
        speed = maxSpeedBase;
        gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        layerMask = LayerMask.GetMask("Environment");
        sizebox = gameObject.GetComponent<CapsuleCollider2D>().size;

    }
	
	// Update is called once per frame
	void Update () {

        CheckCrouch();

        float h = Input.GetAxis("Horizontal");       
        isJumping = gestionnaire.JumpCD;
        Move(h);
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
    }

    public void CheckCrouch()
    {
        Vector3 hautPos = new Vector3(transform.position.x, transform.position.y + 1.6f, 0);
        Vector3 basPos = new Vector3(transform.position.x, transform.position.y - 1, 0);

        RaycastHit2D leftHaut = Physics2D.Raycast(hautPos, Vector2.left, 1, layerMask);
        Debug.DrawRay(hautPos, Vector2.left * 1);       
        RaycastHit2D leftBas = Physics2D.Raycast(basPos, Vector2.left, 1, layerMask);
        Debug.DrawRay(basPos, Vector2.left * 1);       
      
        RaycastHit2D rightHaut = Physics2D.Raycast(hautPos, Vector2.right, 1, layerMask);
        Debug.DrawRay(hautPos, Vector2.right * 1);       
        RaycastHit2D rightBas = Physics2D.Raycast(basPos, Vector2.right, 1, layerMask);
        Debug.DrawRay(basPos, Vector2.right * 1);

        RaycastHit2D centre = Physics2D.Raycast(transform.position, Vector2.up, 2, layerMask);
        Debug.DrawRay(transform.position, Vector2.up * 2);

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
    }

    public void Move( float move )
    {
        if( isJumping > 0 && gestionnaire.SuitActivated == true || gestionnaire.KnockbackCD == true )
        {
            body.velocity = new Vector2( ( move * speed * 0.06f ) + ( body.velocity.x ), body.velocity.y );
        }
        else
        {
            body.velocity = new Vector2( move * speed, body.velocity.y );
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
