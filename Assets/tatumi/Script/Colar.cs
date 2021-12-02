using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colar : MonoBehaviour
{
    private int a, b, c;
    private bool A, B, C;
    private int time;

    private bool OK = false;

    public Button button;
    //合体できるかどうかのサイン用。
    private string[] multi_oks = new string[4];

    MultuAction_Creit script; //参照元Scriptが入る変数

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 255;
        c = 128;

        time = 0;
        //script = button.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する
    }

    // Update is called once per frame
    void Update()
    {
        multi_oks = button.GetComponent<MultuAction_Creit>().get_multi_oks(); //OBJの中にあるScriptを取得して変数に格納する
        OK = false;
       

        for (int i = 0; i != 4; i++)
        {
            //名前の条件見直し
            if (multi_oks[i].Contains("multi") == true)
            {
                OK = true;
               
                
            }
            

        }

        

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
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32(255, 255, 255, 255);
            button.colors = cb;
        }


    }
}
