using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetecterNPC : MonoBehaviour
{

    public GameObject charaDual;
    public Text dialogueText;
    public Image dialoguePanel;

    public string dialogue;
    public GameObject[] toActivate;
    public bool once;
    public int layer_mask;
    public bool Gauche;

    // Use this for initialization
    void Start()
    {
        charaDual = GameObject.Find("character");

        dialogueText.enabled = false;
        dialoguePanel.enabled = false;

        layer_mask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Gauche == true)
        {
            RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.left, 5f * transform.localScale.y, layer_mask);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 5f * transform.localScale.y);

            if (Hit.collider != null)
            {
                if (once == false)
                {
                    once = true;
                    Debug.Log(Hit.collider.name);
                    speak();
                }              
            }
        }
        else
        {
            RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.right, 5f * transform.localScale.y, layer_mask);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 5f * transform.localScale.y);
            if (Hit.collider != null)
            {
                if (once == false)
                {
                    once = true;
                    Debug.Log(Hit.collider.name);
                    speak();
                }
            }
        }
 
    }
    public void activate()
    {       
            for (int i = 0; i < toActivate.Length; i++)
            {
                if (toActivate[i].gameObject.GetComponentInChildren<PorteTrigger>() != null)
                {
                    toActivate[i].gameObject.GetComponentInChildren<PorteTrigger>().Ouverture();
                }
            }      

    }

    public void speak()
    {

        dialogueText.text = dialogue;
        StopAllCoroutines();
        StartCoroutine("ReturnUnlock");

        dialogueText.enabled = true;
        dialoguePanel.enabled = true;
        activate();
    }

    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(2f);
        dialogueText.enabled = false;
        dialoguePanel.enabled = false;
        once = false;
    }


}
