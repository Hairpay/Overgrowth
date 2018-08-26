using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGereur : MonoBehaviour {

  //  public GameObject UI;
    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public Text lifes;
    public int lastLife;
    public float iFrames = 1f;

    public Text analysis;
    public Image analysisPanel;

    // Use this for initialization
    void Awake()
    {
        //UI.SetActive(true);
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;       

        gameObject.transform.position = Gestionnaire.Checkpoint;

        
    }

    void Start () {


     

        if (Gestionnaire.life < 1)
        {
            Gestionnaire.life = 5;
        }

        lastLife = Gestionnaire.life;
        Gestionnaire.invicible = false;

      
       // Gestionnaire.toLoad = SceneManager.GetActiveScene().ToString;
    }
	
	// Update is called once per frame
	void Update () {

        if(lastLife != Gestionnaire.life && Gestionnaire.invicible == false)
        {
            lastLife = Gestionnaire.life;
            Gestionnaire.invicible = true;
            StartCoroutine("ReturnVariables");
    
        }

        lifes.text = "Energy: " + Gestionnaire.life.ToString();

        if (Gestionnaire.life < 1)
        {
          
          
            SceneManager.LoadScene("Level0_1");
        }
		
	}

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(iFrames);
        Gestionnaire.invicible = false;     
    }
}
