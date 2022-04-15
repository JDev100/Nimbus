using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenArea : MonoBehaviour {
    public float speed = 1.0f;
    private Color hidden;
    private Color not_hidden;
    private Color current_color;

    private float start_time;

    private bool show_area = false;
	// Use this for initialization
	void Start () {
        start_time = Time.time;

        hidden = new Color(1, 1, 1, 1);
        not_hidden = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        current_color = GetComponent<SpriteRenderer>().color;

        float t = (Time.time - start_time) * speed;

        if (show_area)
        GetComponent<SpriteRenderer>().color = Color.Lerp(current_color, not_hidden, t);
        else
            GetComponent<SpriteRenderer>().color = Color.Lerp(current_color, hidden, t);

    }

    //private void ShowArea ()
    //{
    //    float t = Time.deltaTime * speed;
    //    GetComponent<SpriteRenderer>().color = Color.Lerp(hidden, not_hidden, t);
    //}

    //private void HideArea()
    //{
    //    float t = Time.deltaTime * speed;
    //    GetComponent<SpriteRenderer>().color = Color.Lerp(not_hidden, hidden, t);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            show_area = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            show_area = false;
        }
    }
}
