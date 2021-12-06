using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultuAction_Creit : MonoBehaviour
{
    [Header("�K��ActionButton���w��")]
    public GameObject AC_button;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string[] multis=new string[4];

    //���̂ł��邩�ǂ����̃T�C���p�B
    private string[] multi_oks = new string[4];

    private GameObject[] blocks1 = new GameObject[2];
    private GameObject[] blocks2 = new GameObject[2];
    private GameObject[] blocks3 = new GameObject[2];
    private GameObject[] blocks4 = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        multis[0] = "jump";
        multis[1] = "stick";
        multis[2] = "run";
        multis[3] = "squat";
    }

    // Update is called once per frame
    void Update()
    {
       

        //���ʒu�ɂ����S�擾
        blocks1 = GameObject.FindGameObjectsWithTag("Multi_action1");

        //��2�ʒu�ɂ����S�擾
         blocks2 = GameObject.FindGameObjectsWithTag("Multi_action2");

        //�E2�ʒu�ɂ����S�擾
        blocks3 = GameObject.FindGameObjectsWithTag("Multi_action3");

        //�E�ʒu�ɂ����S�擾
        blocks4 = GameObject.FindGameObjectsWithTag("Multi_action4");

        if (blocks1.Length >= 2)
            multi_oks[0]= multi_OK(blocks1);
        else
            multi_oks[0] = "null";
        if (blocks2.Length >= 2)
            multi_oks[1] = multi_OK(blocks2);
        else
            multi_oks[1] = "null";
        if (blocks3.Length >= 2)
            multi_oks[2] = multi_OK(blocks3);
        else
            multi_oks[2] = "null";
        if (blocks4.Length >= 2)
            multi_oks[3] = multi_OK(blocks4);
        else
            multi_oks[3] = "null";

    }

    public void PushButton()
    {


        //���ʒu�ɂ����S�擾
        GameObject[] blocks1 = GameObject.FindGameObjectsWithTag("Multi_action1");

        //��2�ʒu�ɂ����S�擾
        GameObject[] blocks2 = GameObject.FindGameObjectsWithTag("Multi_action2");

        //�E2�ʒu�ɂ����S�擾
        GameObject[] blocks3 = GameObject.FindGameObjectsWithTag("Multi_action3");

        //�E�ʒu�ɂ����S�擾
        GameObject[] blocks4 = GameObject.FindGameObjectsWithTag("Multi_action4");

        //�����b�B������
        multi_action(blocks1, 530.0f);

        multi_action(blocks2, 660.0f);

        multi_action(blocks3, 790.0f);

        multi_action(blocks4, 920.0f);

        blocks1 = new GameObject[2];
        blocks2 = new GameObject[2];
        blocks3 = new GameObject[2];
        blocks4 = new GameObject[2];

    }

    private void multi_action(GameObject[] a,float b)
    {
        //�I�u�W�F�N�g����ȏ�̎�
        if (a.Length >= 2)
        {
            //�}���`�A�N�V�����쐬�i�ʒu�͂��� b�j
            // �v���n�u��GameObject�^�Ŏ擾
            if (multi_OK(a) != "null")
            {
                for (int i = 0; i != 2; i++)
                {
                   a[i].GetComponent<ButtonChoice>().Set_Active(true);
                }

                GameObject obj = (GameObject)Resources.Load(multi_OK(a));
                // �v���n�u�����ɁA�C���X�^���X�𐶐��A
                Instantiate(obj, new Vector3(b, -127.0f, 0.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

               
            }
            else
            Debug.Log("�g�ݍ��킹�����݂��Ȃ�");

        }
        //����ȉ��Ȃ�
        else
        {
            Debug.Log("2���݂��Ȃ�");
            
        }
    }

    private string multi_OK(GameObject[] a)
    {
        //0=�W�����v,1=�Ђ���,2=����,3=���Ⴊ��
        bool[] action = new bool[4];

        //������
        for(int i=0;i!=4;i++)
        {
            action[i] = false;
        }

        //���ꂼ�ꉽ�����邪����
        if (a[0].name.Contains(multis[0]) == true|| a[1].name.Contains(multis[0]) == true)
        {
            action[0] = true;
            Debug.Log("���");
        }
        if (a[0].name.Contains(multis[1]) == true || a[1].name.Contains(multis[1]) == true)
        {
            action[1] = true;
            Debug.Log("�Ђ���");
        }
        if (a[0].name.Contains(multis[2]) == true || a[1].name.Contains(multis[2]) == true)
        {
            action[2] = true;
            Debug.Log("����");
        }
        if (a[0].name.Contains(multis[3]) == true || a[1].name.Contains(multis[3]) == true)
        {
            action[3] = true;
            Debug.Log("���Ⴊ��");
        }

        if (action[0] == true)
        {
            if (action[3] == true)
            {
                return "multi_highjump";
            }
            else if (action[1] == true)
            {
                return "multi_wallkick";
            }
            else if (action[2] == true)
            {
                return "multi_longjump";
            }
        }
        else if (action[3] == true && action[2] == true)
            return "multi_sliding";


        return "null";
    }

    public string[] get_multi_oks()
    {
        return multi_oks;
    }
}


        
    

