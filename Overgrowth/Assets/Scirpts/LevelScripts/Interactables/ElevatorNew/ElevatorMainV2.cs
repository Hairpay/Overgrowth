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
    public Vector2 oldPos;
    public Vector2 futurePos;

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
   
        position = basePosition;
        gameObject.transform.position = Arrets[basePosition].GetComponent<ElevatorCallV2>().posPlateforme.transform.position;
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
            Arrets[position].GetComponent<ElevatorCallV2>().myState = ElevatorCallV2.States.fleches;
            myState = States.active;           
        }
        else
        {
            playerOn = false;
            Arrets[position].GetComponent<ElevatorCallV2>().myState = ElevatorCallV2.States.call;
            myState = States.inactive;
        }
    }
    public void Move(Vector2 newPos)
    {
        gameObject.transform.position = new Vector3(
              (Mathf.Lerp(oldPos.x, newPos.x, t)),
              (Mathf.Lerp(oldPos.y, newPos.y, t)),
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
            myState = States.active;
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
    public void Message(string message)
    {
        analysisText.text = message;
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
