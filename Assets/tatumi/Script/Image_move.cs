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
    private float bye;
    //0=-90.6f,1=-64.5,2=-38.5f,3=-12.5f//��26
    public float parent_posx;

    // Start is called before the first frame update
    void Start()
    {
        PA = transform.parent.gameObject;
        tmp = PA.gameObject.transform.position;
        //pos = this.transform.position;
        time = 0;

        this.transform.position = new Vector3(parent_posx,56.23f,-102.0f);

        //���ݒn����ǂꂾ���̈ړ��������߂�
        MAX = parent_posx;

        //�ړ�������ړ��ʂ����߂�
        add = (MAX / 1000 + (MAX / 100));

        //����
        bye = 1;

       // Move_on = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�傫���A�ʒu���擾
        sca = this.transform.localScale;

       

        if (Move_on == true)
        {
            time++;
            if (time < (25/bye))
            {
                //x=10%,y=60%�܂�
               this.transform.position = new Vector3((parent_posx-(add*(time/(bye*2)))),(56.23f+(-0.9966f*(time/(bye/2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((parent_posx - (add * (time / (bye * 2)))), (56.23f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75/bye))
            {
                //x=100%,y=100%�܂�
                this.transform.position = new Vector3((pos.x - (add * ((time-24) / (bye / 1.5f)))), (pos.y + (-0.9966f * ((time-24) / (bye * 5.0f)))), pos.z);
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
