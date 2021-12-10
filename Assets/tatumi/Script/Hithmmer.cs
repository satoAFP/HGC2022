using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hithmmer : MonoBehaviour
{
    [Header("基本ハンマーの元親")]
    //親オブジェ取得（指定）
    public GameObject parent;

    [Header("0=x,1=y,2=z 3つ以上にするとバグります。")]
    public float[] power=new float[3];

    //力
    Vector3 chikara = new Vector3(20.0f, 20.0f, 20.0f);

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
                chikara = new Vector3(-1.0f*power[0], power[1], power[2]);
            }
            //中央
            else if(parent.GetComponent<movehammer>().Getnowrad() == 0)
            {
                chikara = new Vector3(0.0f, power[1], power[2]);
            }
            //右
            else
            {
                chikara = new Vector3(power[0], power[1], power[2]);
            }

            //相手のrigidをゲットしちゃう
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //ドッカーン
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

            Debug.Log("hmmerHit!");
        }
    }
}
