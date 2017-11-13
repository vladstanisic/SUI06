using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCanWalk : MonoBehaviour {

    public Transform destinationPoint;
    private Transform curPos;
    //private Transform lastPos;
    private Vector2 oldPos;

    Vector3 lastPos;
    [SerializeField] Transform obj; // drag the object to monitor here
    float threshold = 0.1f; // minimum displacement to recognize a 
    void Start()
    {
        lastPos = obj.position;
    }
    void Update()
    {
        Vector3 offset = obj.position - lastPos;
        if (offset.x > threshold)
        {
            lastPos = obj.position; // update lastPos
            Debug.Log("Större");                    // code to execute when X is getting bigger
        }
        else
        if (offset.x < -threshold)
        {
            lastPos = obj.position; // update lastPos
            Debug.Log("Större");                   // code to execute when X is getting smaller 
        }
    }
}
