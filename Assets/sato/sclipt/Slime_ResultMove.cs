using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime_ResultMove : MonoBehaviour
{
    [Header("���ɓ������x")]
    public float move_speed;

    [Header("�~�܂�ʒu")]
    public float stop_pos;

    [Header("�������牺�͂�����Ȃ�")]
    public bool goal_move = false;              //�S�[�����̃A�N�V�����Đ�


    private Vector3 mem_pos;    //���̃I�u�W�F�N�g�̏����ʒu�L��
    private Vector3 pos;        //���̃I�u�W�F�N�g�̈ʒu

    // Start is called before the first frame update
    void Start()
    {
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        DontDestroyOnLoad(GameObject.Find("slime_UI"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goal_move)
        {
            //�w�i�ړ�����
            pos.x += move_speed;
            this.gameObject.GetComponent<RectTransform>().position = pos;

            //�ʒu�����S�ɗ������A���U���g�ֈړ�
            if (this.gameObject.GetComponent<RectTransform>().localPosition.x > 0)  
            {
                //���U���g�V�[���łȂ��Ƃ�
                if (SceneManager.GetActiveScene().name != "Result")
                {
                    SceneManager.LoadScene("Result");
                }
            }
            if (pos.x > stop_pos) 
            {
                //�X�g�b�v������W
                pos = mem_pos;
                goal_move = false;
            }
        }
    }
}