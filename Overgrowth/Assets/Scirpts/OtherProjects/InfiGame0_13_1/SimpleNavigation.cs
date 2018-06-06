using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SimpleNavigation : MonoBehaviour {

	public Transform[] targets;
	NavMeshAgent agent;
	private int i = 0;
    public float X;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = targets [i].transform.position;
	}
	
	void Update () {
		float dist = Vector3.Distance (targets [i].transform.position, transform.position);

		if(dist < X){
			i++;
			if(i < targets.Length)	{
				agent.destination = targets [i].transform.position;
			}
		}

		if (i == targets.Length)	{
			i = 0;
			agent.destination = targets [i].transform.position;
		}
	}
}
