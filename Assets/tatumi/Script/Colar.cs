using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//七色変化でUI強調(現在使わないため凍結)
public class Colar : MonoBehaviour
{
    const int MAX_COLOR = 255;

    //ゲーミング色用変数群-------------
    private int a, b, c;
    private bool A, B, C;
    private int time,time2;
    //--------------------------------

    //マルチ作成可能か判断用
    private bool OK = false;
    //合体できるかどうかのサイン用。(文字)
    private string[] multi_oks = new string[2];


    //自身取得。いいかげんthis.gameObject覚えろ
    public Button button;

    MultuAction_Creit script; //参照元Scriptが入る変数

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 255;
        c = 128;

        time = 0;
        time2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        time2++;

        //初期化
        OK = false;

        //最初からやるとバグるんでちょい待ちからの処理
        if (time2 > 10)
        {
            //マルチ作成可能かマルチクリエイトから情報持ってくる
            multi_oks = button.GetComponent<MultuAction_Creit>().get_multi_oks(); //OBJの中にあるScriptを取得して変数に格納する
           
            for (int i = 0; i != 2; i++)
            {
                //名前の条件を見るマルチ~ならOK
                if (multi_oks[i].Contains("multi") == true)
                {
                    OK = true;
                }
            }
        }

        
        //ゲーミング色用
        if (OK == true)
        {
            time++;

            //折り返しflag設定
            if (a < 1)
            {
                A = true;
            }
            else if (a > MAX_COLOR)
            {
                A = false;
            }
            if (b < 1)
            {
                B = true;
            }
            else if (b > MAX_COLOR)
            {
                B = false;
            }
            if (c < 1)
            {
                C = true;
            }
            else if (c > MAX_COLOR)
            {
                C = false;
            }

            //設定に従い増減
            if (A == true)
                a++;
            else
                a--;

            if (B == true)
                b++;
            else
                b--;

            if (C == true)
                c++;
            else
                c--;

            //色をそれぞれのカラーに割り振る。
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32((byte)a, (byte)b, (byte)c, MAX_COLOR);
            if (time == 40)
                cb.highlightedColor = new Color32(MAX_COLOR, MAX_COLOR, 0, MAX_COLOR);
            else if (time == 60)
            {
                cb.highlightedColor = new Color32(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
                time = 0;
            }
            button.colors = cb;
        }
        else 
        {
            //なんもないときは初期色へ
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
            button.colors = cb;
        }


    }
}
