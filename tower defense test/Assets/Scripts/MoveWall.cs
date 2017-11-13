using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<UnityEngine.AI.NavMeshObstacle>().transform.Translate(Vector3.left * Time.deltaTime);
            GetComponent<MeshRenderer>().transform.Translate(Vector3.left * Time.deltaTime);
            //GetComponent<NavMeshBuildSettings> ();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<UnityEngine.AI.NavMeshObstacle>().transform.Translate(Vector3.right * Time.deltaTime);
            GetComponent<MeshRenderer>().transform.Translate(Vector3.right * Time.deltaTime);
            //GetComponent<NavMeshBuildSettings> ();
        }

    }
}
