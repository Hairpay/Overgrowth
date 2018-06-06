using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherAITurret : MonoBehaviour
{

    public RaycastHit hit;
    public Animator[] animators;

    public GameObject Detecteur2;
    public GameObject Detecteur;
    public GameObject player;

    public bool cone = false;

    public float distance;

    public Transform myTransForm;
    public Transform Player;
    public Vector3 Oldpos ;
    public float rotationSpeedOfEnnemi = 5;
    public Vector3 Moveplayer;

    public int counter;
    public int time = 300;

    public Quaternion BaseRotation;
    public bool justeBas = false;

    public bool check = true;
    public bool check2 = true;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Mangora");
        animators = gameObject.GetComponentsInChildren<Animator>();
        Player = player.transform;
        myTransForm = gameObject.transform;
        animators[1].SetBool("Marche", false);
        BaseRotation = gameObject.transform.localRotation;
        Oldpos = new Vector3(0f,0f,0f);

        Moveplayer = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {


        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < 1) cone = true;

        if (distance < 2)
        {
            if (player.GetComponent<ControllerV0>().Crouch == false) cone = true;
        }

        if (distance < 7)
        {
            if (player.GetComponent<ControllerV0>().sprint == 1.5)
            {
                cone = true;
            }
            if (player.GetComponent<ControllerV0>().Cast == true)
            {
                cone = true;
            }
        }
      

        Vector3 forward = Detecteur.transform.TransformDirection(Vector3.forward) * 30;
        Debug.DrawRay(Detecteur.transform.position, forward, Color.green);
        Debug.DrawRay(Detecteur2.transform.position, forward, Color.green);


        if (cone == false)
        {
            counter = time;
            animators[2].SetBool("Rouge", false);
            animators[3].SetBool("On", true);
           
            if (Physics.Raycast(Detecteur2.transform.position, forward, out hit, 20) && hit.transform.tag == "Player")
            {
                cone = true;
                animators[3].SetBool("On", false);
                
            }
            if (Physics.Raycast(Detecteur.transform.position, forward, out hit, 20) && hit.transform.tag == "Player")
            {
                cone = true;
                animators[3].SetBool("On", false);
                
            }
        }

        if (cone == true)
        {
            animators[3].SetBool("On", false);

            if (check == true)
            {
                StartCoroutine("OldPos");
                check = false;
            }

           

            Moveplayer = new Vector3(Player.transform.position.x - Oldpos.x,
                                Player.transform.position.y - Oldpos.y,
                                Player.transform.position.z - Oldpos.z);

            animators[2].SetBool("Rouge", true);
            myTransForm.rotation = Quaternion.Slerp(myTransForm.rotation, Quaternion.LookRotation(Player.position - myTransForm.position), rotationSpeedOfEnnemi * Time.deltaTime);

            counter = counter - 1;
            counter = counter - 1;





            if (Physics.Raycast(Detecteur2.transform.position, forward, out hit, 30) && hit.transform.tag == "Player")
            {
                justeBas = true;
            }

            if (Physics.Raycast(Detecteur.transform.position, forward, out hit, 30) && hit.transform.tag == "Player")
            {
                //print("J'ai vu un truc la bas !");


                if (check2 == true)
                {
                    animators[1].SetBool("Tir", true);
                    StartCoroutine("tir");
                    check2 = false;
                }


                counter = time;

                justeBas = false;
                gameObject.GetComponentInChildren<Flechotron>().Bas = false;
            }
            else
            {
                animators[1].SetBool("Tir", false);
            }

            if (counter < 0)
            {
                cone = false;
                StartCoroutine("retour");
            }

            if (justeBas == true)
            {
                animators[1].SetBool("Tir", true);
                StartCoroutine("tir");
                counter = time;
                gameObject.GetComponentInChildren<Flechotron>().Bas = true;
                justeBas = false;
            }
        }
    }
    IEnumerator tir()
    {

        yield return new WaitForSeconds(0.5f);
        if (distance <= 3)
        {
            myTransForm.rotation = Quaternion.Slerp
            (myTransForm.rotation,
            Quaternion.LookRotation(Player.position + ((Moveplayer) * distance * 4f ) - myTransForm.position),
            rotationSpeedOfEnnemi * Time.deltaTime);
        }
        else if ( distance <= 6)
        {
            myTransForm.rotation = Quaternion.Slerp
                      (myTransForm.rotation,
                      Quaternion.LookRotation(Player.position + ((Moveplayer) * distance * 3f) - myTransForm.position),
                      rotationSpeedOfEnnemi * Time.deltaTime);
        }
        else if (distance <= 10)
        {
            myTransForm.rotation = Quaternion.Slerp
                      (myTransForm.rotation,
                      Quaternion.LookRotation(Player.position + ((Moveplayer) * distance  *2.5f) - myTransForm.position),
                      rotationSpeedOfEnnemi * Time.deltaTime);
        }
        else if (distance <= 13)
        {
            myTransForm.rotation = Quaternion.Slerp
                      (myTransForm.rotation,
                      Quaternion.LookRotation(Player.position + ((Moveplayer) * distance * 1f) - myTransForm.position),
                      rotationSpeedOfEnnemi * Time.deltaTime);
        }
        else if (distance <= 17)
        {
            myTransForm.rotation = Quaternion.Slerp
                      (myTransForm.rotation,
                      Quaternion.LookRotation(Player.position + ((Moveplayer) * distance * 0.7f ) - myTransForm.position),
                      rotationSpeedOfEnnemi * Time.deltaTime);
        }


        gameObject.GetComponentInChildren<Flechotron>().shoot();
        check2 = true;
    }

    IEnumerator retour()
    {

        yield return new WaitForSeconds(3f);
        gameObject.transform.localRotation = BaseRotation;
       
     
    }

    IEnumerator OldPos()
    {

        yield return new WaitForSeconds(0.5f);
        Oldpos = player.transform.position;
        check = true;


    }
}
