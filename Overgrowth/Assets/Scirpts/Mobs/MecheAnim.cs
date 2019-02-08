using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class MecheAnim : MonoBehaviour
{
    public float life;
    public float maxLife = 1;
    public bool ifram;

    public Gestionnaire gestionnaire;

    public Animator animator;
    public float w82destroy = 1f;

  
    // Use this for initialization
    void Start()
    {
        gestionnaire = GameObject.Find("character").GetComponent<PowerUps>().Gestionnaire;

        life = maxLife;
        animator = gameObject.GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (gestionnaire.life < 1)
        {
            life = maxLife;
        }

        if (life < 1)
        {
            ded();
        }


    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Orb" && ifram == false)
        {
            life = life - coll.gameObject.GetComponent<DOIgo>().dammage;            
        }
    }
    public void ded()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.Play("Dead");
        StartCoroutine("selfDestroy");

    }
    IEnumerator selfDestroy()
    {     
        yield return new WaitForSeconds(w82destroy);     
        Destroy(gameObject);
    }
}
