using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject[] PrefabPlayer;
    public bool isPlayer;

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (isPlayer == true)
        {
            GameObject prefabPlayer;
            prefabPlayer = Instantiate(PrefabPlayer[0], this.transform);
            prefabPlayer.transform.localPosition = new Vector3(0, 0, 0);
            prefabPlayer.transform.parent = null;
        }
      
    }

    private void Start ()
    {
        if (isPlayer == false)
        {
            GameObject prefabPlayer;
            int i = Random.Range(0, PrefabPlayer.Length);
            prefabPlayer = Instantiate(PrefabPlayer[i], this.transform);
            prefabPlayer.transform.localPosition = new Vector3(0, 0, 0);                  
        }
    }
}
