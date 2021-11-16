using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC scriptac; //�Q�ƌ�Script������ϐ�

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾
    private Vector3 pos;
    private float first_x;//�����ʒu

   

    // Start is called before the first frame update
    void Start()
    {
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        ActionButton = GameObject.Find("ActionBotton "); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        pos = this.gameObject.transform.position;
        first_x = pos.x;

    }

    // Update is called once per frame
    void Update()
    {
       

       
    }

    public void PushButton(bool set)
    {

        //���N��
        if (Input.GetMouseButtonDown(0)||set==true)
        {
            Debug.Log("Left");

            Button.SetActive(false);
            //�������������ʒu�ɖ߂�
            this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

            script.objs[script.now] = this.gameObject;
            script.now++;
            scriptac.set_text((int)(first_x / 130), -1);
        }
        //�E�N��
        else if(Input.GetMouseButtonDown(1))
        {
            pos = this.gameObject.transform.position;
            Debug.Log("Right");
            Debug.Log($"pos.y={pos.y:0.00}");
            if (pos.x<=600)
            //���݂̈ʒu����ړ�����
            this.gameObject.transform.position = new Vector3(530.0f, pos.y, pos.z);
            else if(pos.x<=1200)
                //���݂̈ʒu����ړ�
                this.gameObject.transform.position = new Vector3(pos.x+130.0f, pos.y, pos.z);
            else
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

            pos = this.gameObject.transform.position;

            if (pos.x == 530.0f && this.tag != "Multi_action1")
            {
                Debug.Log("tag1 get");//ok
                this.tag = "Multi_action1";
            }
            else if (pos.x == 660.0f)
                this.tag = "Multi_action2";
            else if (pos.x == 790.0f)
                this.tag = "Multi_action3";
            else if (pos.x == 920.0f)
                this.tag = "Multi_action4";
            else
                this.tag = "Untagged";
        }
        //�^�񒆃N��
        else if(Input.GetMouseButtonDown(2))
        {
            this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
        }
        else if(set==false)
        {
            Button.SetActive(true);
            scriptac.set_text((int)(first_x / 130), 1);
        }
       
    }

    public void Set_Active(bool set)
    {
        PushButton(set);
    }
}
