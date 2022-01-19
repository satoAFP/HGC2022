using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_multimove : MonoBehaviour
{
    //�ړ��ɕK�v�B���łɃJ�[�\�����킹���̏㉺�ړ������˂Ă�
    private GameObject PA;
    public Vector3 pos, sca;
    private float MAX, add;
    public bool Nomal_mode;
    private float image_alp;

    //�M���󂯎��B�����炩�񂓁i�ȉ���
    public int time;
    public bool Move_on;
    private float bye;
    

    // Start is called before the first frame update
    void Start()
    {
        //�ڂ����𓾂�
        PA = transform.Find("Image").gameObject;
        pos = this.transform.position;
        this.transform.Rotate(-180.0f, 0.0f, 0.0f);
        Nomal_mode = true;
        time = 0;
        image_alp = 0;

        //���ʂƈꏏ
        MAX =pos.x;

        //�ړ�������ړ��ʂ����߂�
        add = (MAX / 10000 + (MAX / 1000));

        //����
        bye = 1;

        Move_on = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = this.transform.position;
        sca = this.transform.localScale;
      
        if (Move_on == true)
        {
            time++;

            //�F���擾
            Image image = PA.GetComponent<Image>();

            //���f
            image.color = new Color(255, 255, 255, 0);

            if (time < (25 / bye))
            {
                //x=10%,y=60%�܂�
                this.transform.position = new Vector3((pos.x - (add * (time / (bye * 4)))), (83.19456f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((pos.x - (add * (time / (bye * 2)))), (83.19456f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75 / bye))
            {
                //x=100%,y=100%�܂�
                this.transform.position = new Vector3((pos.x - (add * ((time - 24) / (bye * 1.5f)))), (pos.y + (-0.9966f * ((time - 24) / (bye * 40.0f)))), pos.z);
            }


            if (time < (75 / bye))
            {
                //�傫��������Ɍ��炷
                this.transform.localScale = new Vector3(sca.x - (0.012f * bye), sca.y - (0.012f * bye), sca.z);
            }

            if (time == (75/bye))
            {
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
        //�㉺�ړ�
        else if(Nomal_mode==true)
        {
            
            if (pos.y < 83.19456f)
            {
                //������
                this.transform.position = new Vector3(pos.x, pos.y + 0.4f, pos.z);
                image_alp -= 0.04f;
            }
            else 
            {
                //Nomal_mode = false;//Nomal����
                image_alp = 0;
            }

            //�F���擾
            Image image = PA.GetComponent<Image>();

            //���f
            image.color = new Color(255, 255, 255, image_alp);

        }
        else if (Nomal_mode == false)
        {
            
            if (pos.y > 77.78191f)
            {
                //�オ��
                this.transform.position = new Vector3(pos.x, pos.y - 0.4f, pos.z);
                image_alp += 0.04f;
            }
            else 
            {
                ;//�����Ȃ�
                image_alp = 255;
            }

            //�F���擾
            Image image = PA.GetComponent<Image>();

            //���f
            image.color = new Color(255, 255, 255, image_alp);
        }


    }

    //�M���Ńt���O�؂�ւ�----------------
    public void Hidding()
    {
        //pos = this.transform.position;
        Nomal_mode = false;

    }

    public void Nomal()
    {
        Nomal_mode = true;
    }
    //---------------------------------------

}
