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
    public int now,multi_now=1;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string target = "multi";
    private int all_multi_flag;//���̃R�}���h�𓯎��ɂ�����

    //���̂����Ƃ���Now�̐����o����悤
    public int[] timing;

    //�v���C���[�p
    public bool multi_backflag;
   
    // Start is called before the first frame update
    void Start()
    {
        now = 0;
        all_multi_flag = 0;
       // multi_now = 1;
        multi_backflag = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        //�������ɖ߂�{�^��������Ώ�
        if (now != 0)
        {
            //�܂����������I�u�W�F��������ĂȂ����m�F
            if (objs[now - 1] != null)
            {
                //�}���`�����ʂ����f�����ꂼ��̃X�N���v�g�֕Ԃ�
                if (objs[now - 1].name.Contains(target) == true)
                {
                    if (objs[now - 1] != null)
                    {
                        objs[now - 1].GetComponent<Multi_Action_move>().Set_Active(true);
                        Debug.Log("thornHit(up)!");
                    }
                }
                else
                {
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                    multi_backflag = false;
                }
            }

            //�Ȃ�ŕK�v�������o���ĂȂ����Ǒ厖�Ȃ�[�B
            int S = now;
           

            for (int i = multi_now+1; i != -1; i--)
            {
                //�}���`�쐬���Ă��Ȃ����̕��ʃA�N�V������Ԃ�
                if (S == timing[i])
                {
                    //�I�u�W�F���̍폜
                    multi_objs[i].GetComponent<Multi_Action_move>().Eff_active();
                    Destroy(multi_objs[i], .1f);
                    multi_now--;
                    all_multi_flag += 1;

                    multi_backflag = true;
                  

                    //��i�߂�
                    now--;
                    if (now != 0)
                    {
                        objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                        timing[i] = 0;
                    }
                }
                
            }

            //�����쐬�Ȃ�
            if(all_multi_flag>=2)
            {
                now--;
                multi_backflag = true;
               
                //�Ȃ�0�ɓ����Ă���낤�Ƃ���̂ł���ȊO�Ŗ߂������B
                if (now != 0)
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            }

            //�����������퉻
            all_multi_flag = 0;
            now--;
        }
    }
}
