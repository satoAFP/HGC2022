using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;  // �ǉ����܂��傤

public class Result : MonoBehaviour
{

    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC script; //�Q�ƌ�Script������ϐ�

    [Header("�\��txt�w��")]
    public GameObject score_object = null; // Text�I�u�W�F�N�g

    [Header("�\��txt�w��(Nest Stage�p)")]
    public GameObject stage_object = null; // Text�I�u�W�F�N�g

    [Header("�Ƃ肠���������iint�j")]
    public int score_num; // �X�R�A�ϐ�

    private string stage; // stage�ϐ�

    // ������
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        stage = script.NowStage;
    }

    // �X�V
    void Update()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        //����
        Text score_text = score_object.GetComponent<Text>();
        //�X�e�[�W
        Text stage_text = stage_object.GetComponent<Text>();

        if (stage == "Stage-2")
            stage_text.text = "coming soon...";

        // �e�L�X�g�̕\�������ւ���
        score_text.text = "Score:" + score_num;

        score_num += 1; // �Ƃ肠����1���Z�������Ă݂�
    }
}