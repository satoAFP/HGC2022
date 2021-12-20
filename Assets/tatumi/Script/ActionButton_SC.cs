using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;



public class ActionButton_SC : MonoBehaviour
{
    //[Header("このシーン以外は表示しない")]
    //[Header("特定しか存在しない-------------------------")]
    //public string SceneName;

    GameObject player; //参照元OBJそのものが入る変数

    Player script; //参照元OBJそのものが入る変数

    //回数表示用変数
    private int[] action_num=new int[8];
    private int[] executed_action = new int[8];

    //[Header("触らない")]
    //public string NowStage;

    [Header("子の要素数")]
    [Header("複数枚表示---------------------------------")]
    public int Child_num;

    [Header("複製数指定")]
    public int[] Duplicate;

    [Header("複製Obj指定")]
    public GameObject[] childGameObjects;

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //消去用受け取り口(要素11)
    private GameObject[] multi_des = new GameObject[11];
    private int multi_des_now = 0;

    //アクション選択時の他オブジェ認識用変数
    public int PL_action_num;

    // Start is called before the first frame update
    void Start()
    {
        //複製処理（現在いらない子）----------------------------------------------------------------------------
        for (int i = 0; i != Child_num; i++)
        {
            for (int k = 0; k != Duplicate[i]; k++)
            {
                GameObject newObj = Instantiate(childGameObjects[i], this.transform, false);
            }
        }

        //for (int i = 0; i != 8; i++)
        //{

        //    //action_numtexts[i]=Instantiate(action_numtext, this.transform, false);
        //    //action_numtexts[i].transform.position = new Vector3(19.0f + (i * 130.0f), 117.0f, 0.0f);
        //}
        //-----------------------------------------------------------------------------------------------------

        //選んだ数出力（管理）の初期化-------------------------------------------------------------------------
        for (int i = 0; i != 8; i++)
        {

            //action_num[i] = Duplicate[i];
            executed_action[i] = 0;

            //action_numtexts[i].text = action_num[i].ToString();
        }
        //------------------------------------------------------------------------------------------------------


        //player関連
        player = GameObject.Find("Player"); //オブジェクトの名前から取得して変数に格納する
        script = player.GetComponent<Player>(); //OBJの中にあるScriptを取得して変数に格納する

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //関数で選んだ数を増減
    public void set_text(int a, int b)
    {
        if ((action_num[a] + b) >= executed_action[a])
            action_num[a] += b;
    }
    //現在の数(取得用)
    public int get_score(int a)
    {
        return action_num[a] + executed_action[a];
    }

    //実行したアクションを最低数として読み込む
    /*
        JUMP,0
        SQUAT,1
        STICK,2
        RUN,3
        HIGHJUMP,4
        WALLKICK,5
        LONGJUMP,6
        SLIDING,7
    */
    public void executed_Action(int num)
    {
       // GameObject[] multi = this.transform.FindGameObjectsWithTag("Multis");

        if (num >= 4)
        {
            
                if (multi_des[0] != null)
                {
                    if (multi_des[0].activeSelf == false)
                    {
                        Destroy(multi_des[0]);

                        for (int i = 1; i != 11; i++)
                        {
                            multi_des[i - 1] = multi_des[i];
                        }

                        multi_des_now--;
                    }
                }
        }

        if (num != -1)
        {
            executed_action[num]++;
            action_num[num]--;
        }
        Debug.Log("thornHit(up)!");
        PL_action_num = num;
    }

    //                    true  false
    //マルチの消去待ち列　追加と削除はフラグで判定
    public void multi_des_Check(GameObject i,bool a)
    {
        if(a==true)
        {
           
            multi_des[multi_des_now] = i;

            multi_des_now++;
        }
        else
        {
            if (multi_des[0] != null)
            {
                multi_des[multi_des_now] = null;

                multi_des_now--;
              
            }
            
        }

        if (multi_des_now < 0)
            multi_des_now = 0;

    }

    public void Set_OffActive()
    {
        this.gameObject.SetActive(false);
    }
}
