using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("�o����ʏ���x��")]
    //�I�u�W�F�i�[�p�ϐ�
    public GameObject[] objs;

    [Header("�o���鍇�̃A�N�V�������x��")]
    //�I�u�W�F�i�[�p�ϐ�
    public GameObject[] multi_objs;

    [Header("�G��Ȃ�")]
    public int now,multi_now;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string target = "multi";

    public int[] timing;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        if (now != 0)
        {

            if (objs[now - 1].name.Contains(target) == true)
            {
                objs[now - 1].GetComponent<Multi_Action_move>().Set_Active(true);
            }
            else
            {
                objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            }

            for (int i = 0; i != multi_now; i++)
            {
                if (now == timing[i])
                {
                    //�I�u�W�F���̍폜
                    Destroy(multi_objs[i], .1f);

                    //��i�߂�
                    now--;
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                }
            }

           
            //object[now]= null;
            
            now--;
        }
    }
}
