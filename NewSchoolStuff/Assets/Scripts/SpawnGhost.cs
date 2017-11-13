using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour {

    [SerializeField] private GameObject ghost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject ghost2 = GameObject.Instantiate(ghost);
            ghost.transform.position = new Vector3(14, 1, 15);
        }
    }
}
