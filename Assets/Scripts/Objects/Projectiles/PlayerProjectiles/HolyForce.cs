using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyForce : MonoBehaviour
{
    public float horizontalForce;
    public float verticalForce;
    private bool shootRight;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootRight = FindObjectOfType<PlayerController>().is_facing_right;

        if (shootRight)
        {
            rb.AddForce(new Vector2(horizontalForce, verticalForce));
        }

        else
        {
            rb.AddForce(new Vector2(-horizontalForce, verticalForce));
        }
    }


    
}
