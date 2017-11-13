using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraging : MonoBehaviour {

    [SerializeField] private float distance = 14.7f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3((int)(Input.mousePosition.x) + 0.5f, (int)(Input.mousePosition.y) + 0.5f, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;

        Debug.Log("X: " + (int)mousePosition.x + " Y: " + (int)mousePosition.y);
    }
}
