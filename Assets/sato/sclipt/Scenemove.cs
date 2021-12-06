using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemove : MonoBehaviour
{
    [Header("移動したいシーン名入力")]
    public string[] SceneName;

    [Header("王冠")]
    public GameObject clown;
    [Header("ミッション")]
    public GameObject mission;

    static public int Scene_num = 0;

    static public bool Clown_OK = false;
    static public bool Mission_OK = false;

    void FixedUpdate() 
    {
        //ステージシーンの時
        if (SceneManager.GetActiveScene().name != "Title"&&
            SceneManager.GetActiveScene().name != "StageSelect" &&
            SceneManager.GetActiveScene().name != "Result")
        {
            if (GameObject.Find("Player").GetComponent<Mission>().Clown_OK == true)
                Clown_OK = true;
            if (GameObject.Find("Player").GetComponent<Mission>().Mission_OK == true)
                Mission_OK = true;
        }

        if (SceneManager.GetActiveScene().name == "Result")
        {
            if(Clown_OK==true)
            {
                clown.gameObject.SetActive(true);
            }
            if (Mission_OK == true)
            {
                mission.gameObject.SetActive(true);
            }
        }

        //ステージの遷移
        if (SceneManager.GetActiveScene().name == "Stage1")
            Scene_num = 1;
        if (SceneManager.GetActiveScene().name == "Stage2")
            Scene_num = 2;
        if (SceneManager.GetActiveScene().name == "Stage3")
            Scene_num = 3;
        if (SceneManager.GetActiveScene().name == "Stage4")
            Scene_num = 4;
        if (SceneManager.GetActiveScene().name == "Stage5")
            Scene_num = 5;
        if (SceneManager.GetActiveScene().name == "Stage6")
            Scene_num = 6;
    }

    public void PushScene() 
    {
        Clown_OK = false;
        Mission_OK = false;
        SceneManager.LoadScene(SceneName[0]);
    }

    public void PushNextScene() 
    {
        Clown_OK = false;
        Mission_OK = false;

        if (Scene_num == 1) 
            SceneManager.LoadScene("Stage2");
        if (Scene_num == 2)
            SceneManager.LoadScene("Stage3");
        if (Scene_num == 3)
            SceneManager.LoadScene("Stage4");
        if (Scene_num == 4)
            SceneManager.LoadScene("Stage5");
        if (Scene_num == 5)
            SceneManager.LoadScene("Stage6");
        if (Scene_num == 6)
            SceneManager.LoadScene("Title");
    }
}
