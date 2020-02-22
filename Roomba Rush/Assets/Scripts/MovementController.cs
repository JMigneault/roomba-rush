using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Vector3 direction = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += direction * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction == Vector3.right)
            {
                direction = Vector3.up;
            } else if(direction == Vector3.up)
            {
                direction = Vector3.left;
            } else if (direction == Vector3.left)
            {
                direction = Vector3.down;
            } else if (direction == Vector3.down)
            {
                direction = Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction == Vector3.right)
            {
                direction = Vector3.down;
            }
            else if (direction == Vector3.up)
            {
                direction = Vector3.right;
            }
            else if (direction == Vector3.left)
            {
                direction = Vector3.up;
            }
            else if (direction == Vector3.down)
            {
                direction = Vector3.left;
            }
        }
    }
}
