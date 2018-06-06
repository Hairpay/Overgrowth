using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directioneur : MonoBehaviour {
    public bool right;
    public bool left;

    public float h;

    

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {                    
        if (Input.GetButtonUp("ArrowRight"))
        {
            right = false;          
        }
        if (Input.GetButtonDown("ArrowRight"))
        {
            right = true;
           
        }

        if (Input.GetButtonUp("ArrowLeft"))
        {
            left = false;
        }
        if (Input.GetButtonDown("ArrowLeft"))
        {
            left = true;
        }



        if (right == true)
        {           
            Move(-h);
        }

        if (left == true)
        {
            Move(h);
        }
    }

    public void Move(float move)
    {
       // body.velocity = new Vector2(move, body.velocity.y);
        gameObject.transform.Rotate(0f, 0f, move);
    }
}
