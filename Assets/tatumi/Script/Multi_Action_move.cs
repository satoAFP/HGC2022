using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    //番号で何するか決める
    [Header("合体番号")]
    public int action_num;

    [Header("複数対象数")]
    public GameObject[] players;

    GameObject player; //参照元OBJそのものが入る変数

    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得
    private Vector3 pos;
    private float first_x;//初期位置

    private bool now_ani,Ps_flag;

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
                    player.GetComponent<Player>().Push_highjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 1)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_highjump();
                        Set_Back();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_wallkick();
                    Set_Back();
                }
               
            }
            else if (action_num == 2)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_highjump();
                        Set_Back();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_longjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 3)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_highjump();
                        Set_Back();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_sliding();
                    Set_Back();
                }
              
            }
        }

        
    }

    void Set_Back()
    {
        //アニメーション（泣）一応動き中に反応しないのも兼ねてる便利
        now_ani = true;

        //アニメーション（泣）
        //Invoke(nameof(null_active), 1.15f);
        //Button.SetActive(false);

        //消えた時初期位置に戻る
        this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

        //バックにマルチ自身を登録
        script.objs[script.now] = this.gameObject;
        script.now++;
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

        //初期状態へ---------------------------------------------------------------------------
        this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);
        this.transform.localScale = new Vector3(1, 1,1);

        GetComponent<Image_multimove>().Move_on = false;
        GetComponent<Image_multimove>().time = 0;

        Button.SetActive(a);
        //---------------------------------------------------------------------------------------
    }
}
