using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{

    public GameObject character;
    public Text ttsText;
    public Image ttsPanel;
    public GameObject ttsPanelObject;
    public bool once;

    public float activeDist;
    public float dist;

    public string[] dialogues;
    public int i;

    public int textTime = 2;

    // Use this for initialization
    void Start()
    {

        character = GameObject.Find("character");
        ttsPanelObject = GameObject.Find("TTSPanel");
        ttsPanel = ttsPanelObject.GetComponent<Image>();
        ttsText = GameObject.Find("TTSText").GetComponent<Text>();
        ttsPanelObject.SetActive(false);
        ttsPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(character.transform.position, transform.position);

        if (Mathf.Abs(dist) < activeDist && once == false)
        {
            once = true;
            Speech();                
        }
    }
    public void Speech()
    {
        Debug.Log("text update");
        ttsPanelObject.SetActive(true);
        ttsText.enabled = true;

        ttsText.text = dialogues[i];
        if (i < dialogues.Length - 1)
        {
            StartCoroutine("NexText"); 
        }
        else
        {
            StartCoroutine("Stahp");
        }
     
    }
    IEnumerator NexText()
    {
        yield return new WaitForSeconds(textTime);
        i++;
        Speech();
    }
    IEnumerator Stahp()
    {
        yield return new WaitForSeconds(textTime);
        ttsPanelObject.SetActive(false);
        ttsText.enabled = false;
    }
}
