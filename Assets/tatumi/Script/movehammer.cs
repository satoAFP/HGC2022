using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//振り子型に回転。数値を限度を超えさせれば一方向のみの回転もできる。
public class movehammer : MonoBehaviour
{
    //インスペクターで設定----------------------------------
    [Header("振り速度")]
    public float moveSpeed;

    [Header("限度振り角度")]
    public float hammerRad;

    [Header("振り方向")]
    public bool back;

    //private変数-----------------------------------------
    [Header("初期角度[限度を超えないように!]")]
    public float nowhammeRad;
    private int z;
    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        x = 0.0f;
        y = 0.0f;
        z = 0;
        transform.Rotate(new Vector3(x, y, nowhammeRad));
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //右回り、左周り回転
        if (back == true)
        {
            nowhammeRad = 1.0f * moveSpeed;
        }
        else
        {
            nowhammeRad = -1.0f * moveSpeed;
        }

       
        //回転
        transform.Rotate(new Vector3(x, y, nowhammeRad));

        //回転角度を整数のみに変換
        z = (int)gameObject.transform.localEulerAngles.z;

        //現在の角度をもとに回転方向を変更
        if (360 - hammerRad >= z && 350 - hammerRad <= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
        else if (hammerRad <= z && hammerRad + 10 >= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
    }

    public float Getnowrad()
    {
        return nowhammeRad;
    }

}
