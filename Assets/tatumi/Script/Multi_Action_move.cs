using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    //�ԍ��ŉ����邩���߂�
    [Header("���̔ԍ�")]
    public int action_num;

    private GameObject[] players;

    GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾
    private Vector3 pos;
    private float first_x;//�����ʒu

    private bool now_ani,Ps_flag;

    //�G�t�F�N�g�n
    private GameObject effect;
    Effect_move EF_script;
    private GameObject UIs;

    // Player script; //�Q�ƌ�Script������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //PL
        player = GameObject.Find("Player"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����

        //�߂��{�^��
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //�o�b�N�Ƃ̘A�g------------------------------
        script.multi_objs[script.multi_now] = this.gameObject;
        script.timing[script.multi_now] = script.now;
        //�쐬�Ɠ����ɓo�^
        script.multi_now++;
        //-------------------------------------------

        //�����擾
        players=GameObject.FindGameObjectsWithTag("Player");

        //�����Ώۏ����̃I���I�t
        if (players.Length>1)
        {
            Ps_flag = true;
        }
        else
        {
            Ps_flag = false;
        }

        //�A�j���[�V�����i���j
        now_ani = false;

        //�G�t�F�N�g�e�擾
        UIs = GameObject.Find("ActionBotton"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����


        if (first_x > 5.0f)
        {
            //�G�t�F�N�g�͐������͓����i�O��ǂ������j
            effect = UIs.transform.Find("PS_Back_Right").gameObject;
        }
        else
        {
            //�G�t�F�N�g�͐������͓����i�ǂ������j
            effect = UIs.transform.Find("PS_Back_Left").gameObject;
        }
        //�X�N���v�g�Q�b�g
        EF_script=effect.GetComponent<Effect_move>();
        //Back�Đ�
        EF_script.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        //�A�j���[�V�����i���j
        if (now_ani == false)
        {
            //�X�N���v�g�ɐM�����M
            GetComponent<Image_multimove>().Move_on = true;
            
            //�ԍ��ɂ��A�N�V�����ύX
            if (action_num == 0)
            {
                if (Ps_flag == true)
                {
                    for(int i=0;i!= players.Length;i++)
                    {
                        players[i].GetComponent<Player>().Push_highjump();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_highjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 1)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_wallkick();
                        Set_Back();
                    }
                }
                else
                {
                   // player.GetComponent<Player>().Push_wallkick();
                    Set_Back();
                }
               
            }
            else if (action_num == 2)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_longjump();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_longjump();
                    Set_Back();
                }
               
            }
            else if (action_num == 3)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_sliding();
                        Set_Back();
                    }
                }
                else
                {
                    //player.GetComponent<Player>().Push_sliding();
                    Set_Back();
                }
              
            }
        }

        
    }

    void Set_Back()
    {
        //�A�j���[�V�����i���j�ꉞ�������ɔ������Ȃ��̂����˂Ă�֗�
        now_ani = true;

        //�G�t�F�N�g�͎~�߂�
        EF_script.SetActive(false);

        //�A�j���[�V�����i���j
        //Invoke(nameof(null_active), 1.15f);
        //Button.SetActive(false);

        //�o�b�N�Ƀ}���`���g��o�^
        script.objs[script.now] = this.gameObject;
        script.now++;
       
        Debug.Log("thornHit(up)!");

        //�������������ʒu�ɖ߂�
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);

        //�o�b�N�Ƀ}���`���g��o�^
        //script.objs[script.now] = this.gameObject;
        //script.now++;

        //���s�����A�N�V�������Œᐔ�Ƃ��Đݒ�
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().set_text(action_num+4,1);
        //���s�����A�N�V�������폜�\��ɒǉ�
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().multi_des_Check(this.gameObject, true);
    }

    //�A�j���[�V�����i���j
    void null_active()
    {
        Button.SetActive(false);
    }

    //�߂��Ƃ�
    public void Set_Active(bool a)
    {
        ////�A�j���[�V�����i���j�Ɣ�������悤��
        now_ani = false;

        //�܂��ĊJ�i�����͂Ȃ��j
        EF_script.SetActive(true);

        //������Ԃ�---------------------------------------------------------------------------
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.transform.localScale = new Vector3(1, 1,1);

        //���s�����A�N�V�������Œᐔ�Ƃ��Đݒ�
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().set_text(action_num + 4, -1);
        //���s�����A�N�V�������폜�\�蕪���Ȃ���
        GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().multi_des_Check(this.gameObject, false);

        GetComponent<Image_multimove>().Move_on = false;
        GetComponent<Image_multimove>().time = 0;
        Debug.Log("thornHit(up)!");
        Button.SetActive(a);
        //---------------------------------------------------------------------------------------
    }
    //Eff�����悤
    public void Eff_active()
    {
        //�G�t�F�N�g�͎~�߂�
        EF_script.SetActive(false);
    }
}
