using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_multimove : MonoBehaviour
{
    //�ړ��ɕK�v�B���łɃJ�[�\�����킹���̏㉺�ړ������˂Ă�
    private GameObject PA;
    private Vector3 pos, sca;
    private float MAX, add;
    private bool Nomal_mode;
    private float image_alp;

    //�M���󂯎��B�����炩�񂓁i�ȉ���
    public int time;
    public bool Move_on;

    // Start is called before the first frame update
    void Start()
    {
        //�ڂ����𓾂�
        PA = transform.Find("Image").gameObject;
        pos = this.transform.position;
        Nomal_mode = true;
        time = 0;
        image_alp = 0;

        //���ʂƈꏏ
        MAX = 510.0f - pos.x;

        add = MAX / 1000 + (MAX / 10000);
    }

    // Update is called once per frame
    void Update()
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
          

            if (time < 100)
            {
                //x=10%
                this.transform.position = new Vector3(pos.x + add, pos.y + 3.5f, pos.z);
            }
            else if (time < 300)
            {
                //x=90%
                this.transform.position = new Vector3(pos.x + add * 4, pos.y + 0.3f, pos.z);
            }

            if (time < 300)
            {
                this.transform.localScale = new Vector3(sca.x - 0.003f, sca.y - 0.003f, sca.z);
            }

            if (time == 300)
            {
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
        //�㉺�ړ�
        else if(Nomal_mode==true)
        {
            
            if (pos.y > -127.0f)
            {
                //������
                this.transform.position = new Vector3(pos.x, pos.y - 0.4f, pos.z);
                image_alp -= 0.02f;
            }
            else if (pos.y == -127.0f)
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
            
            if (pos.y < -100.0f)
            {
                //�オ��
                this.transform.position = new Vector3(pos.x, pos.y + 0.4f, pos.z);
                image_alp += 0.02f;
            }
            else if (pos.y == -100.0f)
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
