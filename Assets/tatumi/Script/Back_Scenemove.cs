using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//����Ȃ�script?
public class Back_Scenemove : MonoBehaviour
{
   
    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC script; //�Q�ƌ�Script������ϐ�

    private string Back_Stage;

    // Use this for initialization
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����

       // Back_Stage = script.NowStage;
    }


    public void PushButton()
    {
        if(Back_Stage=="Stage")
        SceneManager.LoadScene("Stage-2");
        if (Back_Stage == "Stage-2")
           ;
    }
}


