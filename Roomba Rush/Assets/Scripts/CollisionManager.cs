using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public float hitSpeed;
    public float wallTime;
    void OnCollisionEnter(Collider collider) {
        Vector3 hitDirection;
        switch (collider.tag) {
            case "HorzWall": // switch direction of y component of velocity 
                hitDirection = new Vector3(collider.transform.up.x, -collider.transform.up.y, 0);
                collider.GetComponent<MovementController>().GetHit(hitDirection.normalized * hitSpeed, wallTime);
                break;
            case "VertWall": // switch direction of x component of velocity
                hitDirection = new Vector3(-collider.transform.up.x, collider.transform.up.y, 0);
                collider.GetComponent<MovementController>().GetHit(hitDirection.normalized * hitSpeed, wallTime);
                break;
        }
    }

}
