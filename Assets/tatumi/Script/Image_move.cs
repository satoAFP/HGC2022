using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�J�[�h�I�����z�����܂�鉉�o�iAnim�͈ʒu�����I�Ȃ��ߎg�p�s�j
public class Image_move : MonoBehaviour
{
   
    //�����쐬�ɕK�v--------------
    private int time;
    private Vector3 pos,sca;
    private float MAX,add;
    //----------------------------

    //�����ŐM���󂯓n��������������B
    public bool Move_on;
    private float bye;
    //0=-90.6f,1=-64.5,2=-38.5f,3=-12.5f//��26
    public float parent_posx;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        this.transform.position = new Vector3(parent_posx,56.23f,-102.0f);

        //���ݒn����ǂꂾ���̈ړ��������߂�
        MAX = parent_posx;

        //�ړ�������ړ��ʂ����߂�
        add = (MAX / 1000 + (MAX / 100));

        //�ꉞ�����ύX�p�̂��́B���݂͓���
        bye = 1;

      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�傫���A�ʒu���擾
        sca = this.transform.localScale;

        this.transform.position = new Vector3(parent_posx, 56.23f, -102.0f);

        if (Move_on == true)
        {
            time++;
            //�ŏ��̂ݔ��f
            if (time == 1)
            {
                //�F���擾
                Image image = this.gameObject.GetComponent<Image>();

                //���f(���o��)
                image.color = new Color(255, 255, 255, 255);
            }

            //�ړ�����---------------------------------------------------------------------------------------------------------------------------------------
            if (time < (25/bye))
            {
                //x=10%,y=60%�܂ňړ�
               this.transform.position = new Vector3((parent_posx-(add*(time/(bye*2)))),(56.23f+(-0.9966f*(time/(bye/2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((parent_posx - (add * (time / (bye * 2)))), (56.23f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75/bye))
            {
                //x=100%,y=100%�܂ňړ�
                this.transform.position = new Vector3((pos.x - (add * ((time-24) / (bye / 1.5f)))), (pos.y + (-0.9966f * ((time-24) / (bye * 5.0f)))), pos.z);
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            
            //�傫�������Ɍ��炷
            if (time < (75/bye))
            {
                //�傫��������Ɍ��炷
                this.transform.localScale = new Vector3(sca.x - (0.012f*bye), sca.y - (0.012f*bye), sca.z);
            }

            //�v1.3�b�ŏ�����
            if (time == (75/bye))
            {
                //�Ō�ɂ����낧�[
                Destroy(this.gameObject);
            }
        }
    }
}
