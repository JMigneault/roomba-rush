using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Firework : RoombaItem
{

    bool isFired = false;
    public float speed;
    public KeyCode fireButton;
    public float damage;
    public float knockbackSpeed;
    public float knockbackTime;

    void Update() {
        if (isFired) {
            this.transform.position += speed * Time.deltaTime * transform.up; // advance
        } else {
            if (Input.GetKeyDown(this.fireButton)) {
                Fire();                
            }
        }
    }

    private void Fire() {
        this.isFired = true;
        this.transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        switch (collider.tag) {
            case "Roomba":
                if (collider.GetComponentInChildren<Firework>() != this) {
                    collider.GetComponent<HealthController>().Health -= damage; // hit roomba
                    collider.GetComponent<MovementController>().Hit(transform.up, knockbackTime, knockbackSpeed); // knockback roomba
                    GameObject.Destroy(gameObject);
                }
                break;
            case "HorzWall":
            case "VertWall":
                GameObject.Destroy(gameObject); // destroy self
                break;
            default:
                break; // do nothing
        }
    }

}
