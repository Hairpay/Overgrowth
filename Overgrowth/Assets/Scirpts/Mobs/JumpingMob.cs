using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMob : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public Transform currentPoint;
   
    public float dist;
    public Rigidbody2D moBody;
   
    public bool cooldown;
    private Vector2 Jump;

    public float BonusJump = 30000f;

    // Use this for initialization
    void Start () {
        moBody = gameObject.GetComponent<Rigidbody2D>();
        currentPoint = point1;
    }
	
	// Update is called once per frame
	void Update () {

        dist = Vector3.Distance(currentPoint.position, transform.position);

        if (cooldown == false)
        {
            Jump = currentPoint.transform.position - gameObject.transform.position;
            Jump = Jump.normalized;
           
            moBody.AddForce(new Vector2(Jump.x * 35000,Jump.y *35000 + BonusJump));
            cooldown = true;
            StartCoroutine("ReturnVariables");
        }       
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
    IEnumerator ReturnVariables()
    {

        yield return new WaitForSeconds(2.5f);
        cooldown = false;
    }
}
