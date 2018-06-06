using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MongerIA : MonoBehaviour {

    public RaycastHit hit;
    public Animator[] animator;

   
    public GameObject Detecteur;
    public GameObject player;

    public bool cone = false;

    public float distance;

    public Transform myTransForm;
    public Transform Player;
    public float rotationSpeedOfEnnemi = 5;

    public int counter;
    public int time = 300;

    

    NavMeshAgent Nav;
    public Transform[] targets;
    private int i = 0;
    public float X;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Mangora");
        animator = gameObject.GetComponentsInChildren<Animator>();
        Player = player.transform;
        myTransForm = gameObject.transform;

        Nav = GetComponent<NavMeshAgent>();
        Nav.destination = targets[i].transform.position;

        animator[0].SetBool("Marche", true);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = Detecteur.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Detecteur.transform.position, forward, Color.green);

        if (cone == false)
        {

           

            float dist = Vector3.Distance(targets[i].transform.position, transform.position);

            if (dist < X)
            {
                i++;
                if (i < targets.Length)
                {

                    Nav.destination = targets[i].transform.position;
                }
            }

            if (i == targets.Length)
            {
                i = 0;
                Nav.destination = targets[i].transform.position;
            }

            counter = time;
            animator[1].SetBool("On", true);
            
            

            if (Physics.Raycast(Detecteur.transform.position, forward, out hit, 8) && hit.transform.tag == "Player")
            {
                cone = true;
                animator[1].SetBool("On", false);
            }
            
        }



        if (cone == true)
        {
            animator[0].SetBool("Marche", false);
            Nav.destination = gameObject.transform.position;
            
            myTransForm.rotation = Quaternion.Slerp(myTransForm.rotation, Quaternion.LookRotation(Player.position - myTransForm.position), rotationSpeedOfEnnemi * Time.deltaTime);
            counter = counter - 1;
        }


        if (Physics.Raycast(Detecteur.transform.position, forward, out hit, 8) && hit.transform.tag == "Player")
        {
            //print("J'ai vu un truc la bas !");
            animator[0].SetBool("Hit", true);
          
            counter = time;
        }
        else
        {
            animator[0].SetBool("Hit", false);
        }

        if (counter < 0)
        {
            cone = false;
            StartCoroutine("retour");
        }
    }
  
    IEnumerator retour()
    {

        yield return new WaitForSeconds(1f);
     
        animator[0].SetBool("Marche", true);
        Nav.destination = targets[i].transform.position;
    }
}
