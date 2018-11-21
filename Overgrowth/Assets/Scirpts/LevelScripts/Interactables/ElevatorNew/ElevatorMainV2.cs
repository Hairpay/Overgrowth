using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorMainV2 : MonoBehaviour
{
    public States myState;

    public GameObject character;
    public Gestionnaire Gestionnaire;
    public int layer_mask;

    public GameObject[] Arrets;
    public int position;
    public int basePosition;
    public float t;
    public Vector3 oldPos;
    public GameObject futurePos;

    public bool playerOn;

    public bool power;
    public Text analysisText;
    public Image analysisPanel;
    public bool errorBlocked;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("character");
        Gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;
        analysisPanel = character.GetComponent<UIGereur>().analysisPanel;
        analysisText = character.GetComponent<UIGereur>().analysis;
        layer_mask = LayerMask.GetMask("Player");

        oldPos = gameObject.transform.position;
    }
    public enum States
    {
        off,
        inactive,
        active,
        moving        
    }

  
    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case States.off:
               if(power == true)
                {
                    myState = States.inactive;
                }               
                break;
            case States.inactive:
                if (power == false)
                {
                    myState = States.off;
                }
                else
                {
                    ActiveCheck();
                }
                break;
            case States.active:
                if (power == false)
                {
                    myState = States.off;
                }
                else
                {
                    ActiveCheck();
                }
                break;
            case States.moving:
                Move(futurePos);
                break;
        }
    }

    public void ActiveCheck()
    {
        RaycastHit2D upHit = Physics2D.Raycast(new Vector2(transform.position.x + 2, transform.position.y + (gameObject.transform.localScale.y * 0.6f))
          , Vector2.left, 5f, layer_mask);

        Debug.DrawRay(new Vector2(transform.position.x + 2, transform.position.y + (gameObject.transform.localScale.y *0.6f))
            , Vector2.left * 5);

        if (upHit.collider != null)
        {
            Debug.Log(upHit.collider.name);
            playerOn = true;
            myState = States.active;           
        }
        else
        {
            playerOn = false;
            myState = States.inactive;
        }
    }
    public void Move(GameObject newPos)
    {
        gameObject.transform.position = new Vector3(
              (Mathf.Lerp(oldPos.x, newPos.transform.position.x, t)),
              (Mathf.Lerp(oldPos.y, newPos.transform.position.y, t)),
              gameObject.transform.position.z);

        if (t < 1)
        {
            t += 0.4f * Time.deltaTime;
            if (playerOn == true)
            {
                character.GetComponent<Rigidbody2D>().simulated = false;
                character.transform.parent = gameObject.transform;
                Gestionnaire.Locked = true;
            }        
        }
        else
        {
            t = 0;
            oldPos = gameObject.transform.position;
            if (playerOn == true)
            {
                character.GetComponent<Rigidbody2D>().simulated = true;
                character.transform.parent = null;
                Gestionnaire.Locked = false;
            }               
        }
    }
    public void Error()
    {
        if (errorBlocked == false)
        {
            analysisText.text = "Error, this elevator has no power.";
        }
        else
        {
            analysisText.text = "Error, something is blocking the elevator.";
        }

        analysisText.enabled = true;
        analysisPanel.enabled = true;
        StopAllCoroutines();
        StartCoroutine("ReturnUnlock");
    }
    public void Called()
    {
        analysisText.text = "Elevator called.";
        analysisText.enabled = true;
        analysisPanel.enabled = true;
        StopAllCoroutines();
        StartCoroutine("ReturnUnlock");
    }

    IEnumerator ReturnUnlock()
    {
        yield return new WaitForSeconds(2f);
        analysisText.enabled = false;
        analysisPanel.enabled = false;
    }
}
