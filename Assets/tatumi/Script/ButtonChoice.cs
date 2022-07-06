using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    //参照元OBJ&script保存----------------------------------------
    public GameObject BackButton; 
    DeletAction script;

    public GameObject ActionButton; 
    ActionButton_SC scriptac; 

    GameObject player; //script無
    //------------------------------------------------------------

    //スッーとCard消えるよう変数（現在凍結）
    //public bool vanish;
    //private bool now_ani;

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得用
    public Vector3 pos;
    private float first_x;//初期位置
    //--------------------------------

    //複数人対応
    private GameObject[] players;
    //プレイヤー側に命令を送る用(複数人判別)
    private bool Ps_flag;

    //スライムに吸い込まれる番号（位置）
    [SerializeField]
    private int move_num;

    //SEオブジェクト取得
    public AudioClip sound1;
    private GameObject SE_Maneger;
    AudioSource audioSource;

    //アニメーション保存
    Animator myanim;

    // Start is called before the first frame update
    void Start()
    {
        //削除OBjとの連携
        BackButton = GameObject.Find("BackButton"); //オブジェクトの名前から取得して変数に格納する
        script = BackButton.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する

        //ActionButtonとの連携
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する

        //最初に出た位置を覚える（戻る処理に使う）
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //スッーと消えるよう変数（現在凍結）
        //vanish = true;
        //now_ani = false;

        //PL
        player = GameObject.Find("Player"); 
        //複数取得
        players = GameObject.FindGameObjectsWithTag("Player");

        //複数対象処理のオンオフ
        if (players.Length > 1)
        {
            Ps_flag = true;
        }
        else
        {
            Ps_flag = false;
        }

        //SE系取得
        SE_Maneger = GameObject.Find("SE_manager"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        audioSource = SE_Maneger.GetComponent<AudioSource>();

        //自身のアニメーション取得
         myanim= this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    //Buttonが押された時
    public void PushButton(bool set)
    {
        //左クリの場合
        if (Input.GetMouseButtonDown(0))
        {
            //消える処理（凍結）
            //now_ani = true;

            //入力処理(RUN)
            if (move_num == 3)
            {
                //複数人いるなら
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_run();
                    }

                }
                //一人のみ
                else
                {
                    player.GetComponent<Player>().Push_run();
                }


            }
            //STICK
            else if (move_num == 2)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_stick();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_stick();
                }

            }
            //SQUAT
            else if (move_num == 1)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_squat();
                    }
                }
                else
                {

                    player.GetComponent<Player>().Push_squat();
                }

            }
            //JUMP
            else if (move_num == 0)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_jump();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_jump();

                }

            }

            //自身の子供から複製対象選択＆動かす-------------------------------

            //現在位置取得
            pos = this.gameObject.transform.position;

            //image_moveを取得（自身の子供が一番早い）
            GameObject child = transform.Find("image_move").gameObject;

            //複製、動かす信号を発信
            GameObject newObj = Instantiate(child, ActionButton.transform, false);
            newObj.GetComponent<Image_move>().Move_on = true;


            newObj.GetComponent<Image_move>().parent_pos = pos;

            //------------------------------------------------------------------


            //スッーと消えるよう変数（現在凍結）--------------------------------
            //var animator = Button.GetComponent<Animator>();
            //animator.Play("Selected");
            //animator.Update(0f);

            //消す処理をscriptに一任
            //if (set==false)
            //Invoke(nameof(null_active), 1.15f);
            //else
            //    Button.SetActive(false);
            //vanish = true;

            //---------------------------------------------------------------------


            if (pos.x > 0.0f)
            {
                //煙エフェクトを検索（位置により変更）
                GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                efe.GetComponent<Effect_move>().SetActive(false);
                efe.GetComponent<Effect_move>().first_EF = false;
                efe.GetComponent<Effect_move>().now_onecard = false;
            }

            //消えた時初期位置に戻る
            this.gameObject.transform.position = new Vector3(first_x, -365.0f, pos.z);
            this.tag = "Untagged";

            //バックボタンに登録
            script.objs[script.now] = this.gameObject;
            script.now++;

            //使用回数+1
            scriptac.set_text(move_num, 1);
        }
        //右クリ
        else if (Input.GetMouseButtonDown(1) && set == false)
        {
            //現在位置取得
            pos = this.gameObject.transform.position;
            //現在いるマルチを全取得
            GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

            //移動先にmultiCardがいないなら
            if (multi.Length == 0)
            {
                //規定値よりも手前なら
                if (pos.x >= first_x - 1.0f && pos.x <= first_x + 1.0f)
                {
                    this.gameObject.transform.position = new Vector3(17.7f, pos.y, pos.z);
                    audioSource.PlayOneShot(sound1);
                }
                //場所がないので初期位置へ
                else if (pos.x > 0.0f)
                {
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

                    //煙エフェクトを検索（位置により変更）
                    GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                    efe.GetComponent<Effect_move>().SetActive(false);
                    efe.GetComponent<Effect_move>().first_EF = false;
                    efe.GetComponent<Effect_move>().now_onecard = false;

                    audioSource.PlayOneShot(sound1);
                }
            }
            //いるなら
            else if (multi.Length == 1)
            {
                //もとへ返す
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
            }


            //移動後の場所取得
            pos = this.gameObject.transform.position;

            //それぞれの場所でtag付与（マルチクリエイトへ）
            if (pos.x > 0.0f)
            {
                this.tag = "Multi_action1";
            }
            else
                this.tag = "Untagged";
        }
        //真ん中クリ
        else if (Input.GetMouseButtonDown(2))
        {
            //初期位置へ（タグも再付与）
            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
            this.tag = "Untagged";

            //煙エフェクトを検索（位置により変更）
            GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

            efe.GetComponent<Effect_move>().SetActive(false);
            efe.GetComponent<Effect_move>().first_EF = false;
            efe.GetComponent<Effect_move>().now_onecard = false;

            audioSource.PlayOneShot(sound1);
        }
        //他スクリプトより申請時
        else if (set == true)
        {
            //マルチクリエイトからの申請
            //自身の消えた処理と同じ-------------------以下同文
            script.objs[script.now] = this.gameObject;
            script.now++;

            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);

            //タグも初期化
            this.tag = "Untagged";

            //数を増やす
            scriptac.set_text(move_num, 1);
        }

        else if (set == false)
        {
            //バックで戻されたとき、数を戻す
            scriptac.set_text(move_num, -1);

        }
    }

    //他スクリプトからいじるようかんすう
    public void Set_Active(bool set)
    {
        //フェード処理（凍結）
        //now_ani = false;
        PushButton(set);
    }

    //初期位置に戻らす
    public void Set_Back()
    {
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.tag = "Untagged";
    }

    //カーソルが重なっていたら
    public void Choiceing(bool a)
    {
        myanim.SetBool("choiceing", a);
    }
}
