using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusInputManager : MonoBehaviour {
    public float start_jump_time;
    private float jump_time;
    public float start_move_wait_time;
    private float move_wait_time;
    public float start_move_time;
    private float move_time;
    public float start_crouch_time;
    private float crouch_time;
    public float start_crouch_wait_time;
    private float crouch_wait_time;

    public float start_dash_time;
    private float dash_time;

    public float start_fire_time;
    private float fire_time;

    public bool standby = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!standby)
        {



            //For handling jumps
            if (jump_time <= 0)
            {
                GetComponent<EvilNimbusJump>().press_jump = true;
                jump_time = start_jump_time;
            }
            else
            {
                jump_time -= Time.deltaTime;
                GetComponent<EvilNimbusJump>().press_jump = false;
            }


            //For handling movement
            if (move_wait_time <= 0)
            {
                if (move_time <= 0)
                {
                    GetComponent<EvilNimbusController>().press_move = false;
                    move_wait_time = start_move_wait_time;
                }
                else
                {
                    move_time -= Time.deltaTime;
                    GetComponent<EvilNimbusController>().press_move = true;
                }
            }
            else
            {
                move_wait_time -= Time.deltaTime;
                move_time = start_move_time;
            }

            //For crouch
            if (move_wait_time > 0)
            {
                if (crouch_wait_time <= 0)
                {
                    if (crouch_time <= 0)
                    {
                        crouch_wait_time = start_crouch_wait_time;
                        GetComponent<EvilNimbusController>().press_crouch = false;
                    }
                    else
                    {
                        crouch_time -= Time.deltaTime;
                        GetComponent<EvilNimbusController>().press_crouch = true;
                    }
                }
                else
                {
                    crouch_wait_time -= Time.deltaTime;
                    crouch_time = start_crouch_time;
                }
            }

            //For dashes 
            if (GetComponent<Rigidbody2D>().velocity.x != 0)
            {
                if (dash_time < 0)
                {
                    GetComponent<EvilNimbusDash>().press_dash = true;
                    dash_time = start_dash_time;
                }
                else
                {
                    dash_time -= Time.deltaTime;
                    GetComponent<EvilNimbusDash>().press_dash = false;
                }
            }

            //For shooting fire
            if (fire_time <= 0)
            {
                GetComponent<EvilNimbusFire>().press_fire = true;
                //StartCoroutine(ResetPressFire());
                fire_time = start_fire_time;
            }
            else
            {
                GetComponent<EvilNimbusFire>().press_fire = false;
                fire_time -= Time.deltaTime;
            }
        }
    }

    public void SwitchInput(bool val)
    {
        GetComponent<EvilNimbusController>().can_input = val;
        GetComponent<EvilNimbusJump>().can_input = val;
        GetComponent<EvilNimbusDash>().can_input = val;
        GetComponentInChildren<EvilNimbusSlash>().can_input = val;
        //GetComponent<NimbusFowardMagic>().can_input = val;
        //GetComponent<ArcFire>().can_input = val;
    }

    //IEnumerator ResetPressFire()
    //{
    //    yield return new WaitForEndOfFrame();
    //    GetComponent<EvilNimbusFire>().press_fire = false;
    //}
}
