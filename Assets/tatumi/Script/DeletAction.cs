using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("覚える通常限度数")]
    //オブジェ格納用変数
    public GameObject[] objs;

    [Header("覚える合体アクション限度数")]
    //オブジェ格納用変数
    public GameObject[] multi_objs;

    [Header("触らない")]
    public int now,multi_now;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string target = "multi";

    public int[] timing;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        if (now != 0)
        {

            if (objs[now - 1].name.Contains(target) == true)
            {
                objs[now - 1].GetComponent<Multi_Action_move>().Set_Active(true);
            }
            else
            {
                objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            }

            for (int i = 0; i != multi_now; i++)
            {
                if (now == timing[i])
                {
                    //オブジェ自体削除
                    Destroy(multi_objs[i], .1f);

                    //二段戻し
                    now--;
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                }
            }

           
            //object[now]= null;
            
            now--;
        }
    }
}
