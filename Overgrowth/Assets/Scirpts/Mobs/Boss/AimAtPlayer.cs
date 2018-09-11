using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{

    public GameObject Character;
    public Rigidbody2D charBody;

    public Gestionnaire Gestionnaire;
    public Vector2 touche;
    public Vector2 impact;

    public bool cooldown;

    public Vector3 hitLaser;
    public float offset = 0.0f;

    public bool LockPlayer;  
    public bool targetDone;
    public bool DoneShooting;

    public int layer_mask;


    public LineRenderer[] line;

    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        charBody = Character.GetComponent<Rigidbody2D>();
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        layer_mask = LayerMask.GetMask("Player", "Environment");

        for (int i = 0; i < line.Length; i++)
        {
            line[i].SetVertexCount(2);
            line[i].startWidth = 0.5f;
            line[i].endWidth = 0.5f;     
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (LockPlayer == true && targetDone == false)
        {                        
            RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, Character.transform.position - gameObject.transform.position, 200f, layer_mask);
            Debug.DrawRay(transform.position, Character.transform.position - gameObject.transform.position, new Color(252,0,0));
            hitLaser = target.point;

            for (int i = 0; i < line.Length; i++)
            {
                line[i].enabled = true;
                line[i].SetPosition(0, line[i].gameObject.transform.position);
                line[i].startWidth = 0.1f;
                line[i].endWidth = 0.1f;
               
                line[i].SetPosition(1, target.point);                         
            }         
        }
        else if (targetDone == true)
        {
            RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, hitLaser - gameObject.transform.position, 200f, layer_mask);
            Debug.DrawRay(transform.position, hitLaser - gameObject.transform.position, new Color(252, 0, 0));   
            
            if (target.collider != null && target.collider.tag == "Player")
            {
                aie();
            }       

            for (int i = 0; i < line.Length; i++)
            {
                line[i].enabled = true;
                line[i].SetPosition(0, line[i].gameObject.transform.position);
                line[i].startWidth = 1f;
                line[i].endWidth = 1f;                           
                line[i].SetPosition(1, target.point);
            }
        }
        else if (DoneShooting == true)
        {
            DoneShooting = false;

            for (int i = 0; i < line.Length; i++)
            {
                line[i].enabled = false;               
            }
        }    
    }
    public void aie()
    {
        cooldown = true;
        StartCoroutine("ReturnVariables");

        if (Gestionnaire.invicible == false)
        {
            Gestionnaire.life = Gestionnaire.life - 1;
            Debug.Log("aie");

            Gestionnaire.KnockbackCD = true;
            StartCoroutine("resetKCD");



            if (impact.x > 0.5f)
            {
                charBody.AddForce(new Vector2(-5000f, 4000f));
            }
            else if (impact.x < -0.5f)
            {
                charBody.AddForce(new Vector2(5000f, 4000f));
            }
            else
            {
                charBody.AddForce(new Vector2(0f, 4000f));
            }
        }
    }
    IEnumerator resetKCD()
    {
        yield return new WaitForSeconds(0.5f);
        Gestionnaire.KnockbackCD = false;
    }

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
