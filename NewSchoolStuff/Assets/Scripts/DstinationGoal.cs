using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DstinationGoal : MonoBehaviour
{

    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject spawnLocation;
    //[SerializeField] private float ghostInterval;
    //private float currentX;
    //private float currentZ;
    //private GameObject newGhost;

    void Start()
    {
        //StartCoroutine(Example());
    }

    /*IEnumerator Example()
    {
        currentX = spawnLocation.transform.position.x;
        currentZ = spawnLocation.transform.position.z;
        for (int i = 0; i < 1; i++)
        {
            GameObject newGhost = (GameObject)Instantiate(ghost, transform.position, transform.rotation);
            newGhost.transform.position = new Vector3(currentX, 0.3f, currentZ);
            newGhost.transform.SetParent(spawnLocation.transform);
            yield return new WaitForSeconds(ghostInterval);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ghost") == null)
        {
            //StartCoroutine(Example());
            Debug.Log("Finns inte");
            float currentX = spawnLocation.transform.position.x;
            float currentZ = spawnLocation.transform.position.z;

            GameObject newGhost = (GameObject)Instantiate(ghost, transform.position, transform.rotation);

            newGhost.transform.position = new Vector3(currentX, 0.3f, currentZ);
            newGhost.transform.SetParent(spawnLocation.transform);
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ghost")
        {
            Destroy(GameObject.FindGameObjectWithTag("Ghost"));
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ghost")
        {
            Debug.Log("Hej");
            //GameObject newGhost = (GameObject)Instantiate(ghost, transform.position, transform.rotation) as GameObject;

            Destroy(GameObject.FindGameObjectWithTag("Ghost"));

            //Destroy(newGhost);
            /*newGhost = (GameObject)Instantiate(ghost, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            newGhost = (GameObject)Instantiate(ghost, transform.position, transform.rotation);
            Destroy(newGhost);*/
        }
    }
}