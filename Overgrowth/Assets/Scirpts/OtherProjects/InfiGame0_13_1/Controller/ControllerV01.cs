using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControllerV01 : MonoBehaviour
{

    public Animator animator;
    public GameObject CameraPJ;
    public Rigidbody body;
    public CapsuleCollider box;
    public GameObject Rotor;

    public GameObject cognebox;
    public bool cogneHaut = false;

    // public Vector3 Dif;

    public bool une = true;

    public float RotY = 0.0f;
    public float RotX = 0.0f;

    public float h = 0.0f;
    public float v = 0.0f;

    public float Multiply = 0.04f;

    public bool Crouch = false;
    public bool Cast = false;

    public bool death;

    public float sprint = 1f;

    public AudioSource Footsteps;

    public float angleBetween = 0.0F;
    public Transform player;



    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        body = gameObject.GetComponent<Rigidbody>();

        Rotor = GameObject.Find("MangoRotation");
        cognebox = GameObject.Find("CogneBox");

        CameraPJ = GameObject.Find("Main Camera");
        box = gameObject.GetComponent<CapsuleCollider>();

        Footsteps = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //sprint gestion
        if (Input.GetButtonDown("Fire2"))
        {
            Footsteps.Play();
            sprint = 1.5f;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            Footsteps.Stop();
            sprint = 1f;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (death == false)
        {
            // Rotation définie ici.
            RotY = Input.GetAxis("Mouse X");
            RotX = Input.GetAxis("Mouse Y");     
            //crouch Checking.
            if (cogneHaut == false)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    Crouch = !Crouch;
                }
                //Cast Checking
                if (Input.GetButtonDown("Fire1"))
                {

                    Cast = true;
                    animator.SetBool("Cast", Cast);
                    Crouch = false;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                Cast = false;
                animator.SetBool("Cast", Cast);
            }

            if (Crouch == true)
            {
                Multiply = 0.02f;
                animator.SetBool("Crouch", Crouch);
                box.center = new Vector3(0f, 0.6f, 0f);
                box.height = 1.2f;
            }


            if (Crouch == false)
            {

                Multiply = 0.04f;
                animator.SetBool("Crouch", Crouch);
                box.center = new Vector3(0f, 1f, 0f);
                box.height = 2f;
            }

            //Translocation ici.
            Vector3 targetDir = player.position - transform.position;
            angleBetween = Vector3.Angle(transform.forward, targetDir);

            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical") * sprint;

            gameObject.transform.Rotate(0f, h * 6, 0f);
            gameObject.transform.Translate(0f, 0f, v * Multiply);

            
            

            animator.SetFloat("Vitesse", v);



            //Gestion de camera lors des débuts de marche et arrets.
            Rotor.transform.Rotate(0f, RotY, 0f);       
        }
        else
        {
            animator.SetBool("Death", true);
        }
    }
}
