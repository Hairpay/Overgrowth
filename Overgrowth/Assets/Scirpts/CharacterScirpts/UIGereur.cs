using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGereur : MonoBehaviour {

    public GameObject UI;
    public GameObject Character;
    public Gestionnaire Gestionnaire;

    public Text lifes;
    public int lastLife;
    public float iFrames = 1f;

    public Color baseColor;
    public Color otherColor;

    public Text analysis;
    public Image analysisPanel;

    // Use this for initialization
    void Awake()
    {
        UI.SetActive(true);
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        if (Gestionnaire.resetPU == true)
        {
            Gestionnaire.bigCheckpoint = gameObject.transform.position;
            Gestionnaire.currentSalle = 0;
            Gestionnaire.resetPU = false;
        }

        gameObject.transform.position = Gestionnaire.bigCheckpoint;

        
    }

    void Start () {


     

        if (Gestionnaire.life < 1)
        {
            Gestionnaire.life = 5;
        }

        lastLife = Gestionnaire.life;
        Gestionnaire.invicible = false;

        baseColor = Character.GetComponent<SpriteRenderer>().color;
        otherColor = baseColor;
        otherColor.r = 0;
       
       // Gestionnaire.toLoad = SceneManager.GetActiveScene().ToString;
    }
	
	// Update is called once per frame
	void Update () {

        if(lastLife != Gestionnaire.life && Gestionnaire.invicible == false)
        {
            lastLife = Gestionnaire.life;
            Gestionnaire.invicible = true;
            StartCoroutine("ReturnVariables");
            Character.GetComponent<SpriteRenderer>().color = otherColor;
        }

        lifes.text = "Energy: " + Gestionnaire.life.ToString();

        if (Gestionnaire.life < 1)
        {
            SceneManager.LoadScene("1_2");
        }
		
	}

    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(iFrames);
        Gestionnaire.invicible = false;
        Character.GetComponent<SpriteRenderer>().color = baseColor;
    }
}
