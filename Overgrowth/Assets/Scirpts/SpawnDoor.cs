using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoor : MonoBehaviour
{

    public GameObject PrefabDoor;
    public int AnalysisLevel;
    public bool power = true;
    public bool autoClose;
    public bool errorBlocked;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        GameObject prefabDoor;
        prefabDoor = Instantiate(PrefabDoor, this.transform);
        prefabDoor.transform.localPosition = new Vector3(0, 0, 0);
       // prefabDoor.transform.localScale = gameObject.transform.localScale;
        prefabDoor.GetComponent<PorteAnalyseV2>().analysisLevel = AnalysisLevel;

        prefabDoor.GetComponent<PorteAnalyseV2>().autoClose = autoClose;
        prefabDoor.GetComponent<PorteAnalyseV2>().basePower = power;
        prefabDoor.GetComponent<PorteAnalyseV2>().errorBlocked = errorBlocked;

    }
}
