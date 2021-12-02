using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clown : MonoBehaviour
{
    [Header("�㉺�ړ����x")]
    public float max_Speed;
    [Header("��]���x")]
    public float role_Speed;

    private Vector3 first_pos;          //��l�������ʒu
    private float move_speed = 0.0f;    //�㉺�̈ړ���
    private int frame_count = 0;        //�㉺�̓����𐧌�

    // Start is called before the first frame update
    void Start()
    {
        //��l���̏����ʒu�L��
        first_pos = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�㉺�ړ�����
        frame_count++;
        if (frame_count >= 0 && frame_count < 30)
        {
            move_speed += max_Speed;
        }
        else if (frame_count >= 30 && frame_count < 60)
        {
            move_speed -= max_Speed;
        }
        else if (frame_count >= 60 && frame_count < 88)
        {
            move_speed -= max_Speed;
        }
        else if (frame_count >= 88 && frame_count < 116)
        {
            move_speed += max_Speed;
        }
        
        //�ړ��ʉ��Z
        this.gameObject.transform.localPosition += new Vector3(0.0f, move_speed, 0.0f);
        this.gameObject.transform.localEulerAngles += new Vector3(0.0f, role_Speed, 0.0f);

        //�㉺�ړ��̌덷�C��
        if(frame_count>=115)
        {
            frame_count = 0;
            move_speed = 0.0f;
            this.gameObject.transform.localPosition = first_pos;
        }
    }
}
