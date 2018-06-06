using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLyingMob : MonoBehaviour
{

    public Transform point1;
    public Transform point2;
    public Transform currentPoint;

    public float dist;
    public Rigidbody2D moBody;

    public bool cooldown;
    private Vector2 Jump;
    public float vitesse = 10000f;

    // Use this for initialization
    void Start()
    {
        moBody = gameObject.GetComponent<Rigidbody2D>();
        currentPoint = point1;
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector3.Distance(currentPoint.position, transform.position);
        
        Jump = currentPoint.transform.position - gameObject.transform.position;
        Jump = Jump.normalized;
        moBody.velocity = new Vector2(0f, 0f);
        moBody.AddForce(Jump * vitesse);       
        
        if (dist < 8f)
        {
            if (currentPoint == point1)
            {
                currentPoint = point2;
            }
            else
            {
                currentPoint = point1;
            }
        }
    }   
}
