using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{

    [Header("王冠何個取得でクリアか")]
    public int Clown_Clear;

    [Header("ハイジャンプ：4　壁キック：5　幅跳び：6　スライディング：7")]
    [Header("ジャンプ：0　しゃがみ：1　ひっつき：2　走り：3")]
    [Header("どのアクションカードをクリア判定にするか")]
    public int ClearCard;

    [Header("1：～回以上の使用でクリア")]
    [Header("0：～回以内の使用でクリア")]
    [Header("どのミッション内容にするか")]
    public int Minssion_Num;

    [Header("それぞれのミッション何枚でミッションクリアか")]
    public int[] Use_Card_Clear;

    [Header("王冠取得時表示画像")]
    public GameObject Clown_img;

    [Header("ミッション内容テキスト")]
    public Text Mission_substance_text;

    [Header("ミッションクリア画像")]
    public GameObject Mission_img;

    [Header("ここから下いじらない-----------")]
    public bool Clown_OK = false;
    public bool Mission_OK = false;

    private int clown_get = 0;      //プレイヤーから王冠を取得したか判定をとる
    private int[] use_Card_Amount;  //それぞれのカードを使用した回数を取得

    private int Mission_Use_Card_Clear = 0;
    private string mission_action = "";
    private string mission_substance = "";


    public enum Card
    {
        JUMP,
        SQUAT,
        STICK,
        RUN,
        HIGHJUMP,
        WALLKICK,
        LONGJUMP,
        SLIDING,
    }

    // Start is called before the first frame update
    void Start()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //ステージでのみ取得
        if (SceneManager.GetActiveScene().name != "Title" &&
            SceneManager.GetActiveScene().name != "StageSelect" &&
            SceneManager.GetActiveScene().name != "Result")
        {
            //ミッションのアクションカード設定
            if (ClearCard == (int)Card.JUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.JUMP];
                mission_action = "ジャンプを";
            }
            if (ClearCard == (int)Card.SQUAT)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.SQUAT];
                mission_action = "しゃがみを";
            }
            if (ClearCard == (int)Card.STICK)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.STICK];
                mission_action = "ひっつきを";
            }
            if (ClearCard == (int)Card.RUN)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.RUN];
                mission_action = "走るを";
            }
            if (ClearCard == (int)Card.HIGHJUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.HIGHJUMP];
                mission_action = "ハイジャンプを";
            }
            if (ClearCard == (int)Card.WALLKICK)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.WALLKICK];
                mission_action = "壁キックを";
            }
            if (ClearCard == (int)Card.LONGJUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.LONGJUMP];
                mission_action = "幅跳びを";
            }
            if (ClearCard == (int)Card.SLIDING)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.SLIDING];
                mission_action = "スライディングを";
            }

            if (Minssion_Num == 0)
                mission_substance = "回以内の使用でクリア";
            if (Minssion_Num == 1)
                mission_substance = "回以上の使用でクリア";

            //ミッション内容作成
            Mission_substance_text.text = "" + mission_action + "" + Mission_Use_Card_Clear + "" + mission_substance;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //王冠取得
        if (clown_get >= Clown_Clear) 
        {
            //クリアテキスト表示
            Clown_img.gameObject.SetActive(true);
            Clown_OK = true;
        }
        else
        {
            //クリアテキスト表示
            Clown_img.gameObject.SetActive(false);
            Clown_OK = false;
        }

        //ミッション用カード使用回数制限
        if (Minssion_Num == 0)
        {
            if (use_Card_Amount[ClearCard] <= Use_Card_Clear[ClearCard])
            {
                //クリアテキスト表示
                Mission_img.gameObject.SetActive(true);
                Mission_OK = true;
            }
            else
            {
                Mission_img.gameObject.SetActive(false);
                Mission_OK = false;
            }
        }
        if(Minssion_Num == 1)
        {
            if (use_Card_Amount[ClearCard] >= Use_Card_Clear[ClearCard])
            {
                //クリアテキスト表示
                Mission_img.gameObject.SetActive(true);
                Mission_OK = true;
            }
            else
            {
                Mission_img.gameObject.SetActive(false);
                Mission_OK = false;
            }
        }
    }
}
