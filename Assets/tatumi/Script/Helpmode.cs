using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チュートリアル用
public class Helpmode : MonoBehaviour
{
    //PLのOBJとscript取得（textも）----------
    public GameObject PL;

    Player SC_player;

    public GameObject HelpText;

    HelpText SC_Htext;
    //-----------------------------------------

    //何番目のtxtかカウント用
    private int nowint=0;

    // Start is called before the first frame update
    void Start()
    {
        //中身をいれる
        SC_player = PL.GetComponent<Player>();
        SC_Htext = HelpText.GetComponent<HelpText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        //PLと当たると動作
        if (collision.gameObject.tag == "Thorn")
        {
            //ヘルプの文字を出す＆PLの動きを停止させる
            SC_player.Movestop = true;
           
            SC_Htext.Helpmode = nowint;

            SC_Htext.SetHelp();
            nowint++;
        }
    }

   
}
