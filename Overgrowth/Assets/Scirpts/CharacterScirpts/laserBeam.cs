using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class laserBeam : MonoBehaviour
{

    public bool firing;   
   // private int layer_mask;


    public LineRenderer line;
    public Material lineMaterial;
    public bool analazing;
    private Vector3 ray;

    public Text analysisText;
    public Image analysisPanel;

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public RaycastHit2D hit;
    public bool blockAnalysis;
    public float tim2w8t;


    // Use this for initialization
    void Start()
    {

        line = gameObject.GetComponent<LineRenderer>();
        //line.SetVertexCount(2);
        line.positionCount = 2;
        //line.SetWidth(0.2f, 0.25f);
        line.startWidth = 0.2f;
        line.endWidth = 0.25f;
      //  layer_mask = ~LayerMask.GetMask("Player");
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        analysisText.enabled = false;
        analysisPanel.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (firing == true && Gestionnaire.Locked == false && Gestionnaire.disfunction == false)
        {

            if (analazing == false)
            {
                ray = (Camera.main.ScreenToWorldPoint(Input.mousePosition));               
                hit = Physics2D.Raycast(ray, ray, 0.1f);            
            }

            else
            {
                hit = Physics2D.Raycast(ray, ray, 0.1f);
            }

          
                line.enabled = true;
                line.SetPosition(0, gameObject.transform.position);
                line.SetPosition(1, ray);
            

            if (hit.collider != null && 
                hit.collider.gameObject.GetComponent<Description>() != null && 
                Mathf.Abs(hit.point.x-gameObject.transform.position.x) < 10 && 
                Mathf.Abs(hit.point.y - gameObject.transform.position.y) < 10

                || hit.collider != null &&
                hit.collider.gameObject.GetComponent<Description>() != null &&
                analazing == true)
            {
                analazing = true;
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<Description>().addnalyse();
            }

            if (hit.collider != null &&
                hit.collider.gameObject.GetComponent<SerialNPC>() != null &&
                Mathf.Abs(hit.point.x - gameObject.transform.position.x) < 10 &&
                Mathf.Abs(hit.point.y - gameObject.transform.position.y) < 10)
            {
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<SerialNPC>().speak();
            }

            }
        else
        {
            line.enabled = false;
        }

        if (Input.GetButtonDown("Fire4"))
        {
            firing = true;
        }

        if (Input.GetButtonUp("Fire4"))
        {
            firing = false;
            analazing = false;
            StartCoroutine("ReturnVariables");
        }
    }
    public void ReturnWait(float time)
    {
        tim2w8t = time;
        StopAllCoroutines();
        StartCoroutine("ReturnVariables");
    }

    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(tim2w8t + 1f);
        tim2w8t = 0f;
        analysisText.enabled = false;
        analysisPanel.enabled = false;        
    }
}
