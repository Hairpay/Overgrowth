using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ascenseur : MonoBehaviour {

    public GameObject character;
    public Gestionnaire Gestionnaire;

    public bool active;
    public int layer_mask;

    public GameObject[] Arrets;
    public int position;
    public int basePosition;

    public bool isMoving;
    public float t;

    public Vector3 oldPos;
    public float v;
    public bool noHitbox;
    public bool power = true;

    public Text analysisText;
    public Image analysisPanel;

    public Color baseColor;
    public Color activeColor;

    // Use this for initialization
    void Start ()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        analysisPanel = character.GetComponent<UIGereur>().analysisPanel;
        analysisText = character.GetComponent<UIGereur>().analysis;

        layer_mask = LayerMask.GetMask("Player");

        oldPos = gameObject.transform.position;
        position = basePosition;

        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        activeColor = baseColor;
        activeColor.g = baseColor.g * 0.03f;

    }
	
    public void Error()
    {
        analysisText.text = "Error, this elevator has no power.";
        analysisText.enabled = true;
        analysisPanel.enabled = true;
        StopAllCoroutines();
        StartCoroutine("ReturnUnlock");
    }
    public void Called()
    {
        analysisText.text = "Elevator called.";
        analysisText.enabled = true;
        analysisPanel.enabled = true;
        StopAllCoroutines();
        StartCoroutine("ReturnUnlock");
    }

	// Update is called once per frame
	void Update ()
    {
        v = Input.GetAxis("Vertical");

        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, 3f * transform.localScale.y, layer_mask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up)* 3f * transform.localScale.y);
        if(upHit.collider != null)
        {
            Debug.Log(upHit.collider.name);
            active = true;
            gameObject.GetComponent<SpriteRenderer>().color = activeColor;
        }
        else
        {
            active = false;
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        }

        if (noHitbox == true)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (isMoving == false && active == true && v > 0.1f && power == true)
        {
            if (position > 0)
            {
                isMoving = true;
                position = position - 1;
                character.GetComponent<Rigidbody2D>().simulated = false;
                character.transform.parent = gameObject.transform;
                Gestionnaire.Locked = true;
            }         
        }

        if (isMoving == false && active == true && v < -0.1f && power == true)
        {
            if(position < Arrets.Length - 1)
            {
                isMoving = true;
                position = position + 1;
                character.GetComponent<Rigidbody2D>().simulated = false;
                character.transform.parent = gameObject.transform;
                Gestionnaire.Locked = true;
            }                 
        }

        if(isMoving == true)
        {
            if (power == true)
            {               
               gameObject.transform.position = new Vector3(
               (Mathf.Lerp(oldPos.x, Arrets[position].transform.position.x, t)),
               (Mathf.Lerp(oldPos.y, Arrets[position].transform.position.y, t)),
               gameObject.transform.position.z);

                if (t < 1)
                {
                    t += 0.4f * Time.deltaTime;
                }
                else
                {
                    isMoving = false;
                    t = 0;
                    oldPos = gameObject.transform.position;
                    noHitbox = false;
                    character.GetComponent<Rigidbody2D>().simulated = true;
                    character.transform.parent = null;
                    Gestionnaire.Locked = false;
                }
            }
            else
            {
                Error();
                isMoving = false;
                character.GetComponent<Rigidbody2D>().simulated = true;
                character.transform.parent = null;
                Gestionnaire.Locked = false;
                noHitbox = false;
            }
           
        }
    }
    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(2f);
        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }   
}
