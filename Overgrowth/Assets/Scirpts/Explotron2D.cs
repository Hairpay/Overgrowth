using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotron2D : MonoBehaviour {
    public GameObject Shrap;
    public float movescale = 0.3f;
    public bool autoExplo = false;

    // Use this for initialization
    void Start () {

        if (autoExplo == true)
        {
            Explosion();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Explosion()
    {
        GameObject parent = new GameObject();
        parent.transform.name = "Shraparent";  
        parent.transform.position = gameObject.transform.position;
        parent.transform.position = gameObject.transform.position;
        parent.AddComponent<DelePilotron>();

        for (int i = 0; i < 50; i++)
        {            
            GameObject shrapnel = Instantiate(Shrap);
        
            shrapnel.transform.parent = parent.transform;
            shrapnel.transform.localPosition = new Vector3(0, 0, 0);

            Vector3 move = new Vector3(
                Random.Range(movescale, movescale),
                Random.Range(movescale, movescale),
                Random.Range(movescale, movescale)
                );

            shrapnel.transform.localPosition += move;   
             
            Vector3 rotation = new Vector3(
                0f,
                90.0f * Random.Range(-1.0f, 1.0f),
                0f);

            shrapnel.transform.Rotate(rotation, Space.Self);
        
            Vector3 scale = new Vector3(

              (move.y + 3) / 10,
              (move.y + 3) / 10,
              (move.y + 3) / 10
               );

            shrapnel.transform.localScale = scale;
            shrapnel.AddComponent<Rigidbody2D>();
            shrapnel.AddComponent<SelfDestron>();
            shrapnel.GetComponent<Rigidbody2D>().mass = 0.0000001f;
            shrapnel.GetComponent<Rigidbody2D>().AddForce( new Vector2 (Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
        }

        Destroy(gameObject);
    }
}
