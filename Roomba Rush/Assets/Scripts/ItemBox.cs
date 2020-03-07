using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: make randomized, implement wave itembox spawning.
public class ItemBox : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Roomba":
                if (collider.GetComponentInChildren<RoombaItem>() == null)
                {
                    Transform item = GetComponentInChildren<RoombaItem>().transform;
                    item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    item.parent = collider.transform;
                    item.localPosition = Vector2.zero;
                    item.rotation = item.parent.rotation;
                }
                GameObject.Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

}
