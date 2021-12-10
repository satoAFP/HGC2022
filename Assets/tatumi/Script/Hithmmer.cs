using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hithmmer : MonoBehaviour
{
    //親オブジェ取得（指定）
    public GameObject parent;

    //力
    Vector3 chikara = new Vector3(20.0f, 20.0f, 20.0f);

    //力の加わる中心半径
    public float radius = 1.0f;

    //相手のリジッドを格納
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
        //PLと当たると動作
        if (collision.gameObject.tag == "Player")
        {
            float butobi = 0.0f;

            //左
            if(parent.GetComponent<movehammer>().Getnowrad()<0)
            {
                chikara = new Vector3(-20.0f, 5.0f, 0.0f);
            }
            //中央
            else if(parent.GetComponent<movehammer>().Getnowrad() == 0)
            {
                chikara = new Vector3(0.0f, 5.0f, -1.0f);
            }
            //右
            else
            {
                chikara = new Vector3(20.0f, 5.0f, 0.0f);
            }

            //相手のrigidをゲットしちゃう
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //ドッカーン
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

            Debug.Log("hmmerHit!");
        }
    }
}
