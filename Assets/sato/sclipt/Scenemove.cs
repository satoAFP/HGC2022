using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemove : MonoBehaviour
{
    [Header("移動したいシーン名入力")]
    public string[] SceneName;

    [Header("ステージの数")]
    public int stage_num;

    [Header("王冠")]
    public GameObject clown;
    [Header("ミッション")]
    public GameObject mission;

    static public int Scene_num = 0;

    static public bool Clown_OK = false;
    static public bool Mission_OK = false;

    private int clown_time = 0;
    private int mission_time = 0;


    void FixedUpdate() 
    {
        //ステージシーンの時
        if (SceneManager.GetActiveScene().name != "Title"&&
            SceneManager.GetActiveScene().name != "StageSelect" &&
            SceneManager.GetActiveScene().name != "Result")
        {
            //ミッションがクリアされているかを記憶
            if (GameObject.Find("Player").GetComponent<Mission>().Clown_OK == true)
                Clown_OK = true;
            else
                Clown_OK = false;
            if (GameObject.Find("Player").GetComponent<Mission>().Mission_OK == true)
                Mission_OK = true;
            else
                Mission_OK = false;
        }

        if (SceneManager.GetActiveScene().name == "Result")
        {
            clown_time++;
            mission_time++;
            if (Clown_OK == true) 
            {
                if (clown_time >= 90)
                {
                    clown.gameObject.SetActive(true);
                    //王冠が落ちてくる処理
                    if (clown.gameObject.GetComponent<RectTransform>().sizeDelta.x > 240.0f)
                    {
                        clown.gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(24.0f, 21.0f);
                    }
                }
            }
            if (Mission_OK == true)
            {
                if(mission_time >= 120)
                {
                    mission.gameObject.SetActive(true);
                    //星が落ちてくる処理
                    if (mission.gameObject.GetComponent<RectTransform>().sizeDelta.x > 240.0f)
                    {
                        mission.gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(24.0f, 24.0f);
                    }
                }
            }
        }

        for (int i = 1; i <= stage_num; i++)
        {
            if (SceneManager.GetActiveScene().name == "Stage" + i) 
                Scene_num = i;
        }
    }

    //指定したシーンに移動
    public void PushScene() 
    {
        Clown_OK = false;
        Mission_OK = false;
        SceneManager.LoadScene(SceneName[0]);
    }

    //ネクストステージボタン
    public void PushNextScene() 
    {
        Clown_OK = false;
        Mission_OK = false;

        for (int i = 1; i <= stage_num - 1; i++) 
        {
            if (Scene_num == i)
                SceneManager.LoadScene("Stage" + i + 1);
        }

        if (Scene_num == stage_num)
            SceneManager.LoadScene("Title");
    }

    //リトライボタン
    public void PushRetryScene()
    {
        Clown_OK = false;
        Mission_OK = false;

        for (int i = 1; i <= stage_num; i++)
        {
            if (Scene_num == i)
                SceneManager.LoadScene("Stage" + i);
        }

    }
}
