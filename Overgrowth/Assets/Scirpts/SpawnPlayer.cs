using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject PrefabPlayer;
    public bool isPlayer;

    void Awake()
    {
        if (isPlayer == true)
        {
            GameObject prefabPlayer;
            prefabPlayer = Instantiate(PrefabPlayer, this.transform);
            prefabPlayer.transform.localPosition = new Vector3(0, 0, 0);
            prefabPlayer.transform.parent = null;
        }
      
    }

    private void Start ()
    {
        if (isPlayer == false)
        {
            GameObject prefabPlayer;
            prefabPlayer = Instantiate(PrefabPlayer, this.transform);
            prefabPlayer.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
