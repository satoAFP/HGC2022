using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitThorn : MonoBehaviour
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

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Debug.Log("thornHit(up)!");
        }
    }
}
