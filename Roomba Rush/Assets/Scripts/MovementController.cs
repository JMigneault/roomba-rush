using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    public enum roombaState
    {
        Spinning,
        Moving,
        Hit_Ragdoll
    }
    roombaState currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = roombaState.Spinning;
    }

// Update is called once per frame
void Update()
    {
        if (currentState == roombaState.Spinning)
        {
            gameObject.transform.Rotate(0, 0, .25f, Space.Self);
        }
        else if (currentState == roombaState.Moving)
        {
            gameObject.transform.position += transform.up * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentState == roombaState.Moving)
            {
                currentState = roombaState.Spinning;
            }
            else if (currentState == roombaState.Spinning)
            {
                currentState = roombaState.Moving;
            }
        }
    }
}
