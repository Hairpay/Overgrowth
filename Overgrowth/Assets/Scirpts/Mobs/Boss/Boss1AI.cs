using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AI : MonoBehaviour
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
    public bool isCharging;

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
                randomAction = Random.Range(0 , 3);
                Debug.Log("Patern number " + randomAction + " activated !");

                if (randomAction == 0)
                {                  
                    StartCoroutine("Patern0");
                    BossAnim.Play("Charge");
                    BossAnim.SetBool("isCharging", true);
                }
                else if (randomAction == 1)
                {
                    StartCoroutine("Patern1");
                    BossAnim.Play("Charge");
                    BossAnim.SetBool("isCharging", true);
                }
                else if (randomAction == 2)
                {
                    StartCoroutine("Patern2");
                    BossAnim.Play("Charge");
                    BossAnim.SetBool("isCharging", true);
                }
            }
        }
    }
    IEnumerator Patern0()
    {
        yield return new WaitForSeconds(1f);
        BossAnim.SetBool("isCharging", false);
        Jump = Character.transform.position - gameObject.transform.position;
        Jump = Jump.normalized;
        moBody.AddForce(new Vector2(Jump.x * 50000, Jump.y * 50000 + 70000));
        yield return new WaitForSeconds(0.5f);
        Jump = -Jump;
        moBody.AddForce(new Vector2(Jump.x * 35000, Jump.y * 35000 + 70000));
        StartCoroutine("resetCD");
    }
    IEnumerator Patern1()
    {
        yield return new WaitForSeconds(0.5f);
        BossAnim.SetBool("isCharging", false);
        StartCoroutine("resetCD");
    }
    IEnumerator Patern2()
    {
        yield return new WaitForSeconds(0.5f);
        BossAnim.SetBool("isCharging", false);
        StartCoroutine("resetCD");
    }

    IEnumerator resetCD()
    {
        yield return new WaitForSeconds(1f);
        moBody.velocity = new Vector2(0f, 0f);
        cooldown = false;
    }
}
