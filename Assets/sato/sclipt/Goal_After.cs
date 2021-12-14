using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_After : MonoBehaviour
{
    [Header("�ēx�W�����v�܂ł̎���")]
    public int jump_time;

    [Header("�W�����v�̍���")]
    public float jump_power;

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
    void Update()
    {
        if(goal_move)
        {
            this.gameObject.GetComponent<Player>().enabled = false;

            count++;

            if (count == jump_time)
            {
                //�W�����v�����鏈��
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jump_power, 0.0f), ForceMode.Impulse);

                //�W�����v�A�j���[�V�����ڍs
                anim.SetBool("jump", true);
                count = 0;
            }

            if (this.gameObject.transform.localEulerAngles.y <= 180)
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0.0f, 1.0f, 0.0f);
            }
        }



    }
}
