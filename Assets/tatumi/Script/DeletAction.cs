using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("�o������x��")]
    //�I�u�W�F�i�[�p�ϐ�
    public GameObject[] objs;

    [Header("�G��Ȃ�")]
    public int now;

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
