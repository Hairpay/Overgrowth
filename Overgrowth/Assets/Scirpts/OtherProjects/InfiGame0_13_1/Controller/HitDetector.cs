using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HitDetector : MonoBehaviour
{
    public int Startvie = 3;
    public int vie;
    public bool cooldown = false;

    public Animator dieded;

    public Text Vie1;
    public Text Vie2;

    public int ball;
    public Text Ball1;
    public Text Ball2;
    public Animator HeadSprite;

    public GameObject spawnPoint;
    public bool une = true;


    void Start()
    {

        //  Vie1 = GameObject.Find("VieT1");
        spawnPoint = GameObject.Find("SpawnPoint");
        reset();
    }

    public void reset()
    {     
        gameObject.transform.position = spawnPoint.transform.position;
        gameObject.transform.rotation = spawnPoint.transform.rotation;
        vie = Startvie;
        dieded.SetBool("dieded", false);
        gameObject.GetComponent<ControllerV0>().death = false;
        gameObject.GetComponent<ControllerV0>().Crouch = false;
        gameObject.GetComponent<ControllerV0>().sprint = 1;
    }

    void Update ()
    {
       ball = gameObject.GetComponentInChildren<Lazerotron>().ball;

        if (vie < 1)
        {
            if(une == true)
            {
                StartCoroutine("Fin");
                dieded.SetBool("dieded", true);
                gameObject.GetComponent<ControllerV0>().death = true;
                une = false;
            }
        }
       
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Danger")
        {
            if (cooldown == false)
            {
                print("aie");
                vie = vie - 1;
                cooldown = true;
                StartCoroutine("ReturnVariables");
                HeadSprite.SetBool("go", true);
            }
        }
        if (other.tag == "SuperDanger")
        {              
                vie = 0;                        
        }
    }
    void OnGUI()
    {

        // GUI.Label(new Rect(10, 30, 100, 20), "vie : " + vie.ToString());
        Vie1.text = ("x " + vie.ToString());
        Vie2.text = ("x " + vie.ToString());
        Ball1.text = ("x " + ball.ToString());
        Ball2.text = ("x " + ball.ToString());
    }
    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(1.0f);
        HeadSprite.SetBool("go", false);
        cooldown = false;
    }

    IEnumerator Fin()
    {

        yield return new WaitForSeconds(4.0f);
        //  SceneManager.LoadScene("6-L2");
        reset();
        une = true;

    }
}
