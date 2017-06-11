using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateToTarget : MonoBehaviour {

	public string targetTag;
	public Transform targetTransform;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		if (targetTag != "") {
			GameObject targetGO = GameObject.FindGameObjectWithTag(targetTag);
			targetTransform = targetGO.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (targetTransform.position);
	}
}
