using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultuAction_Creit : MonoBehaviour
{
    [Header("�K��ActionButton���w��")]
    public GameObject AC_button;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string target = "G";

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        //pos=800�ʒu�ɂ����S�擾
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Multi_action1");

        if (blocks[0] == null)
        {
            ;
        }
        else
        {
            //���O������i���̕ӂ�͕�script�Ŕ��肵���ق����悳�����j
            if (blocks[0].name.Contains(target) == true)
            {
                if (blocks[1].name.Contains(target) == true)
                {
                    //�n�C�W�����v�쐬�i�ʒu�͂���800�j
                    // �v���n�u��GameObject�^�Ŏ擾
                    GameObject obj = (GameObject)Resources.Load("highjump");
                    // �v���n�u�����ɁA�C���X�^���X�𐶐��A
                    Instantiate(obj, new Vector3(800.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

                    for (int i = 0; i != 2; i++)
                    {
                        blocks[i].GetComponent<ButtonChoice>().Set_Active(true);
                    }
                }
            }
        }
        
    }
}
        
    

