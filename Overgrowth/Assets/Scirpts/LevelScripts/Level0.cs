using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;

    // Use this for initialization
    void Start () {
              
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        Gestionnaire.GravityAnchor = false;
        Gestionnaire.WallProps = false;
        Gestionnaire.Flamenwerfer = false;
        Gestionnaire.ShockWave = false;
        

        SceneManager.LoadScene("1_1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
