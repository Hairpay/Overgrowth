using UnityEngine;
using System.Collections;

public class Lazerotron : MonoBehaviour {

    public GameObject player;
    public Material RougeLazer;
    public GameObject Mangora;
    public bool fire = false;
    public bool cooldown = true;
    public GameObject firePlosion;

    public int ball = 2;

    public bool reloadCd = true;
    public Animator FireSprite;
    public AudioSource fireSound;


    // Use this for initialization
    void Start () {

        player = GameObject.Find("FireballBox");
        Mangora = GameObject.Find("Mangora");
        fireSound = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Mangora.GetComponent<ControllerV0>().death == false)
        {
            if (ball < 2)
            {
                if (reloadCd == true)
                {
                    reloadCd = false;
                    StartCoroutine("Reload");
                }
            }

            fire = Mangora.GetComponent<ControllerV0>().Cast;

            if (fire == true)
            {
                if (cooldown == true)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    StartCoroutine("Cooldown");
                    fireSound.Play();

                    if (ball > 0)
                    {
                        cooldown = false;
                        StartCoroutine("Gofire");

                        ball = ball - 1;
                        FireSprite.SetBool("go", true);
                    }
                }
            }
        }

     
       }

    IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(0.75f);
        cooldown = true;
        FireSprite.SetBool("go", false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator Reload()
    {

        yield return new WaitForSeconds(5.0f);
        reloadCd = true;
        ball = ball + 1;
        FireSprite.SetBool("go", true);
        StartCoroutine("Return");
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(0.5f);
        FireSprite.SetBool("go", false);
    }
        IEnumerator Gofire()
    {
   
        yield return new WaitForSeconds(0.25f);
        GameObject Lazer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Lazer.AddComponent<Foncotron>();
        Lazer.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Lazer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // Lazer.GetComponent<Renderer>().material = RougeLazer;
        Lazer.GetComponent<Renderer>().enabled = false;
        Lazer.GetComponent<SphereCollider>().isTrigger = true;

        Lazer.AddComponent<Rigidbody>();
        Lazer.GetComponent<Rigidbody>().mass = 0.05f;
        // Lazer.GetComponent<Rigidbody>().useGravity = false;            
        Lazer.transform.localRotation = Mangora.transform.localRotation;
        //  Lazer.AddComponent<ParticleSystem>();


        //  Lazer.GetComponent<ParticleSystem>().particleEmitter
        
        Lazer.tag = "Fireball";

        GameObject FireEffect = Instantiate(firePlosion);
        FireEffect.transform.position = Lazer.transform.position;
        FireEffect.transform.parent = Lazer.transform;
        FireEffect.transform.localScale = new Vector3(FireEffect.transform.localScale.x * 0.5f, FireEffect.transform.localScale.x * 0.5f, FireEffect.transform.localScale.x * 0.5f);

        
    }
}
