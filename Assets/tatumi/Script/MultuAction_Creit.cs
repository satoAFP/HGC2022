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

    //�������邩�擾�p�z��i���2�j
    private GameObject[] blocks1 = new GameObject[2];
  

    // Start is called before the first frame update
    void Start()
    {
        //���̎�ނ����ʗpn�̖��O�Q
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

        //�}���`�쐬
        multi_action(blocks1, 17.74148f);

       

        //�I�u�W�F�N�g�̐�����Ȃ珈���J�n�i�J���[�p�j
        if (blocks1.Length >= 2)
            multi_oks[0] = multi_OK(blocks1);
        else
        {
            multi_oks[0] = "null";

            blocks1 = new GameObject[1];
        }
      
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
                    //�Ώۂ̕��ʃA�N�V����������
                   a[i].GetComponent<ButtonChoice>().Set_Active(true);
                  
                }

                //�v���n�u���璼�ڏ���
                GameObject obj = (GameObject)Resources.Load(multi_OK(a));
                // �v���n�u�����ɁA�C���X�^���X�𐶐��A
                Instantiate(obj, new Vector3(b, 83.19456f, -102.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

               
                //�����G�t�F�N�g�������i�ʒu�ɂ��ύX�j
                GameObject efe = AC_button.transform.Find("PS_front_Left").gameObject;
               
                efe.GetComponent<Effect_move>().SetActive(true);
                efe.GetComponent<Effect_move>().first_EF = true;
               
            }
            else 
            {
            
               StartCoroutine(multi_Back(a));
                  
            }
            
        }
        //����ȉ��Ȃ�
        else if(a.Length==1)
        {
            //���G�t�F�N�g�������i�ʒu�ɂ��ύX�j
            GameObject efe = AC_button.transform.Find("PS_Smook_Left").gameObject;
           
            efe.GetComponent<Effect_move>().SetActive(true);
            efe.GetComponent<Effect_move>().now_onecard = true;
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
           
        }
        if (a[0].name.Contains(multis[1]) == true || a[1].name.Contains(multis[1]) == true)
        {
            action[1] = true;
            
        }
        if (a[0].name.Contains(multis[2]) == true || a[1].name.Contains(multis[2]) == true)
        {
            action[2] = true;
            
        }
        if (a[0].name.Contains(multis[3]) == true || a[1].name.Contains(multis[3]) == true)
        {
            action[3] = true;
            
        }

        //���ʂ����ĉ����𔻕�
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

        //�����Ȃ����null
        return "null";
    }

    //�����z��Ԃ��悤�֐��i�J���[�p�j
    public string[] get_multi_oks()
    {
        return multi_oks;
    }

    public IEnumerator multi_Back(GameObject[] a)
    {
        for (int i = 0; i != 3; i++)
        {
            if(i==0)
            yield return new WaitForSeconds(0.1f);
            else
            //�Ώۂ̕��ʃA�N�V����������
            a[i-1].GetComponent<ButtonChoice>().Set_Back();

        }
    }
}


        
    

