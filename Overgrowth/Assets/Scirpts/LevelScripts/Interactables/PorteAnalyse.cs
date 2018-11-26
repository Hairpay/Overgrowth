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
    public int baseLayer;

    public bool unlock;
    public bool ready2unlock;
    public bool autoClose;

    public int AnalysisLevel;
    public string ErrorMessage;
    public string OpeningMessage;

    public bool power = true;
    public bool errorBlocked;

    public Text analysisText;

    // Use this for initialization
    void Awake()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.a = baseColor.a * 0.3f;

        baseLayer = gameObject.layer;
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
        if (autoClose == true && unlock == true)
        {
            dist = (character.transform.position.y - transform.position.y);
            if (Mathf.Abs(dist)< 1)
            {
                ready2unlock = true;
            }
            if (Mathf.Abs(dist) > 3 && ready2unlock == true)
            {
                ready2unlock = false;
                fermeture();
            }         
        }
        // check if door is analyzed
        if (gameObject.GetComponent<Description>().compteur > 50)
        {
            Unlockage();
            gameObject.GetComponent<Description>().stopeth();
        }
    }

    public void Unlockage()
    {
        if (power == false)
        {
            if (errorBlocked == false)
            {
                analysisText.text = ErrorMessage + "This door has no power.";
            }
            else
            {
                analysisText.text = ErrorMessage + "Something is blocking the door.";
            }
        }
        else if (AnalysisLevel > Gestionnaire.PowerUps[0])
        {
            analysisText.text = ErrorMessage + "This door require a level " + AnalysisLevel + " security.";
        }
        else if (Gestionnaire.SuitActivated == false)
        {
            analysisText.text = ErrorMessage + "Unknow biological signature.";
        }
        else
        {
            analysisText.text = OpeningMessage;
            Ouverture();
        }
    }

    public void Ouverture()
    {
        unlock = true;
        gameObject.layer = 24;
        gameObject.GetComponent<SpriteRenderer>().color = otherColor;
    }

    public void fermeture()
    {
        unlock = false;
        gameObject.layer = baseLayer;
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
    }
}
