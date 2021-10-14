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
    private Vector3 push;//加算したいベクトル量
    private int Jump_Count = 0;//連続でジャンプした回数をカウント
    private bool Jump_Flag = true;//ジャンプしているかのフラグ

    // Start is called before the first frame update
    void Start() {
        push = new Vector3(0.0f, push_power, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        float inputX;
        float inputZ;

        if (auto_move == false) {
            //左右操作
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
        }
        else {
            inputX = 0;
            inputZ = 1;
        }
        float moveX = inputX * moveSpeed * Time.deltaTime;
        float moveZ = inputZ * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);

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
        
        if(collision.gameObject.tag=="Wall") {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.2f, 0.0f), ForceMode.Impulse);
        }

    }
}
