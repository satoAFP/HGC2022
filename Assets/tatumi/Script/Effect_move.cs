using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //�G�t�F�N�g�̎��+�ꏊ�Ǘ��p
    //0-1 ���� 0-�� 1-�E
    //2-3 �W�� 2-�� 3-�E
    public int Efeect_number;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf==true&& Efeect_number<2)
        {
            ;
        }
    }

    public void SetActive(bool a)
    {
        this.SetActive(a);
    }
}
