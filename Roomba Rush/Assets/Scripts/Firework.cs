using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Firework : RoombaItem
{

    bool isFired = false;
    public float speed;
    public KeyCode fireButton;

    void Update() {
        if (isFired) {
            this.transform.position += speed * Time.deltaTime * transform.forward; // advance
        } else {
            if (Input.GetKeyDown(this.fireButton)) {
                // todo
            }
        }
    }

    private void Fire() {
        this.isFired = true;
    }

    private void OnTriggerEnter(Collider collider) {
        switch (collider.tag) {
            case "Roomba":
                collider.GetComponent<HealthController>(); // hit roomba
                collider.GetComponent<MovementController>(); // knockback roomba
                break;
            case "Wall":
                GameObject.Destroy(gameObject); // destroy self
                break;
            default:
                break; // do nothing
        }
    }

}
