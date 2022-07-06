using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    //格納用OBJ群-----------------------------

    //「戻る」処理関連
    public GameObject BackButton; 
    DeletAction script; 

    //選択回数処理関連
    public GameObject ActionButton; 
    ActionButton_SC scriptac; 

    //行動命令用
    GameObject player; 
    //----------------------------------------

    //スッーと消えるよう変数（現在凍結）
    //public bool vanish;

    //現在ANIM中
    private bool now_ani;

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得
    public Vector3 pos;
    private float first_x;//初期位置

    //複数人対応
    private GameObject[] players;

    //PLが複数いるか用flag
    private bool Ps_flag;

    //スライムに吸い込まれる番号（位置）
    private int move_num;

    //SEオブジェクト取得
    public AudioClip sound1;
    private GameObject SE_Maneger;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //scriptとObj格納---------------------------------------------
        BackButton = GameObject.Find("BackButton"); 
        script = BackButton.GetComponent<DeletAction>(); 

        ActionButton = GameObject.Find("ActionBotton"); 
        scriptac = ActionButton.GetComponent<ActionButton_SC>();
        //------------------------------------------------------------

        //最初に出た位置を覚える（戻る処理に使う）
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //スッーと消えるよう変数（現在凍結）
        //vanish = true;
        //now_ani = false;

        //PL
        player = GameObject.Find("Player"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
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

        //最初の一から自身の行動命令が何かを取得
        if ((-first_x / 26) < 1)
            move_num = 3;
        else if ((-first_x / 26) < 2)
            move_num = 2;
        else if ((-first_x / 26) < 3)
            move_num = 1;
        else if ((-first_x / 26) < 1)
            move_num = 0;

        //効果音関連
        SE_Maneger = GameObject.Find("SE_manager"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        audioSource = SE_Maneger.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void PushButton(bool set)
    {
        //Anim中じゃなければ行動(凍結)
        //if (now_ani == false)

        //ボタン押したときの処理-----------------------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            //入力処理(RUN)
            if ((-first_x / 26) < 1)
            {
                //PLが複数いるか判別,命令送信
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_run();
                    }

                }
                else
                {
                    player.GetComponent<Player>().Push_run();
                }


            }
            //STICK
            else if (-(first_x / 26) < 2)
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
            else if ((-first_x / 26) < 3)
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
            else if ((-first_x / 26) < 4)
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


            //image_moveを取得（自身の子供が一番早い）
            GameObject child = transform.Find("image_move").gameObject;

            //複製、動かす信号を発信
            GameObject newObj = Instantiate(child, ActionButton.transform, false);
            newObj.GetComponent<Image_move>().Move_on = true;

            //分身の出現位置を調整
            newObj.GetComponent<Image_move>().parent_posx = -85.5f + (28.0f * move_num);
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

            //現在位置取得
            pos = this.gameObject.transform.position;

            //初期位置いるならがった稲荷かけの状態を解除
            if (pos.x > 0.0f)
            {
                //煙エフェクトを検索（位置により変更）
                GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                efe.GetComponent<Effect_move>().SetActive(false);
                efe.GetComponent<Effect_move>().first_EF = false;
                efe.GetComponent<Effect_move>().now_onecard = false;
            }

            //消えた時初期位置に戻る
            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
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

            //MultiCardがいるか取得(飛んでる途中のやつもカウントするため)
            GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

            //いないとき
            if (multi.Length == 0)
            {
                //合体枠へ移動（初期位置からの移動）
                if (pos.x >= first_x - 1.0f && pos.x <= first_x + 1.0f)
                {
                    this.gameObject.transform.position = new Vector3(17.7f, pos.y, pos.z);
                    audioSource.PlayOneShot(sound1);
                }
                //初期位置へ移動（合体枠からの移動）
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
            //いたときは問答無用で返す
            else if (multi.Length == 1)
            {
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
            }


            //移動後の場所取得
            pos = this.gameObject.transform.position;

            //それぞれの場所でtag付与（マルチクリエイトへ）
            if (pos.x > 0.0f)
            {
                //合体カード待機状態
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

            //SE鳴らす
            audioSource.PlayOneShot(sound1);
        }
        //---------------------------------------------------------------
        //他スクリプトより申請時-----------------------------------------
        else if (set == true)
        {
            //マルチクリエイトからの申請
            //自身の消えた処理と同じ-------------------以下同文
            script.objs[script.now] = this.gameObject;
            script.now++;

            //初期位置へ移動
            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
            //合体モード解除
            this.tag = "Untagged";
            //合体分使ったので+1
            scriptac.set_text(move_num, 1);
        }
        else if (set == false)
        {
            //バックで戻されたとき、数を戻す
            scriptac.set_text(move_num, -1);

        }
        //-------------------------------------------------------------------
        
       
    }

   
    //他スクリプトからいじるようかんすう
    public void Set_Active(bool set)
    {
        //アニメーション用（凍結）
        //now_ani = false;

        //他スクリプトからの申請時の行動
        PushButton(set);
    }

    //初期位置に戻らす
    public void Set_Back()
    {
        //初期位置へ戻し、通常の状態へ戻す
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.tag = "Untagged";
    }

}
