using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAnalysisTrigger : MonoBehaviour {

    public Description description;

    public Animator animator;
    public bool reset;
    public float reseTime;

    public bool once;

    // Use this for initialization
    void Start ()
    {
        description = gameObject.GetComponent<Description>();
        animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (description.compteur > 50 && once == false)
        {
            animator.Play("AnimTrigger");
            once = true;
            
            if (reset == true)
            {
                StartCoroutine("SelfReset");
            }
        }
    }
    IEnumerator SelfReset()
    {     
        yield return new WaitForSeconds(reseTime);
        animator.Play("AnimReset");
        once = false;
    }
}
