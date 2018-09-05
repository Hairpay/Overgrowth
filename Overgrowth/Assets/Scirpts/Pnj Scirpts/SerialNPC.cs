using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialNPC : MonoBehaviour
{

    public GameObject charaDual;
    public Text dialogueText;
    public Image dialoguePanel;

    public string dialogue;
    public GameObject[] toActivate;
    public bool once;

    // Use this for initialization
    void Start()
    {
        charaDual = GameObject.Find("character");

        dialogueText.enabled = false;
        dialoguePanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void activate()
    {      
        if (once == false)
        {
            once = true;

            for (int i = 0; i < toActivate.Length; i++)
            {
                if (toActivate[i].gameObject.GetComponentInChildren<PorteTrigger>() != null)
                {
                    toActivate[i].gameObject.GetComponentInChildren<PorteTrigger>().Ouverture();
                }
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
