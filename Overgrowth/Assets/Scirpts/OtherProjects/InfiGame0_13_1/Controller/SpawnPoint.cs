using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject Player;

    void Start () {
        spawn();
	}
	
   public void spawn()
    {
        Player = GameObject.Find("Mangora");
        Player.transform.position = gameObject.transform.position;
    }
}
