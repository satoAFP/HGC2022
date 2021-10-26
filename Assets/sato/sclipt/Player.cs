using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定----------------------------------------------------
    [Header("自動操作モード")]
    public bool auto_move;

    [Header("主人公が壁に触れているときの判定オブジェクト")]
    public GameObject[] Around_collision;

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
    private float inputX = 0;           //X軸の移動ベクトル
    private float inputZ = 1;           //Z軸の移動ベクトル
    private int Select_order = 0;       //ボタンを押された順番を記憶
    private bool[] Action_check;        //アクションを一回しか使えないよう管理
    private bool Movestop = true;       //アクションを選択するとき主人公を止める用
    private int[] Card_order;           //カードを選択した順番を記憶
    private bool wall_stick = false;    //壁にくっつける状態
    private float walljump = 0.0f;      //壁ジャンプするときのジャンプ力
    private bool walljump_check = false;//壁ジャンプかどうか判断
    private int walljump_time = 100;     //横移動する時間

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
        STOP
    }


    // Start is called before the first frame update
    void Start() {
        //初期化
        push = new Vector3(0.0f, push_power, 0.0f);
        Card_order = new int[Max_Card];
        Action_check = new bool[4];
    }

    // Update is called once per frame
    void FixedUpdate() {
        //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //アクションブロックに到達するといったん止める処理
        if (Movestop == false) {
            
            //移動処理
            MOVE(inputX, inputZ);

            //ジャンプを選択したとき--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.JUMP && Action_check[(int)Card.JUMP] == true) {
                //ジャンプさせる処理
                this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) {
                    walljump_check = true;
                    walljump = 0.1f;
                }
                //右に壁がある処理
                if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) {
                    walljump_check = true;
                    walljump = -0.1f;
                }

                //ジャンプ処理終了
                Action_check[(int)Card.JUMP] = false;
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

            //しゃがみを選択したとき--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.SQUAT && Action_check[(int)Card.SQUAT] == true) {
                this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            }
            else {
                this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            

            //くっつきを選択したとき--------------------------------------------------------------
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

            //ストップを選択したとき--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.STOP && Action_check[(int)Card.STOP] == true) {


                
                Action_check[(int)Card.STOP] = false;
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        //くっつき状態の時、壁に触れているとY軸への力付与
        if(collision.gameObject.tag=="Wall") {
            if (wall_stick == true) {
                transform.Translate(0.0f,0.2f,0.0f);
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
            collision.gameObject.SetActive(false);//一度乗ったアクションブロックは消す
            Select_order++;//アクション内容を一つ進める

            //しゃがみ状態解除
            Action_check[(int)Card.SQUAT] = false;
            //くっつき状態解除
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;

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
                case (int)Card.STOP:
                    Action_check[(int)Card.STOP] = true;
                    break;
            }
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
    public void Push_jump() {//ジャンプボタン
        Card_order[Select_order] = (int)Card.JUMP;  //ボタンを押した順番を記憶
        Select_order++;                             //順番を進める用
    }

    public void Push_squat() {//しゃがみボタン
        Card_order[Select_order] = (int)Card.SQUAT;
        Select_order++;
    }

    public void Push_stick() {//くっつきボタン
        Card_order[Select_order] = (int)Card.STICK;
        Select_order++;
    }

    public void Push_stop() {//ストップボタン
        Card_order[Select_order] = (int)Card.STOP;
        Select_order++;
    }

    //アクション開始ボタン
    public void Push_start() {
        Movestop = false;//アクションループのメイン部分を動かす
        Select_order = -1;//アクションブロックに乗った時、最初に加算されてしまうから-1
    }

    public void check() {
        for(int i=0;i<3;i++) {
            Debug.Log($"{Card_order[i]}");
        }
    }


}