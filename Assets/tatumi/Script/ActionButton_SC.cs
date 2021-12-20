using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;



public class ActionButton_SC : MonoBehaviour
{
    //[Header("���̃V�[���ȊO�͕\�����Ȃ�")]
    //[Header("���肵�����݂��Ȃ�-------------------------")]
    //public string SceneName;

    GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    Player script; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    //�񐔕\���p�ϐ�
    private int[] action_num=new int[8];
    private int[] executed_action = new int[8];

    //[Header("�G��Ȃ�")]
    //public string NowStage;

    [Header("�q�̗v�f��")]
    [Header("�������\��---------------------------------")]
    public int Child_num;

    [Header("�������w��")]
    public int[] Duplicate;

    [Header("����Obj�w��")]
    public GameObject[] childGameObjects;

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //�����p�󂯎���(�v�f11)
    private GameObject[] multi_des = new GameObject[11];
    private int multi_des_now = 0;

    //�A�N�V�����I�����̑��I�u�W�F�F���p�ϐ�
    public int PL_action_num;

    // Start is called before the first frame update
    void Start()
    {
        //���������i���݂���Ȃ��q�j----------------------------------------------------------------------------
        for (int i = 0; i != Child_num; i++)
        {
            for (int k = 0; k != Duplicate[i]; k++)
            {
                GameObject newObj = Instantiate(childGameObjects[i], this.transform, false);
            }
        }

        //for (int i = 0; i != 8; i++)
        //{

        //    //action_numtexts[i]=Instantiate(action_numtext, this.transform, false);
        //    //action_numtexts[i].transform.position = new Vector3(19.0f + (i * 130.0f), 117.0f, 0.0f);
        //}
        //-----------------------------------------------------------------------------------------------------

        //�I�񂾐��o�́i�Ǘ��j�̏�����-------------------------------------------------------------------------
        for (int i = 0; i != 8; i++)
        {

            //action_num[i] = Duplicate[i];
            executed_action[i] = 0;

            //action_numtexts[i].text = action_num[i].ToString();
        }
        //------------------------------------------------------------------------------------------------------


        //player�֘A
        player = GameObject.Find("Player"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = player.GetComponent<Player>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //�֐��őI�񂾐��𑝌�
    public void set_text(int a, int b)
    {
        if ((action_num[a] + b) >= executed_action[a])
            action_num[a] += b;
    }
    //���݂̐�(�擾�p)
    public int get_score(int a)
    {
        return action_num[a] + executed_action[a];
    }

    //���s�����A�N�V�������Œᐔ�Ƃ��ēǂݍ���
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
    public void executed_Action(int num)
    {
       // GameObject[] multi = this.transform.FindGameObjectsWithTag("Multis");

        if (num >= 4)
        {
            
                if (multi_des[0] != null)
                {
                    if (multi_des[0].activeSelf == false)
                    {
                        Destroy(multi_des[0]);

                        for (int i = 1; i != 11; i++)
                        {
                            multi_des[i - 1] = multi_des[i];
                        }

                        multi_des_now--;
                    }
                }
        }

        if (num != -1)
        {
            executed_action[num]++;
            action_num[num]--;
        }
        Debug.Log("thornHit(up)!");
        PL_action_num = num;
    }

    //                    true  false
    //�}���`�̏����҂���@�ǉ��ƍ폜�̓t���O�Ŕ���
    public void multi_des_Check(GameObject i,bool a)
    {
        if(a==true)
        {
           
            multi_des[multi_des_now] = i;

            multi_des_now++;
        }
        else
        {
            if (multi_des[0] != null)
            {
                multi_des[multi_des_now] = null;

                multi_des_now--;
              
            }
            
        }

        if (multi_des_now < 0)
            multi_des_now = 0;

    }

    public void Set_OffActive()
    {
        this.gameObject.SetActive(false);
    }
}
