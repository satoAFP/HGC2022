using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultuAction_Creit : MonoBehaviour
{
    [Header("必ずActionButtonを指定")]
    public GameObject AC_button;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string[] multis=new string[4];

    //合体できるかどうかのサイン用。
    private string[] multi_oks = new string[4];

    //何があるか取得用配列（上限2）
    private GameObject[] blocks1 = new GameObject[2];
  

    // Start is called before the first frame update
    void Start()
    {
        //何の種類か判別用nの名前群
        multis[0] = "jump";
        multis[1] = "stick";
        multis[2] = "run";
        multis[3] = "squat";
    }

    // Update is called once per frame
    void Update()
    {
       
        //左位置にいるやつ全取得
        blocks1 = GameObject.FindGameObjectsWithTag("Multi_action1");

        //マルチ作成
        multi_action(blocks1, 17.74148f);

       

        //オブジェクトの数が二つなら処理開始（カラー用）
        if (blocks1.Length >= 2)
            multi_oks[0] = multi_OK(blocks1);
        else
        {
            multi_oks[0] = "null";

            blocks1 = new GameObject[1];
        }
      
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
                for (int i = 0; i != 2; i++)
                {
                    //対象の普通アクションを消す
                   a[i].GetComponent<ButtonChoice>().Set_Active(true);
                  
                }

                //プレハブから直接召喚
                GameObject obj = (GameObject)Resources.Load(multi_OK(a));
                // プレハブを元に、インスタンスを生成、
                Instantiate(obj, new Vector3(b, 83.19456f, -102.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

               
                //爆発エフェクトを検索（位置により変更）
                GameObject efe = AC_button.transform.Find("PS_front_Left").gameObject;
               
                efe.GetComponent<Effect_move>().SetActive(true);
                efe.GetComponent<Effect_move>().first_EF = true;
               
            }
            else 
            {
            
               StartCoroutine(multi_Back(a));
                  
            }
            
        }
        //それ以下なら
        else if(a.Length==1)
        {
            //煙エフェクトを検索（位置により変更）
            GameObject efe = AC_button.transform.Find("PS_Smook_Left").gameObject;
           
            efe.GetComponent<Effect_move>().SetActive(true);
            efe.GetComponent<Effect_move>().now_onecard = true;
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
           
        }
        if (a[0].name.Contains(multis[1]) == true || a[1].name.Contains(multis[1]) == true)
        {
            action[1] = true;
            
        }
        if (a[0].name.Contains(multis[2]) == true || a[1].name.Contains(multis[2]) == true)
        {
            action[2] = true;
            
        }
        if (a[0].name.Contains(multis[3]) == true || a[1].name.Contains(multis[3]) == true)
        {
            action[3] = true;
            
        }

        //結果を見て何かを判別
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

        //何もなければnull
        return "null";
    }

    //文字配列返すよう関数（カラー用）
    public string[] get_multi_oks()
    {
        return multi_oks;
    }

    public IEnumerator multi_Back(GameObject[] a)
    {
        for (int i = 0; i != 3; i++)
        {
            if(i==0)
            yield return new WaitForSeconds(0.1f);
            else
            //対象の普通アクションを消す
            a[i-1].GetComponent<ButtonChoice>().Set_Back();

        }
    }
}


        
    

