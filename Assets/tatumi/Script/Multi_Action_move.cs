using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    [Header("���̔ԍ�")]
    public int action_num;

    public GameObject player; //�Q�ƌ�OBJ���̂��̂�����ϐ�

   // Player script; //�Q�ƌ�Script������ϐ�

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
       // script = player.GetComponent<Player>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        if (action_num == 0)
        {
            player.GetComponent<Player>().Push_highjump();
        }
    }
}
