using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI_1 : MonoBehaviour
{

    public GameObject Character;
    public float dist;
    public Rigidbody2D moBody;
    private Vector2 Jump;

    public bool LockPlayer;
    public bool cooldown;
    private int layer_mask;

    public int randomAction;
    public GameObject BossVisu;
    public Animator BossAnim;
    public bool Lifted;
    public GameObject ArmAttac;
    public int justLifted;

    // Use this for initialization
    void Start()
    {

        Character = GameObject.Find("character");
        moBody = gameObject.GetComponent<Rigidbody2D>();
        layer_mask = ~LayerMask.GetMask("Mobs");
        BossAnim = BossVisu.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(Character.transform.position, transform.position);

        if (dist < 50f)
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Character.transform.position - gameObject.transform.position, dist, layer_mask);
            Debug.DrawLine(gameObject.transform.position, hit.point);

            // Debug.Log(hit.collider.gameObject);
            if (hit.collider != null && hit.collider.tag == "Player" && LockPlayer == false)
            {
                Debug.Log("I'm Awaken");
                LockPlayer = true;
            }

            if (LockPlayer == true && cooldown == false)
            {
                cooldown = true;

                if (dist < 25 && Lifted == false)
                {
                    StartCoroutine("PaternClose");
                    Debug.Log("Close Patern activated !");
                }
                else
                {
                    randomAction = Random.Range(0, 3);
                    Debug.Log("Patern number " + randomAction + " activated !");

                    if (randomAction == 0)
                    {
                        StartCoroutine("Patern0");
                    }
                    else if (randomAction == 1)
                    {
                        StartCoroutine("Patern1");
                    }
                    else if (randomAction == 2)
                    {
                        if(justLifted > 0)
                        {
                            cooldown = false;
                        }
                        else
                        {
                            StartCoroutine("Patern2");                        
                        }
                     
                    }
                }
               
            }
        }
    }
    IEnumerator PaternClose()
    {
        yield return new WaitForSeconds(0.3f);
        BossAnim.Play("Boss_Attac");
        StartCoroutine("resetCD");
    }

    IEnumerator Patern0()
    {
        if( Lifted == false)
        {
            yield return new WaitForSeconds(1f);
            BossAnim.Play("Boss_Hop");
            yield return new WaitForSeconds(7f);
            StartCoroutine("resetCD");
        }
        else
        {
            StartCoroutine("resetCD");
        }
       
    }
   
    IEnumerator Patern1()
    {
        if (Lifted == false)
        {
            yield return new WaitForSeconds(1f);
            BossAnim.Play("Boss_ArmAttac");
            yield return new WaitForSeconds(1.5f);
            GameObject ArmAtak = Instantiate(ArmAttac);           
            ArmAtak.transform.position = new Vector3(Character.transform.position.x, gameObject.transform.position.y, 0);
            yield return new WaitForSeconds(2f);
            Destroy(ArmAtak);
            StartCoroutine("resetCD");
        }
        else
        {
            StartCoroutine("resetCD");
        }
       
    }
    IEnumerator Patern2()
    {
        yield return new WaitForSeconds(0.5f);  
        Lifted = !Lifted;
        BossAnim.SetBool("Lifted", Lifted);
        justLifted = 3;
        StartCoroutine("resetCD");
    }

    IEnumerator resetCD()
    {
        yield return new WaitForSeconds(1.5f);
        moBody.velocity = new Vector2(0f, 0f);
        cooldown = false;
        if (justLifted > 0)
        {
            justLifted = justLifted - 1;
        }
    }
}
