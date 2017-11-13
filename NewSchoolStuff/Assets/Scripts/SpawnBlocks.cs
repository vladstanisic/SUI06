using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour {

    [SerializeField] GameObject cube2;
    [SerializeField] GameObject cubeOrigin;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float currentX = cubeOrigin.transform.position.x;
            float currentZ = cubeOrigin.transform.position.z;

            GameObject cube = GameObject.Instantiate(cube2);
            cube.transform.position = new Vector3(currentX, 0.3f, currentZ);
            cube.transform.SetParent(cubeOrigin.transform);

            Debug.Log("Summon me!");
        }
    }
}
