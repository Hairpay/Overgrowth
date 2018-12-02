using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {

    public GameObject charaDual;
    public Gestionnaire Gestionnaire;
    public Text analysisText;
    public Image analysisPanel;

    public float compteur;
    public float speed = 1;

    public int affichage;
    public bool lastMode;

    public bool lockMode;

	// Use this for initialization
	void Start () {

        charaDual = GameObject.Find("character");
        Gestionnaire = charaDual.GetComponent<PowerUps>().Gestionnaire;
        analysisPanel = charaDual.GetComponent<UIGereur>().analysisPanel;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;

        analysisText.enabled = false;
        analysisPanel.enabled = false;

        if (gameObject.GetComponent<PorteAnalyse>() != null || gameObject.GetComponent<CallElevator>() != null)
        {
            speed = 3;
        }

        if (gameObject.GetComponent<cakeslice.Outline>() == null && gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.AddComponent<cakeslice.Outline>();
        }

        if (gameObject.GetComponent<cakeslice.Outline>() != null)
        {
            gameObject.GetComponent<cakeslice.Outline>().color = 1;
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }        
        lastMode = Gestionnaire.SuitActivated;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (gameObject.GetComponent<cakeslice.Outline>() != null && Input.GetButtonDown("Fire4") )
        {
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }

        if (gameObject.GetComponent<cakeslice.Outline>() != null && Input.GetButtonUp("Fire4"))
        {
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }
    
        if (lastMode != Gestionnaire.SuitActivated)
        {
            compteur = 0;
            lastMode = Gestionnaire.SuitActivated;
        }

    }

    public void addnalyse()
    {
        if (lockMode == false)
        {
            if (compteur < 60)
            {
                {
                    compteur = compteur + speed;
                }
            }

            if (compteur > 1 && compteur < 50)
            {
                affichage = (int)compteur;
                if (Gestionnaire.stopText == false)
                {
                    analysisText.text = "analyzing..." + (affichage * 2).ToString() + " %";
                }
               
            }
            else if (compteur > 50)
            {
                StopAllCoroutines();
                StartCoroutine("ReturnUnlock");
            }

            analysisText.enabled = true;
            analysisPanel.enabled = true;

        }
    }
    public void sayText(string text)
    {
        if (Gestionnaire.stopText == false)
        {
            analysisText.text = text;
            analysisText.enabled = true;
            analysisPanel.enabled = true;
        }
    }

    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(2f);
        compteur = 0;
    }

    public void stopeth()
    {
        compteur = 0;
        lockMode = true;
        StopAllCoroutines();
        StartCoroutine("ReturnLockMode");
    }
    IEnumerator ReturnLockMode()
    {
        yield return new WaitForSeconds(2f);
        lockMode = false;
    }
}
