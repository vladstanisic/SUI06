using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    [SerializeField] float speed = 0.5f;
   
    [SerializeField] string enemytag = "Enemy";

    //[SerializeField] Transform gameObject;
    [SerializeField] float turnSpeed = 10;
    


    // Use this for initialization
    void Start()
    {
       
    }
    

    public void ChaseTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
	
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(base.gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        /*Vector3 rotation = lookRotation.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        */
        gameObject.transform.rotation = lookRotation;
    }
    void HitTarget()
    {
        Destroy(gameObject);
        // damage enemy here
    }
}
