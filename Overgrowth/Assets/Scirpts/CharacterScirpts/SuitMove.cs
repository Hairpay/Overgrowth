using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitMove : MonoBehaviour {

    private Rigidbody2D body;

    public float FiringSpeed = 10f;
    public float MaxSpeedBase = 10f;
    public float MaxSpeedPlant = 13f;
    public float MaxCrouchSpeed = 7f;
  
    public float Speed;
    public int isJumping;

    public Gestionnaire Gestionnaire;
    private int layer_mask;
    public Vector2 sizebox;

    // Use this for initialization
    void Start () {

        body = gameObject.GetComponent<Rigidbody2D>();
        Speed = MaxSpeedBase;
        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        layer_mask = LayerMask.GetMask("Environment");
        sizebox = gameObject.GetComponent<CapsuleCollider2D>().size;

    }
	
	// Update is called once per frame
	void Update () {

        CheckCrouch();

        float h = Input.GetAxis("Horizontal");       
        isJumping = Gestionnaire.JumpCD;
        Move(h);
        Gestionnaire.Speed = Mathf.Abs(body.velocity.x);       

        if (Gestionnaire.Crouch == true)
        {
            Speed = MaxCrouchSpeed;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.1f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(sizebox.x, sizebox.y * 0.5f);
        }
        else if (Gestionnaire.isFiring == true)
        {
            Speed = FiringSpeed;
        }
        else if (Gestionnaire.SuitActivated == true)
        {
            Speed = MaxSpeedBase;
        }
        else if (Gestionnaire.SuitActivated == false)
        {
            Speed = MaxSpeedPlant;
        }

        if (Gestionnaire.Crouch == false)
        {
            gameObject.GetComponent<CapsuleCollider2D>().size = sizebox;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.1f);
        }
    }

    public void CheckCrouch()
    {
        Vector3 hautPos = new Vector3(transform.position.x, transform.position.y + 1.6f, 0);
        Vector3 basPos = new Vector3(transform.position.x, transform.position.y - 1, 0);

        RaycastHit2D leftHaut = Physics2D.Raycast(hautPos, Vector2.left, 2, layer_mask);
        Debug.DrawRay(hautPos, Vector2.left * 2);       
        RaycastHit2D leftBas = Physics2D.Raycast(basPos, Vector2.left, 2, layer_mask);
        Debug.DrawRay(basPos, Vector2.left * 2);       
      
        RaycastHit2D rightHaut = Physics2D.Raycast(hautPos, Vector2.right, 2, layer_mask);
        Debug.DrawRay(hautPos, Vector2.right * 2);       
        RaycastHit2D rightBas = Physics2D.Raycast(basPos, Vector2.right, 2, layer_mask);
        Debug.DrawRay(basPos, Vector2.right * 2);

        RaycastHit2D centre = Physics2D.Raycast(transform.position, Vector2.up, 2, layer_mask);
        Debug.DrawRay(transform.position, Vector2.up * 2);

        if (leftBas.collider == null && leftHaut.collider != null || rightBas.collider == null && rightHaut.collider != null)
        {
            if (Gestionnaire.grounded == true)
            {
                Gestionnaire.Crouch = true;
            }                   
        }
        else
        {
            if (centre.collider == null)
            {
                Gestionnaire.Crouch = false;
            }          
        }
    }  

    public void Move(float move)
    {
        if (isJumping > 0 && Gestionnaire.SuitActivated == true || Gestionnaire.KnockbackCD == true)
        {
            body.velocity = new Vector2((move * Speed * 0.06f) + (body.velocity.x), body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(move * Speed, body.velocity.y);            
        }       
    }
}
