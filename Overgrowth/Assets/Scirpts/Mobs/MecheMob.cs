using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class MecheMob : MonoBehaviour
{
    public GameObject mobdossier;
    public GameObject[] mobVisu;

    public Color[] actualColor;
    public Color[] baseColor;

    public float life;
    public float maxLife;
    public bool isDed;
    public bool ifram;

    void Awake()
    {
        for (int i = 0; i < mobVisu.Length; i++)
        {
            baseColor[i] = mobVisu[i].GetComponent<SpriteMeshInstance>().color;
            actualColor[i] = mobVisu[i].GetComponent<SpriteMeshInstance>().color;
        }
       
       
    }
    // Use this for initialization
    void Start()
    {
        life = maxLife;
        if (mobdossier == null)
        {
            mobdossier = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1)
        {
           // gameObject.GetComponent<Collider2D>().isTrigger = true;

            if (gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if (gameObject.GetComponent<Dangerous>() != null)
                {
                    gameObject.GetComponent<Dangerous>().enabled = false;
                }
                   
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                ifram = true;
                isDed = true;
                StartCoroutine("iFrames");
            }
            else
            {
                ded();
            }
           
        }

       
    }
    public void colorChange()
    {
        for (int i = 0; i < mobVisu.Length; i++)
        {
         //   Debug.Log(life / maxLife);
            actualColor[i].g = baseColor[i].g * life / maxLife;
            mobVisu[i].GetComponent<SpriteMeshInstance>().color = actualColor[i];
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Orb" && ifram == false)
        {
            life = life - coll.gameObject.GetComponent<DOIgo>().dammage;
            colorChange();

            if(isDed == true)
            {
                ded();
            }
                     
        }
    }
    public void ded()
    {
        if (gameObject.GetComponent<Die2Activate>() != null)
        {
            gameObject.GetComponent<Die2Activate>().activateDed();
        }
        gameObject.GetComponent<Explotron2D>().Explosion();
        Destroy(mobdossier);
    }

    IEnumerator iFrames()
    {
        yield return new WaitForSeconds(1.5f);
        ifram = false;
    }
}
