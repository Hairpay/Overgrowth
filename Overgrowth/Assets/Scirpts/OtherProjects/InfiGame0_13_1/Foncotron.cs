using UnityEngine;
using System.Collections;

public class Foncotron : MonoBehaviour {

    private float secondsToWaits = 5.0f;
    public float tiem ;
 //   public int Vroum;
  //  public GameObject player;

    void Start ()
    {
      //  player = GameObject.Find("Mangora");
        StartCoroutine("SelfDestruct");
        gameObject.GetComponent<Rigidbody>().AddForce(0f,10f,0f);

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Vroum = player.GetComponent<Autotron>().Vroum;
        tiem = tiem + 1;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward);
        gameObject.transform.localScale =
            new Vector3(gameObject.transform.localScale.x + gameObject.transform.localScale.x *0.01f,
                        gameObject.transform.localScale.y + gameObject.transform.localScale.y *0.01f,
                        gameObject.transform.localScale.z + gameObject.transform.localScale.z *0.01f);

       
	}

    void OnTriggerEnter(Collider other)

    {

       
        if (other.tag == "Sol"|| other.tag == "Obstacle")
        {
            //gameObject.GetComponent<BoxCollider>().enabled = false;
          //  Destroy(gameObject);
          if (tiem > 50)StartCoroutine("SelfDestruct2");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(secondsToWaits);
        DestroyImmediate(gameObject);
    }

    IEnumerator SelfDestruct2()
    {

        yield return new WaitForSeconds(0.4f);
        DestroyImmediate(gameObject);
    }
}
