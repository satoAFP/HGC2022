using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    [Header("���̔ԍ�")]
    public int action_num;

    GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    //���݂̈ʒu���擾
    private Vector3 pos;
    private float first_x;//�����ʒu

    private bool now_ani;

    // Player script; //�Q�ƌ�Script������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //PL
        player = GameObject.Find("Player"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����

        //�߂��{�^��
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        script.multi_objs[script.multi_now] = this.gameObject;
        script.timing[script.multi_now] = script.now;
        script.multi_now++;

        now_ani = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        if (now_ani == false)
        {
            if (action_num == 0)
            {
                player.GetComponent<Player>().Push_highjump();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 1)
            {
                player.GetComponent<Player>().Push_wallkick();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 2)
            {
                player.GetComponent<Player>().Push_longjump();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 3)
            {
                player.GetComponent<Player>().Push_sliding();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
        }

        
    }

    void Set_Back()
    {
        //now_ani = true;
        //Invoke(nameof(null_active), 1.15f);
        //Button.SetActive(false);
        //�������������ʒu�ɖ߂�
        this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

        script.objs[script.now] = this.gameObject;
        script.now++;
    }

    void null_active()
    {
        Button.SetActive(false);
    }


    public void Set_Active(bool a)
    {
        now_ani = false;
        Button.SetActive(a);
    }
}
