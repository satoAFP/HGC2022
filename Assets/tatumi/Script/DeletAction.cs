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
    private int all_multi_flag;//合体コマンドを同時にした時

    public int[] timing;
   
    // Start is called before the first frame update
    void Start()
    {
        now = 0;
        all_multi_flag = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        //消失中に戻るボタン押すやつ対処
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

            int S = now;

            for (int i = multi_now+1; i != 0; i--)
            {
                if (S == timing[i])
                {
                    //オブジェ自体削除
                    Destroy(multi_objs[i], .1f);
                    multi_now--;
                    all_multi_flag += 1;


                    //二段戻し
                    now--;
                    if (now != 0)
                    {
                        objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                        timing[i] = 0;
                    }
                }
                
            }

           
            //object[now]= null;

            //同時作成なら
            if(all_multi_flag>=2)
            {
                now--;
                if (now != 0)
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            }

            all_multi_flag = 0;
            now--;
        }
    }
}
