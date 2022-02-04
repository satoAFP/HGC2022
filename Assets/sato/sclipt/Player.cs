using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------------------------
    [Header("�������샂�[�h")]
    public bool auto_move;

    [Header("��l�����ǂɐG��Ă���Ƃ��̔���I�u�W�F�N�g")]
    public GameObject[] Around_collision;

    [Header("�J�[�h�ő�I����")]
    public int Max_Card;

    [Header("�J�[�h�̎�ސ�")]
    public int Kind_Card;

    [Header("�ړ����x")]
    public float moveSpeed;

    [Header("���鑬�x(�ړ����x�̉��{��)")]
    public float runSpeed;

    [Header("�O�����ւ̗�(�����сA�X���C�f�B���O�Ŏg�p)")]
    public float flontMove;

    [Header("�W�����v��")]
    public float push_power;

    [Header("�ǃL�b�N�̃W�����v�͂̔{��")]
    public float wall_kick_power;

    [Header("�ǃL�b�N�̉��ړ����鎞��")]
    public int walljump_time;

    [Header("�ǃL�b�N�̉��ړ�����ŏ��̃p���[")]
    public float walljump_first_power;

    [Header("�n�C�W�����v��(�W�����v�̉��{��)")]
    public float highjump_power;

    [Header("�ő�W�����v��")]
    public int Max_Jmup;

    [Header("�I�����ꂽ�A�N�V�����\���p�e�L�X�g")]
    public Text Select_text;

    [Header("�\���p�e�L�X�g�I�u�W�F�N�g")]
    public GameObject Select_text_obj;

    [Header("�ǂŎ~�܂��Ĕ��U����܂ł̎���")]
    public int stop_efect_time;

    [Header("�ǂŎ~�܂��Ď��ʂ܂ł̎���")]
    public int stop_deth_time;

    [Header("��������Ƃ��Ă���̎��ʂ܂ł̎���")]
    public int fall_deth_time;

    [Header("���S�G�t�F�N�g")]
    public GameObject Deth_efect;

    [Header("���S�G�t�F�N�g���o����������l���X�L��")]
    public GameObject Delete_skin;

    [Header("�Q�[�����n�܂�J�E���g��\������e�L�X�g")]
    public Text start_time_text;

    [Header("�Q�[�����n�܂�J�E���g�̕b��")]
    public int start_time;

    [Header("�S�[�����̃N���b�J�[�G�t�F�N�g")]
    public GameObject goal_cracker;

    [Header("�����G�t�F�N�g���̐��G�t�F�N�g")]
    public GameObject clown_star;

    [Header("�X���C���̃A�j���[�V����")]
    [Header("�A�j���[�V�����Ǘ��ϐ�---------------------------------------------------")]
    public Animator anim;

    [Header("�q�X�g���[�̃X���C���̃��f��")]
    public GameObject sura_model;


    [Header("�J�[�h�I������SE")]
    [Header("�T�E���h�Ǘ��ϐ�---------------------------------------------------------")]
    public AudioClip se_card;

    [Header("�A�N�V��������SE")]
    public AudioClip se_action;

    [Header("�����擾����SE")]
    public AudioClip se_clown;

    [Header("�ŏ��̃X�^�[�g�J�E���g����SE")]
    public AudioClip se_start_count;

    [Header("�ŏ��̃X�^�[�g�J�E���g�I������SE")]
    public AudioClip se_start_count_end;

    [Header("�S�[������SE")]
    public AudioClip se_goal;

    //���I�u�W�F�N�g�ł��g�p
    [Header("��l�����~�߂�p")]
    [Header("�������牺�͐G��Ȃ�------------------------------------------------------")]
    public bool Movestop = true;        //��l�����������ǂ���

    public bool count_check = false;    //�ŏ��̃J�E���g���I���Ƃ�������Ƃ�

    [Header("�����擾����p")]
    public int clown_get = 0;

    [Header("�~�b�V��������p")]
    public int[] Use_Card_Amount;

    [Header("�����ш�����������")]
    public bool Longjump_check = false;


    //private�ϐ�--------------------------------------------------------------
    private Vector3 push;                   //���Z�������x�N�g����
    private float inputX = 0;               //X���̈ړ��x�N�g��
    private float inputZ = 1;               //Z���̈ړ��x�N�g��
    private int Select_order = 0;           //�{�^���������ꂽ���Ԃ��L��
    private bool[] Action_check;            //�A�N�V��������񂵂��g���Ȃ��悤�Ǘ�
    private int[] Card_order;               //�J�[�h��I���������Ԃ��L��
    private bool wall_stick = false;        //�ǂɂ���������
    private bool around_collision_check = false;//�v���C���[�̎���̓����蔻��ɕǂ������true�ɂȂ�
    private Vector3 sura_angle;             //�ǂɂ������Ă��鎞�̃X���C���̊p�x����
    private Vector3 sura_pos;               //�ǂɂ������Ă��鎞�̃X���C���̈ʒu����
    private bool all_stick = false;         //�ǂ�������Ԃ��A�N�V�������Z�b�g�܂ő��s
    private Vector3 squrt_check;            //���Ⴊ�ނƂ��̎�l���ʒu�ύX
    private float walljump = 0.0f;          //�ǃW�����v����Ƃ��̃W�����v��
    private bool walljump_check = false;    //�ǃW�����v���ǂ������f
    private float run_power = 1;            //�ړ����x���
    private Vector3 flont_push;             //�ړ������ւ��͂�������(�����тŎg�p)
    private Vector3 flont_sliding;          //�ړ������ւ��͂�������(�X���C�f�B���O�Ŏg�p)
    private string[] text_data;             //�A�N�V�������e�i�[�ϐ�
    private bool select_time = true;        //�J�n�{�^���������ƁA�J�[�h�I���ł��Ȃ�
    private bool safe_flag = true;          //���S����p�t���O
    private Vector3 stop_check;             //�t���[�����Ɏ�l���̍��W�擾
    private int stop_time_count = 0;        //��l���̃X�g�b�v���A���ۃJ�E���g����ϐ�
    private int start_time_count = 0;       //�X�^�[�g���A���ۃJ�E���g����ϐ�
    private int start_text_time_count = 3;  //���ۂɃe�L�X�g�ɕb��������ϐ�
    private AudioSource audio;              //�g�p����I�[�f�B�I�\�[�X
    private Select_Card_Manager SCM;        //Select_Card_Manager�i�[�X�N���v�g
    private int after_card_order = -1;      //�g�p�����ŐV�̃J�[�h���L��
    private bool fall_deth_flag = false;    //�����Ŏ��S��true�ɂȂ�
    private int camera_num = 0;             //���ԃJ�����g�p������p
    private bool first_ground_check = false;//�ŏ��n�ʂɒ��������̔���


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
        RUN,
        HIGHJUMP,
        WALLKICK,
        LONGJUMP,
        SLIDING,
    }


    // Start is called before the first frame update
    void Start() {
        //������
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

        stop_check = new Vector3(0.0f, 0.0f, 0.0f);

        squrt_check = new Vector3(0.0f, 0.25f, 0.0f);

        //�X�e�[�W���ɂ���SE_manager�i�[
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
        //�X�^�[�g�̃J�E���g����SE�Đ�
        audio.PlayOneShot(se_start_count);

        //Select_Card_Manager���擾
        SCM = Select_text_obj.GetComponent<Select_Card_Manager>();


        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        //�X�^�[�g����-------------------------------------------------
        //�C���X�y�N�^�[�Őݒ肵���b���҂��ăX�^�[�g
        if (Movestop == true)
        {
            start_time_count++;
            if (start_time_count == start_time)
            {
                start_time_text.gameObject.SetActive(false);
                count_check = true; //pause�ɔ���𑗂�
                Movestop = false;   //�A�N�V�������[�v�̃��C�������𓮂���
                Select_order = 0;   //�A�N�V�����u���b�N�ɏ�������A�ŏ��ɉ��Z����Ă��܂�����-1
                select_time = false;//�A�N�V�����J�n����ƃJ�[�h��I���ł��Ȃ�
                audio.PlayOneShot(se_start_count_end);
            }
            //60�t���[�����ɂP�b���炷
            if ((start_time_count % 60) == 0 && start_time_count <= 120) 
            {
                start_text_time_count--;
                //�X�^�[�g�̃J�E���g����SE�Đ�
                audio.PlayOneShot(se_start_count);
            }
            //�e�L�X�g�ɕb�����o��
            start_time_text.text = "" + start_text_time_count;
        }


        //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            Card_order[0] = Card_order[1];
            text_data[0] = text_data[1];
            SCM.Select[0].GetComponent<Image>().sprite = SCM.Select[1].GetComponent<Image>().sprite;
            for (int i = 1; i < Max_Card - 1; i++) 
            {
                Card_order[i] = Card_order[i + 1];
                text_data[i] = text_data[i + 1];
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Select[i + 1].GetComponent<Image>().sprite;
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
            text_data[Max_Card - 1] = "";
            SCM.Select[Max_Card - 1].GetComponent<Image>().sprite = null;
        }

        //�I�񂾃A�N�V������text_data�Ɋi�[
        for (int i = 0; i < Max_Card; i++)  {
            if(Card_order[i] == (int)Card.JUMP) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.JUMP];
            }
            if (Card_order[i] == (int)Card.SQUAT) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.SQUAT];
            }
            if (Card_order[i] == (int)Card.STICK) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.STICK];
            }
            if (Card_order[i] == (int)Card.RUN) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.RUN];
            }
            if (Card_order[i] == (int)Card.HIGHJUMP) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.HIGHJUMP];
            }
            if (Card_order[i] == (int)Card.WALLKICK) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.WALLKICK];
            }
            if (Card_order[i] == (int)Card.LONGJUMP) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.LONGJUMP];
            }
            if (Card_order[i] == (int)Card.SLIDING) {
                text_data[i] = "          ��";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.SLIDING];
            }

            if (Card_order[i]==-1)
                SCM.Select[i].SetActive(false);
            else
                SCM.Select[i].SetActive(true);

        }
        //�I�������A�N�V�������ە\��
        Select_text.text = "" + text_data[0] + text_data[1] + text_data[2] + text_data[3] + text_data[4];

        //�ǂɐG����around_collision_check = true�ɂ���
        if (Around_collision[0].GetComponent<Around_collider>().wall_check == true ||
            Around_collision[1].GetComponent<Around_collider>().wall_check == true) 
        {
            //���E���ꂼ��ǂɐG��Ă���Ƃ��̌����ڂ𒲐�
            if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) 
            {
                around_collision_check = true;
                sura_angle = new Vector3(45.0f, 0.0f, -90.0f);
                sura_pos = new Vector3(-0.5f, 0.0f, 0.0f);
            }
            if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) 
            {
                around_collision_check = true;
                sura_angle = new Vector3(-45.0f, 0.0f, 90.0f);
                sura_pos = new Vector3(0.5f, 0.0f, 0.0f);
            }
        }
        else 
        {
            around_collision_check = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        //�����ёI�����A�W�����v�u���b�N�Ɉ��������������
        if (Around_collision[2].GetComponent<Around_collider>().wall_check == true)
        {
            if (after_card_order == (int)Card.LONGJUMP || after_card_order == (int)Card.SLIDING) 
            {
                Longjump_check = true;
            }
        }

        //�����сA�X���C�f�B���O�Ŏg�p����ړ��ʂ̌����ύX
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


        //�ŏ��A�N�V������I������Ƃ��ƁA�Z���N�g�u���b�N�ɓ��B����Ƃ�������~�߂鏈��
        if (Movestop == false) {

            //�ǂŎ~�܂��������ʏ���
            if (stop_check == this.gameObject.transform.position)
            {
                stop_time_count++;

                if (stop_efect_time == stop_time_count) 
                {
                    //���S���̃G�t�F�N�g
                    Deth_efect.SetActive(true);
                    Delete_skin.SetActive(false);
                    
                    //SE����
                    audio.PlayOneShot(se_action);
                }
                if (stop_efect_time + stop_deth_time == stop_time_count) 
                {
                    stop_time_count = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            stop_check = this.gameObject.transform.position;

            //�ړ�����
            MOVE(inputX, inputZ);

            //Select_order��-1�̂Ƃ��z�񂪃G���[���N�����̂�
            if (Select_order != -1) {

                //�W�����v��I�������Ƃ�--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.JUMP] == true) {
                    //�W�����v�����鏈��
                    this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                    //�W�����v�A�j���[�V�����ڍs
                    anim.SetBool("jump", true);

                    //�W�����v�����I��
                    Action_check[(int)Card.JUMP] = false;
                }


                //���Ⴊ�݂�I�������Ƃ�--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.SQUAT] == true) {
                    //y���̃T�C�Y�ύX
                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                    //���Ⴊ�ݏ����I��
                    Action_check[(int)Card.SQUAT] = false;
                }


                //��������I�������Ƃ�--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.STICK] == true) {
                    //��������Ԉێ�
                    all_stick = true;

                    //�O�ɕǂ����鏈��
                    //if (Around_collision[2].GetComponent<Around_collider>().wall_check == true)
                    //    wall_stick = true;
                    //else
                    //    wall_stick = false;
                }
                if (all_stick == true) 
                {
                    if (around_collision_check == true)
                    {
                        //Y���������Ȃ��悤�Œ�
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                        //�X���C���̒���
                        //sura_model.transform.Rotate(sura_angle);
                        sura_model.transform.localEulerAngles = sura_angle;
                        sura_model.transform.localPosition = sura_pos;
                    }
                    else
                    {
                        //�ǂ��Ȃ��Ȃ�ƌ��̏�Ԃɖ߂�
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        sura_model.transform.localEulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
                        sura_model.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
                    }
                }


                //�����I�������Ƃ�------------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.RUN] == true) {

                    //�{�����ς��
                    run_power = runSpeed;

                    //�W�����v�A�j���[�V�����ڍs
                    //anim.SetBool("run", true);

                    Action_check[(int)Card.RUN] = false;
                }


                //�n�C�W�����v��I�������Ƃ�----------------------------------------------------------------------------------------
                if (Action_check[(int)Card.HIGHJUMP] == true) {
                    //�W�����v�����鏈��
                    this.GetComponent<Rigidbody>().AddForce(push * highjump_power, ForceMode.Impulse);

                    //�W�����v�A�j���[�V�����ڍs
                    anim.SetBool("jump", true);

                    Action_check[(int)Card.HIGHJUMP] = false;
                }


                //�ǃL�b�N��I�������Ƃ�--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.WALLKICK] == true) {
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    //�W�����v�����鏈��
                    this.GetComponent<Rigidbody>().AddForce(push * wall_kick_power, ForceMode.Impulse);

                    //�W�����v�A�j���[�V�����ڍs
                    anim.SetBool("jump", true);

                    //���ɕǂ����鏈��
                    if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = walljump_first_power;
                    }
                    //�E�ɕǂ����鏈��
                    if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = -walljump_first_power;
                    }

                    Action_check[(int)Card.WALLKICK] = false;
                }
                //�ǃW�����v����
                if (walljump_check == true) {
                    if (walljump_time != 0) {
                        transform.Translate(walljump, 0.0f, 0.0f);
                        if (walljump < 0)
                            walljump += walljump_first_power / walljump_time;
                        else
                            walljump -= walljump_first_power / walljump_time;
                    }
                    else
                        walljump_check = false;
                    walljump_time--;
                }


                //�����т�I�������Ƃ�----------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.LONGJUMP] == true) {
                    this.GetComponent<Rigidbody>().AddForce(flont_push, ForceMode.Impulse);

                    //�W�����v�A�j���[�V�����ڍs
                    anim.SetBool("jump", true);

                    Action_check[(int)Card.LONGJUMP] = false;
                }

                //�X���C�f�B���O��I�������Ƃ�--------------------------------------------------------------------------------------
                if (Action_check[(int)Card.SLIDING] == true) {

                    this.GetComponent<Rigidbody>().AddForce(flont_sliding, ForceMode.Impulse);

                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                    Action_check[(int)Card.SLIDING] = false;
                }
            }
        }

        //�������S���莞�̃G�t�F�N�g
        if (fall_deth_flag)
        {
            stop_time_count++;

            if (fall_deth_time - 50 == stop_time_count)
            {
                //���S���̃G�t�F�N�g
                Deth_efect.SetActive(true);
                Delete_skin.SetActive(false);
                
                //SE����
                audio.PlayOneShot(se_action);
            }
            if (fall_deth_time == stop_time_count)
            {
                stop_time_count = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
    

    void OnCollisionEnter(Collision collision) {
        //�����ړ��ݒ莞�A���̃I�u�W�F�N�g�ɐG���ƁA�w������Ɉړ�����
        if (collision.gameObject.tag == "Move_direction") {

            if (camera_num == -1)
                camera_num = 3;

            switch(collision.gameObject.GetComponent<Direction>().direction) {
                case 1://��
                    this.gameObject.transform.Rotate(new Vector3(0, -90, 0));

                    //�J�����̌����ύX
                    camera_num--; 
                    if (camera_num == -1)
                        camera_num = 3;
                    GameObject.Find("VCameraManeger").GetComponent<VCamera_maneger>().CameraChange(camera_num);
                    break;
                case 2://�E
                    this.gameObject.transform.Rotate(new Vector3(0, 90, 0));

                    //�J�����̌����ύX
                    if (camera_num == 4)
                        camera_num = 0;
                    camera_num++;
                    GameObject.Find("VCameraManeger").GetComponent<VCamera_maneger>().CameraChange(camera_num);
                    break;
            }
        }

        if (collision.gameObject.tag == "Ground") {
            //���n����
            anim.SetBool("jump", false);
            if (first_ground_check)
                audio.PlayOneShot(se_action);
            first_ground_check = true;
        }


        //�A�N�V������I���������ԂɎ��s�����
        if (collision.gameObject.tag == "Action") 
        {
            //��x������A�N�V�����u���b�N�͏���
            collision.gameObject.SetActive(false);

            //���̃J�[�h�������e�L��
            after_card_order = Card_order[0];

            //���̃A�N�V�����̃t���O��true�ɂ���
            switch (Card_order[0]) {
                case (int)Card.JUMP:
                    Action_check[(int)Card.JUMP] = true;
                    Use_Card_Amount[(int)Card.JUMP]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.SQUAT:
                    Action_check[(int)Card.SQUAT] = true;
                    Use_Card_Amount[(int)Card.SQUAT]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.STICK:
                    Action_check[(int)Card.STICK] = true;
                    Use_Card_Amount[(int)Card.STICK]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.RUN:
                    Action_check[(int)Card.RUN] = true;
                    Use_Card_Amount[(int)Card.RUN]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.HIGHJUMP:
                    Action_check[(int)Card.HIGHJUMP] = true;
                    Use_Card_Amount[(int)Card.HIGHJUMP]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.WALLKICK:
                    Action_check[(int)Card.WALLKICK] = true;
                    Use_Card_Amount[(int)Card.WALLKICK]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.LONGJUMP:
                    Action_check[(int)Card.LONGJUMP] = true;
                    Use_Card_Amount[(int)Card.LONGJUMP]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.SLIDING:
                    Action_check[(int)Card.SLIDING] = true;
                    Use_Card_Amount[(int)Card.SLIDING]++;
                    //�A�N�V��������SE�Đ�
                    audio.PlayOneShot(se_action);
                    break;
            }
            //���s�����A�N�V�������Œᐔ�Ƃ��Đݒ�
            GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().executed_Action(Card_order[0]);
            //�A�N�V�����̓��e����
            Card_order[0] = -1;

            //�����т̃o�O�C���֘A
            Longjump_check = false;
        }

        //�A�N�V�����đI��
        if (collision.gameObject.tag == "Select") {
            //���̃T�C�Y�ɖ߂�
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //���Ⴊ�ݏ�ԉ���
            Action_check[(int)Card.SQUAT] = false;

            //��������ԉ���
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;
            all_stick = false;

            //�����ԉ���
            run_power = 1.0f;

            //�������~�߂�
            Movestop = true;

            //�đI���ł���
            select_time = true;

            //���Ԃ��ŏ�����ɂ���
            Select_order = 0;

            //�A�N�V�����I���̒��g������
            for (int i = 0; i < Max_Card; i++) {
                Card_order[i] = -1;
            }

            //�A�N�V�����\���e�L�X�g�̒��g������
            for (int i = 0; i < Max_Card; i++) {
                text_data[i] = "";
            }
        }

        //�A�N�V�������e���Z�b�g
        if (collision.gameObject.tag == "action_delete") {
            //���̃T�C�Y�ɖ߂�
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //���Ⴊ�ݏ�ԉ���
            Action_check[(int)Card.SQUAT] = false;

            //��������ԉ���
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;
            all_stick = false;

            //�����ԉ���
            run_power = 1.0f;

            //�����т̃o�O�C���֘A
            Longjump_check = false;
        }

        //�S�[�������@���U���g�ɔ��
        if (collision.gameObject.tag == "Goal") {
            this.gameObject.GetComponent<Goal_After>().goal_move = true;

            //�S�[������SE
            audio.PlayOneShot(se_goal);

            Destroy(collision.gameObject);

            //�S�[�����̃G�t�F�N�g
            goal_cracker.SetActive(true);

            //�~�b�V����UI��\��
            GameObject.Find("Mission_UI").SetActive(false);
            GameObject.Find("select_card_UI").SetActive(false);

            GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().Set_OffActive();
        }



        //�W�����v�u���b�N�ɐG�ꂽ��
        if (collision.gameObject.tag == "Jumpblock")
        {
            //�I�u�W�F�N�g�폜
            Destroy(collision.gameObject);
            if (after_card_order == (int)Card.STICK || after_card_order == (int)Card.RUN || after_card_order == -1)
            {
                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                //�M�~�b�N�g�p��������
                after_card_order = -1;

                //�W�����v�A�j���[�V�����ڍs
                anim.SetBool("jump", true);
            }
            else if (after_card_order == (int)Card.SQUAT) 
            {
                //���̃T�C�Y�ɕύX
                this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                
                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(push * 1.6f, ForceMode.Impulse);

                //�M�~�b�N�g�p��������
                after_card_order = -1;

                //�W�����v�A�j���[�V�����ڍs
                anim.SetBool("jump", true);
            }
            else if (after_card_order == (int)Card.JUMP)
            {
                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(push * 0.5f, ForceMode.Impulse);

                //�M�~�b�N�g�p��������
                after_card_order = -1;

                //�W�����v�A�j���[�V�����ڍs
                anim.SetBool("jump", true);
            }
        }
    }


    void OnCollisionStay(Collision collision) {
        //��������Ԃ̎��A�ǂɐG��Ă����Y���ւ̗͕t�^
        if (collision.gameObject.tag == "Wall") {
            if (wall_stick == true) {
                transform.Translate(0.0f, 0.2f, 0.0f);
            }
            //���n����
            anim.SetBool("jump", false);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        //�����擾��
        if (collider.gameObject.tag == "clown")
        {
            //SE����
            audio.PlayOneShot(se_clown);

            clown_get++;
            Destroy(collider.gameObject);

            //�����擾���̃G�t�F�N�g
            clown_star.SetActive(true);

        }

        //�������̏���
        if (collider.gameObject.tag == "Acceleration")
        {
            //�I�u�W�F�N�g�폜
            Destroy(collider.gameObject);
            if (after_card_order == (int)Card.JUMP || after_card_order == (int)Card.SQUAT ||
                after_card_order == (int)Card.STICK || after_card_order == (int)Card.RUN || after_card_order == -1)
            {
                //�{�����ς��
                run_power = runSpeed;

                //�M�~�b�N�g�p��������
                after_card_order = -1;
            }
        }

    }
    //���S����
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "safe_zone") {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            fall_deth_flag = true;
            
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
        float moveX = x * moveSpeed * Time.deltaTime * run_power;
        float moveZ = z * moveSpeed * Time.deltaTime * run_power;
        transform.Translate(moveX, 0.0f, moveZ);
    }

    //�{�^���ł̑���I��----------------------------------------------------------------
    //�ʏ�A�N�V����-----------------------------------
    public void Push_jump() {//�W�����v�{�^��
        //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.JUMP;  //�{�^�������������Ԃ��L��
                break;
            }
        }

        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_squat()
    {//���Ⴊ�݃{�^��
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.SQUAT;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_stick()
    {//�������{�^��
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.STICK;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_run()
    {//����{�^��
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.RUN;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);

    }

    //���̃A�N�V����------------------------------------
    public void Push_highjump()
    {//���Ⴊ��+�W�����v���n�C�W�����v
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.HIGHJUMP;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_wallkick()
    {//������+�W�����v���ǃL�b�N
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.WALLKICK;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_longjump()
    {//����+�W�����v��������
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.LONGJUMP;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    public void Push_sliding()
    {//���Ⴊ��+���遁�X���C�f�B���O
     //Card_order�̈�ԖڂɃf�[�^�������ĂȂ��Ƃ����Ԃ�����炷
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //�J�[�h�̈�ԍŌ�̃f�[�^��������
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.SLIDING;  //�{�^�������������Ԃ��L��
                break;
            }
        }
        //�J�[�h�߂���ꂽ����SE�Đ�
        audio.PlayOneShot(se_card);
    }

    //�A�N�V�����J�n�{�^��
    public void Push_start() {
        Movestop = false;   //�A�N�V�������[�v�̃��C�������𓮂���
        Select_order = 0;  //�A�N�V�����u���b�N�ɏ�������A�ŏ��ɉ��Z����Ă��܂�����-1
        select_time = false;//�A�N�V�����J�n����ƃJ�[�h��I���ł��Ȃ�
    }

    //�I�������A�N�V��������߂�
    public void Push_back() {
        //�}���`��Ԃ�߂��Ƃ��Ɏg��bool���擾
        if (GameObject.Find("BackButton").GetComponent<DeletAction>().multi_backflag == false)
        {
            for (int i = 0; i < Max_Card; i++)
            {
                if (Card_order[i] == -1)
                {
                    //0�Ԗڂ��ŏ��̃A�N�V�����Ȃ̂ł��ꖢ���ɂ͂Ȃ�Ȃ�
                    Card_order[i - 1] = -1;
                    text_data[i - 1] = "";   //�J�[�h�e�L�X�g�̒��g����
                    SCM.Select[i - 1].GetComponent<Image>().sprite = null;
                }
            }
            if (Card_order[Max_Card - 1] != -1)
            {
                Card_order[Max_Card - 1] = -1;
                text_data[Max_Card - 1] = "";   //�J�[�h�e�L�X�g�̒��g����
                SCM.Select[Max_Card - 1].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void check() {
        for(int i=0;i<Select_order;i++) {
            Debug.Log($"{Card_order[i]}");
        }
    }


}