using UnityEngine;
using System.Collections;

public class InputAnimMono : MonoBehaviour {

    public Animator animator;

    public bool Bool1;
   // public bool Bool2;
//    public bool Bool3;

    public float secondsToWaits = 2.0f;


    // Update is called once per frame
    void FixedUpdate () {

        if (Input.GetButtonDown("Jump") && !Bool1)
        {
            Bool1 = true;
            StartCoroutine("ReturnVariables");

        }

        animator.SetBool("IsOpen1", Bool1);
    }

    IEnumerator ReturnVariables() {

            yield return new WaitForSeconds(secondsToWaits);
        Bool1 = false;
    }
}
