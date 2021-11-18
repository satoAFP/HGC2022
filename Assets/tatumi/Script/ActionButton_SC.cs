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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //NowStage = SceneManager.GetActiveScene().name;

        for (int i = 0; i != Child_num; i++)
        {
            for (int k = 0; k != Duplicate[i]; k++)
            {
               GameObject newObj = Instantiate(childGameObjects[i], this.transform, false);
            }
        }

        for (int i = 0; i != 8; i++)
        {
           
            //action_numtexts[i]=Instantiate(action_numtext, this.transform, false);
            //action_numtexts[i].transform.position = new Vector3(19.0f + (i * 130.0f), 117.0f, 0.0f);
        }

        for (int i = 0; i != Child_num; i++)
        {
            action_num[i] = Duplicate[i] + 1;

            //action_numtexts[i].text = action_num[i].ToString();
        }

        //player�֘A
        player = GameObject.Find("Player"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = player.GetComponent<Player>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().name == SceneName|| SceneManager.GetActiveScene().name==NowStage)
        //{
        //    ;
        //}
        //else
        //    Destroy(Object, .01f);

        if (script.Movestop == true)
            Button.SetActive(true);
        else
            Button.SetActive(false);
    }

    public void set_text(int a,int b)
    {
        action_num[a] += b;
    }

    public int get_score(int a)
    {
        return action_num[a];
    }
}
