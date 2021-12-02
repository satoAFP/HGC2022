using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    [Header("王冠何個取得でクリアか")]
    public int Clown_Clear;

    [Header("ハイジャンプ：4　壁キック：5　幅跳び：6　スライディング：7")]
    [Header("ジャンプ：0　しゃがみ：1　くっつき：2　走り：3")]
    [Header("何をクリア判定にするか")]
    public int ClearCard;

    [Header("それぞれ何枚以内でミッションクリアか")]
    public int[] Use_Card_Clear;

    [Header("王冠取得時表示画像")]
    public GameObject Clown_img;

    [Header("ミッションクリアテキスト")]
    public Text Mission_text;

    private int clown_get = 0;
    private int[] use_Card_Amount;

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
    }

    // Update is called once per frame
    void Update()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //王冠取得
        if (clown_get == Clown_Clear) 
        {
            //クリアテキスト表示
            Clown_img.gameObject.SetActive(true);
        }

        //ミッション用カード使用回数制限
        if (use_Card_Amount[ClearCard] <= Use_Card_Clear[ClearCard])
        {
            //クリアテキスト表示
            Mission_text.text = "クリア";
        }
        else
        {
            Mission_text.text = "失敗";
        }
    }
}
