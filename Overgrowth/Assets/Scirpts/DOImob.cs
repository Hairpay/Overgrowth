using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOImob : MonoBehaviour
{
    public Vector2 pos;
    public float speed = 20;
    public float lifetime = 2f;

    // Use this for initialization
    void Start()
    {

        /*
        baseRotation = gameObject.transform.localEulerAngles;
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, w.transform.position.y - gameObject.transform.position.y);
            gameObject.transform.localEulerAngles = baseRotation;
        
            pos = new Vector2(w.transform.position.x - gameObject.transform.position.x, -(w.transform.position.y - gameObject.transform.position.y));
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -(baseRotation.z + 180));
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        
    */
        StartCoroutine("selfdestrucc");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag != gameObject.transform.tag)
        {
            Xploz();
        }
    }

    public void Xploz()
    {

        if (gameObject.GetComponent<Explotron2D>() != null)
        {
            gameObject.GetComponent<Explotron2D>().Explosion();
        }
        Destroy(gameObject);
    }
    IEnumerator selfdestrucc()
    {
        yield return new WaitForSeconds(lifetime);
        Xploz();
    }
}
