using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {

    public Button[] buttons;
    public GameObject[] Cheatpoints;
    public int i;
	
	// Update is called once per frame
	void Update ()
    {
        for ( i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(Cheatpoints[i].GetComponent<Cheatpoint>().ActivatePoint);
        }
        
	}
}
