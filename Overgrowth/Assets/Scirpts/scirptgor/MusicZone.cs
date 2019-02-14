using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour {

    public bool AutoOn;
    public bool active;
    public bool activateWithKey = false;
    public string key = "left ctrl";
    public string Tag;
    public bool[] trax;
    public GameObject musicManager;

	// Use this for initialization
	void Start () {
		if (AutoOn)
        {
            StartCoroutine("Wait");
        }
	}
	
	// Update is called once per frame
	void Update () {       
        if (Input.GetKeyUp(key) && activateWithKey && active)
        {         
            musicManager.SendMessage("Zone", trax);
        }        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {     
        if (coll.gameObject.tag == Tag && active)
        {
           // Debug.Log("yee");
            musicManager.SendMessage("Zone", trax);
        }
    }
    

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == Tag)
        {
          //  Debug.Log("ayy");
           // active = false;
        }
    }

    void Activate()
    {
        active = !active;
    }

    void SendManager()
    {
        musicManager.SendMessage("Zone", trax);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        SendManager();
    }
}
