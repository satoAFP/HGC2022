using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //インスペクターで設定----------------------------------------------------
    [Header("自動操作モード")]
    public bool auto_move;

    [Header("主人公が壁に触れているときの判定オブジェクト")]
    public GameObject[] Around_collision;

    [Header("カード最大選択回数")]
    public int Max_Card;

    [Header("カードの種類数")]
    public int Kind_Card;

    [Header("移動速度")]
    public float moveSpeed;

    [Header("走る速度(移動速度の何倍か)")]
    public float runSpeed;

    [Header("前方向への力(幅跳び、スライディングで使用)")]
    public float flontMove;

    [Header("ジャンプ力")]
    public float push_power;

    [Header("ハイジャンプ力(ジャンプの何倍か)")]
    public float highjump_power;

    [Header("最大ジャンプ回数")]
    public int Max_Jmup;

    [Header("選択されたアクション表示用テキスト")]
    public Text Select_text;

    //private変数--------------------------------------------------------------
    private Vector3 push;               //加算したいベクトル量
    private float inputX = 0;           //X軸の移動ベクトル
    private float inputZ = 1;           //Z軸の移動ベクトル
    private int Select_order = 0;       //ボタンを押された順番を記憶
    private bool[] Action_check;        //アクションを一回しか使えないよう管理
    private bool Movestop = true;       //アクションを選択するとき主人公を止める用
    private int[] Card_order;           //カードを選択した順番を記憶
    private bool wall_stick = false;    //壁にくっつける状態
    private float walljump = 0.0f;      //壁ジャンプするときのジャンプ力
    private bool walljump_check = false;//壁ジャンプかどうか判断
    private int walljump_time = 100;    //横移動する時間
    private float run_power = 1;        //移動速度代入
    private Vector3 flont_push;         //移動方向へより力を加える(幅跳びで使用)
    private Vector3 flont_sliding;      //移動方向へより力を加える(スライディングで使用)
    private string[] text_data;         //アクション内容格納変数
    private bool select_time = true;    //開始ボタンを押すと、カード選択できない
    private bool safe_flag = true;     //死亡判定用フラグ

    //構造体-------------------------------------------------------------------
    //ボタン使用時周り
    //private struct Buttan
    //{
    //    public bool push;      //ボタンが押されたかの判定
    //    public int push_num;   //押された時の順番記憶
    //    //初期化用関数
    //    public Buttan(bool a, int b) {
    //        push = a;
    //        push_num = b;
    //    }
    //}
    ////構造体の初期化
    //Buttan jump = new Buttan(false, 0);
    //Buttan squat = new Buttan(false, 0);
    //Buttan stick = new Buttan(false, 0);
    //Buttan stop = new Buttan(false, 0);

    //列挙----------------------------------------------------------------------
    //カードの種類
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
    void Start() {
        //初期化
        push = new Vector3(0.0f, push_power, 0.0f);
        Card_order = new int[Max_Card];
        for (int i = 0; i < Max_Card; i++)  {
            Card_order[i] = -1;
        }
        Action_check = new bool[Kind_Card];
        text_data = new string[Max_Card];
        for (int i = 0; i < Max_Card; i++) {
            text_data[i] = "";
        }
    }

    // Update is called once per frame
    void FixedUpdate() {

        //選んだアクションをtext_dataに格納
        for (int i = 0; i < Select_order; i++)  {
            if(Card_order[i] == (int)Card.JUMP) {
                text_data[i] = "ジャンプ → ";
            }
            if (Card_order[i] == (int)Card.SQUAT) {
                text_data[i] = "しゃがみ → ";
            }
            if (Card_order[i] == (int)Card.STICK) {
                text_data[i] = "くっつく → ";
            }
            if (Card_order[i] == (int)Card.RUN) {
                text_data[i] = "走る → ";
            }
            if (Card_order[i] == (int)Card.HIGHJUMP) {
                text_data[i] = "ハイジャンプ → ";
            }
            if (Card_order[i] == (int)Card.WALLKICK) {
                text_data[i] = "壁キック → ";
            }
            if (Card_order[i] == (int)Card.LONGJUMP) {
                text_data[i] = "幅跳び → ";
            }
            if (Card_order[i] == (int)Card.SLIDING) {
                text_data[i] = "スライディング → ";
            }
        }
        //選択したアクション実際表示
        Select_text.text = "" + text_data[0] + text_data[1] + text_data[2] + text_data[3]
                 + text_data[4] + text_data[5] + text_data[6] + text_data[7];


        //幅跳び、スライディングで使用する移動量の向き変更
        if (inputX == -1) {
            flont_push = new Vector3(-flontMove, push_power, 0.0f);
            flont_sliding = new Vector3(-flontMove, 0.0f, 0.0f);
        }
        if (inputX == 1) {
            flont_push = new Vector3(flontMove, push_power, 0.0f);
            flont_sliding = new Vector3(flontMove, 0.0f, 0.0f);
        }
        if (inputZ == -1) {
            flont_push = new Vector3(0.0f, push_power, -flontMove);
            flont_sliding = new Vector3(0.0f, 0.0f, -flontMove);
        }
        if (inputZ == 1) {
            flont_push = new Vector3(0.0f, push_power, flontMove);
            flont_sliding = new Vector3(0.0f, 0.0f, flontMove);
        }


        //最初アクションを選択するときと、セレクトブロックに到達するといったん止める処理
        if (Movestop == false) {

            //移動処理
            MOVE(inputX, inputZ);

            //Select_orderが-1のとき配列がエラーを起こすので
            if (Select_order != -1) {

                //ジャンプを選択したとき--------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.JUMP && Action_check[(int)Card.JUMP] == true) {
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                    //ジャンプ処理終了
                    Action_check[(int)Card.JUMP] = false;
                }


                //しゃがみを選択したとき--------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.SQUAT && Action_check[(int)Card.SQUAT] == true) {
                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                }


                //くっつきを選択したとき--------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.STICK && Action_check[(int)Card.STICK] == true) {
                    //左に壁がある処理
                    if (Around_collision[0].GetComponent<Around_collider>().wall_check == true)
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                    else
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    //右に壁がある処理
                    if (Around_collision[1].GetComponent<Around_collider>().wall_check == true)
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                    else
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    //前に壁がある処理
                    if (Around_collision[2].GetComponent<Around_collider>().wall_check == true)
                        wall_stick = true;
                    else
                        wall_stick = false;
                }


                //走るを選択したとき------------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.RUN && Action_check[(int)Card.RUN] == true) {

                    //倍率が変わる
                    run_power = runSpeed;

                    Action_check[(int)Card.RUN] = false;
                }


                //ハイジャンプを選択したとき----------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.HIGHJUMP && Action_check[(int)Card.HIGHJUMP] == true) {
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push * highjump_power, ForceMode.Impulse);

                    Action_check[(int)Card.HIGHJUMP] = false;
                }


                //壁キックを選択したとき--------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.WALLKICK && Action_check[(int)Card.WALLKICK] == true) {
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                    //左に壁がある処理
                    if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = 0.1f;
                    }
                    //右に壁がある処理
                    if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = -0.1f;
                    }

                    Action_check[(int)Card.WALLKICK] = false;
                }
                //壁ジャンプ処理
                if (walljump_check == true) {
                    if (walljump_time != 0) {
                        transform.Translate(walljump, 0.0f, 0.0f);
                        if (walljump < 0)
                            walljump += 0.001f;
                        else
                            walljump -= 0.001f;
                    }
                    else
                        walljump_check = false;
                    walljump_time--;
                }


                //幅跳びを選択したとき----------------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.LONGJUMP && Action_check[(int)Card.LONGJUMP] == true) {
                    this.GetComponent<Rigidbody>().AddForce(flont_push, ForceMode.Impulse);
                    Action_check[(int)Card.LONGJUMP] = false;
                }

                //スライディングを選択したとき--------------------------------------------------------------------------------------
                if (Card_order[Select_order] == (int)Card.SLIDING && Action_check[(int)Card.SLIDING] == true) {
                    this.GetComponent<Rigidbody>().AddForce(flont_sliding, ForceMode.Impulse);
                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                    Action_check[(int)Card.SLIDING] = false;
                }
            }
        }
    }
    

    void OnCollisionEnter(Collision collision) {
        //自動移動設定時、このオブジェクトに触れると、指定方向に移動する
        if (collision.gameObject.tag == "Move_direction") {
            switch(collision.gameObject.GetComponent<Direction>().direction) {
                case 1://左
                    inputX = -1;
                    inputZ = 0;
                    break;
                case 2://右
                    inputX = 1;
                    inputZ = 0;
                    break;
                case 3://前
                    inputZ = 1;
                    inputX = 0;
                    break;
                case 4://後
                    inputZ = -1;
                    inputX = 0;
                    break;
            }
        }

        //アクションを選択した順番に実行される
        if (collision.gameObject.tag == "Action") {
            //collision.gameObject.SetActive(false);//一度乗ったアクションブロックは消す

            Select_order++;//アクション内容を一つ進める

            //元のサイズに戻す
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //しゃがみ状態解除
            Action_check[(int)Card.SQUAT] = false;

            //くっつき状態解除
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;

            //走る状態解除
            run_power = 1.0f;

            //次のアクションのフラグをtrueにする
            switch (Card_order[Select_order]) {
                case (int)Card.JUMP:
                    Action_check[(int)Card.JUMP] = true;
                    break;
                case (int)Card.SQUAT:
                    Action_check[(int)Card.SQUAT] = true;
                    break;
                case (int)Card.STICK:
                    Action_check[(int)Card.STICK] = true;
                    break;
                case (int)Card.RUN:
                    Action_check[(int)Card.RUN] = true;
                    break;
                case (int)Card.HIGHJUMP:
                    Action_check[(int)Card.HIGHJUMP] = true;
                    break;
                case (int)Card.WALLKICK:
                    Action_check[(int)Card.WALLKICK] = true;
                    break;
                case (int)Card.LONGJUMP:
                    Action_check[(int)Card.LONGJUMP] = true;
                    break;
                case (int)Card.SLIDING:
                    Action_check[(int)Card.SLIDING] = true;
                    break;
            }
        }

        //アクション再選択
        if (collision.gameObject.tag == "Select") {
            //動きを止める
            Movestop = true;

            //再選択できる
            select_time = true;

            //順番を最初からにする
            Select_order = 0;

            //アクション選択の中身初期化
            for (int i = 0; i < Max_Card; i++) {
                Card_order[i] = -1;
            }

            //アクション表示テキストの中身初期化
            for (int i = 0; i < Max_Card; i++) {
                text_data[i] = "";
            }
        }


        //加速床の処理
        if (collision.gameObject.tag == "Acceleration") {
            this.GetComponent<Rigidbody>().AddForce(flont_sliding, ForceMode.Impulse);
        }

    }


    void OnCollisionStay(Collision collision) {
        //くっつき状態の時、壁に触れているとY軸への力付与
        if (collision.gameObject.tag == "Wall") {
            if (wall_stick == true) {
                transform.Translate(0.0f, 0.2f, 0.0f);
            }
        }
    }


    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "safe_zone") {
            Debug.Log("aaa");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    //移動処理関数
    private void MOVE(float x, float z) {
        if (auto_move == false) {
            //左右操作
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        //速度の設定
        float moveX = x * moveSpeed * Time.deltaTime * run_power;
        float moveZ = z * moveSpeed * Time.deltaTime * run_power;
        transform.Translate(moveX, 0.0f, moveZ);
    }

    //ボタンでの操作選択----------------------------------------------------------------
    //通常アクション-----------------------------------
    public void Push_jump() {//ジャンプボタン
        if (select_time) {
            Card_order[Select_order] = (int)Card.JUMP;  //ボタンを押した順番を記憶
            Select_order++;                             //順番を進める用
        }
    }

    public void Push_squat() {//しゃがみボタン
        if (select_time) {
            Card_order[Select_order] = (int)Card.SQUAT;
            Select_order++;
        }
    }

    public void Push_stick() {//くっつきボタン
        if (select_time) {
            Card_order[Select_order] = (int)Card.STICK;
            Select_order++;
        }
    }

    public void Push_run() {//走るボタン
        if (select_time) {
            Card_order[Select_order] = (int)Card.RUN;
            Select_order++;
        }
    }

    //合体アクション------------------------------------
    public void Push_highjump() {//しゃがみ+ジャンプ＝ハイジャンプ
        if (select_time) {
            Card_order[Select_order] = (int)Card.HIGHJUMP;
            Select_order++;
        }
    }

    public void Push_wallkick() {//くっつき+ジャンプ＝壁キック
        if (select_time) {
            Card_order[Select_order] = (int)Card.WALLKICK;
            Select_order++;
        }
    }

    public void Push_longjump() {//走る+ジャンプ＝幅跳び
        if (select_time) {
            Card_order[Select_order] = (int)Card.LONGJUMP;
            Select_order++;
        }
    }

    public void Push_sliding() {//しゃがみ+走る＝スライディング
        if (select_time) {
            Card_order[Select_order] = (int)Card.SLIDING;
            Select_order++;
        }
    }

    //アクション開始ボタン
    public void Push_start() {
        Movestop = false;   //アクションループのメイン部分を動かす
        Select_order = -1;  //アクションブロックに乗った時、最初に加算されてしまうから-1
        select_time = false;//アクション開始するとカードを選択できない
    }

    //選択したアクションを一つ戻す
    public void Push_back() {
        //0番目が最初のアクションなのでそれ未満にはならない
        if (Select_order > 0) {
            Select_order -= 1;
            Card_order[Select_order] = -1;
            text_data[Select_order] = "";   //カードテキストの中身消去
        }
    }

    public void check() {
        for(int i=0;i<Select_order;i++) {
            Debug.Log($"{Card_order[i]}");
        }
    }


}