using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Goal_After : MonoBehaviour
{
    [Header("�ēx�W�����v�܂ł̎���")]
    public int jump_time;

    [Header("�W�����v�̍���")]
    public float jump_power;

    [Header("�V�[���ړ��܂ł̃W�����v�̉�")]
    public int jump_num;

    [Header("�X���C���̃A�j���[�V����")]
    public Animator anim;

    [Header("�������牺�͂�����Ȃ�")]
    public bool goal_move = false;              //�S�[�����̃A�N�V�����Đ�


    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(goal_move)
        {
            this.gameObject.GetComponent<Player>().enabled = false;

            count++;

            if (count == jump_time)
            {
                jump_num--;
                if (jump_num == 0) 
                {
                    GameObject.Find("slime_img").GetComponent<Slime_ResultMove>().goal_move = true;
                }

                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jump_power, 0.0f), ForceMode.Impulse);
                //�W�����v�A�j���[�V�����ڍs
                anim.SetBool("jump", true);
                count = 0;
            }

            //��l����]����
            if (this.gameObject.transform.localEulerAngles.y <= 180)
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0.0f, 2.5f, 0.0f);
            }
        }



    }
}
