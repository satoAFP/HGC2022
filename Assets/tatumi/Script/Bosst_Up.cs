using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosst_Up : MonoBehaviour
{
    //吹っ飛ばす力
    [Header("強さ")]
    public float power;

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
    
    void OnTriggerEnter(Collider collision)
    {
        //PLと当たると動作
        if (collision.gameObject.tag == "Player")
        {
           
            chikara = new Vector3(0.0f, power, 0.0f);
            
            //相手のrigidをゲットしちゃう
            aiteRigid = collision.gameObject.GetComponent<Rigidbody>();

            //ドッカーン(飛ばす)
            aiteRigid.AddForce(chikara, ForceMode.Impulse);

        }
    }
}
