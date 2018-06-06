using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWall : MonoBehaviour {

    public int layer_mask;
    public float distance = 20f;

    public GameObject Camera;

	// Use this for initialization
	void Start ()
    {
        layer_mask = LayerMask.GetMask("CameraWall");
        Camera = GameObject.Find("Main Camera");
    }


    void FixedUpdate()
    {
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, distance, layer_mask);
        Debug.DrawLine(gameObject.transform.position, left.point);

        if (left.collider != null)
        {
            //  Debug.Log(left.collider.name);
            Camera.transform.parent = null;
            Camera.transform.localPosition = new Vector3(0, 0, -10f);

        }
        else
        {
            Camera.transform.parent = gameObject.transform;
            Camera.transform.localPosition = new Vector3(0, 0, -10f);
        }
    }
}
