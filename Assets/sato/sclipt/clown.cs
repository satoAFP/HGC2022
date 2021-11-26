using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clown : MonoBehaviour
{
    [Header("ã‰ºˆÚ“®‘¬“x")]
    public float max_Speed;
    [Header("‰ñ“]‘¬“x")]
    public float role_Speed;

    private float move_speed = 0.0f;
    private bool move_check = true;
    private int frame_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frame_count++;
        if (frame_count >= 0 && frame_count < 30)
        {
            move_speed += 0.001f;
        }
        if (frame_count >= 30 && frame_count < 60)
        {
            move_speed -= 0.001f;
        }
        if (frame_count >= 60 && frame_count < 90)
        {
            move_speed -= 0.001f;
        }
        if (frame_count >= 90 && frame_count < 120)
        {
            move_speed += 0.001f;
        }
        if (frame_count == 120)
        {
            frame_count = 0;
            Debug.Log("" + this.gameObject.transform.localPosition.y);
        }

        //if (move_check)
        //{
        //    move_speed += 0.001f;
        //    if (max_Speed <= move_speed) 
        //    {
        //        move_speed = 0.0f;
        //        move_check = false;
        //    }
        //}
        //else
        //{
        //    move_speed -= 0.001f;
        //    if (-max_Speed >= move_speed)
        //    {
        //        move_speed = 0.0f;
        //        move_check = true;
        //    }
        //}

        this.gameObject.transform.localPosition += new Vector3(0.0f, move_speed, 0.0f);
        this.gameObject.transform.localEulerAngles += new Vector3(0.0f, role_Speed, 0.0f);
    }
}
