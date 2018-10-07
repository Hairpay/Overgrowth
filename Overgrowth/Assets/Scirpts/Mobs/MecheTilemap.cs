using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MecheTilemap : MonoBehaviour
{

    public Color actualColor;
    public Color baseColor;

    public float life;
    public float maxLife = 3;

    void Awake()
    {
        baseColor = gameObject.GetComponent<Tilemap>().color;
        actualColor = gameObject.GetComponent<Tilemap>().color;
    }
    // Use this for initialization
    void Start()
    {
        life = maxLife;        
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1)
        {
            ded();      
        }
    }
    public void colorChange()
    {
        actualColor.g = baseColor.g * life / maxLife;
        gameObject.GetComponent<Tilemap>().color = actualColor;
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Orb")
        {
            life = life - coll.gameObject.GetComponent<DOIgo>().dammage;
            colorChange();
        }
    }
    public void ded()
    {
        if (gameObject.GetComponent<Die2Activate>() != null)
        {
            gameObject.GetComponent<Die2Activate>().activateDed();
        }
        gameObject.GetComponent<Explotron2D>().Explosion();
        Destroy(gameObject);
    }
}
