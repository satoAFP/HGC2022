using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hithmmer : MonoBehaviour
{
    [Header("��{�n���}�[�̌��e")]
    //�e�I�u�W�F�擾�i�w��j
    public GameObject parent;

    [Header("0=x,1=y,2=z 3�ȏ�ɂ���ƃo�O��܂��B")]
    public float[] power=new float[3];

    //��
    Vector3 chikara = new Vector3(20.0f, 20.0f, 20.0f);

    //����̃��W�b�h���i�[
    Rigidbody aiteRigid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //PL�Ɠ�����Ɠ���
        if (collision.gameObject.tag == "Player")
        {
            float butobi = 0.0f;

            //��
            if(parent.GetComponent<movehammer>().Getnowrad()<0)
            {
                chikara = new Vector3(-1.0f*power[0], power[1], power[2]);
            }
            //����
            else if(parent.GetComponent<movehammer>().Getnowrad() == 0)
            {
                chikara = new Vector3(0.0f, power[1], power[2]);
            }
            //�E
            else
            {
                chikara = new Vector3(power[0], power[1], power[2]);
            }

            //�����rigid���Q�b�g�����Ⴄ
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //�h�b�J�[��
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

            Debug.Log("hmmerHit!");
        }
    }
}
