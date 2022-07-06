using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    //�Q�ƌ�OBJ&script�ۑ�----------------------------------------
    public GameObject BackButton; 
    DeletAction script;

    public GameObject ActionButton; 
    ActionButton_SC scriptac; 

    GameObject player; //script��
    //------------------------------------------------------------

    //�X�b�[��Card������悤�ϐ��i���ݓ����j
    //public bool vanish;
    //private bool now_ani;

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾�p
    public Vector3 pos;
    private float first_x;//�����ʒu
    //--------------------------------

    //�����l�Ή�
    private GameObject[] players;
    //�v���C���[���ɖ��߂𑗂�p(�����l����)
    private bool Ps_flag;

    //�X���C���ɋz�����܂��ԍ��i�ʒu�j
    [SerializeField]
    private int move_num;

    //SE�I�u�W�F�N�g�擾
    public AudioClip sound1;
    private GameObject SE_Maneger;
    AudioSource audioSource;

    //�A�j���[�V�����ۑ�
    Animator myanim;

    // Start is called before the first frame update
    void Start()
    {
        //�폜OBj�Ƃ̘A�g
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        //ActionButton�Ƃ̘A�g
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        //�ŏ��ɏo���ʒu���o����i�߂鏈���Ɏg���j
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
        //vanish = true;
        //now_ani = false;

        //PL
        player = GameObject.Find("Player"); 
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

        //SE�n�擾
        SE_Maneger = GameObject.Find("SE_manager"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        audioSource = SE_Maneger.GetComponent<AudioSource>();

        //���g�̃A�j���[�V�����擾
         myanim= this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    //Button�������ꂽ��
    public void PushButton(bool set)
    {
        //���N���̏ꍇ
        if (Input.GetMouseButtonDown(0))
        {
            //�����鏈���i�����j
            //now_ani = true;

            //���͏���(RUN)
            if (move_num == 3)
            {
                //�����l����Ȃ�
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_run();
                    }

                }
                //��l�̂�
                else
                {
                    player.GetComponent<Player>().Push_run();
                }


            }
            //STICK
            else if (move_num == 2)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_stick();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_stick();
                }

            }
            //SQUAT
            else if (move_num == 1)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_squat();
                    }
                }
                else
                {

                    player.GetComponent<Player>().Push_squat();
                }

            }
            //JUMP
            else if (move_num == 0)
            {
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_jump();
                    }
                }
                else
                {
                    player.GetComponent<Player>().Push_jump();

                }

            }

            //���g�̎q�����畡���ΏۑI����������-------------------------------

            //���݈ʒu�擾
            pos = this.gameObject.transform.position;

            //image_move���擾�i���g�̎q������ԑ����j
            GameObject child = transform.Find("image_move").gameObject;

            //�����A�������M���𔭐M
            GameObject newObj = Instantiate(child, ActionButton.transform, false);
            newObj.GetComponent<Image_move>().Move_on = true;


            newObj.GetComponent<Image_move>().parent_pos = pos;

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


            if (pos.x > 0.0f)
            {
                //���G�t�F�N�g�������i�ʒu�ɂ��ύX�j
                GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                efe.GetComponent<Effect_move>().SetActive(false);
                efe.GetComponent<Effect_move>().first_EF = false;
                efe.GetComponent<Effect_move>().now_onecard = false;
            }

            //�������������ʒu�ɖ߂�
            this.gameObject.transform.position = new Vector3(first_x, -365.0f, pos.z);
            this.tag = "Untagged";

            //�o�b�N�{�^���ɓo�^
            script.objs[script.now] = this.gameObject;
            script.now++;

            //�g�p��+1
            scriptac.set_text(move_num, 1);
        }
        //�E�N��
        else if (Input.GetMouseButtonDown(1) && set == false)
        {
            //���݈ʒu�擾
            pos = this.gameObject.transform.position;
            //���݂���}���`��S�擾
            GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

            //�ړ����multiCard�����Ȃ��Ȃ�
            if (multi.Length == 0)
            {
                //�K��l������O�Ȃ�
                if (pos.x >= first_x - 1.0f && pos.x <= first_x + 1.0f)
                {
                    this.gameObject.transform.position = new Vector3(17.7f, pos.y, pos.z);
                    audioSource.PlayOneShot(sound1);
                }
                //�ꏊ���Ȃ��̂ŏ����ʒu��
                else if (pos.x > 0.0f)
                {
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

                    //���G�t�F�N�g�������i�ʒu�ɂ��ύX�j
                    GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                    efe.GetComponent<Effect_move>().SetActive(false);
                    efe.GetComponent<Effect_move>().first_EF = false;
                    efe.GetComponent<Effect_move>().now_onecard = false;

                    audioSource.PlayOneShot(sound1);
                }
            }
            //����Ȃ�
            else if (multi.Length == 1)
            {
                //���Ƃ֕Ԃ�
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
            }


            //�ړ���̏ꏊ�擾
            pos = this.gameObject.transform.position;

            //���ꂼ��̏ꏊ��tag�t�^�i�}���`�N���G�C�g�ցj
            if (pos.x > 0.0f)
            {
                this.tag = "Multi_action1";
            }
            else
                this.tag = "Untagged";
        }
        //�^�񒆃N��
        else if (Input.GetMouseButtonDown(2))
        {
            //�����ʒu�ցi�^�O���ĕt�^�j
            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
            this.tag = "Untagged";

            //���G�t�F�N�g�������i�ʒu�ɂ��ύX�j
            GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

            efe.GetComponent<Effect_move>().SetActive(false);
            efe.GetComponent<Effect_move>().first_EF = false;
            efe.GetComponent<Effect_move>().now_onecard = false;

            audioSource.PlayOneShot(sound1);
        }
        //���X�N���v�g���\����
        else if (set == true)
        {
            //�}���`�N���G�C�g����̐\��
            //���g�̏����������Ɠ���-------------------�ȉ�����
            script.objs[script.now] = this.gameObject;
            script.now++;

            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);

            //�^�O��������
            this.tag = "Untagged";

            //���𑝂₷
            scriptac.set_text(move_num, 1);
        }

        else if (set == false)
        {
            //�o�b�N�Ŗ߂��ꂽ�Ƃ��A����߂�
            scriptac.set_text(move_num, -1);

        }
    }

    //���X�N���v�g���炢����悤���񂷂�
    public void Set_Active(bool set)
    {
        //�t�F�[�h�����i�����j
        //now_ani = false;
        PushButton(set);
    }

    //�����ʒu�ɖ߂炷
    public void Set_Back()
    {
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.tag = "Untagged";
    }

    //�J�[�\�����d�Ȃ��Ă�����
    public void Choiceing(bool a)
    {
        myanim.SetBool("choiceing", a);
    }
}
