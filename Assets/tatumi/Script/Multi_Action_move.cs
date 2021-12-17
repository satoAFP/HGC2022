using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    //番号で何するか決める
    [Header("合体番号")]
    public int action_num;

    private GameObject[] players;

    GameObject player; //参照元OBJそのものが入る変数

    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得
    private Vector3 pos;
    private float first_x;//初期位置

    private bool now_ani,Ps_flag;

    //エフェクト系
    private GameObject effect;
    Effect_move EF_script;
    private GameObject UIs;

    // Player script; //参照元Scriptが入る変数

    // Start is called before the first frame update
    void Start()
    {
        //PL
        player = GameObject.Find("Player"); //ActionButtonをオブジェクトの名前から取得して変数に格納する

        //戻すボタン
        BackButton = GameObject.Find("BackButton"); //オブジェクトの名前から取得して変数に格納する
        script = BackButton.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //バックとの連携------------------------------
        script.multi_objs[script.multi_now] = this.gameObject;
        script.timing[script.multi_now] = script.now;
        //作成と同時に登録
        script.multi_now++;
        //-------------------------------------------

        //複数取得
        players=GameObject.FindGameObjectsWithTag("Player");

        //複数対象処理のオンオフ
        if (players.Length>1)
        {
            Ps_flag = true;
        }
        else
        {
            Ps_flag = false;
        }

        //アニメーション（泣）
        now_ani = false;

        //エフェクト親取得
        UIs = GameObject.Find("ActionBotton"); //ActionButtonをオブジェクトの名前から取得して変数に格納する


        if (first_x > 5.0f)
        {
            //エフェクトは生成時は動く（前後どっちも）
            effect = UIs.transform.Find("PS_Back_Right").gameObject;
        }
        else
        {
            //エフェクトは生成時は動く（どっちも）
            effect = UIs.transform.Find("PS_Back_Left").gameObject;
        }
        //スクリプトゲット
        EF_script=effect.GetComponent<Effect_move>();
        //Back再生
        EF_script.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        //アニメーション（泣）
        if (now_ani == false)
        {
            //スクリプトに信号送信
            GetComponent<Image_multimove>().Move_on = true;
            
            //番号によりアクション変更
            if (action_num == 0)
            {
                if (Ps_flag == true)
                {
                    for(int i=0;i!= players.Length;i++)
                    {
                        players[i].GetComponent<Player>().Push_highjump();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_highjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 1)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_wallkick();
                        Set_Back();
                    }
                }
                else
                {
                   // player.GetComponent<Player>().Push_wallkick();
                    Set_Back();
                }
               
            }
            else if (action_num == 2)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_longjump();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_longjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 3)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_sliding();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_sliding();
                    Set_Back();
                }
              
            }
        }

        
    }

    void Set_Back()
    {
        //アニメーション（泣）一応動き中に反応しないのも兼ねてる便利
        now_ani = true;

        //エフェクトは止める
        EF_script.SetActive(false);

        //アニメーション（泣）
        //Invoke(nameof(null_active), 1.15f);
        //Button.SetActive(false);

        //バックにマルチ自身を登録
        script.objs[script.now] = this.gameObject;
        script.now++;
       
        Debug.Log("thornHit(up)!");

        //消えた時初期位置に戻る
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);

        //バックにマルチ自身を登録
        //script.objs[script.now] = this.gameObject;
        //script.now++;

        //実行したアクションを最低数として設定
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().set_text(action_num+4,1);
        //実行したアクションを削除予定に追加
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().multi_des_Check(this.gameObject, true);
    }

    //アニメーション（泣）
    void null_active()
    {
        Button.SetActive(false);
    }

    //戻すとき
    public void Set_Active(bool a)
    {
        ////アニメーション（泣）と反応するように
        now_ani = false;

        //また再開（爆発はなし）
        EF_script.SetActive(true);

        //初期状態へ---------------------------------------------------------------------------
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.transform.localScale = new Vector3(1, 1,1);

        //実行したアクションを最低数として設定
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().set_text(action_num + 4, -1);
        //実行したアクションを削除予定分をなくす
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().multi_des_Check(this.gameObject, false);

        GetComponent<Image_multimove>().Move_on = false;
        GetComponent<Image_multimove>().time = 0;
        Debug.Log("thornHit(up)!");
        Button.SetActive(a);
        //---------------------------------------------------------------------------------------
    }
    //Eff消すよう
    public void Eff_active()
    {
        //エフェクトは止める
        EF_script.SetActive(false);
    }
}
