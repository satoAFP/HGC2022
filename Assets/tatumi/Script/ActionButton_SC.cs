using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

//アクションボタン全体の管理
public class ActionButton_SC : MonoBehaviour
{
    //固定値
    private const int MAX_CARD_RESERVE = 11;
    private const int MAX_CARD_TYPE = 8;
    private const int CARD_MULTI_BOLDER = 4;

    //プレイヤー自身を入れる枠(必ずあるため設定なし)
    GameObject player; //参照元OBJそのものが入る変数
    Player script; //参照元OBJそのものが入る変数

    //回数表示用変数---------------------------------------
    //現在の予約アクション
    private int[] action_num=new int[MAX_CARD_TYPE];
    //実行済みアクション
    private int[] executed_action = new int[MAX_CARD_TYPE];
    //-----------------------------------------------------

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //予約Card消去用受け取り口(要素11)
    public GameObject[] multi_des = new GameObject[MAX_CARD_RESERVE];
    //現在の消去待ちの順番
    private int multi_des_now = 0;

    //アクション選択時の他オブジェ認識用変数
    public int PL_action_num;

    // Start is called before the first frame update
    void Start()
    {
       
        //選択回数初期化
        for (int i = 0; i != MAX_CARD_TYPE; i++)
        {
            //初期化
            executed_action[i] = 0;
        }
     
        //player関連----------------------------------------------------------------------
        player = GameObject.Find("Player"); //オブジェクトの名前から取得して変数に格納する
        script = player.GetComponent<Player>(); //OBJの中にあるScriptを取得して変数に格納する
        //--------------------------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
       
    }

  
    //Action_Cardのintでのtype分け
    /*
        JUMP,0
        SQUAT,1
        STICK,2
        RUN,3
        HIGHJUMP,4
        WALLKICK,5
        LONGJUMP,6
        SLIDING,7
    */

   
    //関数で選んだ数を増減
    public void set_text(int Action_Card, int add)
    {
        //選択回数の数の増減(+,-対応)
        if ((action_num[Action_Card] + add) >= executed_action[Action_Card])
            action_num[Action_Card] += add;
    }

    //現在の数(取得用)
    public int get_score(int Action_Card)
    {
        return action_num[Action_Card] + executed_action[Action_Card];
    }


    //実行したアクションを最低数として読み込む
    public void executed_Action(int Action_Card)
    {
        //合体カードか否か
        if (Action_Card >= CARD_MULTI_BOLDER)
        {
            //エラー回避
            if (multi_des[0] != null)
            {
                //使用した場合記録のみ残し、本体削除
                if (multi_des[0].activeSelf == false)
                {
                    Destroy(multi_des[0]);

                    //削除のため記録の整理
                    for (int i = 1; i != MAX_CARD_RESERVE; i++)
                    {
                        multi_des[i - 1] = multi_des[i];
                    }

                    multi_des_now--;
                }
            }
        }

        //通常アクションの場合（エラー回避含め）
        if (Action_Card != -1)
        {
            executed_action[Action_Card]++;
            action_num[Action_Card]--;
        }
       
        //現在の選択アクション
        PL_action_num = Action_Card;
    }

    //                    true  false
    //マルチの消去待ち列　追加と削除はflagで判定
    public void multi_des_Check(GameObject multi_obj,bool flag)
    {
        //追加
        if(flag==true)
        {
           
            multi_des[multi_des_now] = multi_obj;

            multi_des_now++;
        }
        //削除
        else
        {
            //エラー回避
            if (multi_des[0] != null)
            {
                multi_des[multi_des_now] = null;

                multi_des_now--;
              
            }
            
        }

        //一応数が-領域にいかないよう保護
        if (multi_des_now < 0)
            multi_des_now = 0;

    }

    //自身を非表示
    public void Set_OffActive()
    {
        this.gameObject.SetActive(false);
    }
}
