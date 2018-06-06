using UnityEngine;
using System.Collections;

public class DelePilotron : MonoBehaviour {
    public float secondsToWaits = 11.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine("SelfDestruct");
    }

    IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(secondsToWaits);
        DestroyImmediate(gameObject);
    }
}
