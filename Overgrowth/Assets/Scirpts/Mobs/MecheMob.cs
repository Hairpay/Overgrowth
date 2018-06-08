using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class MecheMob : MonoBehaviour
{
    public GameObject mobdossier;
    public GameObject[] mobVisu;

    public Color[] actualColor;

    public int life;
    public int maxLife;

    void Awake()
    {
        for (int i = 0; i < mobVisu.Length; i++)
        {
            actualColor[i] = mobVisu[i].GetComponent<SpriteMeshInstance>().color;
        }
       
       
    }
    // Use this for initialization
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            gameObject.GetComponent<Explotron2D>().Explosion();
            Destroy(mobdossier);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Orb")
        {
            life = life - 1;

            for (int i = 0; i < mobVisu.Length; i++)
            {
                actualColor[i].g = actualColor[i].g * 0.7f;
                mobVisu[i].GetComponent<SpriteMeshInstance>().color = actualColor[i];
            }          
        }
    }

}
