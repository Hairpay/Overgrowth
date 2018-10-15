using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2AI : MonoBehaviour {

    Actions myState;
    Anims animState;
    public int layer_mask;

    public float distance;
    public float distanceHit;
    public GameObject character;
    public Rigidbody2D charBody;

    public Gestionnaire gestionnaire;
    public Rigidbody2D body;
    public Animator animator;

    public float speed;
    public float side = -1;
    public Vector3 toPlayer;
    public int compteurLoss;
    public bool laserCD;

    public GameObject hitbox;
    public GameObject head;
    public float lastLife;

    public GameObject projectile;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        charBody = character.GetComponent<Rigidbody2D>();
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        body = gameObject.GetComponent<Rigidbody2D>();

        layer_mask = LayerMask.GetMask("Player", "Environment", "Door");
        myState = Actions.patrol;
        lastLife = hitbox.GetComponent<MecheMob>().life;
    }
	public enum Actions
    {
        patrol,
        follow,
        attac,
        shoot
    }
    public enum Anims
    {
        walkAnim,
        attacAnim,
        idleAnim,
        shootAnim
    }
    // Update is called once per frame
    void Update ()
    {      
        distance = Vector3.Distance(character.transform.position, transform.position);      

        switch (myState)
        {
            case Actions.patrol:
                Debug.Log("Patrol ongoing !");
                CheckActions();
                Patrol();
                break;
            case Actions.follow:
                Debug.Log("follow incoming !");
                Follow();
                break;
            case Actions.attac:
                Debug.Log("Attac incoming !");
                Patrol();
                break;
            case Actions.shoot:
                Debug.Log("shoot ongoing !");
                break;
        }
        switch (animState)
        {
            case Anims.idleAnim:
                animator.SetBool("isWalking", false);          
                break;
            case Anims.walkAnim:
                animator.SetBool("isWalking", true);
                break;
            case Anims.attacAnim:
                animator.Play("Atacc");
                animator.SetBool("isWalking", false);
                break;
            case Anims.shootAnim:
                animator.Play("Shoot");
                animator.SetBool("isWalking", false);
                break;
        }

        lastLife = hitbox.GetComponent<MecheMob>().life;
    }
    public void CheckActions()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.right * side, 200f, layer_mask);
        Debug.DrawLine(transform.position, hit.point, new Color(252, 252, 0));       
        distanceHit = Vector3.Distance(hit.point, transform.position);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player" && distanceHit < 50)
            {
                myState = Actions.follow;
            }         
            else if (hit.collider.tag != "Player" && distanceHit < 10)
            {
                TurnBacc();
            }
            else
            {
                myState = Actions.patrol;
            }
        }

        if (lastLife != hitbox.GetComponent<MecheMob>().life)
        {
            myState = Actions.follow;
        }
    }

    public void Patrol()
    {   
        body.velocity = new Vector2(speed * side, 0f);
        animator.SetFloat("walkspeed", 1f);
        animState = Anims.walkAnim;
    }
    public void Follow()
    {
       
        toPlayer = character.transform.position - gameObject.transform.position;
        toPlayer = toPlayer.normalized;

        if (toPlayer.x < -0.1 && side > 0 || toPlayer.x > 0.1 && side < 0)
        {
            TurnBacc();
        }     

        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, character.transform.position - gameObject.transform.position, 200f, layer_mask);
        Debug.DrawLine(transform.position, target.point, new Color(252, 0, 252));

        if (target.collider != null && target.collider.tag == "Player")
        {
            if (distance > 7)
            {
                if (laserCD == false)
                {
                    myState = Actions.shoot;
                    animState = Anims.shootAnim;
                    StartCoroutine("ResetLaser");
                    laserCD = true;
                }
                else
                {
                    body.velocity = new Vector2(speed * side * 2, 0f);
                    animator.SetFloat("walkspeed", 2f);
                    animState = Anims.walkAnim;
                    compteurLoss = 0;
                }                       
            }
            else
            {
                myState = Actions.attac;
                animState = Anims.attacAnim;
                StartCoroutine("Attacc");
                body.velocity = new Vector2(0f, 0f);
            }          
        }
        else
        {
            animState = Anims.idleAnim;
            body.velocity = new Vector2(0f, 0f);
            compteurLoss = compteurLoss + 1;
            if (compteurLoss > 150)
            {
                compteurLoss = 0;
                myState = Actions.patrol;
            }
        }
    } 
    public void TurnBacc()
    {
        Debug.Log("turnbacc ongoing !");
        body.velocity = new Vector2(0f, 0f);
        side = -side;
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y,1f); 
    }
    public void Shoot()
    {
        myState = Actions.follow;  
        GameObject DOI = Instantiate(projectile);

        DOI.transform.position = head.transform.position;
        Vector3 difference = character.transform.position - head.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        DOI.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);                  
    }
    IEnumerator Attacc()
    {
        yield return new WaitForSeconds(0.5f);
        animState = Anims.idleAnim;
        yield return new WaitForSeconds(1.5f);
        myState = Actions.follow;
    }
    IEnumerator ResetLaser()
    {
        yield return new WaitForSeconds(1.3f);
        Shoot();
        yield return new WaitForSeconds(0.3f);
        Shoot();
        yield return new WaitForSeconds(0.3f);
        Shoot();
        animState = Anims.idleAnim;
        yield return new WaitForSeconds(3f);
        laserCD = false;
    }
}
