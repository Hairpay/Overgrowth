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

    public int speed = 1;

	// Use this for initialization
	void Start () {

        charaDual = GameObject.Find("character");
        analysisPanel = charaDual.GetComponent<UIGereur>().analysisPanel;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;

        analysisText.enabled = false;
        analysisPanel.enabled = false;

        if (gameObject.GetComponent<PorteAnalyse>() != null || gameObject.GetComponent<CallElevator>() != null)
        {
            speed = 3;
        }
    
    }
	
	// Update is called once per frame
	void Update () {
		
       

	}

    public void addnalyse()
    {
        if (compteur < 60)
        {             
            {
                compteur = compteur + speed;
            }
        }

        if (compteur > 1 && compteur < 50)
        {
            analysisText.text = "analyzing..." + (compteur * 2).ToString() + " %";
        }
        else if (compteur > 50)
        {
            
            if (gameObject.GetComponent<PorteAnalyse>() != null)
            {
                gameObject.GetComponent<PorteAnalyse>().Unlockage();
            }
            else if(gameObject.GetComponent<CallElevator>() != null)
            {
                gameObject.GetComponent<CallElevator>().Call();
                analysisText.text = description;
            }
            else
            {
                analysisText.text = description;
                //  compteur = 0;                          
            }

            StopAllCoroutines();
            StartCoroutine("ReturnUnlock");
        }

        analysisText.enabled = true;
        analysisPanel.enabled = true;
       
    }

    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(2f);
        compteur = 0;
    }

    public void hidenalyse()
    {      
        analysisText.enabled = false;
        analysisPanel.enabled = false;  
        compteur = 0;        
    }  
}
