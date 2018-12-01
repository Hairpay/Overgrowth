using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{

    public GameObject character;
    public Gestionnaire gestionnaire;
    public Text analysisText;
    public Image analysisPanel;
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
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        analysisPanel = character.GetComponent<UIGereur>().analysisPanel;
        analysisText = character.GetComponent<UIGereur>().analysis;

        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(character.transform.position, transform.position);

        if (Mathf.Abs(dist) < activeDist && once == false)
        {
            once = true;
            Speech();
            GameObject.Find("Directiowerfer").GetComponent<AnalysisBeam>().ReturnWait((dialogues.Length*textTime) + 0.1f);           
        }
    }
    public void Speech()
    {
        Debug.Log("text update");
        analysisText.enabled = true;
        analysisPanel.enabled = true;

        analysisText.text = dialogues[i];
        if (i < dialogues.Length - 1)
        {
            StartCoroutine("NexText"); 
        }
     
    }
    IEnumerator NexText()
    {
        yield return new WaitForSeconds(textTime);
        i++;
        Speech();
    }
}
