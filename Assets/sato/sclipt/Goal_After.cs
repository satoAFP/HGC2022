using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_After : MonoBehaviour
{
    [Header("再度ジャンプまでの時間")]
    public int jump_time;

    [Header("ジャンプの高さ")]
    public float jump_power;

    [Header("スライムのアニメーション")]
    public Animator anim;

    [Header("ここから下はいじらない")]
    public bool goal_move = false;              //ゴール時のアクション再生


    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(goal_move)
        {
            this.gameObject.GetComponent<Player>().enabled = false;

            count++;

            if (count == jump_time)
            {
                //ジャンプさせる処理
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jump_power, 0.0f), ForceMode.Impulse);

                //ジャンプアニメーション移行
                anim.SetBool("jump", true);
                count = 0;
            }

            if (this.gameObject.transform.localEulerAngles.y <= 180)
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0.0f, 1.0f, 0.0f);
            }
        }



    }
}
