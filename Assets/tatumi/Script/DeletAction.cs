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
    public int now,multi_now=1;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string target = "multi";
    private int all_multi_flag;//合体コマンドを同時にした時

    //合体したときのNowの数を覚えるよう
    public int[] timing;

    //プレイヤー用
    public bool multi_backflag;
   
    // Start is called before the first frame update
    void Start()
    {
        now = 0;
        all_multi_flag = 0;
       // multi_now = 1;
        multi_backflag = true;
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
            //まずそもそもオブジェが消されてないか確認
            if (objs[now - 1] != null)
            {
                //マルチか普通か判断しそれぞれのスクリプトへ返す
                if (objs[now - 1].name.Contains(target) == true)
                {
                    if (objs[now - 1] != null)
                    {
                        objs[now - 1].GetComponent<Multi_Action_move>().Set_Active(true);
                        Debug.Log("thornHit(up)!");
                    }
                }
                else
                {
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                    multi_backflag = false;
                }
            }

            //なんで必要か正直覚えてないけど大事なやーつ。
            int S = now;
           

            for (int i = multi_now+1; i != -1; i--)
            {
                //マルチ作成してたなら二つ分の普通アクションを返す
                if (S == timing[i])
                {
                    //オブジェ自体削除
                    multi_objs[i].GetComponent<Multi_Action_move>().Eff_active();
                    Destroy(multi_objs[i], .1f);
                    multi_now--;
                    all_multi_flag += 1;

                    multi_backflag = true;
                  

                    //二段戻し
                    now--;
                    if (now != 0)
                    {
                        objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
                        timing[i] = 0;
                    }
                }
                
            }

            //同時作成なら
            if(all_multi_flag>=2)
            {
                now--;
                multi_backflag = true;
               
                //なんか0に入ってもやろうとするのでそれ以外で戻す処理。
                if (now != 0)
                    objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            }

            //初期化＆正常化
            all_multi_flag = 0;
            now--;
        }
    }
}
