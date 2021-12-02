using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colar : MonoBehaviour
{
    private int a, b, c;
    private bool A, B, C;
    private int time;

    private bool OK = false;

    public Button button;
    //���̂ł��邩�ǂ����̃T�C���p�B
    private string[] multi_oks = new string[4];

    MultuAction_Creit script; //�Q�ƌ�Script������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 255;
        c = 128;

        time = 0;
        //script = button.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
    }

    // Update is called once per frame
    void Update()
    {
        multi_oks = button.GetComponent<MultuAction_Creit>().get_multi_oks(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
        OK = false;
       

        for (int i = 0; i != 4; i++)
        {
            //���O�̏���������
            if (multi_oks[i].Contains("multi") == true)
            {
                OK = true;
               
                
            }
            

        }

        

        if (OK == true)
        {
            time++;
            if (a < 1)
            {
                A = true;
            }
            else if (a > 255)
            {
                A = false;
            }
            if (b < 1)
            {
                B = true;
            }
            else if (b > 255)
            {
                B = false;
            }
            if (c < 1)
            {
                C = true;
            }
            else if (c > 255)
            {
                C = false;
            }

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

            ColorBlock cb = button.colors;
            cb.normalColor = new Color32((byte)a, (byte)b, (byte)c, 255);
            if (time == 40)
                cb.highlightedColor = new Color32(255, 255, 0, 255);
            else if (time == 60)
            {
                cb.highlightedColor = new Color32(255, 255, 255, 255);
                time = 0;
            }
            button.colors = cb;
        }
        else 
        {
            ColorBlock cb = button.colors;
            cb.normalColor = new Color32(255, 255, 255, 255);
            button.colors = cb;
        }


    }
}
