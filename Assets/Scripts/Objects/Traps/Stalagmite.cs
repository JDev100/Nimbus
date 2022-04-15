using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour {

    public Transform left_bound;
    public Transform right_bound;
    public float distance;
    public float fall_gravity;
    public float time_until_fall;

    private bool is_falling;
    private Rigidbody2D rb;

    private bool has_fell_down;
	// Use this for initialization
	void Start () {
        Physics2D.queriesStartInColliders = false;

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D left_hit_info = Physics2D.Raycast(left_bound.position, -transform.up, distance);
        RaycastHit2D right_hit_info = Physics2D.Raycast(right_bound.position, -transform.up, distance);

        if (left_hit_info.collider != null)
        {
            Debug.DrawLine(left_bound.position, left_hit_info.point, Color.red);
        }
        if (right_hit_info.collider != null)
        {
            Debug.DrawLine(right_bound.position, right_hit_info.point, Color.red);
        }

        //When you are under the stalgmite
        if (left_hit_info.collider != null || right_hit_info.collider != null)
        {
            if (left_hit_info.collider.CompareTag("PlayerHealth") || right_hit_info.collider.CompareTag("PlayerHealth"))
            {
                Debug.Log("Spotted player");

                StartCoroutine(FallDown());

                has_fell_down = true;
            } 
        }
        
    }
    
    
    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(time_until_fall);
        rb.gravityScale = fall_gravity;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "LevelCollision")
        {
            rb.velocity = Vector2.zero;
        }
    }
}
