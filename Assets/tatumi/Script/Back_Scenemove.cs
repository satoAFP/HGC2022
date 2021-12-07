using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//いらないscript?
public class Back_Scenemove : MonoBehaviour
{
   
    GameObject ActionButton; //参照元OBJそのものが入る変数

    ActionButton_SC script; //参照元Scriptが入る変数

    private string Back_Stage;

    // Use this for initialization
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する

       // Back_Stage = script.NowStage;
    }


    public void PushButton()
    {
        if(Back_Stage=="Stage")
        SceneManager.LoadScene("Stage-2");
        if (Back_Stage == "Stage-2")
           ;
    }
}


