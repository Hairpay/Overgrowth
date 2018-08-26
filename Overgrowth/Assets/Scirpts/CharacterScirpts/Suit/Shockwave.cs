using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

    public GameObject Shock;
    public ParticleSystem ShockParticle;
    public Gestionnaire Gestionnaire;

    public bool waving;

    // Use this for initialization
    void Start () {
        Shock = GameObject.Find("ShockWaveP");
        Gestionnaire = gameObject.GetComponent<PowerUps>().Gestionnaire;
        ShockParticle = Shock.GetComponent<ParticleSystem>();
        ShockParticle.Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void wave()
    {
      //  if(Gestionnaire.ShockWave == true)
        {
            waving = true;
            ShockParticle.Play();
            StartCoroutine("SteamStop");
        } 
    }

    IEnumerator SteamStop()
    {
        yield return new WaitForSeconds(0.2f);
        ShockParticle.Stop();
        waving = false;

    }
}
