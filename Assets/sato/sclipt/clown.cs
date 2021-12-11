using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clown : MonoBehaviour
{
    [Header("上下移動速度")]
    public float max_Speed;
    [Header("回転速度")]
    public float role_Speed;
    [Header("trueの時上")]
    public bool Y_move;

    private Vector3 first_pos;          //主人公初期位置
    private float move_speed = 0.0f;    //上下の移動量
    private int frame_count = 0;        //上下の動きを制御

    // Start is called before the first frame update
    void Start()
    {
        //主人公の初期位置記憶
        first_pos = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //上下移動処理
        frame_count++;
        if (frame_count >= 0 && frame_count < 30)
        {
            if (Y_move)
                move_speed += max_Speed;
            else
                move_speed -= max_Speed;
        }
        else if (frame_count >= 30 && frame_count < 60)
        {
            if (Y_move)
                move_speed -= max_Speed;
            else
                move_speed += max_Speed;
        }
        else if (frame_count >= 60 && frame_count < 88)
        {
            if (Y_move)
                move_speed -= max_Speed;
            else
                move_speed += max_Speed;
        }
        else if (frame_count >= 88 && frame_count < 116) 
        {
            if (Y_move)
                move_speed += max_Speed;
            else
                move_speed -= max_Speed;
        }
        
        //移動量加算
        this.gameObject.transform.localPosition += new Vector3(0.0f, move_speed, 0.0f);
        this.gameObject.transform.localEulerAngles += new Vector3(0.0f, role_Speed, 0.0f);

        //上下移動の誤差修正
        if (frame_count >= 115)
        {
            frame_count = 0;
            move_speed = 0.0f;
            this.gameObject.transform.localPosition = first_pos;
        }
    }
}
