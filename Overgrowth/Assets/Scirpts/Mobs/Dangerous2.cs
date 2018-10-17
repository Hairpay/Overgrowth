using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous2 : MonoBehaviour
{

    public GameObject Character;
    public Rigidbody2D charBody;

    public Gestionnaire Gestionnaire;
    public Vector2 touche;
    public Vector2 impact;

    public bool cooldown;

    public int SuitDammage;
    // 0 = always damaging
    // 1 = dammage in suit mode
    // 2 = dammage in green mode

    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        charBody = Character.GetComponent<Rigidbody2D>();
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        impact = gameObject.transform.position - coll.gameObject.transform.position;
        impact.Normalize();

        if (coll.gameObject.tag == "Player" && cooldown == false)
        {
            if (Gestionnaire.SuitActivated == true && SuitDammage == 1 || Gestionnaire.SuitActivated == false && SuitDammage == 2 || SuitDammage == 0)
            {
                aie();
            }
        }
    }
    public void aie()
    {
        cooldown = true;
        StartCoroutine("ReturnVariables");

        if (Gestionnaire.invicible == false)
        {
            Gestionnaire.life = Gestionnaire.life - 1;
            Debug.Log("aie2");

            if (impact.x > 0.5f)
            {
                charBody.AddForce(new Vector2(-500f, 400f));
            }
            else if (impact.x < -0.5f)
            {
                charBody.AddForce(new Vector2(500f, 400f));
            }
            else
            {
                charBody.AddForce(new Vector2(0f, 400f));
            }
        }
    }
    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
