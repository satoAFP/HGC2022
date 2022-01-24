using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_move_UI : MonoBehaviour
{
    [Header("���ɓ������x")]
    public float move_speed;

    [Header("���ɓ����|�W�V����")]
    public float stop_pos;

    [Header("�J�[�\�����킹������SE")]
    public AudioClip pic;

    private Vector3 mp;         //�}�E�X�̃|�W�V����
    private Vector3 mem_pos;    //���̃I�u�W�F�N�g�̏����ʒu�L��
    private Vector3 pos;        //���̃I�u�W�F�N�g�̈ʒu
    private Vector2 size;       //���̃I�u�W�F�N�g�̃T�C�Y
    private AudioSource audio;  //�g�p����I�[�f�B�I�\�[�X
    private bool first = true;  //��񂵂����������Ȃ�

    // Start is called before the first frame update
    void Start()
    {
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        size = this.gameObject.GetComponent<RectTransform>().sizeDelta;

        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�}�E�X�̃|�W�V�����X�V
        mp = Input.mousePosition;

        if ((mp.x >= pos.x - (size.x / 2)) && (mp.x <= pos.x + (size.x / 2)) &&
            (mp.y >= pos.y - (size.y / 2)) && (mp.y <= pos.y + (size.y / 2)))
        {
            if (this.gameObject.GetComponent<RectTransform>().position.x > stop_pos)
            {
                pos.x -= move_speed;
                this.gameObject.GetComponent<RectTransform>().position = pos;

                if (first)
                {
                    audio.PlayOneShot(pic);
                    Debug.Log("" + this.gameObject.GetComponent<RectTransform>().position+mp);
                    first = false;
                }
            }
        }
        else
        {
            if (this.gameObject.GetComponent<RectTransform>().position.x < mem_pos.x) 
            {
                pos.x += move_speed;
                this.gameObject.GetComponent<RectTransform>().position = pos;
            }
            first = true;
        }

        
        
    }
}
