﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caryatid : MonoBehaviour
{

    public GameObject charaDual;
    public Gestionnaire Gestionnaire;

    public Text analysisText;

    public GameObject gift;
    public bool giftOnce;

    public bool cd;

    // Use this for initialization
    void Start()
    {
        charaDual = GameObject.Find("character");
        Gestionnaire = charaDual.GetComponent<PowerUps>().Gestionnaire;
        analysisText = charaDual.GetComponent<UIGereur>().analysis;

        gift.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Description>().compteur < 30 && cd == true)
        {
            cd = false;
        }
        else if (gameObject.GetComponent<Description>().compteur > 50 && cd == false)
        {
            cd = true;

            if (Gestionnaire.SuitActivated == false && gameObject.GetComponent<MecheTilemap>().life > 8)
            {
                analysisText.text = "The Caryatid gives you a present.";
                gift.SetActive(true);
            }
            else if (gameObject.GetComponent<MecheTilemap>().life < 5)
            {
                analysisText.text = "The Caryatid seems badly hurt.";
            }
            else
            {
                analysisText.text = "The Caryatid seems to look at you but doesnt react.";
            }                        
        }

        if(gameObject.GetComponent<MecheTilemap>().life < 8 && giftOnce == false)
        {
            gift.GetComponent<Explotron2D>().Explosion();
            giftOnce = true;
        }
    }
}
