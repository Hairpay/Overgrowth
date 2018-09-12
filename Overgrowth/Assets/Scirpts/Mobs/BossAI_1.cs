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

    public GameObject Weapon;
    public GameObject flamer;
    public GameObject[] Flames;
    public bool FlameThrowin;
    public float inverFlame;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Flames.Length; i++)
        {
            Flames[i].GetComponent<ParticleSystem>().Stop();
        }
        Character = GameObject.Find("character");
        moBody = gameObject.GetComponent<Rigidbody2D>();
        layer_mask = ~LayerMask.GetMask("Mobs");
        BossAnim = BossVisu.GetComponent<Animator>();

        inverFlame = gameObject.transform.localScale.x * 11;
    }

    // Update is called once per frame
    void Update()
    {
        if (FlameThrowin == true)
        {
       
           
            

            RaycastHit2D target = Physics2D.Raycast(flamer.transform.position, flamer.transform.right, inverFlame, layer_mask);
            Debug.DrawRay(flamer.transform.position, flamer.transform.right * inverFlame, new Color(0, 252, 0));

            if (target.collider != null && target.collider.tag == "Player")
            {
                Weapon.GetComponent<AimAtPlayer>().aie();
            }

        }


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
                 else if (dist > 40)
                {
                    if (Lifted == false)
                    {
                        StartCoroutine("Patern2");
                    }
                    else
                    {
                        randomAction = Random.Range(0, 2);

                        if (randomAction == 0)
                        {
                            StartCoroutine("Patern0");
                        }
                        else if (randomAction == 1)
                        {
                            StartCoroutine("Patern1");
                        }
                    }
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
            BossAnim.Play("Boss_HopnoAtac");         
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < Flames.Length; i++)
            {
                Flames[i].GetComponent<ParticleSystem>().Play();
            }
            yield return new WaitForSeconds(0.5f);
            FlameThrowin = true;
            yield return new WaitForSeconds(1.5f);
            FlameThrowin = false;
            for (int i = 0; i < Flames.Length; i++)
            {
                Flames[i].GetComponent<ParticleSystem>().Stop();
            }
            yield return new WaitForSeconds(1.5f);
            StartCoroutine("resetCD");
        }
        else
        {
            Weapon.GetComponent<AimAtPlayer>().LockPlayer = true;
            yield return new WaitForSeconds(1f);
            Weapon.GetComponent<AimAtPlayer>().LockPlayer = false;
            yield return new WaitForSeconds(0.5f);
            Weapon.GetComponent<AimAtPlayer>().targetDone = true;
            yield return new WaitForSeconds(0.5f);
            Weapon.GetComponent<AimAtPlayer>().targetDone = false;
            Weapon.GetComponent<AimAtPlayer>().DoneShooting = true;
            StartCoroutine("resetCD");
        }
       
    }
   
    IEnumerator Patern1()
    {
        if (Lifted == false)
        {
            yield return new WaitForSeconds(1f);
            BossAnim.Play("Boss_ArmAttac");
            yield return new WaitForSeconds(1f);
            GameObject ArmAtak = Instantiate(ArmAttac);           
            ArmAtak.transform.position = new Vector3(Character.transform.position.x + Random.Range(-5, 5), gameObject.transform.position.y, 0);
            yield return new WaitForSeconds(1.7f);
            GameObject ArmAtak2 = Instantiate(ArmAttac);
            ArmAtak2.transform.position = new Vector3(Character.transform.position.x + Random.Range(-5, 5), gameObject.transform.position.y, 0);
            yield return new WaitForSeconds(1.5f);
            Destroy(ArmAtak);          
            StartCoroutine("resetCD");
            yield return new WaitForSeconds(1.5f);
            Destroy(ArmAtak2);
        }
        else
        {
            flamer.GetComponent<Animator>().Play("ZoneFlame");
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < Flames.Length; i++)
            {
                Flames[i].GetComponent<ParticleSystem>().Play();
            }
            yield return new WaitForSeconds(0.3f);
            FlameThrowin = true;
            yield return new WaitForSeconds(1.7f);
            FlameThrowin = false;
            for (int i = 0; i < Flames.Length; i++)
            {
                Flames[i].GetComponent<ParticleSystem>().Stop();
            }
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
        yield return new WaitForSeconds(1f);
        cooldown = false;

        if (justLifted > 0)
        {
            justLifted = justLifted - 1;
        }
    }
}
