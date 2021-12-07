using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colar : MonoBehaviour
{
    //ゲーミング色用変数群なお配列使えの模様-------------
    private int a, b, c;
    private bool A, B, C;
    private int time,time2;
    //---------------------------------------------------

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

        //最初からやるとバグるんでちょい待ち
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
            if (a < 1)
            {
                A = true;
            }
            else if (a > 255)
            {
                A = false;
            }
            if (b < 1)
            {
                B = true;
            }
            else if (b > 255)
            {
                B = false;
            }
            if (c < 1)
            {
                C = true;
            }
            else if (c > 255)
            {
                C = false;
            }

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
            cb.normalColor = new Color32((byte)a, (byte)b, (byte)c, 255);
            if (time == 40)
                cb.highlightedColor = new Color32(255, 255, 0, 255);
            else if (time == 60)
            {
                cb.highlightedColor = new Color32(255, 255, 255, 255);
                time = 0;
            }
            button.colors = cb;
        }
        else 
        {
            //なんもないときは初期色へ
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32(255, 255, 255, 255);
            button.colors = cb;
        }


    }
}
