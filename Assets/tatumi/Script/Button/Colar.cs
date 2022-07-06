using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���F�ω���UI����(���ݎg��Ȃ����ߓ���)
public class Colar : MonoBehaviour
{
    const int MAX_COLOR = 255;

    //�Q�[�~���O�F�p�ϐ��Q-------------
    private int a, b, c;
    private bool A, B, C;
    private int time,time2;
    //--------------------------------

    //�}���`�쐬�\�����f�p
    private bool OK = false;
    //���̂ł��邩�ǂ����̃T�C���p�B(����)
    private string[] multi_oks = new string[2];


    //���g�擾�B����������this.gameObject�o����
    public Button button;

    MultuAction_Creit script; //�Q�ƌ�Script������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 255;
        c = 128;

        time = 0;
        time2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        time2++;

        //������
        OK = false;

        //�ŏ�������ƃo�O���ł��傢�҂�����̏���
        if (time2 > 10)
        {
            //�}���`�쐬�\���}���`�N���G�C�g�����񎝂��Ă���
            multi_oks = button.GetComponent<MultuAction_Creit>().get_multi_oks(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
           
            for (int i = 0; i != 2; i++)
            {
                //���O�̏���������}���`~�Ȃ�OK
                if (multi_oks[i].Contains("multi") == true)
                {
                    OK = true;
                }
            }
        }

        
        //�Q�[�~���O�F�p
        if (OK == true)
        {
            time++;

            //�܂�Ԃ�flag�ݒ�
            if (a < 1)
            {
                A = true;
            }
            else if (a > MAX_COLOR)
            {
                A = false;
            }
            if (b < 1)
            {
                B = true;
            }
            else if (b > MAX_COLOR)
            {
                B = false;
            }
            if (c < 1)
            {
                C = true;
            }
            else if (c > MAX_COLOR)
            {
                C = false;
            }

            //�ݒ�ɏ]������
            if (A == true)
                a++;
            else
                a--;

            if (B == true)
                b++;
            else
                b--;

            if (C == true)
                c++;
            else
                c--;

            //�F�����ꂼ��̃J���[�Ɋ���U��B
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32((byte)a, (byte)b, (byte)c, MAX_COLOR);
            if (time == 40)
                cb.highlightedColor = new Color32(MAX_COLOR, MAX_COLOR, 0, MAX_COLOR);
            else if (time == 60)
            {
                cb.highlightedColor = new Color32(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
                time = 0;
            }
            button.colors = cb;
        }
        else 
        {
            //�Ȃ���Ȃ��Ƃ��͏����F��
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
            button.colors = cb;
        }


    }
}
