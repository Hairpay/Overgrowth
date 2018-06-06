using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTrashMob : MonoBehaviour {

   // public bool detec = false;
  //  public bool prox = false;
    public GameObject player;
    public float distance;
    public Animator animator;
    public NavMeshAgent Nav;
   
    
    

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Mangora");
        animator = GetComponentInChildren<Animator>();
        Nav = gameObject.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

         distance = Vector3.Distance(player.transform.position, gameObject.transform.position);


        if ( distance < 8)
        {
            if (distance > 1.3)
            {
                Nav.SetDestination(player.transform.position);
            //    gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3.5f;
                animator.SetBool("Walking", true);
                animator.SetBool("Attack", false);
            }
            if (distance < 1 )
            {
             //   gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
               Nav.SetDestination(gameObject.transform.position);
                animator.SetBool("Attack", true);
            }
        }

       
    }
}
