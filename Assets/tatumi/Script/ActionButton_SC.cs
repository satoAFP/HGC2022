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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //NowStage = SceneManager.GetActiveScene().name;

        for (int i = 0; i != Child_num; i++)
        {
            for (int k = 0; k != Duplicate[i]; k++)
            {
               GameObject newObj = Instantiate(childGameObjects[i], this.transform, false);
            }
        }

        for (int i = 0; i != 8; i++)
        {
           
            //action_numtexts[i]=Instantiate(action_numtext, this.transform, false);
            //action_numtexts[i].transform.position = new Vector3(19.0f + (i * 130.0f), 117.0f, 0.0f);
        }

        for (int i = 0; i != Child_num; i++)
        {
            action_num[i] = Duplicate[i] + 1;

            //action_numtexts[i].text = action_num[i].ToString();
        }

        //player関連
        player = GameObject.Find("Player"); //オブジェクトの名前から取得して変数に格納する
        script = player.GetComponent<Player>(); //OBJの中にあるScriptを取得して変数に格納する

    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().name == SceneName|| SceneManager.GetActiveScene().name==NowStage)
        //{
        //    ;
        //}
        //else
        //    Destroy(Object, .01f);

        if (script.Movestop == true)
            Button.SetActive(true);
        else
            Button.SetActive(false);
    }

    public void set_text(int a,int b)
    {
        action_num[a] += b;
    }

    public int get_score(int a)
    {
        return action_num[a];
    }
}
