using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("�o����ʏ���x��")]
    //�I�u�W�F�i�[�p�ϐ�
    public GameObject[] objs;

    [Header("�o���鍇�̃A�N�V�������x��")]
    //�I�u�W�F�i�[�p�ϐ�
    public GameObject[] multi_objs;

    [Header("�G��Ȃ�")]
    public int now,multi_now;

    public int[] timing;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        if (now != 0)
        {
            objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            //object[now]= null;
            
            now--;
        }
    }
}
