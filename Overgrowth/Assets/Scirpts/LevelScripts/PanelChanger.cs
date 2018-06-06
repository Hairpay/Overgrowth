using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelChanger : MonoBehaviour {

    public GameObject Character;
    public Gestionnaire Gestionnaire;
  
    // Use this for initialization
    void Start () {
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
  
    public void loadPanel()
    {
      //  SceneManager.LoadScene(Gestionnaire.toLoad);
    }


}
