using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosst_Up : MonoBehaviour
{
    //������΂���
    [Header("����")]
    public float power;

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
    
    void OnTriggerEnter(Collider collision)
    {
        //PL�Ɠ�����Ɠ���
        if (collision.gameObject.tag == "Player")
        {
           
            chikara = new Vector3(0.0f, power, 0.0f);
            
            //�����rigid���Q�b�g�����Ⴄ
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //�h�b�J�[��(��΂�)
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

        }
    }
}
