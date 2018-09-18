using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBridge : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;
    private int layer_mask;

    public Vector3 Impact1;
    private Vector3 between;

    public Vector3 AimPoint;
    public bool isHit;

    public int conteur;
    public float maxDistance = 15f;

    public GameObject bloc;

    private GameObject point1;   
    private GameObject bridge;

    public bool isAiming;
    public LineRenderer line;

    // Use this for initialization
    void Start () {

        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        layer_mask = LayerMask.GetMask("Environment");
        line = gameObject.GetComponent<LineRenderer>();
    }
	
    public void aiming()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position, maxDistance, layer_mask);
        Debug.DrawRay(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position, Color.green);
        AimPoint = hit.point;
       
        line.SetPosition(0, gameObject.transform.position);

        if (conteur < 1)
        {
            if (hit.collider != null)
            {
                isHit = true;
                line.SetPosition(1, hit.point);
                point1.transform.position = hit.point;
                point1.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                isHit = false;
                line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                point1.GetComponent<Renderer>().enabled = false;
            }
        }
        if (conteur > 0)
        {
            if (hit.collider != null)
            {
                BridgeMaking();
                line.SetPosition(1, hit.point);
            }  
            else
            {
                bridge.transform.localScale = new Vector3(1, 1, 1);
                bridge.transform.position = Impact1;

                line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }         
        }
    }

    // Create the first and second point for the bridge
    public void BridgePoint()
    {
        if (conteur < 1)
        {
            point1 = Instantiate(bloc, AimPoint, Quaternion.identity);
            point1.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (conteur > 0)
        {
            DestroyImmediate(point1);
            bridge = Instantiate(bloc, Impact1 + (between * 0.5f), Quaternion.identity);
            bridge.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    // Create the bridge by joining the 2 points
    public void BridgeMaking()
    {
        if (conteur > 0)
        {        
            between = (AimPoint - Impact1);
            float distance = between.magnitude;

            bridge.transform.position = Impact1 + (between * 0.5f);
            bridge.transform.localScale = new Vector3(distance, 1, 1);
            bridge.transform.LookAt(AimPoint);
            bridge.transform.localRotation = new Quaternion(bridge.transform.localRotation.x, bridge.transform.localRotation.y, 0f, 0f);    
            
            if (bridge.transform.localScale.x > 15)
            {
                bridge.transform.localScale = new Vector3(1, 1, 1);
                bridge.transform.position = Impact1;
            }      
        }      
    }

    // Update is called once per frame
    void Update () {
        //if (Gestionnaire.VineBridge == true)
        {
            if (isAiming == true)
            {             
                aiming();
            }

            if (Input.GetButtonDown("Fire3") && Gestionnaire.SuitActivated == false && Gestionnaire.PowerUps[5] > 0)
            {
                if (conteur > 1)
                {
                    conteur = 0;
                    DestroyImmediate(bridge);
                }

                BridgePoint();
                isAiming = true;
                line.enabled = true;
           
                //moche ça:
                gameObject.GetComponent<laserBeam>().enabled = false;
            }
                if (Input.GetButtonUp("Fire3") && Gestionnaire.SuitActivated == false && Gestionnaire.PowerUps[5] > 0) 
            {
                // DestroyImmediate(point1);
                Impact1 = AimPoint;

                isAiming = false;
                line.enabled = false;
                conteur++;

                if (conteur > 1)
                {
                    bridge.GetComponent<BoxCollider2D>().enabled = true;
                }

                if (isHit == false)
                {
                    conteur = 0;
                    DestroyImmediate(point1);
                }

                //moche ça:
                gameObject.GetComponent<laserBeam>().enabled = true;

            }            
        }
    }
}
