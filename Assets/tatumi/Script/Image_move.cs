using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_move : MonoBehaviour
{
    //�e�̏�Ԏ擾
    private GameObject PA;
    private Vector3 tmp;
    //-------------------------------

    //�����쐬�ɕK�v--------------
    private int time;
    private Vector3 pos,sca;
    private float MAX,add;
    //----------------------------

    //�����ŐM���󂯓n��������������B�����֐��ł����񂶂�ˁH�H�H�H
    public bool Move_on;
    public float bye;

    // Start is called before the first frame update
    void Start()
    {
        PA = transform.parent.gameObject;
        tmp = PA.gameObject.transform.position;
        pos = this.transform.position;
        time = 0;

        //���ݒn����ǂꂾ���̈ړ��������߂�
        MAX = 510.0f - tmp.x - 51.0f;

        //�ړ�������ړ��ʂ����߂�
        add = MAX / 1000 + (MAX / 10000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�傫���A�ʒu���擾
        pos = this.transform.position;
        sca = this.transform.localScale;

        if (Move_on == true)
        {
            time++;
            if (time < (25/bye))
            {
                //x=10%,y=60%�܂�
                this.transform.position = new Vector3(pos.x + add, pos.y + (10.0f*bye), pos.z);
            }
            else if (time < (75/bye))
            {
                //x=100%,y=100%�܂�
                this.transform.position = new Vector3(pos.x + add * (16*bye), pos.y + (0.8f*bye), pos.z);
            }

            if (time < (75/bye))
            {
                //�傫��������Ɍ��炷
                this.transform.localScale = new Vector3(sca.x - (0.012f*bye), sca.y - (0.012f*bye), sca.z);
            }

            if (time == (75/bye))
            {
                //�Ō�ɂ����낧�[
                Destroy(this.gameObject);
            }
        }
    }
}
