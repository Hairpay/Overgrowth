using UnityEngine;
using System.Collections;

public class SelfDestron : MonoBehaviour {

    private float secondsToWaits = 10.0f;
    private float secondsToWaits2 = 0.2f;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("SelfDestruct");
        StartCoroutine("NoBox");
    }

    IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(secondsToWaits);
        DestroyImmediate(gameObject);
    }
    IEnumerator NoBox()
    {

        yield return new WaitForSeconds(secondsToWaits2);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //    gameObject.AddComponent<Explotron>();
    }
}
