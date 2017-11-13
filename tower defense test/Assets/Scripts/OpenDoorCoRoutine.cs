using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCoRoutine : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Open());
        }
	}

    IEnumerator Open()
    {
        float openingEnd = Time.time + 3;
        while (Time.time<openingEnd)
        {
            transform.Rotate(Vector3.up, 30f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        float closingEnd = Time.time + 3;
        while (Time.time < closingEnd)
        {
            transform.Rotate(Vector3.up, -30f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
