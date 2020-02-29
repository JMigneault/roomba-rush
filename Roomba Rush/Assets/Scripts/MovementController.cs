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
    Vector3 externalVelocity;
    roombaState currentState;
    float ragdollTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = roombaState.Spinning;
    }

// Update is called once per frame
void Update()
    {
        Movement();
        DebugMovementChange();
    }


void GetHit(Vector3 vector, float time)
    {
        externalVelocity = vector;
        ragdollTimer = time * Time.deltaTime;
        currentState = roombaState.Hit_Ragdoll;
    }
    void Movement()
    {
        switch (currentState)
        {
            case roombaState.Hit_Ragdoll:
                RagdollMovement();
                break;
            case roombaState.Spinning:
                gameObject.transform.Rotate(0, 0, .25f, Space.Self);
                break;
            case roombaState.Moving:
                gameObject.transform.position += transform.up * Time.deltaTime;
                break;
        }
    }

    void DebugMovementChange()
    {
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
        else if (Input.GetKeyDown(KeyCode.D))
        {   
            if (currentState == roombaState.Hit_Ragdoll)
            {
                currentState = roombaState.Spinning;
            }
            else
            {
                GetHit(Vector3.right);
                currentState = roombaState.Hit_Ragdoll;
            }

        }
    }
    void RagdollMovement()
    {
        if (ragdollTimer != 0)
        {
            ragdollTimer--;
            gameObject.transform.position += 10f * externalVelocity * Time.deltaTime;
        }
        else
        {
            currentState = roombaState.Spinning;
        }


    }
}

