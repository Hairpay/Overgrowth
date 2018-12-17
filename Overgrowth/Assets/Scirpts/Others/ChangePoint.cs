using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePoint : MonoBehaviour {

    public GameObject[] buttonsObjects;
    public Button[] buttons;
    public GameObject[] cheatpoints;

    void Start()
    {
        for (int i = 0; i < buttonsObjects.Length; i++)
        {
            buttons[i] = buttonsObjects[i].GetComponent<Button>();
            buttonsObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButtonDown("Fire7"))
        {
            for (int i = 0; i < buttonsObjects.Length; i++)
            {
                buttonsObjects[i].SetActive(true);
            }
        }

        if ( Input.GetButtonUp("Fire7"))
        {
            for (int i = 0; i < buttonsObjects.Length; i++)
            {
                buttonsObjects[i].SetActive(false);
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(cheatpoints[i].GetComponent<Cheatpoint>().ActivatePoint);
        }

    }
}
