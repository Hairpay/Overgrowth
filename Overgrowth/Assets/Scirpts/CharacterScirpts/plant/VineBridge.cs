using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBridge : MonoBehaviour {

    public Gestionnaire Gestionnaire;
    private int layer_mask;

    public Vector3 Impact1;
    public Vector3 Impact2;
    private Vector3 between;

    public int conteur;
    public float maxDistance = 25f;

    public GameObject bloc;

    private GameObject point1;   
    private GameObject bridge;

    // Use this for initialization
    void Start () {
        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        layer_mask = ~LayerMask.GetMask("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //if (Gestionnaire.VineBridge == true)
        {
            if (Input.GetButtonDown("Fire3") && Gestionnaire.SuitActivated == false) 
            {
                RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position, maxDistance, layer_mask);
                Debug.DrawLine(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position, Color.green,5f);
                if (conteur < 1)
                {
                    DestroyImmediate(bridge);
                    gameObject.GetComponent<Jump>().isGliding = false;
                }

                    if (hit.collider != null)
                {
                    if (conteur < 1)
                    {
                        Impact1 = hit.point;
                        point1 = Instantiate(bloc, Impact1, Quaternion.identity);
                        point1.GetComponent<BoxCollider2D>().enabled = false;
                        conteur++;                      
                    }
                    else
                    {
                        DestroyImmediate(point1);
                        Impact2 = hit.point;                       
                        between = (Impact2 - Impact1);
                        float distance = between.magnitude;
                        bridge = Instantiate(bloc, Impact1 + (between * 0.5f), Quaternion.identity);                        
                        bridge.transform.localScale = new Vector3(distance,1,1);
                        bridge.transform.LookAt(Impact2);
                        bridge.transform.localRotation = new Quaternion(bridge.transform.localRotation.x, bridge.transform.localRotation.y, 0f,0f);
                        conteur = 0;
                    }                    
                }               
            }            
        }
    }   
}
