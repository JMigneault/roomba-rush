using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoombaState
{
    Spinning,
    Moving,
    Ragdolling
}


public class MovementController : MonoBehaviour
{

    // public roomba parameters to be set in inspector
    public float moveSpeed;
    public float ragdollSpeed;
    public float ragdollDecay;
    public float spinSpeed;
    public float mass;


    // public collision parameters to be set in inspector
    public float wallCollisionTime;
    public float roombaCollisionTime;

    // set up controls for this roomba in inspector
    public KeyCode stopSpinningKey;

    // saved initial roomba parameters
    private float initRagdollSpeed;

    // internal state params
    private RoombaState state;
    private Vector2 velocityDirection;
    private float ragdollTimer;

    // public properties
    public Vector2 Velocity
    {
        get
        {
            switch (this.state)
            {
                case RoombaState.Moving:
                    return velocityDirection * moveSpeed;
                case RoombaState.Ragdolling:
                    return velocityDirection * ragdollSpeed;
                default:
                    return Vector2.zero;
            }
        }
    }

    void Start()
    {
        initRagdollSpeed = ragdollSpeed;
    }
    void Update()
    {
        switch (state)
        {
            case RoombaState.Spinning:
                Spin();
                break;
            case RoombaState.Moving:
                Move();
                break;
            case RoombaState.Ragdolling:
                Ragdoll();
                break;
        }
    }

    void Spin()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKeyDown(stopSpinningKey))
        {
            velocityDirection = transform.up;
            state = RoombaState.Moving;
        }
    }

    void Move()
    {
        transform.Translate(velocityDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void Ragdoll()
    {
        transform.Translate(velocityDirection * ragdollSpeed * Time.deltaTime, Space.World);
        ragdollSpeed = Mathf.Max(ragdollSpeed - Time.deltaTime * ragdollDecay, 0);
        ragdollTimer -= Time.deltaTime;
        if (ragdollTimer < 0 || ragdollSpeed == 0)
        {
            ragdollSpeed = initRagdollSpeed;
            state = RoombaState.Spinning;
            velocityDirection = Vector2.zero; // should not be used while spinning
        }
    }

    public void Hit(Vector2 direction, float time, float startSpeed)
    {
        state = RoombaState.Ragdolling;
        velocityDirection = direction.normalized;
        ragdollSpeed = startSpeed;
        ragdollTimer = time;
    }

    public void Hit(Vector2 direction, float time)
    {
        this.Hit(direction, time, ragdollSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "HorzWall":
                Hit(new Vector2(velocityDirection.x, -velocityDirection.y), wallCollisionTime);
                break;
            case "VertWall":
                Debug.Log("test");
                Hit(new Vector2(-velocityDirection.x, velocityDirection.y), wallCollisionTime);
                break;
            case "Roomba":
                // both roombas will get here so we break the tie on who computes collision using instance IDs
                if (transform.GetInstanceID() > collision.transform.GetInstanceID())
                {
                    // reference on elastic collisions between 2 hard spheres here: https://introcs.cs.princeton.edu/java/assignments/collisions.html
                    MovementController thatMove = collision.GetComponent<MovementController>();
                    float R = (GetComponent<CircleCollider2D>().radius + thatMove.GetComponent<CircleCollider2D>().radius) * 10; // todo remove scale factor
                    float thisM = thatMove.mass;
                    float thatM = thatMove.mass;
                    Vector2 dP = thatMove.transform.position - this.transform.position;
                    Vector2 thisV = this.Velocity;
                    Vector2 thatV = thatMove.Velocity;
                    Vector2 dV = thatV - thisV;
                    float J = 2 * thisM * thatM * (dV.x * dP.x + dV.y * dP.y) / (R * (thisM + thatM));
                    float Jx = J * dP.x / R;
                    float Jy = J * dP.y / R;
                    Vector2 thisNewV = thisV + (new Vector2(Jx / thisM, Jy / thisM));
                    Vector2 thatNewV = thatV - (new Vector2(Jx / thatM, Jy / thatM));
                    this.Hit(thisNewV.normalized, roombaCollisionTime, thisNewV.magnitude);
                    thatMove.Hit(thatNewV.normalized, roombaCollisionTime, thatNewV.magnitude);
                }
                break;
            case "Weapon":
                if (this.GetComponentInChildren<Weapon>() != collision.GetComponent<Weapon>())
                {
                    Hit(collision.transform.up, collision.transform.GetComponent<Weapon>().hitTime); // rebound self
                    collision.transform.parent.GetComponent<MovementController>().Hit(-collision.transform.up, collision.transform.GetComponent<Weapon>().reboundTime); // rebound other roomba
                }
                break;
            default:
                Debug.Log("Roomba collision did not detect tag: " + collision.transform.tag);
                break;
        }
    }


}

