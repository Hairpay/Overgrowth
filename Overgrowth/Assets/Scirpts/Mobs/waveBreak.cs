using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBreak : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void waveBroke()
    {
        if (gameObject.GetComponent<MecheMob>() != null)
        {
            gameObject.GetComponent<MecheMob>().life = 0;
        }
        else
        {
            Destroy(gameObject);
        }
     
    }
}
