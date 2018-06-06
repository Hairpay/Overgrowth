using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flechotron : MonoBehaviour {

    public BoxCollider FlecheBox;
    public GameObject Fleche;
    public bool cooldown = true;
    

    public GameObject Archer;
    public bool Bas = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   

    public void shoot()
    {
        if (cooldown == true)
        {
         
            GameObject ShootArrow = Instantiate(Fleche);
          
            ShootArrow.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            ShootArrow.AddComponent<BoxCollider>();
            FlecheBox =  ShootArrow.GetComponent<BoxCollider>();

            FlecheBox.isTrigger = true;
            FlecheBox.size = new Vector3(0.1f,0.1f,1f);
            ShootArrow.AddComponent<Rigidbody>();
            ShootArrow.GetComponent<Rigidbody>().useGravity = false;
     
            
            ShootArrow.transform.localRotation = Archer.transform.localRotation;
            //  ShootArrow.transform.localRotation = Quaternion.Euler(gameObject.transform.localRotation.x, gameObject.transform.localRotation.y + 180, gameObject.transform.localRotation.z);
            if (Bas == true)
            {
                ShootArrow.transform.Rotate(
                    ShootArrow.transform.rotation.x + 5f,
                    ShootArrow.transform.rotation.y,
                    ShootArrow.transform.rotation.z);
            }
            ShootArrow.tag = "Danger";
           
            ShootArrow.AddComponent<FoncoFleche>();
            
           

            StartCoroutine("Cooldown");

            cooldown = false;
        }
    }
    IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(1.45f);
        cooldown = true;
    }
}
