using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnalysisBeam : MonoBehaviour
{
    public States myState;

    public float dist;
    public float maxDist = 15;
    public LineRenderer line;
    public Material lineMaterial;

    public GameObject analysisTarget;
    public Vector3 targetOffset;

    public Text analysisText;
    public Image analysisPanel;

    public GameObject character;
    public Gestionnaire gestionnaire;

    public RaycastHit2D hit;
    public float tim2w8t;
    public bool lockMode;


    // Use this for initialization
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.3f;
        line.endWidth = 0.7f;
       
        character = GameObject.Find("character");
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }
    public enum States
    {
        wait,
        search,
        follow
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire4"))
        {
            if (gestionnaire.Locked == false && gestionnaire.disfunction == false && lockMode == false)
            {
                line.enabled = true;
                myState = States.search;
            }              
        }

        if (Input.GetButtonUp("Fire4"))
        {         
            line.enabled = false;
            myState = States.wait;
            StartCoroutine("ReturnVariables");
        }
        switch (myState)
        {
            case States.search:
                Search();  
                break;
            case States.follow:
                Follow();
                break;        
        }
    }
    public void Search()
    {
        if (gestionnaire.Locked == false && gestionnaire.disfunction == false)
        {
            Vector3 ray = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            ray = new Vector3(ray.x, ray.y, 0f);         
            ray -= transform.position;

            float scaling = ray.magnitude / maxDist;
            if (scaling > 1)
            {
                ray /= scaling;
            }

            ray += transform.position;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, ray);

            hit = Physics2D.Raycast(ray, ray, 0.1f);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<SerialNPC>() != null )
            {
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<SerialNPC>().speak();
            }
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Description>() != null)
            {
                Debug.Log(hit.collider.gameObject);
                analysisTarget = hit.collider.gameObject;            
                targetOffset = analysisTarget.transform.position - ray;

                myState = States.follow;
            }
        }         
    }
    public void Follow()
    {
        if (analysisTarget != null && gestionnaire.Locked == false && gestionnaire.disfunction == false)
        {
            dist = Vector3.Distance(analysisTarget.transform.position - targetOffset, transform.position);
            if (Mathf.Abs(dist) > maxDist * 1.5)
            {
                myState = States.wait;
                line.enabled = false;
            }
            else
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, analysisTarget.transform.position - targetOffset);

                analysisTarget.GetComponent<Description>().addnalyse();
            }
        }
        else
        {
            myState = States.wait;
            line.enabled = false;
        }       
    }

    public void ReturnWait(float time)
    {
        tim2w8t = time;
        line.enabled = false;
        myState = States.wait;
        lockMode = true;
        StopAllCoroutines();
        StartCoroutine("ReturnVariables");
    }

    IEnumerator ReturnVariables()
    {
        yield return new WaitForSeconds(tim2w8t + 1f);
        tim2w8t = 0f;
        lockMode = false;
        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }
}
