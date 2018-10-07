using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionText : MonoBehaviour
{

    public GameObject charaDual;
    public Text analysisText;
    public Image analysisPanel;

    public string description;

    // Use this for initialization
    void Start()
    {

        charaDual = GameObject.Find("character");
        analysisPanel = charaDual.GetComponent<UIGereur>().analysisPanel;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;            
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Description>().compteur > 50)
        {
            analysisText.text = description;
        }       
    }
}
