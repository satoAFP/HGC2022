using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime_ResultMove : MonoBehaviour
{
    [Header("横に動く速度")]
    public float move_speed;

    [Header("止まる位置")]
    public float stop_pos;

    [Header("スライムの背景が動く時のSE")]
    public AudioClip pic;

    [Header("ここから下はいじらない")]
    public bool goal_move = false;              //ゴール時のアクション再生


    private Vector3 mem_pos;    //このオブジェクトの初期位置記憶
    private Vector3 pos;        //このオブジェクトの位置

    private AudioSource audio;  //使用するオーディオソース
    private bool first = true;  //一回しか処理をしない
    private bool feed_end = false;//フェード終了判定

    // Start is called before the first frame update
    void Start()
    {
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        DontDestroyOnLoad(GameObject.Find("slime_UI"));
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goal_move)
        {
            //背景移動処理
            pos.x += move_speed;
            this.gameObject.GetComponent<RectTransform>().position = pos;

            //if (first)
            //{
            //    //SE流す
            //    audio.PlayOneShot(pic);
            //    first = false;
            //}
            
            //位置が中心に来た時、リザルトへ移動
            if (this.gameObject.GetComponent<RectTransform>().localPosition.x > 0)  
            {
                //リザルトシーンでないとき
                if (SceneManager.GetActiveScene().name != "Result")
                {
                    SceneManager.LoadScene("Result");
                }
            }
            if (pos.x > stop_pos) 
            {
                //ストップする座標
                pos = mem_pos;
                goal_move = false;
            }
        }
        else
        {
            //リザルトシーンの時かつ、フェードが終わった時、ボタンを押せるようになる
            if (SceneManager.GetActiveScene().name == "Result")
            {
                if (!feed_end)
                {
                    GameObject.Find("feed_not_tap").SetActive(false);
                    feed_end = true;
                }
            }
        }
    }
}
