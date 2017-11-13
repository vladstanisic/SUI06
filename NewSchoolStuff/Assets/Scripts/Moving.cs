using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moving : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			GetComponent<NavMeshObstacle>().transform.Translate (Vector3.left * Time.deltaTime);
			/*GetComponent<MeshFilter>().transform.Translate (Vector3.left * Time.deltaTime);
			GetComponent<NavMeshBuildSourceShape> ().Equals (GetComponent<NavMeshBuildSource>()); **Not needed**
			//GetComponent<NavMeshBuildSettings> ();*/
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			GetComponent<NavMeshObstacle>().transform.Translate (Vector3.right * Time.deltaTime);
            /*GetComponent<MeshFilter>().transform.Translate (Vector3.right * Time.deltaTime);
			GetComponent<NavMeshBuildSourceShape> ().Equals (GetComponent<NavMeshBuildSource>());  **Not needed**
			//GetComponent<NavMeshBuildSettings> ();*/
        }

    }
}