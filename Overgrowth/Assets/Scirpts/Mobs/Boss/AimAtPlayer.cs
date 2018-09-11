using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    public GameObject Character;
    public float offset = 0.0f;
    public GameObject projectile;

    public int compteur;
    public int compteur2;
    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        if (compteur < 200)
        {
            compteur = compteur + 1;
            Vector3 difference = Camera.main.ScreenToWorldPoint(Character.transform.position) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
            if( compteur2 < 45)
            {
                compteur2 = compteur2 + 1;
            }
            else
            {
                GameObject DOI = Instantiate(projectile);
                DOI.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                DOI.transform.localRotation = gameObject.transform.localRotation;
                compteur2 = 0;
            }

          
        }
       
    }
}
