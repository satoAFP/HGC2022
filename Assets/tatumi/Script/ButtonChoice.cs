using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    DeletAction script; //�Q�ƌ�Script������ϐ�

    [Header("��\���ΏۃI�u�W�F�N�g")]
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        BackButton = GameObject.Find("BackButton"); //�I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = BackButton.GetComponent<DeletAction>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {

        script.objs[script.now] = this.gameObject;
        script.now += 1;

        Button.SetActive(false);

        //���N��
        if (Input.GetMouseButtonDown(0))
        {
            Button.SetActive(false);
            if(script.now==0)
            {
                script.objs[script.now] = this.gameObject;
            }
            else
            {
                script.now++;
                script.objs[script.now] = this.gameObject;
            }
        }
        //�E�N��
        else if(Input.GetMouseButtonDown(1))
        {

        }
        //�^�񒆃N��
        else if(Input.GetMouseButtonDown(2))
        {

        }
       
    }
}
