using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_pos_UI : MonoBehaviour
{
    [Header("ボタンにマウス乗せたときのSE")]
    public AudioClip se_on_button;

    private Vector3 mp;         //マウスのポジション
    private Vector3 pos;        //このオブジェクトのサイズ
    private Vector2 size;       //このオブジェクトのサイズ

    private bool one_SE = true; //ボタンに乗った時、SEが一回しかならない
    private AudioSource audio;  //使用するオーディオソース

    // Start is called before the first frame update
    void Start()
    {
        //ステージ内にあるSE_manager格納
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();

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
            if (one_SE)
            {
                //SEを流す
                audio.PlayOneShot(se_on_button);
                one_SE = false;
            }
        }
        else
        {
            one_SE = true;
        }

    }
}
