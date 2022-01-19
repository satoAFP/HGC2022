using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pic_se : MonoBehaviour
{
    [Header("横に動く速度")]
    public float move_speed;

    [Header("横に動くポジション")]
    public float stop_pos;

    [Header("カーソル合わせた時のSE")]
    public AudioClip pic;

    private Vector3 mp;         //マウスのポジション
    private Vector3 mem_pos;    //このオブジェクトの初期位置記憶
    private Vector3 pos;        //このオブジェクトの位置
    private Vector2 size;       //このオブジェクトのサイズ
    private AudioSource audio;  //使用するオーディオソース
    private bool first = true;  //一回しか処理をしない

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<RectTransform>().position -= new Vector3(30.6f, 1, 87.9f);
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        size = this.gameObject.GetComponent<RectTransform>().sizeDelta;

        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //マウスのポジション更新
        mp = Input.mousePosition;
        //Debug.Log("" + mp);

        if (mp.x > 100 && mp.x < 320 && mp.y > 50 && mp.y < 130) 
        {
            if (first)
            {
                audio.PlayOneShot(pic);
                first = false;
            }
        }
        else
        {
            first = true;
        }

        if ((mp.x >= pos.x - (size.x / 2)) && (mp.x <= pos.x + (size.x / 2)) &&
            (mp.y >= pos.y - (size.y / 2)) && (mp.y <= pos.y + (size.y / 2)))
        {
            //Debug.Log("" + this.gameObject.GetComponent<RectTransform>().position);
        }
        else
        {
            
        }
    }
}
