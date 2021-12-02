using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_side_move : MonoBehaviour
{
    [Header("�ړ����x(2�̔{����)")]
    public float move_speed;

    [Header("�L�����o�X�̉��̃T�C�Y")]
    public int canvas_size;

    [Header("�y�[�W��")]
    public int page_amount;

    private bool right_flag = false;        //�E�{�^���������Ƃ�true
    private bool left_flag = false;         //���{�^���������Ƃ�true
    private int move_pos = 0;               //�X�e�[�W�I���̈ړ���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (right_flag == true) 
        {
            this.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(-move_speed, 0.0f);

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x == move_pos) 
            {
                right_flag = false;
            }
        }

        if (left_flag == true)
        {
            this.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(move_speed, 0.0f);

            if (this.gameObject.GetComponent<RectTransform>().anchoredPosition.x == move_pos)
            {
                left_flag = false;
            }
        }

    }

    //���E�{�^��
    public void PushRight()
    {
        //�|�W�V�������A�y�[�W��*�L�����o�X�T�C�Y���傫���Ƃ������Ȃ�
        if (move_pos > -((page_amount - 1) * canvas_size))
        {
            right_flag = true;
            move_pos -= canvas_size;
        }
    }
    public void PushLeft()
    {
        //�|�W�V�������A0��菬�����Ƃ��͓����Ȃ�
        if (move_pos < 0)
        {
            left_flag = true;
            move_pos += canvas_size;
        }
    }

}
