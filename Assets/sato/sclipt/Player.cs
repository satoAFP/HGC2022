using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------------------------
    [Header("�������샂�[�h")]
    public bool auto_move;

    [Header("��l�����ǂɐG��Ă���Ƃ��̔���I�u�W�F�N�g")]
    public GameObject[] Around_collision;

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
    private float inputX = 0;           //X���̈ړ��x�N�g��
    private float inputZ = 1;           //Z���̈ړ��x�N�g��
    private int Select_order = 0;       //�{�^���������ꂽ���Ԃ��L��
    private bool[] Action_check;        //�A�N�V��������񂵂��g���Ȃ��悤�Ǘ�
    private bool Movestop = true;       //�A�N�V������I������Ƃ���l�����~�߂�p
    private int[] Card_order;           //�J�[�h��I���������Ԃ��L��
    private bool wall_stick = false;    //�ǂɂ���������
    private float walljump = 0.0f;      //�ǃW�����v����Ƃ��̃W�����v��
    private bool walljump_check = false;//�ǃW�����v���ǂ������f
    private int walljump_time = 100;     //���ړ����鎞��

    //�\����-------------------------------------------------------------------
    //�{�^���g�p������
    //private struct Buttan
    //{
    //    public bool push;      //�{�^���������ꂽ���̔���
    //    public int push_num;   //�����ꂽ���̏��ԋL��
    //    //�������p�֐�
    //    public Buttan(bool a, int b) {
    //        push = a;
    //        push_num = b;
    //    }
    //}
    ////�\���̂̏�����
    //Buttan jump = new Buttan(false, 0);
    //Buttan squat = new Buttan(false, 0);
    //Buttan stick = new Buttan(false, 0);
    //Buttan stop = new Buttan(false, 0);

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
        Action_check = new bool[4];
    }

    // Update is called once per frame
    void FixedUpdate() {
        //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //�A�N�V�����u���b�N�ɓ��B����Ƃ�������~�߂鏈��
        if (Movestop == false) {
            
            //�ړ�����
            MOVE(inputX, inputZ);

            //�W�����v��I�������Ƃ�--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.JUMP && Action_check[(int)Card.JUMP] == true) {
                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) {
                    walljump_check = true;
                    walljump = 0.1f;
                }
                //�E�ɕǂ����鏈��
                if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) {
                    walljump_check = true;
                    walljump = -0.1f;
                }

                //�W�����v�����I��
                Action_check[(int)Card.JUMP] = false;
            }
            //�ǃW�����v����
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

            //���Ⴊ�݂�I�������Ƃ�--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.SQUAT && Action_check[(int)Card.SQUAT] == true) {
                this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            }
            else {
                this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            

            //��������I�������Ƃ�--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.STICK && Action_check[(int)Card.STICK] == true) {
                //���ɕǂ����鏈��
                if (Around_collision[0].GetComponent<Around_collider>().wall_check == true)
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                else
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //�E�ɕǂ����鏈��
                if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) 
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                else
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //�O�ɕǂ����鏈��
                if (Around_collision[2].GetComponent<Around_collider>().wall_check == true) 
                    wall_stick = true;
                else
                    wall_stick = false;
            }

            //�X�g�b�v��I�������Ƃ�--------------------------------------------------------------
            if (Card_order[Select_order] == (int)Card.STOP && Action_check[(int)Card.STOP] == true) {


                
                Action_check[(int)Card.STOP] = false;
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        //��������Ԃ̎��A�ǂɐG��Ă����Y���ւ̗͕t�^
        if(collision.gameObject.tag=="Wall") {
            if (wall_stick == true) {
                transform.Translate(0.0f,0.2f,0.0f);
            }
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

        //�A�N�V������I���������ԂɎ��s�����
        if (collision.gameObject.tag == "Action") {
            collision.gameObject.SetActive(false);//��x������A�N�V�����u���b�N�͏���
            Select_order++;//�A�N�V�������e����i�߂�

            //���Ⴊ�ݏ�ԉ���
            Action_check[(int)Card.SQUAT] = false;
            //��������ԉ���
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;

            //���̃A�N�V�����̃t���O��true�ɂ���
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
    public void Push_jump() {//�W�����v�{�^��
        Card_order[Select_order] = (int)Card.JUMP;  //�{�^�������������Ԃ��L��
        Select_order++;                             //���Ԃ�i�߂�p
    }

    public void Push_squat() {//���Ⴊ�݃{�^��
        Card_order[Select_order] = (int)Card.SQUAT;
        Select_order++;
    }

    public void Push_stick() {//�������{�^��
        Card_order[Select_order] = (int)Card.STICK;
        Select_order++;
    }

    public void Push_stop() {//�X�g�b�v�{�^��
        Card_order[Select_order] = (int)Card.STOP;
        Select_order++;
    }

    //�A�N�V�����J�n�{�^��
    public void Push_start() {
        Movestop = false;//�A�N�V�������[�v�̃��C�������𓮂���
        Select_order = -1;//�A�N�V�����u���b�N�ɏ�������A�ŏ��ɉ��Z����Ă��܂�����-1
    }

    public void check() {
        for(int i=0;i<3;i++) {
            Debug.Log($"{Card_order[i]}");
        }
    }


}