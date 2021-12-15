using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC scriptac; //�Q�ƌ�Script������ϐ�

    GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
    public bool vanish;
    private bool now_ani;

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾
    private Vector3 pos;
    private float first_x;//�����ʒu

    //�����l�Ή�
    private GameObject[] players;
    private bool Ps_flag;

    //�X���C���ɋz�����܂��ԍ��i�ʒu�j
    private int move_num;

    // Start is called before the first frame update
    void Start()
    {
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        ActionButton = GameObject.Find("ActionBotton"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        //�ŏ��ɏo���ʒu���o����i�߂鏈���Ɏg���j
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
        vanish = true;
        now_ani = false;

        //PL
        player = GameObject.Find("Player"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        //�����擾
        players = GameObject.FindGameObjectsWithTag("Player");

        //�����Ώۏ����̃I���I�t
        if (players.Length > 1)
        {
            Ps_flag = true;
        }
        else
        {
            Ps_flag = false;
        }

        if ((-first_x / 26) < 1)
            move_num = 3;
        else if ((-first_x / 26) < 2)
            move_num = 2;
        else if ((-first_x / 26) < 3)
            move_num = 1;
        else if ((-first_x / 26) < 1)
            move_num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.transform.position;

    }

    public void PushButton(bool set)
    {
        //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
        if (now_ani == false)
        {
           
            //���N��
            if (Input.GetMouseButtonDown(0))
            {

                //now_ani = true;

                //���͏���
                if ((-first_x / 26) < 1)
                {
                    Debug.Log("OK");
                    if (Ps_flag == true)
                    {
                        for (int i = 0; i != players.Length; i++)
                        {
                           // players[i].GetComponent<Player>().Push_run();
                        }

                    }
                    else
                    {
                        //player.GetComponent<Player>().Push_run();
                    }

                   
                }
                else if (-(first_x / 26) < 2)
                {
                    if (Ps_flag == true)
                    {
                        for (int i = 0; i != players.Length; i++)
                        {
                           // players[i].GetComponent<Player>().Push_stick();
                        }
                    }
                    else
                    {
                        //player.GetComponent<Player>().Push_stick();
                    }
                   
                }
                else if ((-first_x / 26) < 3)
                {
                    if (Ps_flag == true)
                    {
                       

                        for (int i = 0; i != players.Length; i++)
                        {
                            //players[i].GetComponent<Player>().Push_squat();
                        }
                    }
                    else
                    {
                       
                        //player.GetComponent<Player>().Push_squat();
                    }
                   
                }
                else if ((-first_x / 26) < 4)
                {
                    if (Ps_flag == true)
                    {
                        for (int i = 0; i != players.Length; i++)
                        {
                            //players[i].GetComponent<Player>().Push_jump();
                        }
                    }
                    else
                    {
                        //player.GetComponent<Player>().Push_jump();
                       
                    }
                    
                }

                //���g�̎q�����畡���ΏۑI����������-------------------------------

                //�q�����擾�Ȃ��g���
                //int n = this.transform.parent.childCount;

                //image_move���擾�i���g�̎q������ԑ����j
                GameObject child = transform.Find("image_move").gameObject;

                //�����A�������M���𔭐M
                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;

                //0=-90.6f,1=-64.5,2=-38.5f,3=-12.5f//��26
                newObj.GetComponent<Image_move>().parent_posx = -90.5f+(26.0f*move_num);
                //------------------------------------------------------------------


                //�X�b�[�Ə�����悤�ϐ��i���ݓ����j--------------------------------
                //var animator = Button.GetComponent<Animator>();
                //animator.Play("Selected");
                //animator.Update(0f);

                //����������script�Ɉ�C
                //if (set==false)
                //Invoke(nameof(null_active), 1.15f);
                //else
                //    Button.SetActive(false);
                //vanish = true;

                //---------------------------------------------------------------------

                //�������������ʒu�ɖ߂�
                this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
                this.tag = "Untagged";

                //�o�b�N�{�^���ɓo�^
                script.objs[script.now] = this.gameObject;
                script.now++;

                //�g�p��+1
                scriptac.set_text(move_num, 1);
            }
            //�E�N��
            else if (Input.GetMouseButtonDown(1)&&set==false)
            {
                //���݈ʒu�擾
                pos = this.gameObject.transform.position;
                GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

                if (multi.Length == 0)
                {
                    if (pos.x == first_x)
                        this.gameObject.transform.position = new Vector3(29.7f, pos.y, pos.z);
                    else
                        this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

                }
                else if (multi.Length == 1)
                {
                    Vector3 multi_pos = multi[0].gameObject.transform.position;

                    if (multi_pos.x == 3.85f)
                        this.gameObject.transform.position = new Vector3(29.7f, pos.y, pos.z);
                    else
                        this.gameObject.transform.position = new Vector3(3.85f, pos.y, pos.z);
                }
                else if (multi.Length == 2)
                {
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                   
                }

                //�ړ���̏ꏊ�擾
                pos = this.gameObject.transform.position;

                //���ꂼ��̏ꏊ��tag�t�^�i�}���`�N���G�C�g�ցj
                if (pos.x == 3.85f)
                {
                    this.tag = "Multi_action1";
                }
                else if (pos.x == 29.7f)
                    this.tag = "Multi_action2";
                else
                    this.tag = "Untagged";
            }
            //�^�񒆃N��
            else if (Input.GetMouseButtonDown(2))
            {
                //�����ʒu�ցi�^�O���ĕt�^�j
                this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
                this.tag = "Untagged";
            }
            //���X�N���v�g���\����
            else if (set == true)
            {
                //�}���`�N���G�C�g����̐\��
                //���g�̏����������Ɠ���-------------------�ȉ�����
                script.objs[script.now] = this.gameObject;
                script.now++;
               
                this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
                this.tag = "Untagged";
                scriptac.set_text(move_num, 1);
            }

            else if (set == false)
            {
                //�o�b�N�Ŗ߂��ꂽ�Ƃ��A����߂�
                scriptac.set_text(move_num, -1);

            }

        }
       
    }

    //Inovek�悤���������g���i�A�j���[�V�����Ŏg�p�j
    //void null_active()
    //{
    //    Button.SetActive(false);
    //}

    //���X�N���v�g���炢����悤���񂷂�
    public void Set_Active(bool set)
    {
        now_ani = false;
        PushButton(set);
    }

    //�����ʒu�ɖ߂炷
    public void Set_Back()
    {
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.tag = "Untagged";
    }

}
