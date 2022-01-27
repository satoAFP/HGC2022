using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{

    [Header("�������擾�ŃN���A��")]
    public int Clown_Clear;

    [Header("�n�C�W�����v�F4�@�ǃL�b�N�F5�@�����сF6�@�X���C�f�B���O�F7")]
    [Header("�W�����v�F0�@���Ⴊ�݁F1�@�Ђ����F2�@����F3")]
    [Header("�ǂ̃A�N�V�����J�[�h���N���A����ɂ��邩")]
    public int ClearCard;

    [Header("1�F�`��ȏ�̎g�p�ŃN���A")]
    [Header("0�F�`��ȓ��̎g�p�ŃN���A")]
    [Header("�ǂ̃~�b�V�������e�ɂ��邩")]
    public int Minssion_Num;

    [Header("���ꂼ��̃~�b�V���������Ń~�b�V�����N���A��")]
    public int[] Use_Card_Clear;

    [Header("�����擾���\���摜")]
    public GameObject Clown_img;

    [Header("�~�b�V�������e�e�L�X�g")]
    public Text Mission_substance_text;

    [Header("�~�b�V�����N���A�摜")]
    public GameObject Mission_img;

    [Header("�������牺������Ȃ�-----------")]
    public bool Clown_OK = false;
    public bool Mission_OK = false;

    private int clown_get = 0;      //�v���C���[���牤�����擾������������Ƃ�
    private int[] use_Card_Amount;  //���ꂼ��̃J�[�h���g�p�����񐔂��擾

    private int Mission_Use_Card_Clear = 0;
    private string mission_action = "";
    private string mission_substance = "";


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
    void Start()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //�X�e�[�W�ł̂ݎ擾
        if (SceneManager.GetActiveScene().name != "Title" &&
            SceneManager.GetActiveScene().name != "StageSelect" &&
            SceneManager.GetActiveScene().name != "Result")
        {
            //�~�b�V�����̃A�N�V�����J�[�h�ݒ�
            if (ClearCard == (int)Card.JUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.JUMP];
                mission_action = "�W�����v��";
            }
            if (ClearCard == (int)Card.SQUAT)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.SQUAT];
                mission_action = "���Ⴊ�݂�";
            }
            if (ClearCard == (int)Card.STICK)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.STICK];
                mission_action = "�Ђ�����";
            }
            if (ClearCard == (int)Card.RUN)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.RUN];
                mission_action = "�����";
            }
            if (ClearCard == (int)Card.HIGHJUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.HIGHJUMP];
                mission_action = "�n�C�W�����v��";
            }
            if (ClearCard == (int)Card.WALLKICK)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.WALLKICK];
                mission_action = "�ǃL�b�N��";
            }
            if (ClearCard == (int)Card.LONGJUMP)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.LONGJUMP];
                mission_action = "�����т�";
            }
            if (ClearCard == (int)Card.SLIDING)
            {
                Mission_Use_Card_Clear = Use_Card_Clear[(int)Card.SLIDING];
                mission_action = "�X���C�f�B���O��";
            }

            if (Minssion_Num == 0)
                mission_substance = "��ȓ��̎g�p�ŃN���A";
            if (Minssion_Num == 1)
                mission_substance = "��ȏ�̎g�p�ŃN���A";

            //�~�b�V�������e�쐬
            Mission_substance_text.text = "" + mission_action + "" + Mission_Use_Card_Clear + "" + mission_substance;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //�����擾
        if (clown_get >= Clown_Clear) 
        {
            //�N���A�e�L�X�g�\��
            Clown_img.gameObject.SetActive(true);
            Clown_OK = true;
        }
        else
        {
            //�N���A�e�L�X�g�\��
            Clown_img.gameObject.SetActive(false);
            Clown_OK = false;
        }

        //�~�b�V�����p�J�[�h�g�p�񐔐���
        if (Minssion_Num == 0)
        {
            if (use_Card_Amount[ClearCard] <= Use_Card_Clear[ClearCard])
            {
                //�N���A�e�L�X�g�\��
                Mission_img.gameObject.SetActive(true);
                Mission_OK = true;
            }
            else
            {
                Mission_img.gameObject.SetActive(false);
                Mission_OK = false;
            }
        }
        if(Minssion_Num == 1)
        {
            if (use_Card_Amount[ClearCard] >= Use_Card_Clear[ClearCard])
            {
                //�N���A�e�L�X�g�\��
                Mission_img.gameObject.SetActive(true);
                Mission_OK = true;
            }
            else
            {
                Mission_img.gameObject.SetActive(false);
                Mission_OK = false;
            }
        }
    }
}
