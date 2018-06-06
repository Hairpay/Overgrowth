using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoncoFleche : MonoBehaviour {

    public GameObject player;
   


   

    // Use this for initialization
    void Start () {

        StartCoroutine("SelfDestruct");

        player = GameObject.Find("BoxFleche");
        
      

      

    }
	
	// Update is called once per frame
	void Update () {

       

        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*100);
       
      
    }


    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Sol" || other.tag == "Obstacle")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
          
        
        }

        if (other.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.parent = player.transform;
            gameObject.GetComponent<BoxCollider>().enabled = false;
                    

        }
       
    }

    IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(8f);
        DestroyImmediate(gameObject);
    }
}
