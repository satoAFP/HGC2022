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


    //[Header("触らない")]
    //public string NowStage;

    [Header("子の要素数")]
    [Header("複数枚表示---------------------------------")]
    public int Child_num;

    [Header("複製数指定")]
    public int[] Duplicate;

    [Header("複製Obj指定")]
    public GameObject[] childGameObjects;
  
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

        if(script.Movestop==true)
        this.SetActive(false);
        else
            this.SetActive(true);
    }
}
