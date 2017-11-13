using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    enum State { Opening, Open, Closing, Closed};

    State state;
    float start;
    float openingEnd;
    float openEnd;
    float closingEnd;

    [SerializeField]
    Transform hinge;

	// Use this for initialization
	void Start () {
        state = State.Opening;
        openingEnd = Time.time + 3;
        openEnd = openingEnd + 3;
        closingEnd = openEnd + 3;
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Opening:
                hinge.Rotate(Vector3.up, 30f * Time.deltaTime);
                if(Time.time >= openingEnd)
                {
                    state = State.Open;
                }
                break;

            case State.Open:
                if (Time.time >= openEnd)
                {
                    state = State.Closing;
                }
                break;

            case State.Closing:
                hinge.Rotate(Vector3.up, -30f * Time.deltaTime);
                if (Time.time >= closingEnd)
                {
                    state = State.Closed;
                }
                break;

            case State.Closed:
                break;

          
            
        }
	}
}
