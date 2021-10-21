using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------------------------
    [Header("�������샂�[�h")]
    public bool auto_move;
    [Header("�J�[�h�ő�I����")]
    public int Max_Card;

    [Header("�ړ����x")]
    public float moveSpeed;
    [Header("�W�����v��")]
    public float push_power;
    [Header("�ő�W�����v��")]
    public int Max_Jmup;

    //private�ϐ�--------------------------------------------------------------
    private Vector3 push;               //���Z�������x�N�g����
    private int Jump_Count = 0;         //�A���ŃW�����v�����񐔂��J�E���g
    private bool Jump_Flag = true;      //�W�����v���Ă��邩�̃t���O
    private float inputX = 0;           //X���̈ړ��x�N�g��
    private float inputZ = 1;           //Z���̈ړ��x�N�g��
    private int Select_order = 0;       //�{�^���������ꂽ���Ԃ��L��
    private int Action_count = 0;       //�A�N�V�����������񐔂��J�E���g
    private bool Action_check = false;  //�A�N�V��������񂵂��g���Ȃ��悤�Ǘ�
    private bool Movestop = false;      //�A�N�V������I������Ƃ���l�����~�߂�p
    private int[] Card_order;           //�J�[�h��I���������Ԃ��L��

    //�\����-------------------------------------------------------------------
    //�{�^���g�p������
    private struct Buttan
    {
        public bool push;      //�{�^���������ꂽ���̔���
        public int push_num;   //�����ꂽ���̏��ԋL��
        //�������p�֐�
        public Buttan(bool a, int b) {
            push = a;
            push_num = b;
        }
    }
    //�\���̂̏�����
    Buttan jump = new Buttan(false, 0);
    Buttan squat = new Buttan(false, 0);
    Buttan stick = new Buttan(false, 0);
    Buttan stop = new Buttan(false, 0);

    //��----------------------------------------------------------------------
    //�J�[�h�̎��
    public enum Card
    {
        JUMP,
        SQUAT,
        STICK,
        STOP
    }
    

    // Start is called before the first frame update
    void Start() {
        //������
        push = new Vector3(0.0f, push_power, 0.0f);
        Card_order = new int[Max_Card];
    }

    // Update is called once per frame
    void FixedUpdate() {
        //�A�N�V�����u���b�N�ɓ��B����Ƃ�������~�߂鏈��
        if (Movestop == false) {
            
            //�ړ�����
            MOVE(inputX, inputZ);

            //�W�����v��I�������Ƃ�--------------------------------------------------------------
            if (jump.push_num == Action_count && Action_check == true) {
                //�W�����v����
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

            //���Ⴊ�݂�I�������Ƃ�--------------------------------------------------------------
            if (squat.push_num == Action_count && Action_check == true) {
                this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                squat.push = false;
                Action_check = false;
            }

            //��������I�������Ƃ�--------------------------------------------------------------
            if (stick.push_num == Action_count && Action_check == true) {


                stick.push = false;
                Action_check = false;
            }

            //�X�g�b�v��I�������Ƃ�--------------------------------------------------------------
            if (stop.push_num == Action_count && Action_check == true) {


                stop.push = false;
                Action_check = false;
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        //�ǂɐG��Ă����Y���ւ̗͕t�^
        if(collision.gameObject.tag=="Wall") {
            transform.Translate(0.0f, 0.15f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision collision) {
        //�����ړ��ݒ莞�A���̃I�u�W�F�N�g�ɐG���ƁA�w������Ɉړ�����
        if (collision.gameObject.tag == "Move_direction") {
            switch(collision.gameObject.GetComponent<Direction>().direction) {
                case 1://��
                    inputX = -1;
                    inputZ = 0;
                    break;
                case 2://�E
                    inputX = 1;
                    inputZ = 0;
                    break;
                case 3://�O
                    inputZ = 1;
                    inputX = 0;
                    break;
                case 4://��
                    inputZ = -1;
                    inputX = 0;
                    break;
            }
        }

        //�A�N�V�����u���b�N�ɏ�邽�уJ�E���g
        if (collision.gameObject.tag == "Action") {
            Action_count++;
            Action_check = true;
            Movestop = true;
            collision.gameObject.SetActive(false);
        }

    }

    //�ړ������֐�
    private void MOVE(float x, float z) {
        if (auto_move == false) {
            //���E����
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        //���x�̐ݒ�
        float moveX = x * moveSpeed * Time.deltaTime;
        float moveZ = z * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);
    }

    //�{�^���ł̑���I��----------------------------------------------------------------
    public void Push_jump() {
        jump.push = true;
        Card_order[Select_order] = (int)Card.JUMP;  //�{�^�������������Ԃ��L��
        Select_order++;                             //���Ԃ�i�߂�p
        jump.push_num = Select_order;               //�����ꂽ���ԂƃA�N�V�����u���b�N�𓥂񂾉񐔂���v������
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

    //�A�N�V�����J�n�{�^��
    public void Push_start() {
        Movestop = false;
    }

   

}