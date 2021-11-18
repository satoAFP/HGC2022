using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultuAction_Creit : MonoBehaviour
{
    [Header("必ずActionButtonを指定")]
    public GameObject AC_button;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string[] multis=new string[4];

    // Start is called before the first frame update
    void Start()
    {
        multis[0] = "jump";
        multis[1] = "stick";
        multis[2] = "run";
        multis[3] = "squat";
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        //左位置にいるやつ全取得
        GameObject[] blocks1 = GameObject.FindGameObjectsWithTag("Multi_action1");

        //Debug.LogWarning(blocks1[0].gameObject);
        //Debug.LogWarning(blocks1[1].gameObject);

        //左2位置にいるやつ全取得
        GameObject[] blocks2 = GameObject.FindGameObjectsWithTag("Multi_action2");

        //右2位置にいるやつ全取得
        GameObject[] blocks3 = GameObject.FindGameObjectsWithTag("Multi_action3");

        //右位置にいるやつ全取得
        GameObject[] blocks4 = GameObject.FindGameObjectsWithTag("Multi_action4");

        multi_action(blocks1, 530.0f);
        multi_action(blocks2, 660.0f);
        multi_action(blocks3, 790.0f);
        multi_action(blocks4, 920.0f);

       

    }

    private void multi_action(GameObject[] a,float b)
    {
        //オブジェクトが二つ以上の時
        if (a.Length >= 2)
        {
            //マルチアクション作成（位置はｘ＝ b）
            // プレハブをGameObject型で取得
            if (multi_OK(a) != "null")
            {
                GameObject obj = (GameObject)Resources.Load(multi_OK(a));
                // プレハブを元に、インスタンスを生成、
                Instantiate(obj, new Vector3(b, -127.0f, 0.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

                for (int i = 0; i != 2; i++)
                {
                    a[i].GetComponent<ButtonChoice>().Set_Active(true);
                }
            }
            else
            Debug.Log("組み合わせが存在しない");

        }
        //それ以下なら
        else
        {
            Debug.Log("2つ存在しない");
            
        }
    }

    private string multi_OK(GameObject[] a)
    {
        //0=ジャンプ,1=ひっつき,2=走り,3=しゃがみ
        bool[] action = new bool[4];

        //初期化
        for(int i=0;i!=4;i++)
        {
            action[i] = false;
        }

        //それぞれ何があるが判別
        if (a[0].name.Contains(multis[0]) == true|| a[1].name.Contains(multis[0]) == true)
        {
            action[0] = true;
            Debug.Log("飛び");
        }
        if (a[0].name.Contains(multis[1]) == true || a[1].name.Contains(multis[1]) == true)
        {
            action[1] = true;
            Debug.Log("ひっつき");
        }
        if (a[0].name.Contains(multis[2]) == true || a[1].name.Contains(multis[2]) == true)
        {
            action[2] = true;
            Debug.Log("走り");
        }
        if (a[0].name.Contains(multis[3]) == true || a[1].name.Contains(multis[3]) == true)
        {
            action[3] = true;
            Debug.Log("しゃがみ");
        }

        if (action[0] == true)
        {
            if (action[3] == true)
            {
                return "multi_highjump";
            }
            else if (action[1] == true)
            {
                return "multi_wallkick";
            }
            else if (action[2] == true)
            {
                return "multi_longjump";
            }
        }
        else if (action[3] == true && action[2] == true)
            return "multi_sliding";


        return "null";
    }
}
        
    

