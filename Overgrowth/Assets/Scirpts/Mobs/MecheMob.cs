using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class MecheMob : MonoBehaviour
{
    public GameObject mobdossier;
    public GameObject mobVisu;

    public Color baseColor;
    public Color actualColor;

    public int life;
    public int maxLife;

    void Awake()
    {
        baseColor = mobVisu.GetComponent<SpriteMeshInstance>().color;
        actualColor = baseColor;
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
            actualColor.g = actualColor.g * 0.5f;
            mobVisu.GetComponent<SpriteMeshInstance>().color = actualColor;

        }
    }

}
