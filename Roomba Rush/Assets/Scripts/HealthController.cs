using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float initialHealth;

    private float health;
    // tood: alert game manager off death
    public float Health
    {
        get { return health; }
        set
        {
            Debug.Log("new health" + value);
            health = value;
        }
    }

    void Start()
    {
        health = initialHealth;
    }

    void Update()
    {
        if (health < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Weapon":
                if (this.GetComponentInChildren<Weapon>() != collision.GetComponent<Weapon>())
                {
                    Health -= collision.transform.GetComponent<Weapon>().damage;
                }
                break;
            default:
                break;
        }  
    }

}
