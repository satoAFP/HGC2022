using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hithmmer : MonoBehaviour
{
    //�e�I�u�W�F�擾�i�w��j
    public GameObject parent;

    //��
    Vector3 chikara = new Vector3(20.0f, 20.0f, 20.0f);

    //�͂̉���钆�S���a
    public float radius = 1.0f;

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
                chikara = new Vector3(-20.0f, 5.0f, 0.0f);
            }
            //����
            else if(parent.GetComponent<movehammer>().Getnowrad() == 0)
            {
                chikara = new Vector3(0.0f, 5.0f, -1.0f);
            }
            //�E
            else
            {
                chikara = new Vector3(20.0f, 5.0f, 0.0f);
            }

            //�����rigid���Q�b�g�����Ⴄ
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //�h�b�J�[��
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

            Debug.Log("hmmerHit!");
        }
    }
}
