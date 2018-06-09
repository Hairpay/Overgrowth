using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject PrefabPlayer;

    private void Start ()
    {
        GameObject prefabPlayer;
        prefabPlayer = Instantiate(PrefabPlayer, this.transform);
        prefabPlayer.transform.localPosition = new Vector3(0,0,0);
    }
}
