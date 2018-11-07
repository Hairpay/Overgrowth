using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoor : MonoBehaviour
{

    public GameObject PrefabDoor;
    public int AnalysisLevel;
    public bool power = true;
    public bool errorBlocked;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        GameObject prefabDoor;
        prefabDoor = Instantiate(PrefabDoor, this.transform);
        prefabDoor.transform.localPosition = new Vector3(0, 0, 0);
       // prefabDoor.transform.localScale = gameObject.transform.localScale;
        prefabDoor.GetComponent<PorteAnalyse>().AnalysisLevel = AnalysisLevel;
        prefabDoor.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        prefabDoor.GetComponent<PorteAnalyse>().baseColor = gameObject.GetComponent<SpriteRenderer>().color;

        prefabDoor.GetComponent<PorteAnalyse>().power = power;
        prefabDoor.GetComponent<PorteAnalyse>().errorBlocked = errorBlocked;

    }
}
