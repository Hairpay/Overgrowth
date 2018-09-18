using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFall : MonoBehaviour
{
    private int layer_mask;
    private Vector3 between;

    public Vector3 AimPoint;
    public GameObject bridge;


    // Use this for initialization
    void Start()
    {
        layer_mask = LayerMask.GetMask("Environment","VineBridge");
    }

    public void checkDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down ,Mathf.Infinity, layer_mask);
        Debug.DrawLine(gameObject.transform.position, hit.point);
       
        if (hit.collider != null)
        {
            AimPoint = hit.point;
        }
        else
        {
            AimPoint = gameObject.transform.position;
        }
    }
    // Create the bridge by joining the 2 points
    public void FallMaking()
    {
        between = (AimPoint - gameObject.transform.position);
        float distance = between.magnitude;

        bridge.transform.position = AimPoint - (between * 0.5f);
        bridge.transform.localScale = new Vector3(1, distance*0.5f, 1);       
    }

    // Update is called once per frame
    void Update()
    {
        checkDown();
        FallMaking();
    }
}
