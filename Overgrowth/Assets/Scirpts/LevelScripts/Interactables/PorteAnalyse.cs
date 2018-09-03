using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PorteAnalyse : MonoBehaviour
{
    public GameObject character;
    public Gestionnaire Gestionnaire;
   
    public Color baseColor;
    public Color otherColor;
    public float dist;

    public bool Unlock;
    public bool open;

    public int AnalysisLevel;
    public string ErrorMessage;
    public string OpeningMessage;

    public bool power = true;

    public Text analysisText;

    // Use this for initialization
    void Awake()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.a = baseColor.a * 0.3f;
    }

    void Start()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        analysisText = character.GetComponent<UIGereur>().analysis;
    }

    // Update is called once per frame
    void Update()
    {        
            dist = Vector3.Distance(character.transform.position, transform.position);

            if (open == false && Unlock == true)
            {
                Ouverture();
                StopAllCoroutines();
                StartCoroutine("ReturnUnlock");
            }
            else if (open == true && Unlock == false && dist > 3f)
            {
                fermeture();
            }
        
        
    }

    public void Unlockage()
    {
        if (power == false)
        {
            analysisText.text = ErrorMessage + "This door has no power.";
        }
        else if (AnalysisLevel > Gestionnaire.PowerUps[0])
        {
            analysisText.text = ErrorMessage + "This door require a level " + AnalysisLevel +" security.";
        }
        else if (Gestionnaire.SuitActivated == false)
        {
            analysisText.text = ErrorMessage + "Unknow biological signature.";
        }
        else
        {
            analysisText.text = OpeningMessage;
            Unlock = true;                
        }
    }

    public void Ouverture()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().color = otherColor;
        open = true;
    }

    public void fermeture()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        open = false;
    }

    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(5f);
        Unlock = false;
    }
}
