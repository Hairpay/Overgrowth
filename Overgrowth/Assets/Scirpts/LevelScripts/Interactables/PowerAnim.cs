using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAnim : MonoBehaviour
{
    public Animator animator;
    public PowerSource source;
	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<PowerSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        animator.SetBool("power", source.power);
	}
}
