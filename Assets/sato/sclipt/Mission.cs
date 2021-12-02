using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    [Header("�������擾�ŃN���A��")]
    public int Clown_Clear;

    [Header("�n�C�W�����v�F4�@�ǃL�b�N�F5�@�����сF6�@�X���C�f�B���O�F7")]
    [Header("�W�����v�F0�@���Ⴊ�݁F1�@�������F2�@����F3")]
    [Header("�����N���A����ɂ��邩")]
    public int ClearCard;

    [Header("���ꂼ�ꉽ���ȓ��Ń~�b�V�����N���A��")]
    public int[] Use_Card_Clear;

    [Header("�����擾���\���摜")]
    public GameObject Clown_img;

    [Header("�~�b�V�����N���A�e�L�X�g")]
    public Text Mission_text;

    private int clown_get = 0;
    private int[] use_Card_Amount;

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
    }

    // Update is called once per frame
    void Update()
    {
        clown_get = this.gameObject.GetComponent<Player>().clown_get;
        use_Card_Amount = this.gameObject.GetComponent<Player>().Use_Card_Amount;

        //�����擾
        if (clown_get == Clown_Clear) 
        {
            //�N���A�e�L�X�g�\��
            Clown_img.gameObject.SetActive(true);
        }

        //�~�b�V�����p�J�[�h�g�p�񐔐���
        if (use_Card_Amount[ClearCard] <= Use_Card_Clear[ClearCard])
        {
            //�N���A�e�L�X�g�\��
            Mission_text.text = "�N���A";
        }
        else
        {
            Mission_text.text = "���s";
        }
    }
}
