using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitMove : MonoBehaviour {

    private Rigidbody2D body;

    public float FiringSpeed = 10f;
    public float MaxSpeedBase = 10f;
    public float MaxSpeedPlant = 13f;
  
    public float Speed;
    public int isJumping;

    public Gestionnaire Gestionnaire;

    // Use this for initialization
    void Start () {

        body = gameObject.GetComponent<Rigidbody2D>();
        Speed = MaxSpeedBase;
        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;

    }
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");       
        isJumping = Gestionnaire.JumpCD;
        Move(h);
        Gestionnaire.Speed = Mathf.Abs(body.velocity.x);

        if (Gestionnaire.isFiring == true)
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
