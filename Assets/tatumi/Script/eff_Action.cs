using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eff_Action : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Copied()
    {
        //����
        GameObject copied = Object.Instantiate(this.gameObject) as GameObject;

        //�X�N���v�g��
        eff_Action script = copied.gameObject.GetComponent<eff_Action>();


        //�ړ�������
        copied.gameObject.transform.position = this.gameObject.transform.position;

        //�{�̂̓G���[�N�������ߏ����x�点�č폜
        Destroy(script);
        Destroy(copied, 1.0f);

    }
}
