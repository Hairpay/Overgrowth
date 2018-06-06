using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous : MonoBehaviour {

    public GameObject Character;
    public Rigidbody2D charBody;

    public Gestionnaire Gestionnaire;
    public Vector2 touche;

    public bool cooldown;

    // Use this for initialization
    void Start () {
        Character = GameObject.Find("character");
        charBody = Character.GetComponent<Rigidbody2D>();
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }
    public void aie()
    {
        if (Gestionnaire.invicible == false)
        {
            Gestionnaire.life = Gestionnaire.life - 1;
        }     
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && cooldown == false)
        {
            cooldown = true;
            StartCoroutine("ReturnVariables");
            Character.transform.position = new Vector3(Gestionnaire.Checkpoint.x, Gestionnaire.Checkpoint.y, 0f);
          
        }
    }
    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
