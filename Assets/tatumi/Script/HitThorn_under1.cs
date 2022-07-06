using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//針（凍結）
public class HitThorn_under1 : MonoBehaviour
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
            //PLの現在のアクション内容取得
            int pl_num = GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().PL_action_num;

            //何もせず
            if (pl_num == -1||pl_num==0)
            {
                ;
            }
            //ジャンプ系統以外
            else
            {
                //リトライ
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }
    }
}

