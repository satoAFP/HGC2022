using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //�G�t�F�N�g�̎��+�ꏊ�Ǘ��p
    //0-1 ���� 0-�� 1-�E
    //2-3 �W�� 2-�� 3-�E
    public int Efeect_number;

    //����񂫂�����
    public bool first_EF;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (this.gameObject.activeSelf==true&& Efeect_number<2&& first_EF == true)
        {
            Debug.Log("YES");
            StartCoroutine(Set_Active(false));
            first_EF = false;
        }
    }

    public void SetActive(bool a)
    {
        first_EF = true;
       
        this.gameObject.SetActive(a);
       
    }

    public IEnumerator Set_Active(bool b)
    {
        for (int i = 0; i != 3; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(2.0f);
            else
                //�Ώۂ̕��ʃA�N�V����������
                this.gameObject.SetActive(b);
           
        }
    }
}
