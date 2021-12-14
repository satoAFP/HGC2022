using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_move_UI : MonoBehaviour
{
    [Header("横に動く速度")]
    public float move_speed;

    [Header("横に動くポジション")]
    public float stop_pos;

    private Vector3 mp;         //マウスのポジション
    private Vector3 mem_pos;    //このオブジェクトの初期位置記憶
    private Vector3 pos;        //このオブジェクトの位置
    private Vector2 size;       //このオブジェクトのサイズ

    // Start is called before the first frame update
    void Start()
    {
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        size = this.gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        //マウスのポジション更新
        mp = Input.mousePosition;

        if ((mp.x >= pos.x - (size.x / 2)) && (mp.x <= pos.x + (size.x / 2)) &&
            (mp.y >= pos.y - (size.y / 2)) && (mp.y <= pos.y + (size.y / 2)))
        {
            if (this.gameObject.GetComponent<RectTransform>().position.x > stop_pos)
            {
                pos.x -= move_speed;
                this.gameObject.GetComponent<RectTransform>().position = pos;
            }
        }
        else
        {
            if (this.gameObject.GetComponent<RectTransform>().position.x < mem_pos.x) 
            {
                pos.x += move_speed;
                this.gameObject.GetComponent<RectTransform>().position = pos;
            }
        }

        
        
    }
}
