using UnityEngine;
using System.Collections;

public class Explotron : MonoBehaviour {

   
    public Material Mat;
    public GameObject ghost;

	// Use this for initialization
	void Start () {
        
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            GameObject Ghosty = Instantiate(ghost);
            Ghosty.transform.position = gameObject.transform.position;

            GameObject parent = new GameObject();
            parent.transform.name = "pilonRocheux2";
            parent.AddComponent<DelePilotron>();




            parent.transform.position = gameObject.transform.position;
          //  parent.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,gameObject.transform.position.z);

          //  parent.transform.localPosition = new Vector3(gameObject.transform.position.x, 0 , gameObject.transform.position.z);


            //  distance = Vector3.Distance(player.transform.localPosition, parent.transform.localPosition);


            GameObject matrice = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            matrice.transform.parent = parent.transform;


            // parent.transform.localPosition = hit.point;  
            matrice.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.5f, gameObject.transform.localScale.y * 0.5f, gameObject.transform.localScale.z * 0.5f);
    

            for (int i = 0; i < Random.Range(gameObject.transform.localScale.y + 40, 2* gameObject.transform.localScale.y)+ 60; i++)
            {



                GameObject roche = GameObject.CreatePrimitive(PrimitiveType.Cube);

                roche.transform.parent = parent.transform;
                roche.GetComponent<Renderer>().material = Mat;
                //Object monMaterial = Resources.Load("Materials/adolphin", typeof(Material));
               // Material matos = monMaterial as Material;
               // Renderer r = roche.GetComponent<Renderer>();
             //   r.sharedMaterial = matos;

               




                roche.transform.localPosition = new Vector3(0, 0, 0);

                Vector3 move = new Vector3(
                    Random.Range(-matrice.transform.localScale.x * 0.5f, matrice.transform.localScale.x * 0.5f),
                    Random.Range(0, matrice.transform.localScale.y),
                    Random.Range(-matrice.transform.localScale.z * 0.5f, matrice.transform.localScale.z * 0.5f)
                    );

                roche.transform.localPosition += move;
                //DestroyImmediate(roche.GetComponent<Collider>());

                Vector3 rotation = new Vector3(
                    0f,
                    90.0f * Random.Range(-1.0f, 1.0f),
                    0f);
                roche.transform.Rotate(rotation, Space.Self);

                //  float m = move.y / gaussian(matrice.transform.localScale.y);

                Vector3 scale = new Vector3(

                  (move.y + 3) / 10,
                  (move.y + 3) / 10,
                  (move.y + 3) / 10
                   );

                // new Vector3((move.y - 20.0f) * 0.5f, (move.y - 20.0f) * 0.5f, (move.y - 20.0f) * 0.5f
                //   0.5f - Mathf.Abs(move.x),
                //    0.5f - Mathf.Abs(move.z),

                roche.transform.localScale = scale;
                roche.AddComponent<Rigidbody>();
                roche.AddComponent<SelfDestron>();
                roche.GetComponent<Rigidbody>().mass = 0.001f;
               



                //  var myScript =roche.gameObject.AddComponent<Move>();



            }
            Destroy(matrice);
            Destroy(gameObject);
        }
    }
   

}
