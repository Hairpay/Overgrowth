using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {

    public GameObject charaDual;
    public Text analysisText;
    public Image analysisPanel;

    public string description;
    public int compteur;

	// Use this for initialization
	void Start () {

        charaDual = GameObject.Find("character");
        analysisPanel = charaDual.GetComponent<UIGereur>().analysisPanel;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;

        analysisText.enabled = false;
        analysisPanel.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
       

	}

    public void addnalyse()
    {
        if (compteur < 60)
        {
            compteur = compteur + 1;
        }

        if (compteur > 1 && compteur < 50)
        {
            analysisText.text = "analyzing..." + (compteur * 2).ToString() + " %";
        }
        else if (compteur > 50)
        {
            analysisText.text = description;
        }

        analysisText.enabled = true;
        analysisPanel.enabled = true;
       
    } 
    public void hidenalyse()
    {
        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }  
}
