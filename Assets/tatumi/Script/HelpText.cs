using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    //���݂̃`���[�g���A���ŉ��Ԗڂ��F���p
    public int Helpmode;

    //���g�擾�p
    public Text text;

    //���ꂼ���UI�̊l��-------------------------------
    public GameObject[] yazirusi = new GameObject[2];
    public GameObject oya;
    public GameObject PL;
    public GameObject Card;
    public GameObject CardChoice;
    public GameObject BackButton;
    public GameObject ActionButton;
    //-------------------------------------------------

    //�X�N���v�g���擾--------------------------------------
    Player SC_player;
    DeletAction SC_Back;
    ActionButton_SC SC_action; //�Q�ƌ�Script������ϐ�
    //------------------------------------------------------

    //�e�L�X�g����(�A���ŕ\������悤)
    public int nowtext;

    [Header("�\�����͏��i��s16����17�������Ƃɉ��s�j")]
    public string[] chars;
    [Header("���͂̐؂�ւ��I���̐����w��")]
    public int[] Endchar;

    

    // Start is called before the first frame update
    void Start()
    {
        SC_player = PL.GetComponent<Player>();
        SC_Back = BackButton.GetComponent<DeletAction>();
        SC_action = ActionButton.GetComponent<ActionButton_SC>();
    }

    // Update is called once per frame
    void Update()
    {
        //�`���[�g���A��txt�̓��e���ǂ�����F�����\��
       if(Helpmode>0)
        text.text = chars[nowtext+1];
       else
            text.text = chars[nowtext];


       //�E�N���Ŏ��̃y�[�W��
        if (Input.GetMouseButtonDown(0))
        {
            nowtext++;
        }
        //���N���Ō��ݕ\���ł���͈͂ň�O�̃y�[�W�֖߂�
        else if (Input.GetMouseButtonDown(1))
        {
            nowtext--;
        }

        //-1�̃G���[���p
        if (nowtext < 0)
            nowtext = 0;

       
        //�ŏ��̎~�܂�ZONE
        if(Helpmode==0)
        {
            if (nowtext > Endchar[0])
                nowtext = Endchar[0];

            if (nowtext == 2)
            {
                //�G���^�[�œ�������������UI��\��
                if (Input.GetKey(KeyCode.Return))
                {
                   
                    oya.SetActive(false);
                    SC_player.Movestop = false;
                    
                }
            }
        }
        //���
        else if(Helpmode==1)
        {
            //�O�̒i�K��text���o���Ȃ��悤�ɂ���
            if (nowtext < Endchar[0])
                nowtext = Endchar[0];

            else if (nowtext >= Endchar[0] && nowtext <= Endchar[1])
            {
                Card.SetActive(true);
                if (nowtext == 3)
                {
                   //�����\��
                    yazirusi[0].SetActive(true);
                }
            }
           
            if (nowtext > Endchar[1]-1)
                nowtext = Endchar[1]-1;

            //Jump���I������Ă���
            if(SC_Back.objs[0].name.Contains("jump")==true && nowtext == Endchar[1]-1)
            {
                //���֐i�܂���i����Ȃ�UI�⓮�������ɖ߂��j
                oya.SetActive(false);
                SC_player.Movestop = false;
               
                Card.SetActive(false);
                yazirusi[0].SetActive(false);

                SC_Back.now--;
            }
        }
        //�R��
        else if (Helpmode == 2)
        {
            //�O�̒i�K��text���o���Ȃ��悤�ɂ���
            if (nowtext < Endchar[1])
                nowtext = Endchar[1];

            //UI�\���^�C�~���O
            else if (nowtext >= Endchar[1] && nowtext <= Endchar[2])
            {
                if (nowtext == 5)
                {
                    CardChoice.SetActive(true);
                  
                    yazirusi[1].SetActive(true);
                }
            }

            if (nowtext > Endchar[Helpmode]-1)
                nowtext = Endchar[Helpmode]-1;

            //���Ⴊ�ނ��I�����ꂽ��
            if (SC_Back.objs[0].name.Contains("squat") == true && nowtext == Endchar[Helpmode]-1)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                CardChoice.SetActive(false);
                yazirusi[1].SetActive(false);

            }
        }
        //�R��
        else if (Helpmode == 3)
        {
            //�O�̒i�K��text���o���Ȃ��悤�ɂ���
            if (nowtext < Endchar[2])
                nowtext = Endchar[2];

            //UI�\��
            else if (yazirusi[1].activeSelf == true)
            {
                if (nowtext < Endchar[2]+1)
                    nowtext = Endchar[2] + 1;
            }

            if (nowtext >= 7 && nowtext <=10)
            {
               Card.SetActive(true);

               for (int i = 0; i != 2; i++)
               {
                  yazirusi[i].SetActive(true);
               }

            }
            

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            //�n�C�W�����v�����ꂽ��I�Ԃ悤UI�ړ�
            if (SC_Back.multi_objs[0].name.Contains("multi_highjump(Clone)")==true)
            {
               
                yazirusi[0].SetActive(false);
                

                yazirusi[1].gameObject.transform.position = new Vector3(23.0f, 299.6f, -102.0f);
            }

            //�n�C�W�����v���I�����ꂽ��
            if (SC_action.multi_des[0].name.Contains("multi_highjump(Clone)") == true)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                Card.SetActive(false);
                for (int i = 0; i != 2; i++)
                {
                    yazirusi[i].SetActive(false);
                }

            }
        }
        //4��
        else if (Helpmode == 4)
        {
            //�O�̒i�K��text���o���Ȃ��悤�ɂ���
            if (nowtext < Endchar[3])
                nowtext = Endchar[3];

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            //�ǂݐ؂�܂œ������Ȃ�
            if (nowtext == Endchar[Helpmode] - 1)
            {
                //�ǂݐ؂�{�G���^�[�œ���������
                if (Input.GetKey(KeyCode.Return))
                {

                    oya.SetActive(false);
                    SC_player.Movestop = false;

                }
            }
        }


    }

    //Helpmode�ɂĎg�p
    public void SetHelp()
    {
        oya.SetActive(true);
    }
}
