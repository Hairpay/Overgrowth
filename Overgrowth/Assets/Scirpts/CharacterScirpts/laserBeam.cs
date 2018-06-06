using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class laserBeam : MonoBehaviour
{

    public bool firing;
    public float maxDistance = 150f;
    private int layer_mask;

    public Vector2 mouse;
    public LineRenderer line;
    public Material lineMaterial;
    public bool analazing;
    private Vector3 ray;

    public Text analysisText;
    public Image analysisPanel;

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public RaycastHit2D hit;


    // Use this for initialization
    void Start()
    {

        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetWidth(0.2f, 0.25f);
        layer_mask = ~LayerMask.GetMask("Player");
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

    }

    // Update is called once per frame
    void Update()
    {

        if (firing == true)
        {

            if (analazing == false)
            {
                ray = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
                mouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition));


            }
                       
            if (Gestionnaire.manetteMode == true && analazing == false)
            {                                           
                hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right * 1000, 200f);
                ray = hit.point;
            }
            else
            {
                hit = Physics2D.Raycast(ray, ray, 0.1f);
            }

            line.enabled = true;
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, ray);


            if (hit.collider != null && hit.collider.gameObject.GetComponent<Description>() != null 
                && Mathf.Abs(hit.point.x-gameObject.transform.position.x) < 10 && Mathf.Abs(hit.point.y - gameObject.transform.position.y) < 10
                || hit.collider != null && hit.collider.gameObject.GetComponent<Description>() != null && analazing == true)
            {
                analazing = true;
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<Description>().addnalyse();
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


    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(1f);
        analysisText.enabled = false;
        analysisPanel.enabled = false;        
    }
}
