using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (gameObject.GetComponent<cakeslice.Outline>() == null && gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.AddComponent<cakeslice.Outline>();
        }

        if (gameObject.GetComponent<cakeslice.Outline>() != null)
        {          
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.GetComponent<cakeslice.Outline>() != null && Input.GetButtonDown("Fire4"))
        {
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }

        if (gameObject.GetComponent<cakeslice.Outline>() != null && Input.GetButtonUp("Fire4"))
        {
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }

        if (gameObject.GetComponent<PowerSource>().power == true)
        {
            gameObject.GetComponent<cakeslice.Outline>().color = 1;
        }
        else
        {
            gameObject.GetComponent<cakeslice.Outline>().color = 2;
        }
    }
}
