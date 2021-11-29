using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_side_move : MonoBehaviour
{
    [Header("移動速度(2の倍数で)")]
    public float move_speed;

    [Header("キャンバスの横のサイズ")]
    public int canvas_size;

    [Header("ページ数")]
    public int page_amount;

    private bool right_flag = false;        //右ボタン押したときtrue
    private bool left_flag = false;         //左ボタン押したときtrue
    private int move_pos = 0;               //ステージ選択の移動先

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (right_flag == true) 
        {
            this.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(-move_speed, 0.0f);

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x == move_pos) 
            {
                right_flag = false;
            }
        }

        if (left_flag == true)
        {
            this.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(move_speed, 0.0f);

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x == move_pos)
            {
                left_flag = false;
            }
        }

    }

    //左右ボタン
    public void PushRight()
    {
        //ポジションが、ページ数*キャンバスサイズより大きいとき動かない
        if (move_pos > -((page_amount - 1) * canvas_size))
        {
            right_flag = true;
            move_pos -= canvas_size;
        }
    }
    public void PushLeft()
    {
        //ポジションが、0より小さいときは動かない
        if (move_pos < 0)
        {
            left_flag = true;
            move_pos += canvas_size;
        }
    }

}
