using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定----------------------------------------------------
    [Header("自動操作モード")]
    public bool auto_move;
    [Header("移動速度")]
    public float moveSpeed;
    [Header("ジャンプ力")]
    public float push_power;
    [Header("最大ジャンプ回数")]
    public int Max_Jmup;

    //private変数--------------------------------------------------------------
    private Vector3 push;           //加算したいベクトル量
    private int Jump_Count = 0;     //連続でジャンプした回数をカウント
    private bool Jump_Flag = true;  //ジャンプしているかのフラグ
    private float inputX = 0;       //X軸の移動ベクトル
    private float inputZ = 1;       //Z軸の移動ベクトル


    // Start is called before the first frame update
    void Start() {
        push = new Vector3(0.0f, push_power, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {

        //移動処理
        MOVE(inputX, inputZ);

        //ジャンプ操作
        if (transform.position.y <= 1.0f || Max_Jmup - 1 != Jump_Count) {
            if (Input.GetKey(KeyCode.Space)) {
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
        if(transform.position.y<=1.0f) {
            Jump_Count = 0;
        }
    }

    void OnCollisionStay(Collision collision) {
        //壁に触れているとY軸への力付与
        if(collision.gameObject.tag=="Wall") {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.2f, 0.0f), ForceMode.Impulse);
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
}