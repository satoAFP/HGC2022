using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定----------------------------------------------------
    [Header("自動操作モード")]
    public bool auto_move;
    [Header("カード最大選択回数")]
    public int Max_Card;

    [Header("移動速度")]
    public float moveSpeed;
    [Header("ジャンプ力")]
    public float push_power;
    [Header("最大ジャンプ回数")]
    public int Max_Jmup;

    //private変数--------------------------------------------------------------
    private Vector3 push;               //加算したいベクトル量
    private int Jump_Count = 0;         //連続でジャンプした回数をカウント
    private bool Jump_Flag = true;      //ジャンプしているかのフラグ
    private float inputX = 0;           //X軸の移動ベクトル
    private float inputZ = 1;           //Z軸の移動ベクトル
    private int Select_order = 0;       //ボタンを押された順番を記憶
    private int Action_count = 0;       //アクションをした回数をカウント
    private bool Action_check = false;  //アクションを一回しか使えないよう管理
    private bool Movestop = false;      //アクションを選択するとき主人公を止める用
    private int[] Card_order;           //カードを選択した順番を記憶

    //構造体-------------------------------------------------------------------
    //ボタン使用時周り
    private struct Buttan
    {
        public bool push;      //ボタンが押されたかの判定
        public int push_num;   //押された時の順番記憶
        //初期化用関数
        public Buttan(bool a, int b) {
            push = a;
            push_num = b;
        }
    }
    //構造体の初期化
    Buttan jump = new Buttan(false, 0);
    Buttan squat = new Buttan(false, 0);
    Buttan stick = new Buttan(false, 0);
    Buttan stop = new Buttan(false, 0);

    //列挙----------------------------------------------------------------------
    //カードの種類
    public enum Card
    {
        JUMP,
        SQUAT,
        STICK,
        STOP
    }
    

    // Start is called before the first frame update
    void Start() {
        //初期化
        push = new Vector3(0.0f, push_power, 0.0f);
        Card_order = new int[Max_Card];
    }

    // Update is called once per frame
    void FixedUpdate() {
        //アクションブロックに到達するといったん止める処理
        if (Movestop == false) {
            
            //移動処理
            MOVE(inputX, inputZ);

            //ジャンプを選択したとき--------------------------------------------------------------
            if (jump.push_num == Action_count && Action_check == true) {
                //ジャンプ操作
                if (transform.position.y <= 1.0f || Max_Jmup - 1 != Jump_Count) {
                    if (jump.push == true) {
                        if (Jump_Flag == true) {
                            this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
                            Jump_Count++;
                            Jump_Flag = false;
                        }
                    }
                    else {
                        Jump_Flag = true;
                    }
                }
                if (transform.position.y <= 1.0f) {
                    Jump_Count = 0;
                }
                jump.push = false;
                Action_check = false;
            }

            //しゃがみを選択したとき--------------------------------------------------------------
            if (squat.push_num == Action_count && Action_check == true) {
                this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                squat.push = false;
                Action_check = false;
            }

            //くっつきを選択したとき--------------------------------------------------------------
            if (stick.push_num == Action_count && Action_check == true) {


                stick.push = false;
                Action_check = false;
            }

            //ストップを選択したとき--------------------------------------------------------------
            if (stop.push_num == Action_count && Action_check == true) {


                stop.push = false;
                Action_check = false;
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        //壁に触れているとY軸への力付与
        if(collision.gameObject.tag=="Wall") {
            transform.Translate(0.0f, 0.15f, 0.0f);
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

        //アクションブロックに乗るたびカウント
        if (collision.gameObject.tag == "Action") {
            Action_count++;
            Action_check = true;
            Movestop = true;
            collision.gameObject.SetActive(false);
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
        float moveX = x * moveSpeed * Time.deltaTime;
        float moveZ = z * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);
    }

    //ボタンでの操作選択----------------------------------------------------------------
    public void Push_jump() {
        jump.push = true;
        Card_order[Select_order] = (int)Card.JUMP;  //ボタンを押した順番を記憶
        Select_order++;                             //順番を進める用
        jump.push_num = Select_order;               //押された順番とアクションブロックを踏んだ回数を一致させる
    }

    public void Push_squat() {
        squat.push = true;
        Card_order[Select_order] = (int)Card.SQUAT;
        Select_order++;
        squat.push_num = Select_order;
    }

    public void Push_stick() {
        stick.push = true;
        Card_order[Select_order] = (int)Card.STICK;
        Select_order++;
        stick.push_num = Select_order; 
    }

    public void Push_stop() {
        stop.push = true;
        Card_order[Select_order] = (int)Card.STOP;
        Select_order++;
        stop.push_num = Select_order;
    }

    //アクション開始ボタン
    public void Push_start() {
        Movestop = false;
    }

   

}