using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputs : MonoBehaviour {

   public Vector2 firstPressPos;
   public Vector2 secondPressPos;
   public Vector2 currentSwipe;

    public int LastSwipe = 0;
    public bool swiping = false;

    public bool JustSwiped = false;
         

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            swiping = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                Debug.Log("You clicked on the " + hit.transform.name); // test about clicked object
                hit.transform.tag = ("gotHit0");
            }
            }
        if (Input.GetMouseButtonUp(0))
        {
            JustSwiped = true;
            swiping = false;

            StartCoroutine("resetSwipe");

            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0.5  && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f)
        {
                Debug.Log("up swipe");
                LastSwipe = 1;
            }
            //swipe down
            if (currentSwipe.y < -0.5 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
                Debug.Log("down swipe");
                LastSwipe = 2;
            }
            //swipe left
            if (currentSwipe.x < -0.5 && currentSwipe.y > -0.5f  &&  currentSwipe.y < 0.5f)
        {
                Debug.Log("left swipe");
                LastSwipe = 3;
            }
            //swipe right
            if (currentSwipe.x > 0.5 &&  currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
                Debug.Log("right swipe");
                LastSwipe = 4;
            }
            //click
            if( currentSwipe.x > -0.2 && currentSwipe.x < 0.2 && currentSwipe.y > -0.2 && currentSwipe.y < 0.2)
            {

                LastSwipe = 0;

                //select object with only click, not swipe
                Debug.Log("click");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                   
                    Debug.Log("You selected the " + hit.transform.name); // test about clicked object
                    hit.transform.tag = ("gotHit");
                }

            }
        }
    }
    
    IEnumerator resetSwipe()
    {
        yield return new WaitForSeconds(0.5f);
        JustSwiped = false;
    }
}
