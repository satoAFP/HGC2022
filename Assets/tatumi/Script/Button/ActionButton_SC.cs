using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

//�A�N�V�����{�^���S�̂̊Ǘ�
public class ActionButton_SC : MonoBehaviour
{
    //�Œ�l
    private const int MAX_CARD_RESERVE = 11;
    private const int MAX_CARD_TYPE = 8;
    private const int CARD_MULTI_BOLDER = 4;

    //�v���C���[���g������g(�K�����邽�ߐݒ�Ȃ�)
    GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�
    Player script; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    //�񐔕\���p�ϐ�---------------------------------------
    //���݂̗\��A�N�V����
    private int[] action_num=new int[MAX_CARD_TYPE];
    //���s�ς݃A�N�V����
    private int[] executed_action = new int[MAX_CARD_TYPE];
    //-----------------------------------------------------

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //�\��Card�����p�󂯎���(�v�f11)
    public GameObject[] multi_des = new GameObject[MAX_CARD_RESERVE];
    //���݂̏����҂��̏���
    private int multi_des_now = 0;

    //�A�N�V�����I�����̑��I�u�W�F�F���p�ϐ�
    public int PL_action_num;

    // Start is called before the first frame update
    void Start()
    {
       
        //�I���񐔏�����
        for (int i = 0; i != MAX_CARD_TYPE; i++)
        {
            //������
            executed_action[i] = 0;
        }
     
        //player�֘A----------------------------------------------------------------------
        player = GameObject.Find("Player"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = player.GetComponent<Player>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
        //--------------------------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
       
    }

  
    //Action_Card��int�ł�type����
    /*
        JUMP,0
        SQUAT,1
        STICK,2
        RUN,3
        HIGHJUMP,4
        WALLKICK,5
        LONGJUMP,6
        SLIDING,7
    */

   
    //�֐��őI�񂾐��𑝌�
    public void set_text(int Action_Card, int add)
    {
        //�I���񐔂̐��̑���(+,-�Ή�)
        if ((action_num[Action_Card] + add) >= executed_action[Action_Card])
            action_num[Action_Card] += add;
    }

    //���݂̐�(�擾�p)
    public int get_score(int Action_Card)
    {
        return action_num[Action_Card] + executed_action[Action_Card];
    }


    //���s�����A�N�V�������Œᐔ�Ƃ��ēǂݍ���
    public void executed_Action(int Action_Card)
    {
        //���̃J�[�h���ۂ�
        if (Action_Card >= CARD_MULTI_BOLDER)
        {
            //�G���[���
            if (multi_des[0] != null)
            {
                //�g�p�����ꍇ�L�^�̂ݎc���A�{�̍폜
                if (multi_des[0].activeSelf == false)
                {
                    Destroy(multi_des[0]);

                    //�폜�̂��ߋL�^�̐���
                    for (int i = 1; i != MAX_CARD_RESERVE; i++)
                    {
                        multi_des[i - 1] = multi_des[i];
                    }

                    multi_des_now--;
                }
            }
        }

        //�ʏ�A�N�V�����̏ꍇ�i�G���[����܂߁j
        if (Action_Card != -1)
        {
            executed_action[Action_Card]++;
            action_num[Action_Card]--;
        }
       
        //���݂̑I���A�N�V����
        PL_action_num = Action_Card;
    }

    //                    true  false
    //�}���`�̏����҂���@�ǉ��ƍ폜��flag�Ŕ���
    public void multi_des_Check(GameObject multi_obj,bool flag)
    {
        //�ǉ�
        if(flag==true)
        {
           
            multi_des[multi_des_now] = multi_obj;

            multi_des_now++;
        }
        //�폜
        else
        {
            //�G���[���
            if (multi_des[0] != null)
            {
                multi_des[multi_des_now] = null;

                multi_des_now--;
              
            }
            
        }

        //�ꉞ����-�̈�ɂ����Ȃ��悤�ی�
        if (multi_des_now < 0)
            multi_des_now = 0;

    }

    //���g���\��
    public void Set_OffActive()
    {
        this.gameObject.SetActive(false);
    }
}
