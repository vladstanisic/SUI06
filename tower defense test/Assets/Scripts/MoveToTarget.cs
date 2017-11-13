using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour {

    public Transform targetPoint;

	// Use this for initialization
	void Start () {
       //transform.GetComponent<NavMeshAgent>().destination = targetPoint.position; //stops on arrival
    }
	
	// Update is called once per frame
	void Update () {
       transform.GetComponent<NavMeshAgent>().destination = targetPoint.position;

       
    }
}


