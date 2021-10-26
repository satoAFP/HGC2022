using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;  // 追加しましょう

public class Result : MonoBehaviour
{

    GameObject ActionButton; //参照元OBJそのものが入る変数

    ActionButton_SC script; //参照元Scriptが入る変数

    [Header("表示txt指定")]
    public GameObject score_object = null; // Textオブジェクト

    [Header("表示txt指定(Nest Stage用)")]
    public GameObject stage_object = null; // Textオブジェクト

    [Header("とりあえず整数（int）")]
    public int score_num; // スコア変数

    private string stage; // stage変数

    // 初期化
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する

        stage = script.NowStage;
    }

    // 更新
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        //整数
        Text score_text = score_object.GetComponent<Text>();
        //ステージ
        Text stage_text = stage_object.GetComponent<Text>();

        if (stage == "Stage-2")
            stage_text.text = "coming soon...";

        // テキストの表示を入れ替える
        score_text.text = "Score:" + score_num;

        score_num += 1; // とりあえず1加算し続けてみる
    }
}