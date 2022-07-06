using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    //現在のチュートリアルで何番目か認識用
    public int Helpmode;

    //自身取得用
    public Text text;

    //それぞれのUIの獲得-------------------------------
    public GameObject[] yazirusi = new GameObject[2];
    public GameObject oya;
    public GameObject PL;
    public GameObject Card;
    public GameObject CardChoice;
    public GameObject BackButton;
    public GameObject ActionButton;
    //-------------------------------------------------

    //スクリプトを取得--------------------------------------
    Player SC_player;
    DeletAction SC_Back;
    ActionButton_SC SC_action; //参照元Scriptが入る変数
    //------------------------------------------------------

    //テキスト順番(連続で表示するよう)
    public int nowtext;

    [Header("表示文章順（一行16文字17文字ごとに改行）")]
    public string[] chars;
    [Header("文章の切り替え終わりの数を指示")]
    public int[] Endchar;

    

    // Start is called before the first frame update
    void Start()
    {
        SC_player = PL.GetComponent<Player>();
        SC_Back = BackButton.GetComponent<DeletAction>();
        SC_action = ActionButton.GetComponent<ActionButton_SC>();
    }

    // Update is called once per frame
    void Update()
    {
        //チュートリアルtxtの内容がどこかを認識＆表示
       if(Helpmode>0)
        text.text = chars[nowtext+1];
       else
            text.text = chars[nowtext];


       //右クリで次のページへ
        if (Input.GetMouseButtonDown(0))
        {
            nowtext++;
        }
        //左クリで現在表示できる範囲で一つ前のページへ戻る
        else if (Input.GetMouseButtonDown(1))
        {
            nowtext--;
        }

        //-1のエラー回避用
        if (nowtext < 0)
            nowtext = 0;

       
        //最初の止まるZONE
        if(Helpmode==0)
        {
            if (nowtext > Endchar[0])
                nowtext = Endchar[0];

            if (nowtext == 2)
            {
                //エンターで動き解除＆強調UI非表示
                if (Input.GetKey(KeyCode.Return))
                {
                   
                    oya.SetActive(false);
                    SC_player.Movestop = false;
                    
                }
            }
        }
        //二つ目
        else if(Helpmode==1)
        {
            //前の段階のtextを出さないようにする
            if (nowtext < Endchar[0])
                nowtext = Endchar[0];

            else if (nowtext >= Endchar[0] && nowtext <= Endchar[1])
            {
                Card.SetActive(true);
                if (nowtext == 3)
                {
                   //強調表じ
                    yazirusi[0].SetActive(true);
                }
            }
           
            if (nowtext > Endchar[1]-1)
                nowtext = Endchar[1]-1;

            //Jumpが選択されてたら
            if(SC_Back.objs[0].name.Contains("jump")==true && nowtext == Endchar[1]-1)
            {
                //次へ進ませる（いらないUIや動きを元に戻す）
                oya.SetActive(false);
                SC_player.Movestop = false;
               
                Card.SetActive(false);
                yazirusi[0].SetActive(false);

                SC_Back.now--;
            }
        }
        //３つめ
        else if (Helpmode == 2)
        {
            //前の段階のtextを出さないようにする
            if (nowtext < Endchar[1])
                nowtext = Endchar[1];

            //UI表示タイミング
            else if (nowtext >= Endchar[1] && nowtext <= Endchar[2])
            {
                if (nowtext == 5)
                {
                    CardChoice.SetActive(true);
                  
                    yazirusi[1].SetActive(true);
                }
            }

            if (nowtext > Endchar[Helpmode]-1)
                nowtext = Endchar[Helpmode]-1;

            //しゃがむが選択されたら
            if (SC_Back.objs[0].name.Contains("squat") == true && nowtext == Endchar[Helpmode]-1)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                CardChoice.SetActive(false);
                yazirusi[1].SetActive(false);

            }
        }
        //３つめ
        else if (Helpmode == 3)
        {
            //前の段階のtextを出さないようにする
            if (nowtext < Endchar[2])
                nowtext = Endchar[2];

            //UI表示
            else if (yazirusi[1].activeSelf == true)
            {
                if (nowtext < Endchar[2]+1)
                    nowtext = Endchar[2] + 1;
            }

            if (nowtext >= 7 && nowtext <=10)
            {
               Card.SetActive(true);

               for (int i = 0; i != 2; i++)
               {
                  yazirusi[i].SetActive(true);
               }

            }
            

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            //ハイジャンプが作られたら選ぶようUI移動
            if (SC_Back.multi_objs[0].name.Contains("multi_highjump(Clone)")==true)
            {
               
                yazirusi[0].SetActive(false);
                

                yazirusi[1].gameObject.transform.position = new Vector3(23.0f, 299.6f, -102.0f);
            }

            //ハイジャンプが選択されたら
            if (SC_action.multi_des[0].name.Contains("multi_highjump(Clone)") == true)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                Card.SetActive(false);
                for (int i = 0; i != 2; i++)
                {
                    yazirusi[i].SetActive(false);
                }

            }
        }
        //4つめ
        else if (Helpmode == 4)
        {
            //前の段階のtextを出さないようにする
            if (nowtext < Endchar[3])
                nowtext = Endchar[3];

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            //読み切るまで動かさない
            if (nowtext == Endchar[Helpmode] - 1)
            {
                //読み切り＋エンターで動かさせる
                if (Input.GetKey(KeyCode.Return))
                {

                    oya.SetActive(false);
                    SC_player.Movestop = false;

                }
            }
        }


    }

    //Helpmodeにて使用
    public void SetHelp()
    {
        oya.SetActive(true);
    }
}
