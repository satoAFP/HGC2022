using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//針（凍結）
public class HitThorn : MonoBehaviour
{
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
            //リトライさせる
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

           
        }
    }
}
