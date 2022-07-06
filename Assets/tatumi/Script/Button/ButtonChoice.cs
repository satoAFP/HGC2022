using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    //�i�[�pOBJ�Q-----------------------------

    //�u�߂�v�����֘A
    public GameObject BackButton; 
    DeletAction script; 

    //�I���񐔏����֘A
    public GameObject ActionButton; 
    ActionButton_SC scriptac; 

    //�s�����ߗp
    GameObject player; 
    //----------------------------------------

    //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
    //public bool vanish;

    //����ANIM��
    private bool now_ani;

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾
    public Vector3 pos;
    private float first_x;//�����ʒu

    //�����l�Ή�
    private GameObject[] players;

    //PL���������邩�pflag
    private bool Ps_flag;

    //�X���C���ɋz�����܂��ԍ��i�ʒu�j
    private int move_num;

    //SE�I�u�W�F�N�g�擾
    public AudioClip sound1;
    private GameObject SE_Maneger;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //script��Obj�i�[---------------------------------------------
        BackButton = GameObject.Find("BackButton"); 
        script = BackButton.GetComponent<DeletAction>(); 

        ActionButton = GameObject.Find("ActionBotton"); 
        scriptac = ActionButton.GetComponent<ActionButton_SC>();
        //------------------------------------------------------------

        //�ŏ��ɏo���ʒu���o����i�߂鏈���Ɏg���j
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //�X�b�[�Ə�����悤�ϐ��i���ݓ����j
        //vanish = true;
        //now_ani = false;

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

        //�ŏ��̈ꂩ�玩�g�̍s�����߂��������擾
        if ((-first_x / 26) < 1)
            move_num = 3;
        else if ((-first_x / 26) < 2)
            move_num = 2;
        else if ((-first_x / 26) < 3)
            move_num = 1;
        else if ((-first_x / 26) < 1)
            move_num = 0;

        //���ʉ��֘A
        SE_Maneger = GameObject.Find("SE_manager"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        audioSource = SE_Maneger.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void PushButton(bool set)
    {
        //Anim������Ȃ���΍s��(����)
        //if (now_ani == false)

        //�{�^���������Ƃ��̏���-----------------------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            //���͏���(RUN)
            if ((-first_x / 26) < 1)
            {
                //PL���������邩����,���ߑ��M
                if (Ps_flag == true)
                {
                    for (int i = 0; i != players.Length; i++)
                    {
                        players[i].GetComponent<Player>().Push_run();
                    }

                }
                else
                {
                    player.GetComponent<Player>().Push_run();
                }


            }
            //STICK
            else if (-(first_x / 26) < 2)
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
            else if ((-first_x / 26) < 3)
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
            else if ((-first_x / 26) < 4)
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


            //image_move���擾�i���g�̎q������ԑ����j
            GameObject child = transform.Find("image_move").gameObject;

            //�����A�������M���𔭐M
            GameObject newObj = Instantiate(child, ActionButton.transform, false);
            newObj.GetComponent<Image_move>().Move_on = true;

            //���g�̏o���ʒu�𒲐�
            newObj.GetComponent<Image_move>().parent_posx = -85.5f + (28.0f * move_num);
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

            //���݈ʒu�擾
            pos = this.gameObject.transform.position;

            //�����ʒu����Ȃ炪������ׂ����̏�Ԃ�����
            if (pos.x > 0.0f)
            {
                //���G�t�F�N�g�������i�ʒu�ɂ��ύX�j
                GameObject efe = ActionButton.transform.Find("PS_Smook_Left").gameObject;

                efe.GetComponent<Effect_move>().SetActive(false);
                efe.GetComponent<Effect_move>().first_EF = false;
                efe.GetComponent<Effect_move>().now_onecard = false;
            }

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
        else if (Input.GetMouseButtonDown(1) && set == false)
        {
            //���݈ʒu�擾
            pos = this.gameObject.transform.position;

            //MultiCard�����邩�擾(���ł�r���̂���J�E���g���邽��)
            GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

            //���Ȃ��Ƃ�
            if (multi.Length == 0)
            {
                //���̘g�ֈړ��i�����ʒu����̈ړ��j
                if (pos.x >= first_x - 1.0f && pos.x <= first_x + 1.0f)
                {
                    this.gameObject.transform.position = new Vector3(17.7f, pos.y, pos.z);
                    audioSource.PlayOneShot(sound1);
                }
                //�����ʒu�ֈړ��i���̘g����̈ړ��j
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
            //�����Ƃ��͖ⓚ���p�ŕԂ�
            else if (multi.Length == 1)
            {
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
            }


            //�ړ���̏ꏊ�擾
            pos = this.gameObject.transform.position;

            //���ꂼ��̏ꏊ��tag�t�^�i�}���`�N���G�C�g�ցj
            if (pos.x > 0.0f)
            {
                //���̃J�[�h�ҋ@���
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

            //SE�炷
            audioSource.PlayOneShot(sound1);
        }
        //---------------------------------------------------------------
        //���X�N���v�g���\����-----------------------------------------
        else if (set == true)
        {
            //�}���`�N���G�C�g����̐\��
            //���g�̏����������Ɠ���-------------------�ȉ�����
            script.objs[script.now] = this.gameObject;
            script.now++;

            //�����ʒu�ֈړ�
            this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
            //���̃��[�h����
            this.tag = "Untagged";
            //���̕��g�����̂�+1
            scriptac.set_text(move_num, 1);
        }
        else if (set == false)
        {
            //�o�b�N�Ŗ߂��ꂽ�Ƃ��A����߂�
            scriptac.set_text(move_num, -1);

        }
        //-------------------------------------------------------------------
        
       
    }

   
    //���X�N���v�g���炢����悤���񂷂�
    public void Set_Active(bool set)
    {
        //�A�j���[�V�����p�i�����j
        //now_ani = false;

        //���X�N���v�g����̐\�����̍s��
        PushButton(set);
    }

    //�����ʒu�ɖ߂炷
    public void Set_Back()
    {
        //�����ʒu�֖߂��A�ʏ�̏�Ԃ֖߂�
        this.gameObject.transform.position = new Vector3(first_x, 83.19456f, pos.z);
        this.tag = "Untagged";
    }

}
