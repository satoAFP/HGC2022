using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC scriptac; //�Q�ƌ�Script������ϐ�

    public bool vanish;
    private bool now_ani;

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

        vanish = true;
        now_ani = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void PushButton(bool set)
    {
        if (now_ani == false)
        {
            Debug.Log("���������Ă͂Ȃ�");
            //���N��
            if (Input.GetMouseButtonDown(0) || set == true)
            {
                Debug.Log("Left");
                //now_ani = true;

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}"); 

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;


                //var animator = Button.GetComponent<Animator>();
                //animator.Play("Selected");
                //animator.Update(0f);

                //����������script�Ɉ�C
                //if (set==false)
                //Invoke(nameof(null_active), 1.15f);
                //else
                //    Button.SetActive(false);
                //vanish = true;
                //�������������ʒu�ɖ߂�
                this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

                script.objs[script.now] = this.gameObject;
                script.now++;
                scriptac.set_text((int)(first_x / 130), 1);
            }
            //�E�N��
            else if (Input.GetMouseButtonDown(1))
            {
                pos = this.gameObject.transform.position;
                Debug.Log("Right");
                // Debug.Log($"pos.y={pos.y:0.00}");
                if (pos.x <= 500)
                    //���݂̈ʒu����ړ�����
                    this.gameObject.transform.position = new Vector3(530.0f, pos.y, pos.z);
                else if (pos.x < 790)
                    //���݂̈ʒu����ړ�
                    this.gameObject.transform.position = new Vector3(pos.x + 130.0f, pos.y, pos.z);
                else
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

                pos = this.gameObject.transform.position;

                if (pos.x == 530.0f)
                {
                    Debug.Log("tag1 get");//ok
                    this.tag = "Multi_action1";
                }
                else if (pos.x == 660.0f)
                    this.tag = "Multi_action2";
                else if (pos.x == 790.0f)
                    this.tag = "Multi_action3";
               
                else
                    this.tag = "Untagged";
            }
            //�^�񒆃N��
            else if (Input.GetMouseButtonDown(2))
            {
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
            }
            else if (set == false)
            {
               
                Button.SetActive(true);
                vanish = true;
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
                scriptac.set_text((int)(first_x / 130), 1);
            }
        }
       
    }

    void null_active()
    {
        Button.SetActive(false);
    }

    public void Set_Active(bool set)
    {
        now_ani = false;
        PushButton(set);
    }

}
