using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitThorn_under1 : MonoBehaviour
{
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
            //PL�̌��݂̃A�N�V�������e�擾
            int pl_num = GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().PL_action_num;

            Debug.Log(pl_num);

            //��������
            if (pl_num == -1||pl_num==0)
            {
              

                Debug.Log("thornHit(under)! SEFE");
            }
            //�W�����v�n���ȊO
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                Debug.Log("thornHit(under)! OUT");
            }
        }
    }
}

