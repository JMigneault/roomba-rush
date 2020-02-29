using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public float hitSpeed;
    public float wallTime;
    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("called");
        Vector3 hitDirection;
        switch (collision.transform.tag) {
            case "HorzWall": // switch direction of y component of velocity 
                hitDirection = new Vector3(collision.transform.up.x, -collision.transform.up.y, 0);
                this.GetComponent<MovementController>().GetHit(hitDirection.normalized * hitSpeed, wallTime);
                break;
            case "VertWall": // switch direction of x component of velocity
                hitDirection = new Vector3(-collision.transform.up.x, collision.transform.up.y, 0);
                this.GetComponent<MovementController>().GetHit(hitDirection.normalized * hitSpeed, wallTime);
                break;
        }
    }

}
