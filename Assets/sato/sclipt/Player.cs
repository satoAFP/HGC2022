using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------------------------
    [Header("�������샂�[�h")]
    public bool auto_move;
    [Header("�ړ����x")]
    public float moveSpeed;
    [Header("�W�����v��")]
    public float push_power;
    [Header("�ő�W�����v��")]
    public int Max_Jmup;

    //private�ϐ�--------------------------------------------------------------
    private Vector3 push;           //���Z�������x�N�g����
    private int Jump_Count = 0;     //�A���ŃW�����v�����񐔂��J�E���g
    private bool Jump_Flag = true;  //�W�����v���Ă��邩�̃t���O
    private float inputX = 0;       //X���̈ړ��x�N�g��
    private float inputZ = 1;       //Z���̈ړ��x�N�g��

    // Start is called before the first frame update
    void Start() {
        push = new Vector3(0.0f, push_power, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (auto_move == false) {
            //���E����
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
        }

        float moveX = inputX * moveSpeed * Time.deltaTime;
        float moveZ = inputZ * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);

        //�W�����v����
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

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Move_direction") {
            //collision.gameObject.GetComponent<"">

        }

    }
}
